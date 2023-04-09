using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Repository;
using WpfApp1.Model;
using WpfApp.Observer;

namespace WpfApp1.Service
{
    public class VoucherService
    {
        private VoucherRepository _voucherDAO;

        public VoucherService()
        {
            _voucherDAO = VoucherRepository.GetInstance();
        }

        public Voucher Get(int id)
        {
            return _voucherDAO.Get(id);
        }

        public List<Voucher> GetAll()
        {
            return _voucherDAO.GetAll();
        }

        public void Create(Voucher voucher)
        {
            _voucherDAO.Create(voucher);
        }

        public void Delete(Voucher voucher)
        {
            _voucherDAO.Delete(voucher);
        }

        public void Update(Voucher voucher)
        {
            _voucherDAO.Update(voucher);
        }


        public void Subscribe(IObserver observer)
        {
            _voucherDAO.Subscribe(observer);
        }

        public void Unsubscribe(IObserver observer)
        {
            _voucherDAO.Unsubscribe(observer);
        }
    }
}
