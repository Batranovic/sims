using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Domain.Models;
using WpfApp1.Serializer;
using WpfApp1.Domain.RepositoryInterfaces;
using WpfApp.Observer;

namespace WpfApp1.Repository
{
    public class TourPointRepository : ITourPointRepository
    {
         

        private const string _filePath = "../../../Resources/Data/tourPoints.csv";

        private static ITourPointRepository instance = null;

        private readonly List<IObserver> _observers;

        private readonly Serializer<TourPoint> _serializer;

        private List<TourPoint> _tourPoints;

        private TourPointRepository()
        {

            _serializer = new Serializer<TourPoint>();
            _tourPoints = _serializer.FromCSV(_filePath);
            _observers = new List<IObserver>();
        }

        public static ITourPointRepository GetInstance()
        {
            if (instance == null)
            {
                instance = new TourPointRepository();
            }
            return instance;
        }
        public TourPoint Create(TourPoint entity)
        {
            entity.Id = NextId();
            _tourPoints.Add(entity);
            Save();
            return entity;
        }
      
        public void Save()
        {
            _serializer.ToCSV(_filePath, _tourPoints);
        }

        public int NextId()
        {
            if (_tourPoints.Count < 1)
            {
                return 1;
            }
            return _tourPoints.Max(tp => tp.Id) + 1;
        }

        public void Delete(TourPoint tourPoint)
        {
            TourPoint founded = _tourPoints.Find(tp => tp.Id == tourPoint.Id);
            _tourPoints.Remove(founded);
            _serializer.ToCSV(_filePath, _tourPoints);
        }

        public TourPoint Update(TourPoint tourPoint)
        {
            TourPoint current = _tourPoints.Find(tp => tp.Id == tourPoint.Id);
            int index = _tourPoints.IndexOf(current);
            _tourPoints.Remove(current);
            _tourPoints.Insert(index, tourPoint);
            _serializer.ToCSV(_filePath, _tourPoints);
            return tourPoint;
        }

        public List<TourPoint> GetAll()
        {

            return _tourPoints;

        }


        public TourPoint Get(int id)
        {

            return _tourPoints.Find(x => x.Id == id);

        }

        public void Subscribe(IObserver observer)
        {
            _observers.Add(observer);
        }

        public void Unsubscribe(IObserver observer)
        {
            _observers.Remove(observer);
        }

        public void NotifyObservers()
        {
            foreach (var observer in _observers)
            {
                observer.Update();
            }
        }




    }
}
