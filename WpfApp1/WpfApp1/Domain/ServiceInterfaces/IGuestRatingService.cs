using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp.Observer;
using WpfApp1.Domain.Models;
using WpfApp1.Service;

namespace WpfApp1.Domain.ServiceInterfaces
{
    public interface IGuestRatingService : IService<GuestRating>
    {
        void Create(GuestRating entity);
        void Delete(GuestRating entity);
        void Subscribe(IObserver observer);
        void Unsubscribe(IObserver observer);

        List<GuestRating> GetAllGuestReviews(int idGuest);
    }
}
