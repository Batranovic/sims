using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfApp.Observer;
using WpfApp1.Model;
using WpfApp1.Repository;

namespace WpfApp1.Service
{
    public class AccommodationService
    {
        private  AccommodationDAO _accommodationDAO;

        public AccommodationService()
        {
            _accommodationDAO = AccommodationDAO.GetInstance();
        }

        public List<Accommodation> GetAll()
        {
            return _accommodationDAO.GetAll();
        }

        public Accommodation Get(int id)
        {
            return _accommodationDAO.Get(id);
        }

        public void Create(Accommodation accommodation)
        {
            _accommodationDAO.Create(accommodation);
        }

        public void Delete(Accommodation accommodation)
        {
            _accommodationDAO.Delete(accommodation);
        }

        public void Update(Accommodation accommodation)
        {
            _accommodationDAO.Update(accommodation);
        }

        public void Subscribe(IObserver observer)
        {
            _accommodationDAO.Subscribe(observer);
        }

        public void Unsubscribe(IObserver observer)
        {
            _accommodationDAO.Unsubscribe(observer);
        }


        public List<Accommodation> SearchAccommodation(string name, string city, string state, string type, int guestsNumber, int reservationDays)
        {
            name ??= "";
            city ??= "";
            state ??= "";
            type ??= "";
            return _accommodationDAO.GetAll().Where(a => a.Name.Contains(name) && a.Location.City.Contains(city) && a.Location.State.Contains(state) && a.AccommodationKind.ToString().Contains(type) && a.MaxGuests >= guestsNumber && a.MinResevation <= reservationDays).ToList();
        }

    }
}
