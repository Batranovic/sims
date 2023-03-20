using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp.Observer;
using WpfApp1.Model;
using WpfApp1.Model.Enums;
using WpfApp1.Repository;

namespace WpfApp1.Service
{
    public class ReservationService
    {
        private  ReservationDAO _reservationDAO;

        public ReservationService()
        {
            _reservationDAO = ReservationDAO.GetInstance();
        }

        public Reservation Get(int id)
        {
            return _reservationDAO.Get(id);
        }

        public List<Reservation> GetAll()
        {
            return _reservationDAO.GetAll();
        }

        public void Create(Reservation reservation)
        {
            _reservationDAO.Create(reservation);
        }

        public void Delete(Reservation reservation)
        {
            _reservationDAO.Delete(reservation);
        }

        public void Update(Reservation reservation)
        {
            _reservationDAO.Update(reservation);
        }



        public void Subscribe(IObserver observer)
        {
            _reservationDAO.Subscribe(observer);
        }

        public void Unsubscribe(IObserver observer)
        {
            _reservationDAO.Unsubscribe(observer);
        }

        public List<Reservation> GetUnratedById(int id)
        {
            List<Reservation> list = _reservationDAO.GetAll().FindAll(r => r.Status == RatingGuestStatus.unrated && r.Accommodation.OwnerId == id).ToList();
            if (list == null)
            {
                return new List<Reservation>();
            }
            return list;
        }

    }
}
