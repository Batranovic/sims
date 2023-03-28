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

        public RatingOwner Get(int id)
        {
            return _ratingOwnerService.Get(id);
        }

        public List<RatingOwner> GetAll()
        {
            return _ratingOwnerService.GetAll();
        }

        public void Create(RatingOwner ratingOwner)
        {
            _ratingOwnerService.Create(ratingOwner);
        }

        public void Delete(RatingOwner ratingOwner)
        {
            _ratingOwnerService.Delete(ratingOwner);
        }

        public void Update(RatingOwner ratingOwner)
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
    }
}
