using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp.Observer;
using WpfApp1.Domain.Models;

namespace WpfApp1.Domain.ServiceInterfaces
{
    public interface ISimpleTourRequestService : IService<SimpleTourRequest>
    {
        void Create(SimpleTourRequest entity);
        void Delete(SimpleTourRequest entity);
        void Subscribe(IObserver observer);
        void Unsubscribe(IObserver observer);

        List<SimpleTourRequest> RequestsForTourist(int userId);

    }
}
