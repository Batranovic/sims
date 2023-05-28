using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp.Observer;
using WpfApp1.Domain.Models.Enums;
using WpfApp1.Domain.Models;
using WpfApp1.Domain.RepositoryInterfaces;
using WpfApp1.Domain.ServiceInterfaces;
using WpfApp1.Repository;

namespace WpfApp1.Service
{
    class ComplexTourRequestService : IComplexTourRequestService
    {
        private readonly IComplexTourRequestRepository _complexTourRequestRepository;

        public ComplexTourRequestService()
        {
            _complexTourRequestRepository = InjectorRepository.CreateInstance<IComplexTourRequestRepository>();

        }
        public List<ComplexTourRequest> RequestsForTourist(int userId)
        {
            List<ComplexTourRequest> complex = new List<ComplexTourRequest>();
            var allRequests = _complexTourRequestRepository.GetAll();

            for (int i = 0; i < allRequests.Count(); i++)
            {
                var request = allRequests.ElementAt(i);
                if (request.Tourist.Id == userId && (request.RequestStatus == RequestStatus.Pending || request.RequestStatus == RequestStatus.Denied))
                {
                    if ((request.SimpleTourRequests[0].StartDate - DateTime.Today).TotalDays <= 2 && request.RequestStatus != RequestStatus.Accepted)
                    {
                        request.RequestStatus = RequestStatus.Denied;
                        _complexTourRequestRepository.Update(request);
                    }
                    complex.Add(request);
                }

            }
            return complex;
        }

        public List<ComplexTourRequest> GetAllForUser(int userId)
        {
            return GetAll().Where(r => r.Tourist.Id == userId).ToList();
        }

        public List<ComplexTourRequest> GetAll()
        {
            return _complexTourRequestRepository.GetAll();
        }

        public ComplexTourRequest Get(int id)
        {
            return _complexTourRequestRepository.Get(id);
        }

        public void Save()
        {
            _complexTourRequestRepository.Save();
        }

        public void Delete(ComplexTourRequest tour)
        {
            _complexTourRequestRepository.Delete(tour);
        }

        public ComplexTourRequest Update(ComplexTourRequest tour)
        {
            return _complexTourRequestRepository.Update(tour);
        }

        public int NextId()
        {
            return _complexTourRequestRepository.NextId();
        }

        public void Create(ComplexTourRequest tour)
        {
            _complexTourRequestRepository.Create(tour);
        }

        public void Subscribe(IObserver observer)
        {
            _complexTourRequestRepository.Subscribe(observer);
        }

        public void Unsubscribe(IObserver observer)
        {
            _complexTourRequestRepository.Unsubscribe(observer);
        }
    }
}
