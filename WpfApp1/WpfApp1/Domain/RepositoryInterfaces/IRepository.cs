using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WpfApp.Observer;
using WpfApp1.Serializer;

namespace WpfApp1.Repository
{
    public interface IRepository<T> 
    {
        void Save();
        T Update(T entity);
        T Get(int id);
        List<T> GetAll();
    }
}