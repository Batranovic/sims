using System;
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

        private readonly ISimpleTourRequestRepository _simpleTourRequestRepository;
        public ILocationRepository _locationRepository { get; set; }

        public SimpleTourRequestService()
        {
            _simpleTourRequestRepository = InjectorRepository.CreateInstance<ISimpleTourRequestRepository>();
            _locationRepository = InjectorRepository.CreateInstance<ILocationRepository>();
            BindLocation();
        }
        private void BindLocation()
        {
            foreach (SimpleTourRequest tour in _simpleTourRequestRepository.GetAll())
            {
                tour.Location = _locationRepository.Get(tour.IdLocation);
            }
        }

        public List<SimpleTourRequest> RequestsForTourist(int userId)
        {
            List<SimpleTourRequest> simple = new List<SimpleTourRequest>();
            var allRequests = _simpleTourRequestRepository.GetAll();

            for (int i=0; i< allRequests.Count();i++)
            {
                var request = allRequests.ElementAt(i);
                if (request.Tourist.Id == userId)
                {
                    if ((request.StartDate - DateTime.Today).TotalDays <= 2)
                    {
                        request.RequestStatus = (RequestStatus)Enum.Parse(typeof(RequestStatus), "Denied");
                        _simpleTourRequestRepository.Update(request);
                    }
                    simple.Add(request);
                }
            }
            return simple;
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
