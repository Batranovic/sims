using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp.Observer;
using WpfApp1.Domain.RepositoryInterfaces;
using WpfApp1.Domain.ServiceInterfaces;
using WpfApp1.Model;
using WpfApp1.Repository;

namespace WpfApp1.Service
{
    public class LocationService : ILocationService
    {
        private readonly ILocationRepository _locationRepository;

        public LocationService()
        {
            _locationRepository = InjectorRepository.CreateInstance<ILocationRepository>();
        }
        public void Save()
        {
            _locationRepository.Save();
        }
        public Location Get(int id)
        {
            return _locationRepository.Get(id);
        }
        public List<Location> GetAll()
        {
            return _locationRepository.GetAll();
        }
        public void Create(Location location)
        {
            _locationRepository.Create(location);
        }
        public void Delete(Location location)
        {
            _locationRepository.Delete(location);
        }
        public void Update(Location location)
        {
            _locationRepository.Update(location);
        }
        public void Subscribe(IObserver observer)
        {
            _locationRepository.Subscribe(observer);
        }
        public void Unsubscribe(IObserver observer)
        {
            _locationRepository.Subscribe(observer);
        }
        public List<string> GetStates()
        {
            List<string> temp = _locationRepository.GetAll().Select(l => l.State).Distinct().ToList();
            var states = new List<string>();
            states.Add(string.Empty);
            return states.Concat(temp).ToList();


        }
        public List<string> GetCitiesFromStates(string state)
        {
            List<string> temp = _locationRepository.GetAll().FindAll(l => l.State.Equals(state)).Select(l => l.City).ToList();
            var cities = new List<string>();
            cities.Add(string.Empty);
            return cities.Concat(temp).ToList();
        }

        public Location GetByCityAndState(string city, string state)
        {
            return _locationRepository.GetAll().Find(l => l.State.ToLower().Equals(state.ToLower()) && l.City.ToLower().Equals(city.ToLower()));
        }

    
   
    }
}
