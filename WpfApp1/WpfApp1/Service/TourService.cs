using System;
using System.Collections.Generic;
using WpfApp1.Domain.Models;
using WpfApp1.Repository;
using System.Linq;
using WpfApp.Observer;
using WpfApp1.Domain.RepositoryInterfaces;
using WpfApp1.Domain.ServiceInterfaces;

namespace WpfApp1.Service
{
    public class TourService : ITourService
    {
        private readonly ITourRepository _tourRepository;
        public ILocationRepository _locationRepository { get; set; }

        public TourService()
        {
            _tourRepository = InjectorRepository.CreateInstance<ITourRepository>();
            _locationRepository = InjectorRepository.CreateInstance<ILocationRepository>();
             BindLocation();
        }
        private void BindLocation()
        {
            foreach (Tour tour in _tourRepository.GetAll())
            {
                tour.Location = _locationRepository.Get(tour.Location.Id);
            }
        }
        public List<Tour> GetAll()
        {
            return _tourRepository.GetAll();
        }

        public Tour Get(int id)
        {
            return _tourRepository.Get(id);
        }

        public void Save() 
        {
            _tourRepository.Save();
        }

        public void Delete(Tour tour)
        {
            _tourRepository.Delete(tour);
        }

        public Tour Update(Tour tour)
        {
             return _tourRepository.Update(tour);
        }

        public int NextId()
        {
            return _tourRepository.NextId();
        }

        public void Create(Tour tour)
        {
            _tourRepository.Create(tour);
        }

        public void Subscribe(IObserver observer)
        {
            _tourRepository.Subscribe(observer);
        }

        public void Unsubscribe(IObserver observer)
        {
            _tourRepository.Unsubscribe(observer);
        }
        public bool SearchCondition(Tour tour, string state, string city, string language, string numberOfPeople, string duration)
        {
            bool retVal = tour.Location.State.Contains(state, StringComparison.OrdinalIgnoreCase) && tour.Location.City.Contains(city, StringComparison.OrdinalIgnoreCase) && tour.Languages.Contains(language, StringComparison.OrdinalIgnoreCase);

            if (numberOfPeople != null && numberOfPeople != "")
            {
                int numberOfPeopleNum = Convert.ToInt32(numberOfPeople);
                retVal = retVal && tour.MaxGuests >= numberOfPeopleNum;
            }

            if (duration != null && duration != "")
            {
               double durationNum = Convert.ToDouble(duration);
                retVal = retVal && tour.Duration >= durationNum;
            }
            return retVal;

        }

        public List<Tour> TourSearch(string state, string city, string language, string numberOfPeople, string duration)
        {
            try
            {
                List<Tour> tours = TourSearchLogic(state, city, language, numberOfPeople, duration);
                return tours;
            }
            catch (Exception e)
            {
                return new List<Tour>();
            }
        }

        public List<Tour> TourSearchLogic(string state, string city, string language, string numberOfPeople, string duration)
        {
            List<Tour> tours = new List<Tour>();

            foreach (Tour tour in _tourRepository.GetAll())
            {
                if (SearchCondition(tour, state, city, language, numberOfPeople, duration))
                {
                    tours.Add(tour);
                }
            }
            return tours;
        }

        
        

    }
}
