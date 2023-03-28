using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp.Observer;
using WpfApp1.Model;
using WpfApp1.Repository;

namespace WpfApp1.Service
{
    public class LocationService
    {
        private LocationDAO _locationDAO;

        public LocationService()
        {
            _locationDAO = LocationDAO.GetInstance();
        }

        public Location Get(int id)
        {
            return _locationDAO.Get(id);
        }

        public List<Location> GetAll()
        {
            return _locationDAO.GetAll();
        }

        public void Create(Location location)
        {
            _locationDAO.Create(location);
        }

        public void Delete(Location location)
        {
            _locationDAO.Delete(location);
        }

        public void Update(Location location)
        {
            _locationDAO.Update(location);
        }

        public void Subscribe(IObserver observer)
        {
            _locationDAO.Subscribe(observer);
        }

        public void Unsubscribe(IObserver observer)
        {
            _locationDAO.Subscribe(observer);
        }

        public List<string> GetStates()
        {
            List<string> temp = _locationDAO.GetAll().Select(l => l.State).Distinct().ToList();
            var states = new List<string>();
            states.Add(string.Empty);
            return states.Concat(temp).ToList();


        }
        public List<string> GetCitiesFromStates(string state)
        {
            List<string> temp =_locationDAO.GetAll().FindAll(l => l.State.Equals(state)).Select(l => l.City).ToList();
            var cities = new List<string>();
            cities.Add(string.Empty);
            return cities.Concat(temp).ToList();
        }

        public Location GetByCityAndState(string city, string state)
        {
            return _locationDAO.GetAll().Find(l => l.State.ToLower().Equals(state.ToLower()) && l.City.ToLower().Equals(city.ToLower()));
        }

    
   
    }
}
