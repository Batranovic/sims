using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Domain.Models;
using WpfApp.Observer;

namespace WpfApp1.Domain.ServiceInterfaces
{
    internal interface ITouristService : IService<Tourist>
    {

        void Subscribe(IObserver observer);
        void Unsubscribe(IObserver observer);

        Tourist GetByUsernameAndPassword(string username, string password);
        Tourist getName(int userId);
    }
}
