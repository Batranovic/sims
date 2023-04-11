using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp.Observer;
using WpfApp1.Domain.Models;

namespace WpfApp1.Domain.ServiceInterfaces
{
    public interface IReservationService : IService<Reservation>
    {
        void Create(Reservation entity);
        void Delete(Reservation entity);
        void Subscribe(IObserver observer);
        void Unsubscribe(IObserver observer);
        List<Reservation> GetUnratedById(int id);
        bool IsDateInRange(Reservation reservation, DateTime date);
        DateTime CheckAvailableDate(int idAccommodation, DateTime startDate, DateTime endDate, int duration);
        List<Reservation> GetAheadReservationsForAccommodation(int idAccommodation);
        bool IsDateFree(int idAccommodation, DateTime date);
        Dictionary<DateTime, DateTime> GetAvailableDates(int idAccommodation, DateTime endDate, int duration);
        List<Reservation> GetGuestReservations(int id);
        
    }
}
