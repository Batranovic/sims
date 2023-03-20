using System;
using System.Collections.Generic;
using System.Linq;
using System.Printing;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using WpfApp.Observer;
using WpfApp1.Model;
using WpfApp1.Serializer;
using WpfApp1.Model.Enums;

namespace WpfApp1.Repository
{
    public class ReservationDAO : IDAO<Reservation>, ISubject
    {

        private const string _filePath = "../../../Resources/data/reservations.csv";
        private readonly List<IObserver> _observers;

        private readonly Serializer<Reservation> _serializer;

        private List<Reservation> _reservations;
        private static ReservationDAO _instance = null;

        public AccommodationDAO AccommodationDAO { get; set; }

        public GuestRepository GuestRepository { get; set; }
        
        public static ReservationDAO GetInstance()
        {
            if(_instance == null)
            {
                _instance = new ReservationDAO();
            }
            return _instance;
        }
        private ReservationDAO()
        {
            _reservations = new List<Reservation>();
            _serializer= new Serializer<Reservation>();
            _reservations = _serializer.FromCSV(_filePath);
            _observers = new List<IObserver>();
            AccommodationDAO = AccommodationDAO.GetInstance();
            GuestRepository = GuestRepository.GetInsatnce();
            SetStatus();                                //Status trenutne rezervacije (da li je u toku, prosla, ocenja ili neocenjena
        }

        public void SetStatus()
        {
            foreach(Reservation reservation in _reservations)
            {
                if (reservation.Status == RatingGuestStatus.rated)
                {
                    continue;
                }
                else if (reservation.EndDate < DateTime.Now)
                {
                    reservation.Status = RatingGuestStatus.inprogres;
                }
                else if (reservation.EndDate > DateTime.Now.AddDays(-5))
                {
                    reservation.Status = RatingGuestStatus.expired;
                }
                reservation.Status = RatingGuestStatus.unrated;
            }
        }

        public void BindAccommodation()
        {
            foreach(Reservation r in _reservations)
            {
                r.Accommodation = AccommodationDAO.Get(r.IdAccommodation);
            }
        }

        public Reservation Create(Reservation entity)
        {
            entity.Id = NextId();
            _reservations.Add(entity);
            Save();
            NotifyObservers();
            return entity;
        }

        public void BindGuest()
        {
            foreach (Reservation r in _reservations)
            {
                r.Guest = GuestRepository.Get(r.IdGuest);
            }
        }
        public void BindAccommodation()
        {
            foreach(Reservation r in _reservations)
            {
                r.Accommodation = AccommodationDAO.Get(r.IdAccommodation);
            }
        }

        public Reservation Delete(Reservation entity)
        {
            _reservations.Remove(entity);
            Save();
            NotifyObservers();
            return entity;
        }

        public Reservation Get(int id)
        {
            return _reservations.Find(r => r.Id == id);
        }

        public List<Reservation> GetAll()
        {
            return _reservations;
        }

        public int NextId()
        {
            if(_reservations.Count == 0)
                return 0;
            int nextId = _reservations[_reservations.Count - 1].Id + 1;
            foreach(Reservation r in _reservations)
            {
                if(nextId == r.Id)
                {
                    nextId++;
                }
            }
            return nextId;
        }

        public void NotifyObservers()
        {
            foreach(var observer in _observers)
            {
                observer.Update();
            }
        }

        public void Save()
        {
            _serializer.ToCSV(_filePath, _reservations);
        }

        public void Subscribe(IObserver observer)
        {
            _observers.Add(observer);
        }

        public void Unsubscribe(IObserver observer)
        {
            _observers.Remove(observer);
        }


        public Reservation Update(Reservation entity)
        {
            var oldEntity = Get(entity.Id);
            if(oldEntity == null)
            {
                return null;
            }
            oldEntity = entity;
            Save();
            NotifyObservers();
            return oldEntity;

        }
    }
}
