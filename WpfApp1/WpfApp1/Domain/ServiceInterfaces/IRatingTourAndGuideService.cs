using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Domain.Models;
using WpfApp.Observer;

namespace WpfApp1.Domain.ServiceInterfaces
{
    internal interface IRatingTourAndGuideService : IService<RatingTourAndGuide>
    {
        void Create(RatingTourAndGuide entity);
        void Delete(RatingTourAndGuide entity);
        void Subscribe(IObserver observer);
        void Unsubscribe(IObserver observer);

        public List<RatingTourAndGuide> GetReviewFromTourist(int tourBooking, int userId);
    }
}
