using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp.Observer;
using WpfApp1.Model;
using WpfApp1.Serializer;

namespace WpfApp1.Repository
{
    public class OwnerDAO : IDAO<Owner>, ISubject
    {
        private const string _filePath = "../../../Resources/Data/owners.csv";
        private readonly List<IObserver> _observers;
        private readonly Serializer<Owner> _serializer;
        private List<Owner> _owners;
        private static OwnerDAO _instance = null;
        public RatingOwnerDAO RatingOwnerDAO { get; set; }
        public static OwnerDAO GetInsatnce()
        {
            if(_instance == null)
            {
                _instance = new OwnerDAO();
            }
            return _instance;
        }

        private OwnerDAO() 
        {
            _serializer = new Serializer<Owner>();
            _owners = new List<Owner>();
            _owners = _serializer.FromCSV(_filePath);
            _observers = new List<IObserver>();
            RatingOwnerDAO = RatingOwnerDAO.GetInstance();
        }
        public void SetKind()
        {
            foreach(Owner o in _owners)
            {
                if(o.AverageRating >= 9.5)
                {
                    o.OwnerKind = Model.Enums.OwnerKind.Super;
                }
                else
                {
                    o.OwnerKind = Model.Enums.OwnerKind.Basic;
                }
            }
        }
        private double GetAverageRating(List<RatingOwner> ratings)
        {
            double avg = 0;
            foreach(RatingOwner ro in ratings)
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
            foreach(RatingOwner ro in RatingOwnerDAO.GetAll())
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

        public void NotifyObservers()
        {
            foreach (var observer in _observers)
            {
                observer.Update();
            }
        }

        public void Subscribe(IObserver observer)
        {
            _observers.Add(observer);
        }

        public void Unsubscribe(IObserver observer)
        {
            _observers.Remove(observer);
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public Owner Create(Owner entity)
        {
            throw new NotImplementedException();
        }

        public Owner Update(Owner entity)
        {
            throw new NotImplementedException();
        }

        public Owner Delete(Owner entity)
        {
            throw new NotImplementedException();
        }

        public int NextId()
        {
            throw new NotImplementedException();
        }
    }
}
