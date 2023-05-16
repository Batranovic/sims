using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp.Observer;
using WpfApp1.Domain.RepositoryInterfaces;
using WpfApp1.Domain.ServiceInterfaces;
using WpfApp1.Domain.Models;
using WpfApp1.Repository;

namespace WpfApp1.Service
{
    public class GuestRatingService : IGuestRatingService
    {
        private readonly IGuestRatingRepository _guestRatingRepository;
        private readonly IReservationRepository _reservationRepository;
        public GuestRatingService()
        {
            _guestRatingRepository = InjectorRepository.CreateInstance<IGuestRatingRepository>();
            _reservationRepository = InjectorRepository.CreateInstance<IReservationRepository>();
            BindReservation();
        }
        private void BindReservation()
        {
            foreach (GuestRating r in GetAll())
            {
                r.Reservation = _reservationRepository.Get(r.IdReservation);
            }
        }

        public GuestRating Get(int id)
        {
            return _guestRatingRepository.Get(id);
        }

        public List<GuestRating> GetAll()
        {
            return _guestRatingRepository.GetAll();
        }

        public void Create(GuestRating entity)
        {
            _guestRatingRepository.Create(entity);
        }

        public void Delete(GuestRating entity)
        {
            _guestRatingRepository.Delete(entity);
        }

        public GuestRating Update(GuestRating entity)
        {
           return _guestRatingRepository.Update(entity);
        }
        public void Save()
        {
            _guestRatingRepository.Save();
        }
        public void Subscribe(IObserver observer)
        {
            _guestRatingRepository.Subscribe(observer);
        }

        public void Unsubscribe(IObserver observer)
        {
            _guestRatingRepository.Unsubscribe(observer);
        }

        public List<GuestRating> GetAllGuestReviews(int idGuest)
        {
            return GetAll().FindAll(r => r.Reservation.IdGuest == idGuest && r.Reservation.Status == Domain.Models.Enums.GuestRatingStatus.Rated);
        }

     
    }
}
