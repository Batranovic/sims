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
    public class RatingTourAndGuideService
    {
        private RatingTourAndGuideDAO _ratingTourAndGuideDAO;

        public RatingTourAndGuideService()
        {
            _ratingTourAndGuideDAO = RatingTourAndGuideDAO.GetInstance();
        }

        public RatingTourAndGuide Get(int id)
        {
            return _ratingTourAndGuideDAO.Get(id);
        }

        public List<RatingTourAndGuide> GetAll()
        {
            return _ratingTourAndGuideDAO.GetAll();
        }

        public void Create(RatingTourAndGuide location)
        {
            _ratingTourAndGuideDAO.Create(location);
        }

        public void Delete(RatingTourAndGuide location)
        {
            _ratingTourAndGuideDAO.Delete(location);
        }

        public void Update(RatingTourAndGuide image)
        {
            _ratingTourAndGuideDAO.Update(image);
        }

        public void Subscribe(IObserver observer)
        {
            _ratingTourAndGuideDAO.Subscribe(observer);
        }

        public void Unsubscribe(IObserver observer)
        {
            _ratingTourAndGuideDAO.Unsubscribe(observer);
        }

    }
}
