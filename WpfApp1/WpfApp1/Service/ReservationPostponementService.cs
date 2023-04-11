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
using System.Security.Cryptography;
using WpfApp1.Domain.ServiceInterfaces;

namespace WpfApp1.Service
{
    public class ReservationPostponementService : IReservationPostponementService
    {
        private readonly IReservationPostponementRepository _reservationPostponementRepository;
        private readonly IReservationRepository _reservationRepository;
        public ReservationPostponementService()
        {
            _reservationPostponementRepository = InjectorRepository.CreateInstance<IReservationPostponementRepository>();
            _reservationRepository = InjectorRepository.CreateInstance<IReservationRepository>();
            BindReservation();
        }
        private void BindReservation()
        {
            foreach (ReservationPostponement p in GetAll())
            {
                p.Reservation = _reservationRepository.Get(p.IdReservation);
            }
        }
        public void Save()
        {
            _reservationPostponementRepository.Save();
        }
        public ReservationPostponement Get(int id)
        {
            return _reservationPostponementRepository.Get(id);
        }

        public List<ReservationPostponement> GetAll()
        {
            return _reservationPostponementRepository.GetAll();
        }

        public void Create(ReservationPostponement entity)
        {
            _reservationPostponementRepository.Create(entity);
        }

        public void Delete(ReservationPostponement entity)
        {
            _reservationPostponementRepository.Delete(entity);
        }

        public void Update(ReservationPostponement entity)
        {
            _reservationPostponementRepository.Update(entity);
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
            try
            {
                return GetAll().FindAll(r => r.Reservation.Accommodation.OwnerId == idOwner && r.Status == ReservationPostponementStatus.Waiting);
            }
            catch(Exception ex)
            {
                return new List<ReservationPostponement>();
            }
        }

        public List<ReservationPostponement> GetByReservation(int idReservation)
        {
            return _reservationPostponementRepository.GetByReservation(idReservation);
        }
    }
}
