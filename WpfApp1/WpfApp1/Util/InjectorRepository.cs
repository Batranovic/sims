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
            { typeof(IAccommodationRepository),  AccommodationRepository.GetInstance() },
            { typeof(IGuestRatingRepository), GuestRatingRepository.GetInstance() },
            { typeof(IImageRepository), ImageRepository.GetInsatnce() },
            { typeof(IGuestRepository), GuestRepository.GetInsatnce() },
            { typeof(ILocationRepository), LocationRepository.GetInstance() },
            { typeof(IOwnerRatingRepository), OwnerRatingRepository.GetInstance() },
            { typeof(IOwnerRepository), OwnerRepository.GetInsatnce() },
            { typeof(IReservationPostponementRepository), ReservationPostponementRepository.GetInstance() },
            { typeof(IReservationRepository), ReservationRepository.GetInstance() }
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
