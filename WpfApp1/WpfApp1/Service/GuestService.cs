using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp.Observer;
using WpfApp1.Domain.RepositoryInterfaces;
using WpfApp1.Domain.ServiceInterfaces;
using WpfApp1.Domain.Models;
using WpfApp1.Repository;

namespace WpfApp1.Service
{
    public class GuestService : IGuestService
    {
        private readonly IGuestRepository _guestRepository;

        public GuestService()
        {
            _guestRepository = InjectorRepository.CreateInstance<IGuestRepository>();
        }

        //public void SetKind()
        //{
        //    foreach(Guest g in GetAll())
        //    {
        //        if(g.Reservations.Count >= 10)
        //        {
        //            g.Super = true;
        //            g.BonusPoints = 5;
        //            g.SuperGuestExpirationDate = DateTime.Now.AddYears(1);
        //        }
        //        else
        //        {
        //            g.Super = false;
        //            g.BonusPoints = 0;
        //            g.SuperGuestExpirationDate = DateTime.MinValue;
        //        }
        //    }
        //}

        public bool CanUseBonusPoints(Guest guest)
        {
            return guest.Super && guest.BonusPoints > 0 && DateTime.Now.Date <= guest.SuperGuestExpirationDate.Date;
        }

        public void ResetBonusPoints(Guest guest)
        {
            if (DateTime.Now > guest.SuperGuestExpirationDate || !guest.Super)
            {
                guest.BonusPoints = 0;
            }
        }

        public Guest Get(int id)
        {
            return _guestRepository.Get(id);
        }

        public List<Guest> GetAll()
        {
            return _guestRepository.GetAll();
        }
        public Guest Update(Guest entity)
        {
            return _guestRepository.Update(entity);
        }
        public void Save()
        {
            _guestRepository.Save();
        }

        public Guest GetByUsernameAndPassword(string username, string password)
        {
            return GetAll().Find(o => o.Username.Equals(username) && o.Password.Equals(password));
        }

    }
}