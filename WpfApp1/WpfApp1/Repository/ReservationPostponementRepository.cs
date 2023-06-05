using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp.Observer;
using WpfApp1.Domain.RepositoryInterfaces;
using WpfApp1.Domain.Models;
using WpfApp1.Serializer;

namespace WpfApp1.Repository
{
    public class ReservationPostponementRepository : IReservationPostponementRepository
    {
        private const string _filePath = "../../../Resources/data/reservationPostponements.csv";
        private readonly List<IObserver> _observers;
        private readonly Serializer<ReservationPostponement> _serializer;
        private List<ReservationPostponement> _postponements;
        private static IReservationPostponementRepository _instance = null;

        public static IReservationPostponementRepository GetInstance()
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
            entity.Deleted = true;
            Save();
            NotifyObservers();
            return entity;
        }
        public ReservationPostponement Get(int id)
        {
            return _postponements.Find(r => r.Id == id && r.Deleted == false);
        }
        public List<ReservationPostponement> GetAll()
        {
            return _postponements.FindAll(r => r.Deleted == false);
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
        public List<ReservationPostponement> GetByReservation(int idReservation)
        {
            try
            { 
                return GetAll().FindAll(r => r.Reservation.Id == idReservation);
            }
            catch(Exception ex)
            {
                return new List<ReservationPostponement>();
            }
        }

    }
}
