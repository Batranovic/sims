﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp.Observer;
using WpfApp1.Domain.Models;
using WpfApp1.Domain.Models.Enums;
using WpfApp1.Domain.RepositoryInterfaces;
using WpfApp1.Domain.ServiceInterfaces;
using WpfApp1.Repository;

namespace WpfApp1.Service
{
    class SimpleTourRequestService : ISimpleTourRequestService
    {
        private readonly INewTourNotificationRepository _notificationRepository;
        private readonly ISimpleTourRequestRepository _simpleTourRequestRepository;
        private readonly IAcceptedRequestGuideRepositry _acceptedRequestRepository;
        public ILocationRepository _locationRepository { get; set; }

        public SimpleTourRequestService()
        {
              _acceptedRequestRepository = InjectorRepository.CreateInstance<IAcceptedRequestGuideRepositry>();
            _simpleTourRequestRepository = InjectorRepository.CreateInstance<ISimpleTourRequestRepository>();
            _notificationRepository = InjectorRepository.CreateInstance<INewTourNotificationRepository>();
            _locationRepository = InjectorRepository.CreateInstance<ILocationRepository>();
            BindAccepted();
        }


        private void BindAccepted()
        {
            foreach (SimpleTourRequest simpleTourRequest in _simpleTourRequestRepository.GetAll())
            {
                int acceptedId = simpleTourRequest.AcceptedRequestGuide.Id;
                AcceptedRequestGuide acceptedRequest = AcceptedRequestGuideRepository.GetInstance().Get(acceptedId);
                if (acceptedRequest != null)
                {
                    simpleTourRequest.AcceptedRequestGuide = acceptedRequest;
                }
                else
                {
                    Console.WriteLine("Error in binding tour and tourEvent");
                }
            }
        }



        public string GetDeniedRequestsCount(string SelectedYear)
        {
            IEnumerable<SimpleTourRequest> requests = _simpleTourRequestRepository.GetAll()
                .Where(r => r.Tourist.Id == MainWindow.LogInUser.Id);

            if (SelectedYear != "All Years" && int.TryParse(SelectedYear, out int year))
            {
                requests = requests.Where(r => r.StartDate.Year == year);
            }

            int totalRequests = requests.Count();
            int deniedCount = requests.Count(r => r.RequestStatus == RequestStatus.Denied && r.Tourist.Id == MainWindow.LogInUser.Id);

            if (totalRequests == 0)
            {
                return "0%";
            }
            else
            {
                double percentage = (double)deniedCount / totalRequests * 100;
                return Math.Round(percentage).ToString() + "%";
            }
        }


        public string GetAcceptedRequestsCount(string selectedYear)
        {
            IEnumerable<SimpleTourRequest> requests = _simpleTourRequestRepository.GetAll()
                .Where(r => r.Tourist.Id == MainWindow.LogInUser.Id);

            if (selectedYear != "All Years" && int.TryParse(selectedYear, out int year))
            {
                requests = requests.Where(r => r.StartDate.Year == year);
            }

            int totalRequests = requests.Count();
            int acceptedCount = requests.Count(r => r.RequestStatus == RequestStatus.Accepted && r.Tourist.Id == MainWindow.LogInUser.Id);

            if (totalRequests == 0)
            {
                return "0%";
            }
            else
            {
                return Math.Round((double)acceptedCount / totalRequests * 100).ToString() + "%";
            }
        }


        public int GetAverageMaxGuests(string SelectedYear)
        {
            IEnumerable<SimpleTourRequest> requests = _simpleTourRequestRepository.GetAll()
                .Where(r => r.Tourist.Id == MainWindow.LogInUser.Id);

            if (SelectedYear != "All Years" && int.TryParse(SelectedYear, out int year))
            {
                requests = requests.Where(r => r.StartDate.Year == year);
            }

            int acceptedCount = requests.Count(r => r.RequestStatus == RequestStatus.Accepted);
            if (acceptedCount == 0)
            {
                return 0;
            }
            else
            {
                int averageMaxGuests = (int)Math.Round(requests.Where(r => r.RequestStatus == RequestStatus.Accepted).Average(r => r.MaxGuests));
                return averageMaxGuests;
            }
        }


        public Dictionary<string, int> CountRequestsByLocation(int userId)
        {
            var locationsCount = new Dictionary<string, int>();
            var requests = _simpleTourRequestRepository.GetAll().Where(r => r.Tourist.Id == userId);
            foreach (var request in requests)
            {
                var location = request.Location.City;
                if (locationsCount.ContainsKey(location))
                {
                    locationsCount[location]++;
                }
                else
                {
                    locationsCount[location] = 1;
                }
            }
            return locationsCount;
        }
        public Dictionary<string, int> CountRequestsByLanguage(int userId)
        {
            var languagesCount = new Dictionary<string, int>();
            var allRequests = _simpleTourRequestRepository.GetAll();

            foreach (var request in allRequests)
            {
                if (request.Tourist.Id == userId)
                {
                    var languages = request.Languages.Split(',');

                    foreach (var language in languages)
                    {
                        if (languagesCount.ContainsKey(language))
                        {
                            languagesCount[language]++;
                        }
                        else
                        {
                            languagesCount[language] = 1;
                        }
                    }
                }
            }

            return languagesCount;
        }
    


         public List<string> GetAllYears()
        {
            List<string> years = _simpleTourRequestRepository.GetAll()
                .Select(l => l.StartDate.Year.ToString())
                .Distinct()
                .OrderBy(y => y)
                .ToList();

            years.Insert(0, "All Years");

            return years;
        }

    
        public List<SimpleTourRequest> RequestsForTourist(int userId)
        {
            List<SimpleTourRequest> simple = new List<SimpleTourRequest>();
            var allRequests = _simpleTourRequestRepository.GetAll();

            for (int i=0; i< allRequests.Count();i++)
            {
                var request = allRequests.ElementAt(i);
                if (request.Tourist.Id == userId && request.ComplexTourRequestId.Id == 0 && (request.RequestStatus == RequestStatus.Pending || request.RequestStatus == RequestStatus.Denied))
                {
                    if ((request.StartDate - DateTime.Today).TotalDays <= 2 && request.RequestStatus != RequestStatus.Accepted)
                    {
                        request.RequestStatus = RequestStatus.Denied;
                        _simpleTourRequestRepository.Update(request);
                    }
                    simple.Add(request);
                }

            }
            return simple;
        }

        public List<SimpleTourRequest> AcceptedRequestsForTourist(int userId)
        {
            List<SimpleTourRequest> simple = new List<SimpleTourRequest>();
           
            foreach (SimpleTourRequest request in _simpleTourRequestRepository.GetAll())
            {
                if (request.Tourist.Id == userId && request.RequestStatus == RequestStatus.Accepted && request.ComplexTourRequestId.Id == 0)
                {
                    simple.Add(request);
                }
            }
            return simple;
        }


        public List<SimpleTourRequest> GetAllForUser(int userId)
        {
            return GetAll().Where(r => r.Tourist.Id == userId).ToList();
        }

        public void AddIfRequestWasNeverFullfilled(SimpleTourRequest requestForAdding, List<SimpleTourRequest> notFullfilledRequests)
        {
            foreach(SimpleTourRequest simpleTourRequest in notFullfilledRequests)
            {
                if(simpleTourRequest.Tourist.Id == requestForAdding.Tourist.Id)
                {
                    return;
                }
            }

            foreach (SimpleTourRequest request in GetAllForUser(requestForAdding.Tourist.Id))
            {
                if(request.RequestStatus == RequestStatus.Accepted && request.Languages == requestForAdding.Languages && request.Location.City == requestForAdding.Location.City)
                {
                    return;
                }
            }
            notFullfilledRequests.Add(requestForAdding);
        }

        public void NewTourFromStatistics(Tour tour)
        {
            List<SimpleTourRequest> notFullfilledRequests = new List<SimpleTourRequest>();
            foreach (SimpleTourRequest request in _simpleTourRequestRepository.GetAll())
            {
                if (request.RequestStatus != RequestStatus.Accepted && (request.Location.City == tour.Location.City || request.Languages == tour.Languages))
                {
                    AddIfRequestWasNeverFullfilled(request, notFullfilledRequests);
                }
            }


            foreach(SimpleTourRequest simpleTourRequest in notFullfilledRequests)
            {
                NewTourNotification newTourNotification = new NewTourNotification() { Tour = tour, Tourist = simpleTourRequest.Tourist, IsDelivered = false};
                _notificationRepository.Create(newTourNotification);
            }
        }

      
    
        public List<SimpleTourRequest> GetAll()
        {
            return _simpleTourRequestRepository.GetAll();
        }

        public SimpleTourRequest Get(int id)
        {
            return _simpleTourRequestRepository.Get(id);
        }

        public void Save()
        {
            _simpleTourRequestRepository.Save();
        }

        public void Delete(SimpleTourRequest tour)
        {
            _simpleTourRequestRepository.Delete(tour);
        }

        public SimpleTourRequest Update(SimpleTourRequest tour)
        {
            return _simpleTourRequestRepository.Update(tour);
        }

        public int NextId()
        {
            return _simpleTourRequestRepository.NextId();
        }

        public void Create(SimpleTourRequest tour)
        {
            _simpleTourRequestRepository.Create(tour);
        }

        public void Subscribe(IObserver observer)
        {
            _simpleTourRequestRepository.Subscribe(observer);
        }

        public void Unsubscribe(IObserver observer)
        {
            _simpleTourRequestRepository.Unsubscribe(observer);
        }
    }
}
