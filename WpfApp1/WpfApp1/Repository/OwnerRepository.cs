using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp.Observer;
using WpfApp1.Domain.RepositoryInterfaces;
using WpfApp1.Domain.Models;
using WpfApp1.Serializer;
using WpfApp1.Service;

namespace WpfApp1.Repository
{
    public class OwnerRepository : IOwnerRepository
    {
        private const string _filePath = "../../../Resources/Data/owners.csv";
        private readonly List<IObserver> _observers;
        private readonly Serializer<Owner> _serializer;
        private List<Owner> _owners;
        private static IOwnerRepository _instance = null;
        public IOwnerRatingRepository IOwnerRatingRepository { get; set; }
        public static IOwnerRepository GetInsatnce()
        {
            if(_instance == null)
            {
                _instance = new OwnerRepository();
            }
            return _instance;
        }

        private OwnerRepository() 
        {
            _serializer = new Serializer<Owner>();
            _owners = new List<Owner>();
            _owners = _serializer.FromCSV(_filePath);
            _observers = new List<IObserver>();
            IOwnerRatingRepository = OwnerRatingRepository.GetInstance();
        }
        public void SetKind()
        {
            foreach(Owner o in _owners)
            {
                if(o.AverageRating >= 4.5)
                {
                    o.Super = true;
                }
                else
                {
                    o.Super = false;
                }
            }
        }
        public double GetAverageRating(List<OwnerRating> ratings)
        {
            double avg = 0;
            foreach(OwnerRating ro in ratings)
            {
                avg += (ro.Timeliness + ro.Cleanliness + ro.OwnerCorrectness) / 3;
            }
            return avg / ratings.Count;
        }
        public void CalculateAverageRating()
        {
            foreach(Owner o in _owners)
            {
                o.AverageRating = GetAverageRating(o.Ratings);
            }
        }

        public void BindRating()
        {
            foreach(OwnerRating ro in IOwnerRatingRepository.GetAll())
            {
                Get(ro.Reservation.Accommodation.OwnerId).Ratings.Add(ro); 
            }
        }
        public  Owner Get(int id)
        {
            return _owners.Find(o => o.Id == id);
        }

        public  List<Owner> GetAll()
        {
            return _owners;
        }
        public void Save()
        {
            throw new NotImplementedException();
        }
        public Owner Update(Owner entity)
        {
            throw new NotImplementedException();
        }
       
    }
}
