using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp.Observer;
using WpfApp1.Model;
using WpfApp1.Service;

namespace WpfApp1.Controller
{
    public class ReservationPostponementController
    {
        private readonly ReservationPostponementService _postponementService;

        public ReservationPostponementController()
        {
            _postponementService = new ReservationPostponementService();
        }

        public ReservationPostponement Get(int id)
        {
            return _postponementService.Get(id);
        }

        public List<ReservationPostponement> GetAll()
        {
            return _postponementService.GetAll();
        }

        public void Create(ReservationPostponement postponement)
        {
            _postponementService.Create(postponement);
        }

        public void Delete(ReservationPostponement postponement)
        {
            _postponementService.Delete(postponement);
        }

        public void Update(ReservationPostponement postponement)
        {
            _postponementService.Update(postponement);
        }

        public void Subscribe(IObserver observer)
        {
            _postponementService.Subscribe(observer);
        }

        public void Unsubscribe(IObserver observer)
        {
            _postponementService.Unsubscribe(observer);
        }
    }
}
