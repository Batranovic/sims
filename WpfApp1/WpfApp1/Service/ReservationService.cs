using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using WpfApp.Observer;
using WpfApp1.Domain.RepositoryInterfaces;
using WpfApp1.Domain.ServiceInterfaces;
using WpfApp1.Domain.Models;
using WpfApp1.Domain.Domain.Models.Enums;
using WpfApp1.Repository;
using WpfApp1.Domain.Models.Enums;
using WpfApp1.DTO;

namespace WpfApp1.Service
{
    public class ReservationService : IReservationService
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly IAccommodationRepository _accommodationRepository;
        private readonly IGuestRepository _guestRepository;
        private readonly IReservationPostponementRepository _reservationPostponementRepository;
        public ReservationService()
        {
            _reservationRepository = InjectorRepository.CreateInstance<IReservationRepository>();
            _accommodationRepository = InjectorRepository.CreateInstance<IAccommodationRepository>();
            _guestRepository = InjectorRepository.CreateInstance<IGuestRepository>();
            _reservationPostponementRepository = InjectorRepository.CreateInstance<IReservationPostponementRepository>();
            BindAccommodation();
            BindGuest();
            SetKind();
        }
        private void BindAccommodation()
        {
            foreach (Reservation r in GetAllWithDeleted())
            {
                r.Accommodation = _accommodationRepository.Get(r.Accommodation.Id);
            }
        }
        private void BindGuest()
        {
            foreach (Reservation r in GetAllWithDeleted())
            {
                r.Guest = _guestRepository.Get(r.IdGuest);
                r.Guest.Reservations.Add(r);
            }
        }

        private void SetKind()
        {
            foreach (Guest g in _guestRepository.GetAll())
            {

                if(g.Super)
                {
                    continue;
                }
                if (g.Reservations.Count >= 10)
                {
                    g.Super = true;
                    g.BonusPoints = 5;
                    g.SuperGuestExpirationDate = DateTime.Now.AddYears(1);
                }
                else
                {
                    g.Super = false;
                    g.BonusPoints = 0;
                    g.SuperGuestExpirationDate = DateTime.MinValue;
                }
            }
        }



        public void Save()
        {
            _reservationRepository.Save();
        }
        public Reservation Get(int id)
        {
            return _reservationRepository.Get(id);
        }

        public Reservation GetWithDeleted(int id)
        {
            return _reservationRepository.GetWithDeleted(id);
        }

        public List<Reservation> GetAll()
        {
            return _reservationRepository.GetAll();
        }

        public List<Reservation> GetAllWithDeleted()
        {
            return _reservationRepository.GetAllWithDeleted();
        }

        public void Create(Reservation reservation)
        {
            _reservationRepository.Create(reservation);
        }
        private void DeleteReservationPostponement(Reservation reservation)
        {
            var reservationPostponets = _reservationPostponementRepository.GetByReservation(reservation.Id);
            if (reservationPostponets.Count != 0)
            {
                foreach (var r in reservationPostponets)
                {
                    _reservationPostponementRepository.Delete(r);
                }
            }
        }
        public void Delete(Reservation reservation)
        {
            DeleteReservationPostponement(reservation);
            _reservationRepository.Delete(reservation);
        }
        public Reservation Update(Reservation reservation)
        {
           return  _reservationRepository.Update(reservation);
        }
        public void Subscribe(IObserver observer)
        {
            _reservationRepository.Subscribe(observer);
        }
        public void Unsubscribe(IObserver observer)
        {
            _reservationRepository.Unsubscribe(observer);
        }
        public List<Reservation> GetUnratedById(int id)
        {
            List<Reservation> list = _reservationRepository.GetAll().FindAll(r => r.Status == GuestRatingStatus.Unrated && r.Accommodation.Owner.Id == id).ToList();
            if (list == null)
            {
                return new List<Reservation>();
            }
            
            return list;
        }

        public List<Reservation> GetGuestReservations(int id)
        {
            List<Reservation> list = _reservationRepository.GetAll().FindAll(r => r.IdGuest == id).ToList();
            if (list == null)
            {
                return new List<Reservation>();
            }
            return list;
        }
        public bool IsDateInRange(Reservation reservation, DateTime date)
        {
            return date >= reservation.StartDate && date <= reservation.EndDate;
        }
        private DateTime CheckDateAvailability(Reservation r, DateTime startDate, DateTime endDate, int duration)
        {
            while ((endDate - startDate).Days >= duration)
            {
                if (IsDateInRange(r, startDate) && InjectorService.CreateInstance<IRenovationService>().IsDateFree(startDate, r.Accommodation.Id))
                {
                    startDate = r.EndDate.AddDays(1);
                }
                else if (IsDateInRange(r, startDate.AddDays(duration)) && InjectorService.CreateInstance<IRenovationService>().IsDateFree(startDate.AddDays(duration), r.Accommodation.Id))
                {
                    startDate = r.EndDate.AddDays(1);
                }
                else
                {
                    return startDate;
                }

            }

            return endDate;
        }
        public DateTime CheckAvailableDate(int idAccommodation, DateTime startDate, DateTime endDate, int duration)
        {
            if(GetAheadReservationsForAccommodation(idAccommodation).Count == 0)
            {
                return startDate;
            }

            foreach (Reservation r in GetAheadReservationsForAccommodation(idAccommodation))
            {
                return CheckDateAvailability(r, startDate, endDate, duration);
            }

            return endDate;
        }
        public List<Reservation> GetAheadReservationsForAccommodation(int idAccommodation)
        {
            try
            {
                return GetAll().Where(r => r.Accommodation.Id == idAccommodation && (r.Status == Domain.Models.Enums.GuestRatingStatus.Inprogres || r.Status == Domain.Models.Enums.GuestRatingStatus.Reserved)).ToList();
            }
            catch
            {
                return new List<Reservation>();
            }
        }
        public bool IsDateFree(int idAccommodation, DateTime date)
        {
            foreach (Reservation r in GetAheadReservationsForAccommodation(idAccommodation))
            {
                if(IsDateInRange(r, date))
                {
                    return false;
                }
            }
            return true; 
        }
        public Dictionary<DateTime, DateTime> GetAvailableDates(int idAccommodation, DateTime endDate, int duration)
        {

            Dictionary<DateTime, DateTime> availableDates = new Dictionary<DateTime, DateTime>();
            DateTime temp = endDate;

            for (int i = 0; i < 10; i++)
            {
                endDate = temp.AddDays(duration);

                if (InjectorService.CreateInstance<IRenovationService>().IsDateFree(endDate.AddDays(i), idAccommodation) && InjectorService.CreateInstance<IRenovationService>().IsDateFree(endDate.AddDays(duration), idAccommodation) && IsDateFree(idAccommodation, endDate.AddDays(i)) && IsDateFree(idAccommodation, endDate.AddDays(duration)))
                {
                      availableDates.Add(endDate.AddDays(i), temp.AddDays(i)); //contra bind
                }

            }
            return availableDates;
        }

        

    }
}
