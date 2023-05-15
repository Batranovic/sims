using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp.Observer;
using WpfApp1.Domain.Models;
using WpfApp1.Domain.RepositoryInterfaces;
using WpfApp1.Serializer;

namespace WpfApp1.Repository
{
    public class NewTourNotificationRepository : INewTourNotificationRepository
    {
        private const string _filePath = "../../../Resources/Data/newTourNotifications.csv";
        private static INewTourNotificationRepository instance = null;

        private readonly List<IObserver> _observers;
        private readonly Serializer<NewTourNotification> _serializer;

        private List<NewTourNotification> _notifications;

        private NewTourNotificationRepository()
        {
            _serializer = new Serializer<NewTourNotification>();
            _notifications = _serializer.FromCSV(_filePath);
            _observers = new List<IObserver>();
        }
        public static INewTourNotificationRepository GetInstance()
        {
            if (instance == null)
            {
                instance = new NewTourNotificationRepository();
            }
            return instance;
        }

        public NewTourNotification Create(NewTourNotification entity)
        {
            entity.Id = NextId();
            _notifications.Add(entity);
            Save();
            NotifyObservers();
            return entity;
        }

        public List<NewTourNotification> GetAll()
        {
            return _notifications;
        }

        public void Save()
        {
            _serializer.ToCSV(_filePath, _notifications);
        }

        public int NextId()
        {
            if (_notifications.Count < 1)
            {
                return 1;
            }
            return _notifications.Max(l => l.Id) + 1;
        }
        public void Delete(NewTourNotification notification)
        {
            NewTourNotification founded = _notifications.Find(l => l.Id == notification.Id);
            _notifications.Remove(founded);
            _serializer.ToCSV(_filePath, _notifications);
        }

        public NewTourNotification Get(int id)
        {
            return _notifications.Find(t => t.Id == id);
        }

        public NewTourNotification Update(NewTourNotification entity)
        {
            var oldEntity = Get(entity.Id);
            if (oldEntity == null)
            {
                return null;
            }
            oldEntity = entity;
            Save();
            return oldEntity;
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
