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

namespace WpfApp1.Service
{
    public class ReservationPostponementService
    {
        private ReservationPostponementDAO _postponementDAO;

        public ReservationPostponementService()
        {
            _postponementDAO = ReservationPostponementDAO.GetInstance();
        }

        public ReservationPostponement Get(int id)
        {
            return _postponementDAO.Get(id);
        }

        public List<ReservationPostponement> GetAll()
        {
            return _postponementDAO.GetAll();
        }

        public void Create(ReservationPostponement postponement)
        {
            _postponementDAO.Create(postponement);
        }

        public void Delete(ReservationPostponement postponement)
        {
            _postponementDAO.Delete(postponement);
        }

        public void Update(ReservationPostponement postponement)
        {
            _postponementDAO.Update(postponement);
        }

        public void Subscribe(IObserver observer)
        {
            _postponementDAO.Subscribe(observer);
        }

        public void Unsubscribe(IObserver observer)
        {
            _postponementDAO.Unsubscribe(observer);
        }
    }
}
