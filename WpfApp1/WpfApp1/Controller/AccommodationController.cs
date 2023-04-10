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
    public class AccommodationController
    {
        private readonly AccommodationService _accommodationService;

        public AccommodationController()
        {
            _accommodationService = new AccommodationService();
        }

        public List<Accommodation> GetAll()
        {
            return _accommodationService.GetAll();
        }

        public Accommodation Get(int id)
        {
            return _accommodationService.Get(id);
        }

        public void Create(Accommodation accommodation)
        {
            _accommodationService.Create(accommodation);
        }

        public void Delete(Accommodation accommodation)
        {
            _accommodationService.Delete(accommodation);
        }


        public void Update(Accommodation accommodation)
        {
            _accommodationService.Update(accommodation);
        }

        public void Subscribe(IObserver observer)
        {
            _accommodationService.Subscribe(observer);
        }

        public void Unsubscribe(IObserver observer)
        {
            _accommodationService.Unsubscribe(observer);
        }
        public List<Accommodation> SearchAccommodation(string name, string city, string state, string type, int guestsNumber, int reservationDays)
        {
            return _accommodationService.SearchAccommodation(name, city, state, type, guestsNumber, reservationDays);
        }

        public List<Accommodation> GetSortedListBySuperOwner()
        {
            return _accommodationService.GetSortedListBySuperOwner();
        }

    }
}
