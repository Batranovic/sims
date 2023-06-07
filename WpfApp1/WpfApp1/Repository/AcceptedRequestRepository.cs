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
    public class AcceptedRequestGuideRepository : IAcceptedRequestGuideRepositry
    {
        private const string FilePath = "../../../Resources/Data/acceptedRequestGuide.csv";

        private readonly Serializer<AcceptedRequestGuide> _serializer;
        private readonly List<IObserver> _observers;

        private List<AcceptedRequestGuide> _acceptedRequestGuide;
        private static IAcceptedRequestGuideRepositry instance = null;

        public AcceptedRequestGuideRepository()
        {
            _serializer = new Serializer<AcceptedRequestGuide>();
            _observers = new List<IObserver>();
            _acceptedRequestGuide = _serializer.FromCSV(FilePath);
        }

        public static IAcceptedRequestGuideRepositry GetInstance()
        {
            if (instance == null)
            {
                instance = new AcceptedRequestGuideRepository();
            }
            return instance;
        }

      


        public List<AcceptedRequestGuide> GetAll()
        {
            return _acceptedRequestGuide;
        }
        public AcceptedRequestGuide Get(int id)
        {
            return _acceptedRequestGuide.Find(t => t.Id == id);
        }
        public void Save()
        {
            _serializer.ToCSV(FilePath, _acceptedRequestGuide);

        }
        public int NextId()
        {
            if (_acceptedRequestGuide.Count < 1)
            {
                return 1;
            }
            return _acceptedRequestGuide.Max(a => a.Id) + 1;
        }
        public void Delete(AcceptedRequestGuide acceptedRequestGuide)
        {
            AcceptedRequestGuide founded = _acceptedRequestGuide.Find(a => a.Id == acceptedRequestGuide.Id);
            _acceptedRequestGuide.Remove(founded);
            _serializer.ToCSV(FilePath, _acceptedRequestGuide);
        }

        public AcceptedRequestGuide Update(AcceptedRequestGuide acceptedRequestGuide)
        {
            AcceptedRequestGuide current = _acceptedRequestGuide.Find(a => a.Id == acceptedRequestGuide.Id);
            int index = _acceptedRequestGuide.IndexOf(current);
            _acceptedRequestGuide.Remove(current);
            _acceptedRequestGuide.Insert(index, acceptedRequestGuide);
            _serializer.ToCSV(FilePath, _acceptedRequestGuide);
            return acceptedRequestGuide;
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
