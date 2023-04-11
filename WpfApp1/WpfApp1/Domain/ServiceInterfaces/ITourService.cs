using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp.Observer;
using WpfApp1.Domain.Models;

namespace WpfApp1.Domain.ServiceInterfaces
{
    public interface ITourService : IService<Tour>
    {
        void Create(Tour entity);
        void Delete(Tour entity);
        void Subscribe(IObserver observer);
        void Unsubscribe(IObserver observer);
        List<Tour> TourSearch(string state, string city, string language, string numberOfPeople, string duration);

        public List<Tour> TourSearchLogic(string state, string city, string language, string numberOfPeople, string duration);

        public bool SearchCondition(Tour tour, string state, string city, string language, string numberOfPeople, string duration);

    }
}
