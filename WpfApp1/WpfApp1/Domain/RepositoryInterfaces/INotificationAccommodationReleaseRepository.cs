using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Domain.Models;
using WpfApp1.Repository;

namespace WpfApp1.Domain.RepositoryInterfaces
{
    public interface INotificationAccommodationReleaseRepository : IRepository<NotificationAccommodationRelease>
    {
        NotificationAccommodationRelease Create(NotificationAccommodationRelease entity);
        int NextId();
        NotificationAccommodationRelease Delete(NotificationAccommodationRelease entity);
    }
}
