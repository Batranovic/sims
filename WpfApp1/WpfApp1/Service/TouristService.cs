using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp.Observer;
using WpfApp1.Models;
using WpfApp1.Repository;

namespace WpfApp1.Service
{
    public class TouristService
    {
        private TouristRepository _touristDAO;

        public TouristService()
        {
            _touristDAO = TouristRepository.GetInstance();
        }
        public Tourist Get(int id)
        {
            return _touristDAO.Get(id);
        }

        public List<Tourist> GetAll()
        {
            return _touristDAO.GetAll();
        }
        public void Subscribe(IObserver observer)
        {
            _touristDAO.Subscribe(observer);
        }
        public void Unsubscribe(IObserver observer)
        {
            _touristDAO.Unsubscribe(observer);
        }

        public Tourist GetByUsernameAndPassword(string username, string password)
        {
            return GetAll().Find(o => o.Username.Equals(username) && o.Password.Equals(password));
        }
    }
}
