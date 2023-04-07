using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfApp.Observer;
using WpfApp1.Domain.RepositoryInterfaces;
using WpfApp1.Model;
using WpfApp1.Repository;

namespace WpfApp1.Service
{
    public class AccommodationService
    {
        private  IAccommodationRepository _accommodationRepository;
        public AccommodationService()
        {
            _accommodationRepository = AccommodationRepository.GetInstance();
        }
        public List<Accommodation> GetAll()
        {
            return _accommodationRepository.GetAll();
        }
        public Accommodation Get(int id)
        {
            return _accommodationRepository.Get(id);
        }
        public void Create(Accommodation accommodation)
        {
            _accommodationRepository.Create(accommodation);
        }
        public void Update(Accommodation accommodation)
        {
            _accommodationRepository.Update(accommodation);
        }
        public void Subscribe(IObserver observer)
        {
            _accommodationRepository.Subscribe(observer);
        }
        public void Unsubscribe(IObserver observer)
        {
            _accommodationRepository.Unsubscribe(observer);
        }
        public List<Accommodation> SearchAccommodation(string name, string city, string state, string type, int guestsNumber, int reservationDays)
        {
            name ??= "";
            city ??= "";
            state ??= "";
            type ??= "";
            return _accommodationRepository.GetAll().Where(a => a.Name.Contains(name) && a.Location.City.Contains(city) && a.Location.State.Contains(state) && a.AccommodationKind.ToString().Contains(type) && a.MaxGuests >= guestsNumber && a.MinResevation <= reservationDays).ToList();
        }
        public List<Accommodation> GetSortedListBySuperOwner()
        {
            return GetAll().OrderBy(a => a.Owner.AverageRating).ToList();
        }
    }
}
