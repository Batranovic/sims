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
    public interface IGuestRatingRepository : IRepository<GuestRating>, ISubject
    {
        GuestRating Create(GuestRating entity);
        GuestRating Delete(GuestRating entity);
        int NextId();
    }
}
