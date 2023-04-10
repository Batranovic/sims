using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Repository;
using WpfApp1.Models;

namespace WpfApp1.Service
{
    public class NotificationService
    {
        private NotificationRepository _notificationDAO;
        public NotificationService()
        {
            _notificationDAO = NotificationRepository.GetInstance();
        }

        public void Save()
        {
             _notificationDAO.Save();
        }

        public Notification Get(int id)
        {
            return _notificationDAO.Get(id);
        }

        public void Delete(Notification notification)
        {
            _notificationDAO.Delete(notification);
        }

        public List<Notification> GetAll()
        {
            return _notificationDAO.GetAll();
        }


        public Notification Update(Notification entity)
        { 
            return _notificationDAO.Update(entity);
        }

        public List<Notification> GetNotificationForUser(int userId)
        {
            List<Notification> notificationList = new List<Notification>();
            var allNotifications = _notificationDAO.GetAll();
            for (int i = 0; i < allNotifications.Count(); i++)
            {
                var notification = allNotifications.ElementAt(i);
                if (!notification.IsDelivered && notification.TourBooking.Tourist.Id == userId)
                {
                    notification.IsDelivered = true;
                    _notificationDAO.Update(notification);
                    notificationList.Add(notification);
                }
            }
            return notificationList;
        }
    }
}
