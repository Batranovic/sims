using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp.Observer;
using WpfApp1.Domain.Models;

namespace WpfApp1.Domain.ServiceInterfaces
{
    public interface ILocationService : IService<Location>
    {
        void Create(Location entity);
        void Delete(Location entity);


        void Subscribe(IObserver observer);

        void Unsubscribe(IObserver observer);

        List<string> GetStates();
        List<string> GetCitiesFromStates(string state);

        Location GetByCityAndState(string city, string state);
    }
}
