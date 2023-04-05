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
    public class TouristRepository : IRepository<Tourist>, ISubject
    {
        private const string _filePath = "../../../Resources/Data/tourists.csv";
        private readonly List<IObserver> _observers;
        private readonly Serializer<Tourist> _serializer;

        private List<Tourist> _tourists;


        private static TouristRepository _instance = null;

        public static TouristRepository GetInsatnce()
        {
            if (_instance == null)
            {
                _instance = new TouristRepository();
            }
            return _instance;
        }

        private TouristRepository()
        {
            _serializer = new Serializer<Tourist>();
            _tourists = new List<Tourist>();
            _tourists = _serializer.FromCSV(_filePath);
            _observers = new List<IObserver>();
        }


        public Tourist Get(int id)
        {
            return _tourists.Find(t => t.Id == id);
        }

        public List<Tourist> GetAll()
        {
            return _tourists;
        }

        public void NotifyObservers()
        {
            foreach (var observer in _observers)
            {
                observer.Update();
            }
        }

        public void Subscribe(IObserver observer)
        {
            _observers.Add(observer);
        }

        public void Unsubscribe(IObserver observer)
        {
            _observers.Remove(observer);
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public Tourist Create(Tourist entity)
        {
            throw new NotImplementedException();
        }

        public Tourist Update(Tourist entity)
        {
            throw new NotImplementedException();
        }

        public Tourist Delete(Tourist entity)
        {
            throw new NotImplementedException();
        }

        public int NextId()
        {
            throw new NotImplementedException();
        }
    }
}
