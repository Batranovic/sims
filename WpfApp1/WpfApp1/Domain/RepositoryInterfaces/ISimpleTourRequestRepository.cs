﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp.Observer;
using WpfApp1.Domain.Models;
using WpfApp1.Repository;

namespace WpfApp1.Domain.RepositoryInterfaces
{
    public interface ISimpleTourRequestRepository : IRepository<SimpleTourRequest>, ISubject
    {
        SimpleTourRequest Create(SimpleTourRequest entity);
        int NextId();
        SimpleTourRequest Delete(SimpleTourRequest entity);
        List<SimpleTourRequest> GetAll();
        SimpleTourRequest Get(int id);
    }
}
