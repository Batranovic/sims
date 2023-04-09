using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp.Observer;
using WpfApp1.Model;
using WpfApp1.Repository;
              
namespace WpfApp1.Domain.RepositoryInterfaces
{
    public interface IAccommodationRepository : IRepository<Accommodation>, ISubject
    {
        Accommodation Create(Accommodation entity);
        int NextId();
        Accommodation Delete(Accommodation entity);
    }
}