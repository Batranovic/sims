using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp.Observer;
using WpfApp1.Model;
using WpfApp1.Repository;

namespace WpfApp1.Service
{
    public class RatingGuestService
    {
        private  RatingGuestDAO _ratingGuestDAO;

        public RatingGuestService()
        {
            _ratingGuestDAO = RatingGuestDAO.GetInstance();
        }

        public RatingGuest Get(int id)
        {
            return _ratingGuestDAO.Get(id);
        }

        public List<RatingGuest> GetAll()
        {
            return _ratingGuestDAO.GetAll();
        }

        public void Create(RatingGuest location)
        {
            _ratingGuestDAO.Create(location);
        }

        public void Delete(RatingGuest location)
        {
            _ratingGuestDAO.Delete(location);
        }

        public void Update(RatingGuest image)
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
