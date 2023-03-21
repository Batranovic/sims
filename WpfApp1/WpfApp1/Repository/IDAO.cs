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
        void Save();

        public T Create(T entity);

        public T Update(T entity);

        public T Delete(T entity);

        public int NextId();

    }
}