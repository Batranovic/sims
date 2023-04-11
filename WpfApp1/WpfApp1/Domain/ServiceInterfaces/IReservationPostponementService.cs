using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp.Observer;
using WpfApp1.Domain.Models;
using WpfApp1.Domain.Domain.Models.Enums;

namespace WpfApp1.Domain.ServiceInterfaces
{
    public interface IReservationPostponementService : IService<ReservationPostponement>
    {
        public void Create(ReservationPostponement entity);
        public void Delete(ReservationPostponement entity);
        public void Subscribe(IObserver observer);
        public void Unsubscribe(IObserver observer);
        public List<ReservationPostponement> GetAllByOwnerIdAhead(int idOwner);
        public List<ReservationPostponement> GetByReservation(int idReservation);
    }
}
