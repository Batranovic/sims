using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp.Observer;
using WpfApp1.Models;
using WpfApp1.Repository;
using WpfApp1.Service;

namespace WpfApp1.Controller
{
    public class RatingTourAndGuideController
    {
        private readonly RatingTourAndGuideService _ratingTourAndGuideService;

        public RatingTourAndGuideController()
        {
            _ratingTourAndGuideService = new RatingTourAndGuideService();
        }

        public RatingTourAndGuide Get(int id)
        {
            return _ratingTourAndGuideService.Get(id);
        }

        public List<RatingTourAndGuide> GetAll()
        {
            return _ratingTourAndGuideService.GetAll();
        }

        public void Create(RatingTourAndGuide location)
        {
            _ratingTourAndGuideService.Create(location);
        }

        public void Delete(RatingTourAndGuide location)
        {
            _ratingTourAndGuideService.Delete(location);
        }

        public void Update(RatingTourAndGuide image)
        {
            _ratingTourAndGuideService.Update(image);
        }

        public void Subscribe(IObserver observer)
        {
            _ratingTourAndGuideService.Subscribe(observer);
        }

        public void Unsubscribe(IObserver observer)
        {
            _ratingTourAndGuideService.Unsubscribe(observer);
        }
    }
}
