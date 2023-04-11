using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp.Observer;
using WpfApp1.Model;
using WpfApp1.Repository;

namespace WpfApp1.Domain.RepositoryInterfaces
{
    public interface IReservationPostponementRepository : IRepository<ReservationPostponement>, ISubject
    {
        ReservationPostponement Create(ReservationPostponement entity);
        ReservationPostponement Delete(ReservationPostponement entity);
        int NextId();
        List<ReservationPostponement> GetByReservation(int idReservation);
    }
}
