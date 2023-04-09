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
            { typeof(IAccommodationService), new AccommodationService() },
            { typeof(IGuestRatingService), new GuestRatingService() },
            { typeof(IImageService), new ImageService() },
            { typeof(IGuestService), new GuestService() },
            { typeof(ILocationService), new LocationService() },
            { typeof(IOwnerRatingService), new OwnerRatingService() },
            { typeof(IOwnerService), new OwnerService() },
            { typeof(IReservationPostponementService), new ReservationPostponementService() },
            { typeof(IReservationService), new ReservationService() }
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
