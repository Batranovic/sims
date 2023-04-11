using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp.Observer;
using WpfApp1.Domain.Models;

namespace WpfApp1.Domain.ServiceInterfaces
{
    public interface IService<T>
    {
        void Save();
        T Update(T entity);
        List<T> GetAll();
        T Get(int id);
    }
}
