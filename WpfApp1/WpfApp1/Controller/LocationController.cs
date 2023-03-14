using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Model;
using WpfApp1.Repository;

namespace WpfApp1.Controller
{
    public class LocationController
    {
        private readonly LocationDAO _locations;

        public LocationController(LocationDAO locationDAO)
        {
            _locations = locationDAO;
        }

        public Location Get(int id)
        {
            return _locations.Get(id);
        }

        public List<Location> GetAll()
        {
            return _locations.GetAll();
        }

        public void Create(Location location)
        {
            _locations.Create(location);
        }

        public void Delete(Location location)
        {
            _locations.Delete(location);
        }

        public void Update(Location location)
        {
            _locations.Update(location);
        }

        public Location GetByCityAndState(string city, string state)
        {
            return _locations.GetByCityAndState(city, state);
        }
    }
}
