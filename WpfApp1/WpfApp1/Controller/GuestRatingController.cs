using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp.Observer;
using WpfApp1.Model;
using WpfApp1.Repository;
using WpfApp1.Service;

namespace WpfApp1.Controller
{
    public class GuestRatingController
    {
        private readonly GuestRatingService _ratingGuestService;

        public GuestRatingController()
        {
            _ratingGuestService = new GuestRatingService();
        }

        public GuestRating Get(int id)
        {
            return _ratingGuestService.Get(id);
        }

        public List<GuestRating> GetAll()
        {
            return _ratingGuestService.GetAll();
        }

        public void Create(GuestRating location)
        {
            _ratingGuestService.Create(location);
        }

        public void Delete(GuestRating location)
        {
            _ratingGuestService.Delete(location);
        }

        public void Update(GuestRating image)
        {
            _ratingGuestService.Update(image);
        }

        public void Subscribe(IObserver observer)
        {
            _ratingGuestService.Subscribe(observer);
        }

        public void Unsubscribe(IObserver observer)
        {
            _ratingGuestService.Unsubscribe(observer);
        }
    }
}
