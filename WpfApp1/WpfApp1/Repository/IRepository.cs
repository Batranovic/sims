using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Repository
{
    public interface IRepository<T>
    {
        public T Get(int id);

        public List<T> GetAll();
    }
}


//dve vrste klasa
//vrsta ce bit sva deca od usera ovde nema Addd Delete Update, Get(), GetAll() 
//a druga vrsta sve ostalo 