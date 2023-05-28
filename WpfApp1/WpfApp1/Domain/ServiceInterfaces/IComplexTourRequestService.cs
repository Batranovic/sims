using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp.Observer;
using WpfApp1.Domain.Models;

namespace WpfApp1.Domain.ServiceInterfaces
{
    public interface IComplexTourRequestService : IService<ComplexTourRequest>
    {

        void Create(ComplexTourRequest entity);
        void Delete(ComplexTourRequest entity);
        void Subscribe(IObserver observer);
        void Unsubscribe(IObserver observer);

        List<ComplexTourRequest> RequestsForTourist(int userId);
    }
}
