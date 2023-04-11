using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Repository;
using WpfApp1.Domain.Models;
using WpfApp1.Domain.ServiceInterfaces;
using WpfApp1.Domain.RepositoryInterfaces;

namespace WpfApp1.Service
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationRepository _notificationRepository;
        public NotificationService()
        {
            _notificationRepository = InjectorRepository.CreateInstance<INotificationRepository>();
            BindTourBooking();
        }
        private void BindTourBooking()
        {
            foreach (Notification notification in _notificationRepository.GetAll())
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

        public void Save()
        {
            _notificationRepository.Save();
        }

        public Notification Get(int id)
        {
            return _notificationRepository.Get(id);
        }

        public void Delete(Notification notification)
        {
            _notificationRepository.Delete(notification);
        }

        public List<Notification> GetAll()
        {
            return _notificationRepository.GetAll();
        }


        public Notification Update(Notification entity)
        { 
            return _notificationRepository.Update(entity);
        }

        public List<Notification> GetNotificationForUser(int userId)
        {
            List<Notification> notificationList = new List<Notification>();
            var allNotifications = _notificationRepository.GetAll();
            for (int i = 0; i < allNotifications.Count(); i++)
            {
                var notification = allNotifications.ElementAt(i);
                if (!notification.IsDelivered && notification.TourBooking.Tourist.Id == userId)
                {
                    notification.IsDelivered = true;
                    _notificationRepository.Update(notification);
                    notificationList.Add(notification);
                }
            }
            return notificationList;
        }
    }
}
