using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Service;
using WpfApp1.Models;

namespace WpfApp1.Controller
{
    public class NotificationController
    {
        private readonly NotificationService _notificationService;



        public NotificationController()
        {
            _notificationService = new NotificationService();
        }

        public void Save()
        {_notificationService.Save();
        }

        public List<Notification> GetAll()
        {
            return _notificationService.GetAll();
        }

        public void Delete(Notification notification)
        { 
            _notificationService.Delete(notification);
        }
            public Notification Get(int id)
        {
            return _notificationService.Get(id);
        }

        public Notification Update(Notification entity)
        { 
            return _notificationService.Update(entity); 
        }

            public List<Notification> GetNotificationForUser(int userId)
        {
            return _notificationService.GetNotificationForUser(userId);
        }

    }
}
