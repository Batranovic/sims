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
    public interface IOwnerRatingRepository : IRepository<OwnerRating>, ISubject
    {
        OwnerRating Create(OwnerRating entity);
        OwnerRating Delete(OwnerRating entity);
        int NextId();
    }
}
