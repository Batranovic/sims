using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Domain.Models;
using WpfApp.Observer;

namespace WpfApp1.Domain.ServiceInterfaces
{
    public interface IVoucherService : IService<Voucher>
    {
        void Create(Voucher entity);
        void Delete(Voucher entity);
        void Subscribe(IObserver observer);
        void Unsubscribe(IObserver observer);

        public List<Voucher> GetVouchersThatDidntExpire();
        public List<Voucher> GetVouchersThatArentUsed(List<Voucher> entity);

        public List<Voucher> VoucherForTourist(int entity);
    }
}
