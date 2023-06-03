using System;
using System.Collections.Generic;
using System.Linq;
using WpfApp.Observer;
using WpfApp1.Domain.Models;
using WpfApp1.Domain.Models.Enums;
using WpfApp1.Domain.RepositoryInterfaces;
using WpfApp1.Domain.ServiceInterfaces;

namespace WpfApp1.Service
{
    class ComplexTourRequestService : IComplexTourRequestService
    {
        private readonly IComplexTourRequestRepository _complexTourRequestRepository;
        private readonly ISimpleTourRequestRepository _simpleTourRequestRepository;

        public ComplexTourRequestService()
        {
            _complexTourRequestRepository = InjectorRepository.CreateInstance<IComplexTourRequestRepository>();
            _simpleTourRequestRepository = InjectorRepository.CreateInstance<ISimpleTourRequestRepository>();
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
            bool allAccepted = true;

            for (int i = 0; i < allRequests.Count(); i++)
            {
                var request = allRequests.ElementAt(i);
                if (request.Tourist.Id == userId)
                {
                    foreach (SimpleTourRequest simpleTourRequest in _simpleTourRequestRepository.GetAll())
                    {
                        if (simpleTourRequest.ComplexTourRequestId.Id == request.Id)
                        {
                            simple.Add(simpleTourRequest);

                            if (simpleTourRequest.RequestStatus != RequestStatus.Accepted)
                            {
                                allAccepted = false;
                            }
                        }
                    }

                    if (allAccepted)
                    {
                        request.RequestStatus = RequestStatus.Accepted;
                        _complexTourRequestRepository.Update(request);
                    }
                }
            }

            return simple;
        }


        public List<SimpleTourRequest> DeniedSimpleTourRequests(int userId)
        {
            var allSimpleTourRequests = PartsOfComplexTourRequest(userId); 
            var allRequests = _complexTourRequestRepository.GetAll();
        

            for (int i = 0; i < allRequests.Count(); i++)
            {
                var request = allRequests.ElementAt(i);
                if (request.Tourist.Id == userId)
                {
                    foreach (SimpleTourRequest simpleTourRequest in allSimpleTourRequests)
                    { 

                        if (request.RequestStatus != RequestStatus.Accepted && simpleTourRequest.StartDate.AddDays(-2) <= DateTime.Today && simpleTourRequest.ComplexTourRequestId.Id == request.Id)
                        {
                            request.RequestStatus = RequestStatus.Denied;
                            _complexTourRequestRepository.Update(request);
                        }
                    }

                    foreach (SimpleTourRequest requests in allSimpleTourRequests)
                    {
                        if(request.RequestStatus == RequestStatus.Denied && requests.ComplexTourRequestId.Id == request.Id)
                        {
                            requests.RequestStatus = RequestStatus.Denied;
                            _simpleTourRequestRepository.Update(requests);

                        }
                     
                    }

                }
            }
            return allSimpleTourRequests;
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
