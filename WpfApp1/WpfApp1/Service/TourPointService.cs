using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Repository;
using WpfApp1.Models;

namespace WpfApp1.Service
{
    public class TourPointService
    {
        private TourPointRepository _tourPointDAO;

        public TourPointService()
        {
            _tourPointDAO = TourPointRepository.GetInstance();
        }

        public List<TourPoint> GetAll()
        {
            return _tourPointDAO.GetAll();
        }

        public TourPoint Get(int id)
        {
            return _tourPointDAO.Get(id);
        }

        public TourPoint Save(TourPoint tourPoint)
        {

            return _tourPointDAO.Save(tourPoint);
        }

        public void Delete(TourPoint tourPoint)
        {

            _tourPointDAO.Delete(tourPoint);

        }

        public TourPoint Update(TourPoint tourPoint)
        {
            return _tourPointDAO.Update(tourPoint);
        }

       
    }
}
