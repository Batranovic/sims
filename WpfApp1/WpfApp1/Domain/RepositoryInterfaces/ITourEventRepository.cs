using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp.Observer;
using WpfApp1.Models;
using WpfApp1.Repository;

namespace WpfApp1.Domain.RepositoryInterfaces
{
    public interface ITourEventRepository : IRepository<TourEvent>, ISubject
    {
        TourEvent Create(TourEvent entity);
        TourEvent Delete(TourEvent entity);
        int NextId();
        void BindTour();
    }
}
