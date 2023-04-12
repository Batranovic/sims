using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Repository;
using WpfApp1.Domain.Models;
using WpfApp.Observer;
using WpfApp1.Domain.ServiceInterfaces;
using WpfApp1.Domain.RepositoryInterfaces;

namespace WpfApp1.Service
{
    public class TourPointService : ITourPointService
    {
        private readonly ITourPointRepository _tourPointRepository;

        public TourPointService()
        {
            _tourPointRepository = InjectorRepository.CreateInstance<ITourPointRepository>();
            BindTourPointTour();
        }
        private void BindTourPointTour()
        {
            foreach (TourPoint tourPoint in _tourPointRepository.GetAll())
            {
                int tourId = tourPoint.Tour.Id;
                Tour tour = TourRepository.GetInstance().Get(tourId);
                if (tour != null)
                {
                    tourPoint.Tour = tour;
                    tour.TourPoints.Add(tourPoint);
                }
                else
                {
                    Console.WriteLine("Error in tourPointTourLocation binding");
                }
            }
        }
        public List<TourPoint> GetAll()
        {
            return _tourPointRepository.GetAll();
        }
        public TourPoint Create(TourPoint voucher)
        {
            return _tourPointRepository.Create(voucher);
        }
        public TourPoint Get(int id)
        {
            return _tourPointRepository.Get(id);
        }

        public  void Save()
        {

             _tourPointRepository.Save();
        }

        public void Delete(TourPoint tourPoint)
        {

            _tourPointRepository.Delete(tourPoint);

        }

        public TourPoint Update(TourPoint tourPoint)
        {
            return _tourPointRepository.Update(tourPoint);
        }
        public void Subscribe(IObserver observer)
        {
            _tourPointRepository.Subscribe(observer);
        }

        public void Unsubscribe(IObserver observer)
        {
            _tourPointRepository.Unsubscribe(observer);
        }

    }
}
