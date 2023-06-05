using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp.Observer;
using WpfApp1.Domain.Models;
using WpfApp1.Domain.RepositoryInterfaces;
using WpfApp1.Domain.ServiceInterfaces;

namespace WpfApp1.Service
{
    public class AccommodationRenovationSuggestionService : IAccommodationRenovationSuggestionService
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly IAccommodationRenovationSuggestionRepository _accommodationRenovationSuggestionRepository;
        public AccommodationRenovationSuggestionService()
        {
            _reservationRepository = InjectorRepository.CreateInstance<IReservationRepository>();
            _accommodationRenovationSuggestionRepository = InjectorRepository.CreateInstance<IAccommodationRenovationSuggestionRepository>();
            BindReservation();
        }

        public void BindReservation()
        {
            foreach(AccommodationRenovationSuggestion a in _accommodationRenovationSuggestionRepository.GetAll())
            {
                a.Reservation = _reservationRepository.Get(a.Reservation.Id);
            }
        }

        public void Create(AccommodationRenovationSuggestion entity)
        {
            _accommodationRenovationSuggestionRepository.Create(entity);
        }

        public void Delete(AccommodationRenovationSuggestion entity)
        {
            _accommodationRenovationSuggestionRepository.Delete(entity);
        }

        public AccommodationRenovationSuggestion Get(int id)
        {
            return _accommodationRenovationSuggestionRepository.Get(id);
        }

        public List<AccommodationRenovationSuggestion> GetAll()
        {
            return _accommodationRenovationSuggestionRepository.GetAll();
        }

        public void Save()
        {
            _accommodationRenovationSuggestionRepository.Save();
        }

        public void Subscribe(IObserver observer)
        {
            _accommodationRenovationSuggestionRepository.Subscribe(observer);
        }

        public void Unsubscribe(IObserver observer)
        {
            _accommodationRenovationSuggestionRepository.Unsubscribe(observer);
        }

        public AccommodationRenovationSuggestion Update(AccommodationRenovationSuggestion entity)
        {
            return _accommodationRenovationSuggestionRepository.Update(entity);
        }
    }
}
