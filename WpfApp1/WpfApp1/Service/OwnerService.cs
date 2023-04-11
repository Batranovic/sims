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
    public class OwnerService : IOwnerService
    {
        private readonly IOwnerRepository _ownerRepository;
        private readonly IOwnerRatingRepository _ownerRatingRepository;
        public OwnerService()
        {
            _ownerRepository = InjectorRepository.CreateInstance<IOwnerRepository>();
            _ownerRatingRepository = InjectorRepository.CreateInstance<IOwnerRatingRepository>();
        }
        public void SetKind()
        {
            foreach (Owner o in GetAll())
            {
                if (o.AverageRating >= 4.5)
                {
                    o.Super = true;
                }
                else
                {
                    o.Super = false;
                }
            }
            Save();
        }

        public double GetAverageRating(List<OwnerRating> ratings)
        {
            double avg = 0;
            foreach (OwnerRating ro in ratings)
            {
                avg += (ro.Timeliness + ro.Cleanliness + ro.OwnerCorrectness) / 3;
            }
            return avg / ratings.Count;
        }
        public void CalculateAverageRating()
        {
            BindRating();
            foreach (Owner o in GetAll())
            {
                o.AverageRating = GetAverageRating(o.Ratings);
            }
            SetKind();
        }
        private void BindRating()
        {
            foreach (OwnerRating ro in _ownerRatingRepository.GetAll())
            {
                Get(ro.Reservation.Accommodation.OwnerId).Ratings.Add(ro);
            }
        }

        public void Save()
        {
            _ownerRepository.Save();
        }
     
        public Owner Get(int id)
        {
            return _ownerRepository.Get(id);
        }

        public List<Owner> GetAll()
        {
            return _ownerRepository.GetAll();
        }

        public Owner Update(Owner entity)
        {
           return _ownerRepository.Update(entity);
        }

        public Owner GetByUsernameAndPassword(string username, string password)
        {
            return GetAll().Find(o => o.Username.Equals(username) && o.Password.Equals(password));
        }

    }
}
