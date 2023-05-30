using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp.Observer;
using WpfApp1.Domain.Models;
using WpfApp1.Domain.RepositoryInterfaces;
using WpfApp1.Domain.ServiceInterfaces;
using WpfApp1.DTO;

namespace WpfApp1.Service
{
    public class NotificationAccommodationReleaseService : INotificationAccommodationReleaseService
    {
        private readonly INotificationAccommodationReleaseRepository _notificationRepository;
        private readonly IAccommodationRepository _accommodationRepository;
        public NotificationAccommodationReleaseService()
        {
            _notificationRepository = InjectorRepository.CreateInstance<INotificationAccommodationReleaseRepository>();
            _accommodationRepository = InjectorRepository.CreateInstance<IAccommodationRepository>();
            BindAccommodation();
    
        }

        private void BindAccommodation()
        {
            foreach (var n in _notificationRepository.GetAll())
            {
                n.Accommodation = _accommodationRepository.Get(n.Accommodation.Id);
           //     n.Accommodation.Owner.Notifications.Add(n);
            }
        }

        public List<NotificationAccommodationRelease> GetAll()
        {
            return _notificationRepository.GetAll();
        }
        public NotificationAccommodationRelease Get(int id)
        {
            return _notificationRepository.Get(id);
        }
        public void Create(NotificationAccommodationRelease entity)
        {
            _notificationRepository.Create(entity);
        }
        public void Delete(NotificationAccommodationRelease entity)
        {
            _notificationRepository.Delete(entity);
        }
        public NotificationAccommodationRelease Update(NotificationAccommodationRelease entity)
        {
            return _notificationRepository.Update(entity);
        }
        public void Save()
        {
            _notificationRepository.Save();
        }

        public List<NotificationAccommodationRelease> GetForOwner(int ownerId)
        {
            return GetAll().FindAll(n => n.Accommodation.Owner.Id == ownerId && !n.IsDelivered);
        }

        public void FindNotification(int ownerId)
        {
            foreach(Reservation r in InjectorService.CreateInstance<IReservationService>().GetUnratedById(ownerId))
            {
                if(GetAll().Find(n => n.Accommodation.Id == r.Accommodation.Id && !n.IsDelivered) == null)
                {
                    Create(new(r.Accommodation));
                }
            }
        }

    }
}
