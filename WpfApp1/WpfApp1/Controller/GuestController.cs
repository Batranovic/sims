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
    public class GuestController
    {
        private readonly GuestService _guestService;


        public GuestController()
        {
            _guestService = new GuestService();
        }

        public Guest Get(int id)
        {
            return _guestService.Get(id);
        }

        public List<Guest> GetAll()
        {
            return _guestService.GetAll();
        }

        public void Subscribe(IObserver observer)
        {
            _guestService.Subscribe(observer);
        }

        public void Unsubscribe(IObserver observer)
        {
            _guestService.Unsubscribe(observer);
        }

        public Guest GetByUsernameAndPassword(string username, string password)
        {
            return _guestService.GetByUsernameAndPassword(username, password);
        }
    }
}
