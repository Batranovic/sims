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
    public interface IComplexTourRequestRepository : IRepository<ComplexTourRequest>, ISubject
    {
        ComplexTourRequest Create(ComplexTourRequest entity);
        int NextId();
        ComplexTourRequest Delete(ComplexTourRequest entity);
    }
}
