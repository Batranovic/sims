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
    public class TouristController
    {
        private readonly TouristService _touristService;
        public TouristController()
        {
            _touristService = new TouristService();
        }
        public Tourist Get(int id)
        {
            return _touristService.Get(id);
        }
        public List<Tourist> GetAll()
        {
            return _touristService.GetAll();
        }
        public void Subscribe(IObserver observer)
        {
            _touristService.Subscribe(observer);
        }
        public void Unsubscribe(IObserver observer)
        {
            _touristService.Unsubscribe(observer);
        }
        public Tourist GetByUsernameAndPassword(string username, string password)
        {
            return _touristService.GetByUsernameAndPassword(username, password);
        }
    }
}
