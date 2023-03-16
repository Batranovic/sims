using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp.Observer;
using WpfApp1.Model;
using WpfApp1.Repository;

namespace WpfApp1.Controller
{
    public class ReservationController
    {
        private readonly ReservationDAO _reservations;

        public ReservationController(ReservationDAO reservationDAO)
        {
            _reservations = reservationDAO;
        }

        public Reservation Get(int id)
        {
            return _reservations.Get(id);
        }

        public List<Reservation> GetAll()
        {
            return _reservations.GetAll();
        }

        public void Create(Reservation reservation)
        {
            _reservations.Create(reservation);  
        }

        public void Delete(Reservation reservation)
        {
            _reservations.Delete(reservation);
        }

        public void Update(Reservation reservation)
        {
            _reservations.Update(reservation);
        }

        public void Subscribe(IObserver observer)
        {
            _reservations.Subscribe(observer);
        }

        public void Unsubscribe(IObserver observer)
        {
            _reservations.Unsubscribe(observer);
        }
    }
}
