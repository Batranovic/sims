using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Domain.RepositoryInterfaces;
using WpfApp1.Domain.ServiceInterfaces;
using WpfApp1.Repository;

namespace WpfApp1.Service
{
    public class InjectorService
    {
        private static Dictionary<Type, object> _implementations = new Dictionary<Type, object>
        {
            { typeof(ILocationService), new LocationService() },
            { typeof(IAccommodationService), new AccommodationService() },
            { typeof(IGuestRatingService), new GuestRatingService() },
            { typeof(IImageService), new ImageService() },
            { typeof(IGuestService), new GuestService() },
            { typeof(IOwnerRatingService), new OwnerRatingService() },
            { typeof(IOwnerService), new OwnerService() },
            { typeof(IReservationPostponementService), new ReservationPostponementService() },
            { typeof(IReservationService), new ReservationService() },
            { typeof(IVoucherService), new VoucherService() },
            { typeof(ITourService), new TourService() },
            { typeof(ITourPointService), new TourPointService() },
            { typeof(ITouristService), new TouristService() },
            { typeof(ITourEventService), new TourEventService() },
            { typeof(ITourBookingService), new TourBookingService() },
            { typeof(IRatingTourAndGuideService), new RatingTourAndGuideService() },
            { typeof(INotificationService), new NotificationService() },
            { typeof(IRenovationService), new RenovationService() },
            { typeof(ISimpleTourRequestService), new SimpleTourRequestService() },
            { typeof(IRequestNotifactionService), new RequestNotificationService() },
            { typeof(INewTourNotificationService), new NewTourNotificationService() },
        };
        public static T CreateInstance<T>()
        {
            Type type = typeof(T);
            if (_implementations.ContainsKey(type))
            {
                return (T)_implementations[type];
            }
            throw new ArgumentException($"No implementation found for type {type}");
        }
    }
}
