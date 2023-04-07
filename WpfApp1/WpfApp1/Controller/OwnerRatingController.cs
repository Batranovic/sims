using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp.Observer;
using WpfApp1.Model;
using WpfApp1.Service;

namespace WpfApp1.Controller
{
    public class RatingOwnerController
    {

        private readonly RatingOwnerService _ratingOwnerService;

        public RatingOwnerController()
        {
            _ratingOwnerService = new RatingOwnerService();
        }

        public OwnerRating Get(int id)
        {
            return _ratingOwnerService.Get(id);
        }

        public List<OwnerRating> GetAll()
        {
            return _ratingOwnerService.GetAll();
        }

        public void Create(OwnerRating ratingOwner)
        {
            _ratingOwnerService.Create(ratingOwner);
        }

        public void Delete(OwnerRating ratingOwner)
        {
            _ratingOwnerService.Delete(ratingOwner);
        }

        public void Update(OwnerRating ratingOwner)
        {
            _ratingOwnerService.Update(ratingOwner);
        }

        public void Subscribe(IObserver observer)
        {
            _ratingOwnerService.Subscribe(observer);
        }

        public void Unsubscribe(IObserver observer)
        {
            _ratingOwnerService.Unsubscribe(observer);
        }

        public OwnerRating GetByIdReservation(int idReservation)
        {
            return _ratingOwnerService.GetByIdReservation(idReservation);
        }

        public List<OwnerRating> GetAllOwnerRewies(int idOwner)
        {
            return _ratingOwnerService.GetAllOwnerRewies(idOwner);
        }


    }
}
