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
    public class RatingGuestController
    {
        private readonly RatingGuestService _ratingGuestService;

        public RatingGuestController()
        {
            _ratingGuestService = new RatingGuestService();
        }

        public RatingGuest Get(int id)
        {
            return _ratingGuestService.Get(id);
        }

        public List<RatingGuest> GetAll()
        {
            return _ratingGuestService.GetAll();
        }

        public void Create(RatingGuest location)
        {
            _ratingGuestService.Create(location);
        }

        public void Delete(RatingGuest location)
        {
            _ratingGuestService.Delete(location);
        }

        public void Update(RatingGuest image)
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
