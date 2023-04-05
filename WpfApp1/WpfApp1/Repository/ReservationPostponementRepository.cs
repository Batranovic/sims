using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp.Observer;
using WpfApp1.Model;
using WpfApp1.Serializer;

namespace WpfApp1.Repository
{
    public class ReservationPostponementRepository : IRepository<ReservationPostponement>, ISubject
    {
        private const string _filePath = "../../../Resources/data/reservationPostponements.csv";
        private readonly List<IObserver> _observers;

        private readonly Serializer<ReservationPostponement> _serializer;

        private List<ReservationPostponement> _postponements;
        private static ReservationPostponementRepository _instance = null;

        public ReservationRepository ReservationDAO { get; set; }

        public static ReservationPostponementRepository GetInstance()
        {
            if (_instance == null)
            {
                _instance = new ReservationPostponementRepository();
            }
            return _instance;
        }

        private ReservationPostponementRepository()
        {
            _postponements = new List<ReservationPostponement>();
            _serializer = new Serializer<ReservationPostponement>();
            _postponements = _serializer.FromCSV(_filePath);
            _observers = new List<IObserver>();
            ReservationDAO = ReservationRepository.GetInstance();

        }



        public void BindReservation()
        {
            foreach (ReservationPostponement p in _postponements)
            {
                p.Reservation = ReservationDAO.Get(p.IdReservation);
            }
        }


        public ReservationPostponement Create(ReservationPostponement entity)
        {
            entity.Id = NextId();
            _postponements.Add(entity);
            Save();
            NotifyObservers();
            return entity;
        }


        public ReservationPostponement Delete(ReservationPostponement entity)
        {
            _postponements.Remove(entity);
            Save();
            NotifyObservers();
            return entity;
        }

        public ReservationPostponement Get(int id)
        {
            return _postponements.Find(r => r.Id == id);
        }

        public List<ReservationPostponement> GetAll()
        {
            return _postponements;
        }

        public int NextId()
        {
            if (_postponements.Count == 0)
                return 0;
            int nextId = _postponements[_postponements.Count - 1].Id + 1;
            foreach (ReservationPostponement r in _postponements)
            {
                if (nextId == r.Id)
                {
                    nextId++;
                }
            }
            return nextId;
        }

        public void NotifyObservers()
        {
            foreach (var observer in _observers)
            {
                observer.Update();
            }
        }

        public void Save()
        {
            _serializer.ToCSV(_filePath, _postponements);
        }

        public void Subscribe(IObserver observer)
        {
            _observers.Add(observer);
        }

        public void Unsubscribe(IObserver observer)
        {
            _observers.Remove(observer);
        }


        public ReservationPostponement Update(ReservationPostponement entity)
        {
            var oldEntity = Get(entity.Id);
            if (oldEntity == null)
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
