using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp.Observer;
using WpfApp1.Model;

namespace WpfApp1.Domain.ServiceInterfaces
{
    public interface IOwnerRatingService : IService<OwnerRating>
    {
        void Create(OwnerRating entity);
        void Delete(OwnerRating entity);
        void Subscribe(IObserver observer);
        void Unsubscribe(IObserver observer);
        OwnerRating GetByIdReservation(int idReservation);
        List<OwnerRating> GetAllOwnerRewies(int idOwner);
    }
}
