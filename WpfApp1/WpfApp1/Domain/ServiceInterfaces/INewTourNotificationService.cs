using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Domain.Models;

namespace WpfApp1.Domain.ServiceInterfaces
{
    internal interface INewTourNotificationService : IService<NewTourNotification>
    {
        void Delete(NewTourNotification notification);
        void Create(NewTourNotification entity);

        List<NewTourNotification> GetAll();

        List<NewTourNotification> GetNotificationForUser(int userId);
    }
}
