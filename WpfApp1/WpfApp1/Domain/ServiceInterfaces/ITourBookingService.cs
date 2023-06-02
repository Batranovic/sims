using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Domain.Models;
using WpfApp.Observer;

namespace WpfApp1.Domain.ServiceInterfaces
{
    internal interface ITourBookingService : IService<TourBooking>
    {
        TourBooking Create(TourBooking entity);
        void Delete(TourBooking entity);
        void Subscribe(IObserver observer);
        void Unsubscribe(IObserver observer);

        List<TourEvent> TouristTourEvents(int userId);

        TourBooking GetTourBookingForTourEventAndUser(int tourEventId, int userId);

        void GetExistingTourBooking(int tourEvent, int user, int numOfPeople);

        List<TourBooking> GetTourBookingsForTourist(int userId);
        Voucher WonVoucher(int userId, int tourB, DateTime time);


    }
}
