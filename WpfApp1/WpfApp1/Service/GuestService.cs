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
    public class GuestService
    {
        private GuestRepository _guestDAO;

        public GuestService()
        {
            _guestDAO = GuestRepository.GetInsatnce();
        }

        public Guest Get(int id)
        {
            return _guestDAO.Get(id);
        }

        public List<Guest> GetAll()
        {
            return _guestDAO.GetAll();
        }
        public void Subscribe(IObserver observer)
        {
            _guestDAO.Subscribe(observer);
        }
        public void Unsubscribe(IObserver observer)
        {
            _guestDAO.Unsubscribe(observer);
        }

        public Guest GetByUsernameAndPassword(string username, string password)
        {
            return GetAll().Find(o => o.Username.Equals(username) && o.Password.Equals(password));
        }
    }
}
