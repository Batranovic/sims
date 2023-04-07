using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp.Observer;
using WpfApp1.Domain.RepositoryInterfaces;
using WpfApp1.Model;
using WpfApp1.Repository;

namespace WpfApp1.Service
{
    public class GuestRatingService
    {
        private readonly IGuestRatingRepository _guestRatingRepository;

        public GuestRatingService()
        {
            _guestRatingRepository = GuestRatingRepository.GetInstance();
        }

        public GuestRating Get(int id)
        {
            return _guestRatingRepository.Get(id);
        }

        public List<GuestRating> GetAll()
        {
            return _guestRatingRepository.GetAll();
        }

        public void Create(GuestRating location)
        {
            _guestRatingRepository.Create(location);
        }

        public void Delete(GuestRating location)
        {
            _guestRatingRepository.Delete(location);
        }

        public void Update(GuestRating image)
        {
            _guestRatingRepository.Update(image);
        }

        public void Subscribe(IObserver observer)
        {
            _guestRatingRepository.Subscribe(observer);
        }

        public void Unsubscribe(IObserver observer)
        {
            _guestRatingRepository.Unsubscribe(observer);
        }

    }
}
