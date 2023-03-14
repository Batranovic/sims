using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Model;
using WpfApp1.Repository;

namespace WpfApp1.Controller
{
    public class TourController
    {
        private readonly TourDAO _tours;

        public TourController(TourDAO tourDAO)
        {
            _tours = tourDAO;
        }
    
        public List<Tour> GetAll()
        {
            return _tours.GetAll();
        }

        public Tour Get(int id)
        {
            return _tours.Get(id);
        }

        public List<Tour> Search(Location location, TimeSpan duration, string language, int maxGuests)
        {
            return _tours.Search(location, duration, language, maxGuests);
        }
        public void Create(Tour tour)
        {
            _tours.Create(tour);
        }
        public void Update(Tour tour)
        {
            _tours.Update(tour);
        }

    }

}
