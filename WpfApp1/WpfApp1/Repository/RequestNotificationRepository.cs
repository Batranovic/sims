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
    public class RequestNotificationRepository : IRequestNotifactionRepository
    {

        private const string _filePath = "../../../Resources/Data/requestNotifications.csv";
        private static IRequestNotifactionRepository instance = null;

        private readonly List<IObserver> _observers;
        private readonly Serializer<RequestNotification> _serializer;

        private List<RequestNotification> _notifications;

        private RequestNotificationRepository()
        {
            _serializer = new Serializer<RequestNotification>();
            _notifications = _serializer.FromCSV(_filePath);
            _observers = new List<IObserver>();
        }
        public static IRequestNotifactionRepository GetInstance()
        {
            if (instance == null)
            {
                instance = new RequestNotificationRepository();
            }
            return instance;
        }
        public List<RequestNotification> GetAll()
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
        public void Delete(RequestNotification notification)
        {
            RequestNotification founded = _notifications.Find(l => l.Id == notification.Id);
            _notifications.Remove(founded);
            _serializer.ToCSV(_filePath, _notifications);
        }

        public RequestNotification Get(int id)
        {
            return _notifications.Find(t => t.Id == id);
        }

        public RequestNotification Update(RequestNotification entity)
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
