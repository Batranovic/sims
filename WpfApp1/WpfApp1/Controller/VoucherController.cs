using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Models;
using WpfApp1.Service;
using WpfApp.Observer;

namespace WpfApp1.Controller
{
    public class VoucherController
    {

        private readonly VoucherService _voucherService;

        public VoucherController()
        {
            _voucherService = new VoucherService();
        }

        public List<Voucher> GetAll()
        {
            return _voucherService.GetAll();
        }

        public Voucher Get(int id)
        {
            return _voucherService.Get(id);
        }


        public void Create(Voucher voucher)
        {
            _voucherService.Create(voucher);
        }
        public void Update(Voucher voucher)
        {
            _voucherService.Update(voucher);
        }

        public void Delete(Voucher voucher)
        {
            _voucherService.Delete(voucher);
        }


        public void Subscribe(IObserver observer)
        {
            _voucherService.Subscribe(observer);
        }

        public void Unsubscribe(IObserver observer)
        {
            _voucherService.Unsubscribe(observer);
        }

       
        public List<Voucher> VoucherForTourist(int userId)
        {
            return _voucherService.VoucherForTourist(userId);
        }

        
    }
}
