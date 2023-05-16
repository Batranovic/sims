using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Domain.Models;

namespace WpfApp1.Domain.ServiceInterfaces
{
    internal interface IRequestNotifactionService : IService<RequestNotification>
    {
        void Delete(RequestNotification notification);

        public List<RequestNotification> GetNotificationForUser(int userId);
    }
}
