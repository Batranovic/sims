using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp.Observer;
using WpfApp1.Model;
using WpfApp1.Repository;

namespace WpfApp1.Service
{
    public class AccommodationService
    {
        private  AccommodationDAO _accommodationDAO;

        public AccommodationService()
        {
            _accommodationDAO = AccommodationDAO.GetInstance();
        }

        public List<Accommodation> GetAll()
        {
            return _accommodationDAO.GetAll();
        }

        public Accommodation Get(int id)
        {
            return _accommodationDAO.Get(id);
        }

        public void Create(Accommodation accommodation)
        {
            _accommodationDAO.Create(accommodation);
        }

        public void Delete(Accommodation accommodation)
        {
            _accommodationDAO.Delete(accommodation);
        }

        public void Update(Accommodation accommodation)
        {
            _accommodationDAO.Update(accommodation);
        }

        public void Subscribe(IObserver observer)
        {
            _accommodationDAO.Subscribe(observer);
        }

        public void Unsubscribe(IObserver observer)
        {
            _accommodationDAO.Unsubscribe(observer);
        }
    }
}
