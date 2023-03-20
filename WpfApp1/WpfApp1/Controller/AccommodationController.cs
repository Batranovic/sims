using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp.Observer;
using WpfApp1.Model;
using WpfApp1.Repository;

namespace WpfApp1.Controller
{
    public class AccommodationController
    {
        private readonly AccommodationDAO _accommodations;

        public AccommodationController(AccommodationDAO accommodationDAO)
        {
            _accommodations = accommodationDAO;
        }

        public List<Accommodation> GetAll()
        {
            return _accommodations.GetAll();
        }

        public Accommodation Get(int id)
        {
            return _accommodations.Get(id);
        }

        public void Create(Accommodation accommodation)
        {
            _accommodations.Create(accommodation);
        }

        public void Delete(Accommodation accommodation)
        {
            _accommodations.Delete(accommodation);
        }


        public void Update(Accommodation accommodation)
        {
            _accommodations.Update(accommodation);
        }

        public List<Accommodation> SearchAccommodation(string name, string city, string state, string type, int guestsNumber, int reservationDays)
        {
           return _accommodations.SearchAccommodation(name, city,state,type,guestsNumber,reservationDays);
        }

       

        public void Subscribe(IObserver observer)
        {
            _accommodations.Subscribe(observer);
        }

        public void Unsubscribe(IObserver observer)
        {
            _accommodations.Unsubscribe(observer);
        }
    }
}
