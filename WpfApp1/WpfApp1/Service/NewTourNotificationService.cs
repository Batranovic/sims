using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WpfApp1.Domain.Models;
using WpfApp1.Domain.Models.Enums;
using WpfApp1.Domain.RepositoryInterfaces;
using WpfApp1.Domain.ServiceInterfaces;
using WpfApp1.Repository;

namespace WpfApp1.Service
{
    internal class NewTourNotificationService : INewTourNotificationService
    {
        private readonly INewTourNotificationRepository _notificationRepository;
        private readonly ISimpleTourRequestRepository _simpleTourRequestRepository;
        private readonly ITourRepository _tourRepository;
        private readonly ITouristRepository _touristRepository;
        public NewTourNotificationService()
        {
            _notificationRepository = InjectorRepository.CreateInstance<INewTourNotificationRepository>();
            _simpleTourRequestRepository = InjectorRepository.CreateInstance<ISimpleTourRequestRepository>();
            _tourRepository = InjectorRepository.CreateInstance<ITourRepository>();
            _touristRepository = InjectorRepository.CreateInstance<ITouristRepository>();
            BindTour();
        }
        private void BindTour()
        {
            foreach (NewTourNotification notification in _notificationRepository.GetAll())
            {
                int simpleTourRequestId = notification.Tour.Id;
                Tour simpleTourRequest = TourRepository.GetInstance().Get(simpleTourRequestId);
                if (simpleTourRequest != null)
                {
                    notification.Tour = simpleTourRequest;
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

        public NewTourNotification Get(int id)
        {
            return _notificationRepository.Get(id);
        }

        public void Delete(NewTourNotification notification)
        {
            _notificationRepository.Delete(notification);
        }

        public List<NewTourNotification> GetAll()
        {
            return _notificationRepository.GetAll();
        }


        public NewTourNotification Update(NewTourNotification entity)
        {
            return _notificationRepository.Update(entity);
        }

        public void Create(NewTourNotification entity)
        {
            _notificationRepository.Create(entity);
        }

        public List<NewTourNotification> GetNotificationForUser(int userId)
        {
            List<NewTourNotification> notificationList = new List<NewTourNotification>();
            var allNotifications = _notificationRepository.GetAll();
            for (int i = 0; i < allNotifications.Count(); i++)
            {
                var notification = allNotifications.ElementAt(i);
                if (!notification.IsDelivered && notification.TouristId == userId)
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
