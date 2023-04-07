using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp.Observer;
using WpfApp1.Model;
using WpfApp1.Repository;
using WpfApp1.Model.Enums;
using WpfApp1.Repository;
using WpfApp1.Domain.RepositoryInterfaces;

namespace WpfApp1.Service
{
    public class ReservationPostponementService
    {
        private readonly IReservationPostponementRepository _reservationPostponementRepository;

        public ReservationPostponementService()
        {
            _reservationPostponementRepository = ReservationPostponementRepository.GetInstance();
        }

        public ReservationPostponement Get(int id)
        {
            return _reservationPostponementRepository.Get(id);
        }

        public List<ReservationPostponement> GetAll()
        {
            return _reservationPostponementRepository.GetAll();
        }

        public void Create(ReservationPostponement postponement)
        {
            _reservationPostponementRepository.Create(postponement);
        }

        public void Delete(ReservationPostponement postponement)
        {
            _reservationPostponementRepository.Delete(postponement);
        }

        public void Update(ReservationPostponement postponement)
        {
            _reservationPostponementRepository.Update(postponement);
        }

        public void Subscribe(IObserver observer)
        {
            _reservationPostponementRepository.Subscribe(observer);
        }

        public void Unsubscribe(IObserver observer)
        {
            _reservationPostponementRepository.Unsubscribe(observer);
        }

        public List<ReservationPostponement> GetAllByOwnerIdAhead(int idOwner)
        {
            return GetAll().FindAll(r => r.Reservation.Accommodation.OwnerId == idOwner && r.Status == ReservationPostponementStatus.Waiting);
        }

    }
}
