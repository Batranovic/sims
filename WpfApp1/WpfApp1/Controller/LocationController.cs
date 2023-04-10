using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp.Observer;
using WpfApp1.Models;
using WpfApp1.Repository;
using WpfApp1.Service;

namespace WpfApp1.Controller
{
    public class LocationController
    {
        private readonly LocationService _locationService;

        public LocationController()
        {
            _locationService = new LocationService();
        }

        public Location Get(int id)
        {
            return _locationService.Get(id);
        }

        public List<Location> GetAll()
        {
            return _locationService.GetAll();
        }

        public void Create(Location location)
        {
            _locationService.Create(location);
        }

        public void Delete(Location location)
        {
            _locationService.Delete(location);
        }

        public void Update(Location location)
        {
            _locationService.Update(location);
        }

        public void Subscribe(IObserver observer)
        {
            _locationService.Subscribe(observer);
        }

        public void Unsubscribe(IObserver observer)
        {
            _locationService.Unsubscribe(observer);
        }


        public Location GetByCityAndState(string city, string state)
        {
            return _locationService.GetByCityAndState(city, state);
        }
        public List<string> GetStates()
        {
            return _locationService.GetStates();
        }
        public List<string> GetCitiesFromStates(string state)
        {
            return _locationService.GetCitiesFromStates(state);
        }
    

    }
}
