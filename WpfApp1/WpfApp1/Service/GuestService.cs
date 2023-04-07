using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp.Observer;
using WpfApp1.Domain.RepositoryInterfaces;
using WpfApp1.Model;
using WpfApp1.Repository;

namespace WpfApp1.Service
{
    public class GuestService
    {
        private readonly IGuestRepository _guestRepository;

        public GuestService()
        {
            _guestRepository = GuestRepository.GetInsatnce();
        }

        public Guest Get(int id)
        {
            return _guestRepository.Get(id);
        }

        public List<Guest> GetAll()
        {
            return _guestRepository.GetAll();
        }

        public Guest GetByUsernameAndPassword(string username, string password)
        {
            return GetAll().Find(o => o.Username.Equals(username) && o.Password.Equals(password));
        }
    }
}
