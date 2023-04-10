using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Repository;
using WpfApp1.Models;
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


        public List<Voucher> GetVouchersThatDidntExpire()  
        {
            List<Voucher> voucherList = new List<Voucher>();
            var allVouchers = _voucherDAO.GetAll();
            for (int i = 0; i < allVouchers.Count(); i++)
            {
                var voucher = allVouchers.ElementAt(i);
                if (voucher.ExpirationDate >= DateTime.Now)
                {
                    voucherList.Add(voucher);
                } else
                {
                    //ako vaucer nije iskoristen                        ........
                    _voucherDAO.Delete(voucher);
                }
            }
            return voucherList;
        }

        public List<Voucher> GetVouchersThatArentUsed(List<Voucher> vouchers)
        {
            List<Voucher> voucherList = new List<Voucher>();
            foreach (Voucher voucher in vouchers)
            {
                if (voucher.IsUsed == false)
                {
                    voucherList.Add(voucher);
                }
            }
            return voucherList;
        }


        public List<Voucher> VoucherForTourist(int userId)
        {
            List<Voucher> vouchers = new List<Voucher>();
            var validVouchers = GetVouchersThatDidntExpire();
            var unusedValidVouchers = GetVouchersThatArentUsed(validVouchers);
            foreach (Voucher voucher in unusedValidVouchers)
            {
                if (voucher.Tourist.Id == userId)
                {
                    vouchers.Add(voucher);
                }
            }
            return vouchers;
        }

    }
}
