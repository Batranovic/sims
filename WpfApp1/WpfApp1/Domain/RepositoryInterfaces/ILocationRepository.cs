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
    public interface ILocationRepository : IRepository<Location>, ISubject
    {
        Location Create(Location entity);
        Location Delete(Location entity);
        int NextId();
    }
}
