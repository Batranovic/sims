using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using WpfApp.Observer;
using WpfApp1.Model;
using WpfApp1.Model.Enums;
using WpfApp1.Repository;

namespace WpfApp1.Service
{
    public class ReservationService
    {
        private ReservationDAO _reservationDAO;

        public ReservationService()
        {
            _reservationDAO = ReservationDAO.GetInstance();
        }

        public Reservation Get(int id)
        {
            return _reservationDAO.Get(id);
        }

        public List<Reservation> GetAll()
        {
            return _reservationDAO.GetAll();
        }

        public void Create(Reservation reservation)
        {
            _reservationDAO.Create(reservation);
        }

        public void Delete(Reservation reservation)
        {
            _reservationDAO.Delete(reservation);
        }

        public void Update(Reservation reservation)
        {
            _reservationDAO.Update(reservation);
        }



        public void Subscribe(IObserver observer)
        {
            _reservationDAO.Subscribe(observer);
        }

        public void Unsubscribe(IObserver observer)
        {
            _reservationDAO.Unsubscribe(observer);
        }

        public List<Reservation> GetUnratedById(int id)
        {
            List<Reservation> list = _reservationDAO.GetAll().FindAll(r => r.Status == RatingGuestStatus.unrated && r.Accommodation.OwnerId == id).ToList();
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
                if (IsDateInRange(r, startDate))
                {
                    startDate = r.EndDate.AddDays(1);
                }
                else if (IsDateInRange(r, startDate.AddDays(duration)))
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

        public DateTime GetAvailableDate(int idAccommodation, DateTime startDate, DateTime endDate, int duration)
        {
            if (GetAll().Find(r => r.IdAccommodation == idAccommodation) == null)
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
            return GetAll().Where(r => r.IdAccommodation == idAccommodation && (r.Status == Model.Enums.RatingGuestStatus.inprogres || r.Status == Model.Enums.RatingGuestStatus.reserved )).ToList();
        }

        public bool IsDateFree(int idAccommodation, DateTime date)
        {
            bool retVal = true;

            foreach (Reservation r in GetAheadReservationsForAccommodation(idAccommodation))
            {
                retVal = retVal && !IsDateInRange(r, date);
            }
            return retVal;
        }

        public Dictionary<DateTime, DateTime> GetAvailableDates(int idAccommodation, DateTime endDate, int duration)
        {

            Dictionary<DateTime, DateTime> availableDates = new Dictionary<DateTime, DateTime>();
            DateTime temp = endDate;
            DateTime original = endDate;

            for (int i = 0; i < 10; i++)
            {
                if (IsDateFree(idAccommodation, endDate.AddDays(i)) && IsDateFree(idAccommodation, endDate.AddDays(duration)))
                {
                    availableDates.Add(temp.AddDays(i), endDate);
                }
                temp = endDate;
                endDate = original;
            }
            return availableDates;
        }

    }
}
