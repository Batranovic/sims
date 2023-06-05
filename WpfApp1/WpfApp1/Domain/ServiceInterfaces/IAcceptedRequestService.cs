using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp.Observer;
using WpfApp1.Domain.Models;
using WpfApp1.Domain.RepositoryInterfaces;

namespace WpfApp1.Domain.ServiceInterfaces
{
    internal interface IAcceptedRequestService : IService<AcceptedRequestGuide>
    {
        void Delete(AcceptedRequestGuide entity);
        void Subscribe(IObserver observer);
        void Unsubscribe(IObserver observer);
    }
}
