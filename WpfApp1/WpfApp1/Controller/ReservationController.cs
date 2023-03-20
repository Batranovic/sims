using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp.Observer;
using WpfApp1.Model;
using WpfApp1.Repository;
using WpfApp1.Service;

namespace WpfApp1.Controller
{
    public class ReservationController
    {
        private readonly ReservationService _reservationService;

        public ReservationController()
        {
            _reservationService = new ReservationService();
        }

        public Reservation Get(int id)
        {
            return _reservationService.Get(id);
        }

        public List<Reservation> GetAll()
        {
            return _reservationService.GetAll();
        }

        public void Create(Reservation reservation)
        {
            _reservationService.Create(reservation);  
        }

        public void Delete(Reservation reservation)
        {
            _reservationService.Delete(reservation);
        }

        public void Update(Reservation reservation)
        {
            _reservationService.Update(reservation);
        }

        public void Subscribe(IObserver observer)
        {
            _reservationService.Subscribe(observer);
        }

        public void Unsubscribe(IObserver observer)
        {
            _reservationService.Unsubscribe(observer);
        }

        public List<Reservation> GetUnratedById(int id)
        {
            return _reservationService.GetUnratedById(id);
        }

    }
}
