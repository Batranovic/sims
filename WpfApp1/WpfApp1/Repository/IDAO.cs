using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Serializer;

namespace WpfApp1.Repository
{
    public interface IDAO<T> : IRepository<T>
    {

        private extern void Save();

        public T Create(T entity);

        public T Update(T entity);

        public void Delete(T entity);

        public int NextId();

    }
}