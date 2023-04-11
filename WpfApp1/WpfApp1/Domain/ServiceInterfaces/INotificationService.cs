using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Domain.Models;
using WpfApp.Observer;

namespace WpfApp1.Domain.ServiceInterfaces
{
    internal interface INotificationService : IService<Notification>
    {
        void Delete(Notification notification);

        public List<Notification> GetNotificationForUser(int userId);
    }
}
