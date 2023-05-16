using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Domain.Models;
using WpfApp1.Domain.RepositoryInterfaces;
using WpfApp1.Domain.ServiceInterfaces;
using WpfApp1.Repository;

namespace WpfApp1.Service
{
    public class RequestNotificationService : IRequestNotifactionService
    {
        private readonly IRequestNotifactionRepository _notificationRepository;
        private readonly ISimpleTourRequestRepository _simpleTourRequestRepository;
        public RequestNotificationService()
        {
            _notificationRepository = InjectorRepository.CreateInstance<IRequestNotifactionRepository>();
            _simpleTourRequestRepository = InjectorRepository.CreateInstance<ISimpleTourRequestRepository>();
            BindSimpleTourRequest();
        }
        private void BindSimpleTourRequest()
        {
            foreach (RequestNotification notification in _notificationRepository.GetAll())
            {
                int simpleTourRequestId = notification.SimpleTourRequest.Id;
                SimpleTourRequest simpleTourRequest = SimpleTourRequestRepository.GetInstance().Get(simpleTourRequestId);
                if (simpleTourRequest != null)
                {
                    notification.SimpleTourRequest = simpleTourRequest;
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

        public RequestNotification Get(int id)
        {
            return _notificationRepository.Get(id);
        }

        public void Delete(RequestNotification notification)
        {
            _notificationRepository.Delete(notification);
        }

        public List<RequestNotification> GetAll()
        {
            return _notificationRepository.GetAll();
        }


        public RequestNotification Update(RequestNotification entity)
        {
            return _notificationRepository.Update(entity);
        }
        public List<RequestNotification> GetNotificationForUser(int userId)
        {
            List<RequestNotification> notificationList = new List<RequestNotification>();
            var allNotifications = _notificationRepository.GetAll();
            for (int i = 0; i < allNotifications.Count(); i++)
            {
                var notification = allNotifications.ElementAt(i);
                if (!notification.IsDelivered && notification.SimpleTourRequest.Tourist.Id == userId)
                {
                    notification.IsDelivered = true;
                    _notificationRepository.Update(notification);

                    // Update the request status and start date
                    var request = notification.SimpleTourRequest;
                    request.RequestStatus = notification.RequestStatus;
                    request.StartDate = notification.Date;
                    _simpleTourRequestRepository.Update(request);

                    notificationList.Add(notification);
                }
            }
            return notificationList;
        }


    }
}
