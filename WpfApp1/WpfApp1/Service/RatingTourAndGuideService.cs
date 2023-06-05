using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp.Observer;
using WpfApp1.Domain.Models;
using WpfApp1.Repository;
using WpfApp1.Domain.ServiceInterfaces;
using WpfApp1.Domain.RepositoryInterfaces;

namespace WpfApp1.Service
{
    public class RatingTourAndGuideService : IRatingTourAndGuideService
    {
        private readonly IRatingTourAndGuideRepository _ratingTourAndGuideRepository;
        public ITourBookingRepository _tourBookingRepository;

        public RatingTourAndGuideService()
        {
            _ratingTourAndGuideRepository = InjectorRepository.CreateInstance<IRatingTourAndGuideRepository>();
            _tourBookingRepository = InjectorRepository.CreateInstance<ITourBookingRepository>();
            BindTourBooking();
        }
        private void BindTourBooking()
        {
            foreach (RatingTourAndGuide r in _ratingTourAndGuideRepository.GetAll())
            {
                r.TourBooking = _tourBookingRepository.Get(r.TourBooking.Id);
            }
        }
        public RatingTourAndGuide Get(int id)
        {
            return _ratingTourAndGuideRepository.Get(id);
        }

        public List<RatingTourAndGuide> GetAll()
        {
            return _ratingTourAndGuideRepository.GetAll();
        }
        public void Save()
        {

            _ratingTourAndGuideRepository.Save();
        }

        public void Create(RatingTourAndGuide location)
        {
            _ratingTourAndGuideRepository.Create(location);
        }

        public void Delete(RatingTourAndGuide location)
        {
            _ratingTourAndGuideRepository.Delete(location);
        }

        public RatingTourAndGuide Update(RatingTourAndGuide image)
        {
             return _ratingTourAndGuideRepository.Update(image);
        }

        public void Subscribe(IObserver observer)
        {
            _ratingTourAndGuideRepository.Subscribe(observer);
        }

        public void Unsubscribe(IObserver observer)
        {
            _ratingTourAndGuideRepository.Unsubscribe(observer);
        }

        public List<RatingTourAndGuide> GetReviewFromTourist(int tourBooking, int userId)
        {
            List<RatingTourAndGuide> ratingTourAndGuides = new List<RatingTourAndGuide>();


            foreach (RatingTourAndGuide ratingTourAndGuide in _ratingTourAndGuideRepository.GetAll())
            {
                if (ratingTourAndGuide.TourBooking.Tourist.Id == userId && ratingTourAndGuide.TourBooking.Id == tourBooking)
                {
                    ratingTourAndGuides.Add(ratingTourAndGuide);
                }
            }
            return ratingTourAndGuides;
        }

    }
}
