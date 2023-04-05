using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Serializer;

namespace WpfApp1.Repository
{
    public interface IRepository<T> 
    {
        void Save();
        public T Create(T entity);
        public T Update(T entity);
        public T Delete(T entity);
        public T Get(int id);
        public List<T> GetAll();
        public int NextId();
    }
}