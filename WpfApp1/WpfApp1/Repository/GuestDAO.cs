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
    public class GuestDAO : IDAO<Guest>, ISubject
    {
        private const string _filePath = "../../../Resources/Data/guests.csv";

        private readonly Serializer<Guest> _serializer;
        private List<IObserver> _observers;
        private List<Guest> _guests;
        private static GuestDAO _instance = null;

        public static GuestDAO GetInsatnce()
        {
            if(_instance == null)
            {
                _instance = new GuestDAO();
            }
            return _instance;
        }

        private GuestDAO()
        {
            _serializer = new Serializer<Guest>();
            _guests = new List<Guest>();
            _guests = _serializer.FromCSV(_filePath);
            _observers = new List<IObserver>();
        }

        public Guest Get(int id)
        {
            return _guests.Find(o => o.Id == id);
        }

        public List<Guest> GetAll()
        {
            return _guests;
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public Guest Create(Guest entity)
        {
            throw new NotImplementedException();
        }

        public Guest Update(Guest entity)
        {
            throw new NotImplementedException();
        }

        public Guest Delete(Guest entity)
        {
            throw new NotImplementedException();
        }

        public int NextId()
        {
            throw new NotImplementedException();
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
            foreach(var o in _observers)
            {
                o.Update();
            }
        }
    }
}
