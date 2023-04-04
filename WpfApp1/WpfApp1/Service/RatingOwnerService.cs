using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using WpfApp.Observer;
using WpfApp1.Model;
using WpfApp1.Repository;

namespace WpfApp1.Service
{
    public class RatingOwnerService
    {
        private RatingOwnerDAO _ratingOwnerDAO;

        public RatingOwnerService()
        {
            _ratingOwnerDAO = RatingOwnerDAO.GetInstance();
        }

        public RatingOwner Get(int id)
        {
            return _ratingOwnerDAO.Get(id);
        }

        public List<RatingOwner> GetAll()
        {
            return _ratingOwnerDAO.GetAll();
        }

        public void Create(RatingOwner accommodationDAO)
        {
            _ratingOwnerDAO.Create(accommodationDAO);
        }

        public void Delete(RatingOwner accommodationDAO)
        {
            _ratingOwnerDAO.Delete(accommodationDAO);
        }

        public void Update(RatingOwner ratingOwner)
        {
            _ratingOwnerDAO.Update(ratingOwner);
        }
        
        public void Subscribe(IObserver observer)
        {
            _ratingOwnerDAO.Subscribe(observer);
        }

        public void Unsubscribe(IObserver observer)
        {
            _ratingOwnerDAO.Unsubscribe(observer);
        }

        public RatingOwner GetByIdReservation(int idReservation)
        {
            return GetAll().Find(r => r.IdReservation == idReservation);
        }

        public List<RatingOwner> GetAllOwnerRewies(int idOwner)
        {
            return GetAll().FindAll(r => r.Reservation.Accommodation.OwnerId == idOwner && r.Reservation.Status == Model.Enums.GuestRatingStatus.Rated);
        }

    }
}
