using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp.Observer;
using WpfApp1.Domain.RepositoryInterfaces;
using WpfApp1.Domain.ServiceInterfaces;
using WpfApp1.Model;
using WpfApp1.Repository;

namespace WpfApp1.Service
{
    public class GuestService : IGuestService
    {
        private readonly IGuestRepository _guestRepository;

        public GuestService()
        {
            _guestRepository = InjectorRepository.CreateInstance<IGuestRepository>();
        }

        public Guest Get(int id)
        {
            return _guestRepository.Get(id);
        }

        public List<Guest> GetAll()
        {
            return _guestRepository.GetAll();
        }
        public void Update(Guest entity)
        {
            _guestRepository.Update(entity);
        }
        public void Save()
        {
            _guestRepository.Save();
        }

        public Guest GetByUsernameAndPassword(string username, string password)
        {
            return GetAll().Find(o => o.Username.Equals(username) && o.Password.Equals(password));
        }

    }
}
