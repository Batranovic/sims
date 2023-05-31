using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Domain.RepositoryInterfaces;
using WpfApp1.Repository;

namespace WpfApp1.Service
{
    public class InjectorRepository
    {
        private static Dictionary<Type, object> _implementations = new Dictionary<Type, object>
        {
            { typeof(ILocationRepository), LocationRepository.GetInstance() },
            { typeof(IAccommodationRepository),  AccommodationRepository.GetInstance() },
            { typeof(IImageRepository), ImageRepository.GetInsatnce() },
            { typeof(IReservationRepository), ReservationRepository.GetInstance() },
            { typeof(IGuestRatingRepository), GuestRatingRepository.GetInstance() },
            { typeof(IOwnerRepository), OwnerRepository.GetInsatnce() },
            { typeof(IGuestRepository), GuestRepository.GetInsatnce() },
            { typeof(IOwnerRatingRepository), OwnerRatingRepository.GetInstance() },
            { typeof(IReservationPostponementRepository), ReservationPostponementRepository.GetInstance() },
            { typeof(IVoucherRepository), VoucherRepository.GetInstance() },
            { typeof(ITourRepository), TourRepository.GetInstance() },
            { typeof(ITourPointRepository), TourPointRepository.GetInstance() },
            { typeof(ITouristRepository), TouristRepository.GetInstance() },
            { typeof(ITourEventRepository), TourEventRepository.GetInstance() },
            { typeof(ITourBookingRepository), TourBookingRepository.GetInstance() },
            { typeof(INotificationRepository), NotificationRepository.GetInstance() },
            { typeof(IRatingTourAndGuideRepository), RatingTourAndGuideRepository.GetInstance() },
            { typeof(IAccommodationRenovationSuggestionRepository), AccommodationRenovationSuggestionRepository.GetInstance() },
            { typeof(IRenovationRepository), RenovationRepository.GetInstance() },
            { typeof(ISimpleTourRequestRepository), SimpleTourRequestRepository.GetInstance() },
            { typeof(IRequestNotifactionRepository), RequestNotificationRepository.GetInstance() },
            { typeof(INewTourNotificationRepository), NewTourNotificationRepository.GetInstance() },
            { typeof(INotificationAccommodationReleaseRepository), NotificationAccommodationReleaseRepository.GetInstance() },
            { typeof(IForumNotificationRepository), ForumNotificationRepository.GetInstance() },
            { typeof(IForumRepository), ForumRepository.GetInstance() }

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
