﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp.Observer;
using WpfApp1.Domain.Models;

namespace WpfApp1.Domain.ServiceInterfaces
{
    public interface IComplexTourRequestService : IService<ComplexTourRequest>
    {

        void Create(ComplexTourRequest entity);
        void Delete(ComplexTourRequest entity);
        void Subscribe(IObserver observer);
        void Unsubscribe(IObserver observer);
        int NextId();
        List<ComplexTourRequest> RequestsForTourist(int userId);
        List<SimpleTourRequest> PartsOfComplexTourRequest(int userId);
        List<SimpleTourRequest> DeniedSimpleTourRequests(int userId);
    }
}
