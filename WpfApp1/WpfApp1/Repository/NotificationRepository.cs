using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Models;
using WpfApp1.Serializer;
using WpfApp1.Domain.RepositoryInterfaces;
using WpfApp.Observer;

namespace WpfApp1.Repository 
{
    public class NotificationRepository : INotificationRepository
    {
        private const string _filePath = "../../../Resources/Data/notifications.csv";
        private static NotificationRepository instance = null;

        private readonly List<IObserver> _observers;
        private readonly Serializer<Notification> _serializer;

        private List<Notification> _notifications;

        private NotificationRepository()
        {
            _serializer = new Serializer<Notification>();
            _notifications = _serializer.FromCSV(_filePath);
            _observers = new List<IObserver>();
        }
        public static NotificationRepository GetInstance()
        {
            if (instance == null)
            {
                instance = new NotificationRepository();
            }
            return instance;
        }
         public List<Notification> GetAll()
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
        public void Delete(Notification notification)
        {
            Notification founded = _notifications.Find(l => l.Id == notification.Id);
            _notifications.Remove(founded);
            _serializer.ToCSV(_filePath, _notifications);
        }

        public Notification Get(int id)
        {
            return _notifications.Find(t => t.Id == id);
        }

        public Notification Update(Notification entity)
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

        public void BindTourBooking()
        {
            foreach (Notification notification in _notifications)
            {
                int tourBookingId = notification.TourBooking.Id;
                TourBooking tourBooking = TourBookingRepository.GetInstance().Get(tourBookingId);
                if (tourBooking != null)
                {
                   notification.TourBooking = tourBooking;
                }
                else
                {
                    Console.WriteLine("Error in binding tourBooking and notification");
                }
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

        public void NotifyObservers()
        {
            foreach (var observer in _observers)
            {
                observer.Update();
            }
        }
    }
}
