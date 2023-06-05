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
    public class ForumNotificationRepository : IForumNotificationRepository
    {
        private const string _filePath = "../../../Resources/Data/forumNotification.csv";
        private readonly List<IObserver> _observers;
        private readonly Serializer<ForumNotification> _serializer;
        private static IForumNotificationRepository _instance = null;
        private List<ForumNotification> _notifications;

        public static IForumNotificationRepository GetInstance()
        {
            if (_instance == null)
            {
                _instance = new ForumNotificationRepository();
            }
            return _instance;
        }

        private ForumNotificationRepository()
        {
            _serializer = new Serializer<ForumNotification>();
            _notifications = new List<ForumNotification>();
            _notifications = _serializer.FromCSV(_filePath);
            _observers = new List<IObserver>();
        }

        public ForumNotification Create(ForumNotification entity)
        {
            entity.Id = NextId();
            _notifications.Add(entity);
            Save();
            NotifyObservers();
            return entity;
        }

        public ForumNotification Delete(ForumNotification entity)
        {
            _notifications.Remove(entity);
            Save();
            NotifyObservers();
            return entity;
        }

        public ForumNotification Get(int id)
        {
            return _notifications.Find(a => a.Id == id);
        }

        public List<ForumNotification> GetAll()
        {
            return _notifications;
        }

        public int NextId()
        {
            if (_notifications.Count == 0) return 0;
            int newId = _notifications[_notifications.Count() - 1].Id + 1;
            foreach (ForumNotification a in _notifications)
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

        public ForumNotification Update(ForumNotification entity)
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
