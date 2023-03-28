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
    public class OwnerService
    {
        private  OwnerDAO _ownerDAO;
    
        public OwnerService()
        {
            _ownerDAO = OwnerDAO.GetInsatnce();
        }

        public Owner Get(int id)
        {
            return _ownerDAO.Get(id);
        }

        public List<Owner> GetAll()
        {
            return _ownerDAO.GetAll();
        }

       

        public void Subscribe(IObserver observer)
        {
            _ownerDAO.Subscribe(observer);
        }

        public void Unsubscribe(IObserver observer)
        {
            _ownerDAO.Unsubscribe(observer);
        }

    }
}
