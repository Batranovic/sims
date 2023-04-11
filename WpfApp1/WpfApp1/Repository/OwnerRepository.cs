using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp.Observer;
using WpfApp1.Domain.RepositoryInterfaces;
using WpfApp1.Domain.Models;
using WpfApp1.Serializer;
using WpfApp1.Service;

namespace WpfApp1.Repository
{
    public class OwnerRepository : IOwnerRepository
    {
        private const string _filePath = "../../../Resources/Data/owners.csv";
        private readonly List<IObserver> _observers;
        private readonly Serializer<Owner> _serializer;
        private List<Owner> _owners;
        private static IOwnerRepository _instance = null;
     
        public static IOwnerRepository GetInsatnce()
        {
            if(_instance == null)
            {
                _instance = new OwnerRepository();
            }
            return _instance;
        }

        private OwnerRepository() 
        {
            _serializer = new Serializer<Owner>();
            _owners = new List<Owner>();
            _owners = _serializer.FromCSV(_filePath);
            _observers = new List<IObserver>();
        }
     
        public  Owner Get(int id)
        {
            return _owners.Find(o => o.Id == id);
        }

        public  List<Owner> GetAll()
        {
            return _owners;
        }
        public void Save()
        {
            _serializer.ToCSV(_filePath, _owners);
        }
        public Owner Update(Owner entity)
        {
            throw new NotImplementedException();
        }
       
    }
}
