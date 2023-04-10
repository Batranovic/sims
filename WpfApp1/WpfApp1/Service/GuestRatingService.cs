using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp.Observer;
using WpfApp1.Models;
using WpfApp1.Repository;

namespace WpfApp1.Service
{
    public class GuestRatingService
    {
        private  GuestRatingRepository _ratingGuestDAO;

        public GuestRatingService()
        {
            _ratingGuestDAO = GuestRatingRepository.GetInstance();
        }

        public GuestRating Get(int id)
        {
            return _ratingGuestDAO.Get(id);
        }

        public List<GuestRating> GetAll()
        {
            return _ratingGuestDAO.GetAll();
        }

        public void Create(GuestRating location)
        {
            _ratingGuestDAO.Create(location);
        }

        public void Delete(GuestRating location)
        {
            _ratingGuestDAO.Delete(location);
        }

        public void Update(GuestRating image)
        {
            _ratingGuestDAO.Update(image);
        }

        public void Subscribe(IObserver observer)
        {
            _ratingGuestDAO.Subscribe(observer);
        }

        public void Unsubscribe(IObserver observer)
        {
            _ratingGuestDAO.Unsubscribe(observer);
        }

    }
}
