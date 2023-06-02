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
using System.Collections.ObjectModel;

namespace WpfApp1.Service
{
    class ComplexTourRequestService : IComplexTourRequestService
    {
        private readonly IComplexTourRequestRepository _complexTourRequestRepository;
        private readonly ISimpleTourRequestRepository _simpleTourRequestRepository;

        public ComplexTourRequestService()
        {
            _complexTourRequestRepository = InjectorRepository.CreateInstance<IComplexTourRequestRepository>();
            _simpleTourRequestRepository = InjectorRepository.CreateInstance<ISimpleTourRequestRepository> ();
        }

        public List<ComplexTourRequest> RequestsForTourist(int userId)
        {
            List<ComplexTourRequest> requests = new List<ComplexTourRequest>();
            var allRequests = _complexTourRequestRepository.GetAll();

            for (int i = 0; i < allRequests.Count(); i++)
            {
                var request = allRequests.ElementAt(i);
                if (request.Tourist.Id == userId)
                {
                    if (request.SimpleTourRequests.Count > 0 && (request.SimpleTourRequests[0].StartDate - DateTime.Today).TotalDays <= 2 && request.RequestStatus != RequestStatus.Accepted)
                    {
                        request.RequestStatus = RequestStatus.Denied;
                        _complexTourRequestRepository.Update(request);
                    }
                    requests.Add(request);
                }
            }
            return requests;
        }


        public List<SimpleTourRequest> PartsOfComplexTourRequest(int userId)
        {
            List<SimpleTourRequest> simple = new List<SimpleTourRequest>();
            var allRequests = _complexTourRequestRepository.GetAll();

            for (int i = 0; i < allRequests.Count(); i++)
            {
                var request = allRequests.ElementAt(i);
                if (request.Tourist.Id == userId)
                {
                    foreach (SimpleTourRequest simpleTourRequest in _simpleTourRequestRepository.GetAll())
                    {
                        if (simpleTourRequest.ComplexTourRequestId == request.Id)
                        {
                            simple.Add(simpleTourRequest);
                            if (request.SimpleTourRequests.Count > 0 && (request.SimpleTourRequests[0].StartDate - DateTime.Today).TotalDays <= 2 && request.SimpleTourRequests[0].RequestStatus != RequestStatus.Accepted)
                            {
                                request.RequestStatus = RequestStatus.Denied;
                                foreach (SimpleTourRequest innerSimpleTourRequest in request.SimpleTourRequests)
                                {
                                    if (innerSimpleTourRequest.ComplexTourRequestId == request.Id)
                                    {
                                        innerSimpleTourRequest.RequestStatus = RequestStatus.Denied;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return simple;
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
