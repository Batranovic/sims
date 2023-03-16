using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp.Observer;
using WpfApp1.Model;
using WpfApp1.Repository;

namespace WpfApp1.Controller
{
    public class RatingGuestController
    {
        private readonly RatingGuestDAO _ratingGuests;

        public RatingGuestController(RatingGuestDAO ratingGuestDAO)
        {
            _ratingGuests = ratingGuestDAO;
        }

        public RatingGuest Get(int id)
        {
            return _ratingGuests.Get(id);
        }

        public List<RatingGuest> GetAll()
        {
            return _ratingGuests.GetAll();
        }

        public void Create(RatingGuest location)
        {
            _ratingGuests.Create(location);
        }

        public void Delete(RatingGuest location)
        {
            _ratingGuests.Delete(location);
        }

        public void Update(RatingGuest image)
        {
            _ratingGuests.Update(image);
        }

        public void Subscribe(IObserver observer)
        {
            _ratingGuests.Subscribe(observer);
        }

        public void Unsubscribe(IObserver observer)
        {
            _ratingGuests.Unsubscribe(observer);
        }
    }
}
