using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp.Observer;
using WpfApp1.Domain.Models;
using WpfApp1.Repository;

namespace WpfApp1.Domain.RepositoryInterfaces
{
    public interface ITourRepository : IRepository<Tour>, ISubject
    {
        Tour Create(Tour entity);
        int NextId();
        Tour Delete(Tour entity);

    }
}
