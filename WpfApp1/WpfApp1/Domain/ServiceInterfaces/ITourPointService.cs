using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Domain.Models;
using WpfApp.Observer;

namespace WpfApp1.Domain.ServiceInterfaces
{
    public interface ITourPointService : IService<TourPoint>
    {
        TourPoint Create(TourPoint entity);
        void Delete(TourPoint entity);
        void Subscribe(IObserver observer);
        void Unsubscribe(IObserver observer);
    }
}
