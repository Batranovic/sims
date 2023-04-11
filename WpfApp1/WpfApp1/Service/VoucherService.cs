using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Repository;
using WpfApp1.Domain.Models;
using WpfApp.Observer;
using WpfApp1.Domain.ServiceInterfaces;
using WpfApp1.Domain.RepositoryInterfaces;

namespace WpfApp1.Service
{
    public class VoucherService : IVoucherService
    {

        private readonly IVoucherRepository _voucherRepository;

       

        public VoucherService()
        {
            _voucherRepository = InjectorRepository.CreateInstance<IVoucherRepository>();
        }

        public Voucher Get(int id)
        {
            return _voucherRepository.Get(id);
        }
        public void Save()
        {

             _voucherRepository.Save();
        }
        public List<Voucher> GetAll()
        {
            return _voucherRepository.GetAll();
        }

        public void Create(Voucher voucher)
        {
            _voucherRepository.Create(voucher);
        }

        public void Delete(Voucher voucher)
        {
            _voucherRepository.Delete(voucher);
        }

        public Voucher Update(Voucher voucher)
        {
             return _voucherRepository.Update(voucher);
        }


        public void Subscribe(IObserver observer)
        {
            _voucherRepository.Subscribe(observer);
        }

        public void Unsubscribe(IObserver observer)
        {
            _voucherRepository.Unsubscribe(observer);
        }


        public List<Voucher> GetVouchersThatDidntExpire()  
        {
            List<Voucher> voucherList = new List<Voucher>();
            var allVouchers = _voucherRepository.GetAll();
            for (int i = 0; i < allVouchers.Count(); i++)
            {
                var voucher = allVouchers.ElementAt(i);
                if (voucher.ExpirationDate >= DateTime.Now)
                {
                    voucherList.Add(voucher);
                } else
                {
                    _voucherRepository.Delete(voucher);
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
