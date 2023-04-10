using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp.Observer;
using WpfApp1.Models;
using WpfApp1.Serializer;
using WpfApp1.Domain.RepositoryInterfaces;

namespace WpfApp1.Repository
{
    public class TouristRepository : ITouristRepository
    {
        private const string _filePath = "../../../Resources/Data/tourists.csv";
        private readonly List<IObserver> _observers;
        private readonly Serializer<Tourist> _serializer;

        private List<Tourist> _tourists;


        private static TouristRepository _instance = null;

        public static TouristRepository GetInstance()
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
            _serializer.ToCSV(_filePath, _tourists);
        }

        public Tourist Update(Tourist tourist)
        {
            Tourist current = _tourists.Find(tp => tp.Id == tourist.Id);
            int index = _tourists.IndexOf(current);
            _tourists.Remove(current);
            _tourists.Insert(index, tourist);
            _serializer.ToCSV(_filePath, _tourists);
            return tourist;
        }
    }
}
