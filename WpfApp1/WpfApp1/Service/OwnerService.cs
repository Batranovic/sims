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
    public class OwnerService
    {
        private  IOwnerRepository _ownerRepository;
    
        public OwnerService()
        {
            _ownerRepository = OwnerRepository.GetInsatnce();
        }

        public Owner Get(int id)
        {
            return _ownerRepository.Get(id);
        }

        public List<Owner> GetAll()
        {
            return _ownerRepository.GetAll();
        }

        public Owner GetByUsernameAndPassword(string username, string password)
        {
            return GetAll().Find(o => o.Username.Equals(username) && o.Password.Equals(password));
        }

    }
}
