using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp.Observer;
using WpfApp1.Model;
using WpfApp1.Serializer;


namespace WpfApp1.Repository
{
    public class VoucherDAO : IDAO<Voucher>, ISubject
    {
        private const string _filePath = "../../../Resources/Data/vouchers.csv";

        private readonly Serializer<Voucher> _serializer;

        private readonly List<IObserver> _observers;

        private static VoucherDAO _instance = null;

        private List<Voucher> _vouchers;

        public static VoucherDAO GetInstance()
        {
            if (_instance == null)
            {
                _instance = new VoucherDAO();
            }
            return _instance;
        }
        private VoucherDAO()
        {
            _vouchers = new List<Voucher>();
            _serializer = new Serializer<Voucher>();
            _vouchers = _serializer.FromCSV(_filePath);
            _observers = new List<IObserver>();
        }
        public Voucher Create(Voucher entity)
        {
            entity.Id = NextId();
            _vouchers.Add(entity);
            Save();
            return entity;
        }



        public Voucher Delete(Voucher entity)
        {   
            _vouchers.Remove(entity);
            Save();
            return entity;
        }

        public Voucher Get(int id)
        {
            return _vouchers.Find(i => i.Id == id);
        }

        public List<Voucher> GetAll()
        {
            return _vouchers;
        }

        public int NextId()
        {
            if (_vouchers.Count == 0)
                return 0;
            int nextId = _vouchers[_vouchers.Count - 1].Id + 1;
            foreach (Voucher i in _vouchers)
            {
                if (nextId == i.Id)
                {
                    nextId++;
                }
            }
            return nextId;
        }



        public Voucher Update(Voucher entity)
        {
            var oldEntity = Get(entity.Id);
            if (oldEntity == null)
            {
                return null;
            }
            oldEntity = entity;
            Save();
            return oldEntity;
        }

       
        public void Save()
        {

            _serializer.ToCSV(_filePath, _vouchers);
        }

        public void Subscribe(IObserver observer)
        {
            _observers.Add(observer);
        }

        public void Unsubscribe(IObserver observer)
        {
            _observers.Remove(observer);
        }

        public void NotifyObservers()
        {
            foreach (var observer in _observers)
            {
                observer.Update();
            }
        }
    }
}
