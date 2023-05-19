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
    public class NotificationAccommodationReleaseRepository : INotificationAccommodationReleaseRepository
    {
        private const string _filePath = "../../../Resources/Data/notificationAccommodationRelease.csv";
        private readonly List<IObserver> _observers;
        private readonly Serializer<NotificationAccommodationRelease> _serializer;
        private static INotificationAccommodationReleaseRepository _instance = null;
        private List<NotificationAccommodationRelease> _notifications;

        public static INotificationAccommodationReleaseRepository GetInstance()
        {
            if (_instance == null)
            {
                _instance = new NotificationAccommodationReleaseRepository();
            }
            return _instance;
        }
        private NotificationAccommodationReleaseRepository()
        {
            _serializer = new Serializer<NotificationAccommodationRelease>();
            _notifications = new List<NotificationAccommodationRelease>();
            _notifications = _serializer.FromCSV(_filePath);
            _observers = new List<IObserver>();
        }


        public NotificationAccommodationRelease Create(NotificationAccommodationRelease entity)
        {
            entity.Id = NextId();
            _notifications.Add(entity);
            Save();
            NotifyObservers();
            return entity;
        }
        public NotificationAccommodationRelease Delete(NotificationAccommodationRelease entity)
        {
            _notifications.Remove(entity);
          //  Save();
            NotifyObservers();
            return entity;
        }
        public NotificationAccommodationRelease Get(int id)
        {
            return _notifications.Find(a => a.Id == id);
        }
        public List<NotificationAccommodationRelease> GetAll()
        {
            return _notifications;
        }
        public int NextId()
        {
            if (_notifications.Count == 0) return 0;
            int newId = _notifications[_notifications.Count() - 1].Id + 1;
            foreach (NotificationAccommodationRelease a in _notifications)
            {
                if (newId == a.Id)
                {
                    newId++;
                }
            }
            return newId;
        }
        public void Save()
        {
            _serializer.ToCSV(_filePath, _notifications);
        }
        public NotificationAccommodationRelease Update(NotificationAccommodationRelease entity)
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
