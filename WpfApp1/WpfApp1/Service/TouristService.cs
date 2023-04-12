using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp.Observer;
using WpfApp1.Domain.Models;
using WpfApp1.Repository;
using WpfApp1.Domain.ServiceInterfaces;
using WpfApp1.Domain.RepositoryInterfaces;

namespace WpfApp1.Service
{
    public class TouristService : ITouristService
    {
        private readonly ITouristRepository _touristRepository;

        public TouristService()
        {
            _touristRepository = InjectorRepository.CreateInstance<ITouristRepository>();
        }
        public Tourist Get(int id)
        {
            return _touristRepository.Get(id);
        }

        public List<Tourist> GetAll()
        {
            return _touristRepository.GetAll();
        }

        public void Save()
        {

            _touristRepository.Save();
        }

        public Tourist Update(Tourist tourist)
        {
            return _touristRepository.Update(tourist);
        }
        public void Subscribe(IObserver observer)
        {
            _touristRepository.Subscribe(observer);
        }
        public void Unsubscribe(IObserver observer)
        {
            _touristRepository.Unsubscribe(observer);
        }

        public Tourist GetByUsernameAndPassword(string username, string password)
        {
            return GetAll().Find(o => o.Username.Equals(username) && o.Password.Equals(password));
        }
    }
}
