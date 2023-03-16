using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Model;
using WpfApp1.Repository;
using WpfApp1.Service;

namespace WpfApp1.Controller
{
    public class TourController
    {

        private readonly TourService _tourService;

        public TourController()
        {
            _tourService = new TourService();
        }
    
        public List<Tour> GetAll()
        {
            return _tourService.GetAll();
        }

        public Tour Get(int id)
        {
            return _tourService.Get(id);
        }

       
        public void Create(Tour tour)
        {
            _tourService.Create(tour);
        }
        public void Update(Tour tour)
        {
            _tourService.Update(tour);
        }

        public void Delete(Tour tour)
        {
            _tourService.Delete(tour);
        }

        public List<Tour> TourSearch(string state, string city, string language, string numberOfPeople, string duration)
        {
            return _tourService.TourSearch(state, city, language, numberOfPeople, duration);
        }

    }

}
