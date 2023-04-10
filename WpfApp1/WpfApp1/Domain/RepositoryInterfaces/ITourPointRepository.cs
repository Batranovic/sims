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
    public interface ITourPointRepository : IRepository<TourPoint>, ISubject
    {
        void Delete(TourPoint entity);
        int NextId();
        void BindTourPointTour();
        public TourPoint Save(TourPoint entity);

        

    }
}
