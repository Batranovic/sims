using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp.Observer;
using WpfApp1.Domain.Models;

namespace WpfApp1.Domain.ServiceInterfaces
{
    public interface INotificationAccommodationReleaseService : IService<NotificationAccommodationRelease>
    {
        void Create(NotificationAccommodationRelease entity);
        void Delete(NotificationAccommodationRelease entity);

   
        List<NotificationAccommodationRelease> GetForOwner(int ownerId);
        public void FindNotification(int ownerId);
    }
}
