using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp.Observer;
using WpfApp1.Domain.Models;
using WpfApp1.Domain.RepositoryInterfaces;
using WpfApp1.Domain.ServiceInterfaces;
using WpfApp1.DTO;

namespace WpfApp1.Service
{
    public class RenovationService : IRenovationService
    {
        private readonly IRenovationRepository _renovationRepository;
        private readonly IAccommodationRepository _accommodationRepository;
        public RenovationService()
        {
            _renovationRepository = InjectorRepository.CreateInstance<IRenovationRepository>(); 
            _accommodationRepository = InjectorRepository.CreateInstance<IAccommodationRepository>();
            BindAccommodation();
        }

        private void BindAccommodation()
        {
            foreach (Renovation r in _renovationRepository.GetAll())
            {
                r.Accommodation = _accommodationRepository.Get(r.Accommodation.Id);
                if(r.Accommodation.IsRenovated)
                {
                    continue;
                }
                r.Accommodation.IsRenovated = r.EndDate.AddYears(1).Date < DateTime.Now.Date ? false : true;
            }
        }

        public void Create(Renovation entity)
        {
            _renovationRepository.Create(entity);
        }

        public void Delete(Renovation entity)
        {
            _renovationRepository.Delete(entity);
        }

        public Renovation Get(int id)
        {
            return _renovationRepository.Get(id);
        }

        public List<Renovation> GetAll()
        {
            return _renovationRepository.GetAll();
        }

        public List<Renovation> GetAllForAccommodation(int idAccommodation)
        {
            return _renovationRepository.GetAll().FindAll(r => r.Accommodation.Id == idAccommodation);
        }


        public void Save()
        {
            _renovationRepository.Save();
        }

        public void Subscribe(IObserver observer)
        {
            _renovationRepository.Subscribe(observer);
        }

        public void Unsubscribe(IObserver observer)
        {
            _renovationRepository.Unsubscribe(observer);
        }

        public Renovation Update(Renovation entity)
        {
            return _renovationRepository.Update(entity);
        }

        private List<Renovation> GetAheadRenovationForAccommodaion(int idAccommodation)
        {
            return GetAllForAccommodation(idAccommodation).FindAll(r => !(r.IsCanceled) && r.StartDate.Date < DateTime.Now.Date);
        }

        private bool InRangeDate(Renovation renovation, DateTime date)
        {
            return renovation.StartDate.Date >= date.Date && renovation.EndDate.Date <= date;
        }

        public bool IsDateFree(DateTime date, int idAccommodation)
        {
            foreach(Renovation r in GetAheadRenovationForAccommodaion(idAccommodation))
            {
                if(InRangeDate(r, date))
                {
                    return false;
                }
            }
            return true;
        }

        public List<AvailableDate> GetAvailableDate(DateTime start, DateTime end, int duration, int idAccommodation)
        {
            List<AvailableDate> availableDate = new();
            for(var i = start.Date; i <= end.Date.AddDays(-duration); i = i.AddDays(1))
            {
                if(IsDateFree(i, idAccommodation) && IsDateFree(i.AddDays(duration), idAccommodation) && InjectorService.CreateInstance<IReservationService>().IsDateFree(idAccommodation, i) && InjectorService.CreateInstance<IReservationService>().IsDateFree(idAccommodation, i.AddDays(duration)))
                {
                    availableDate.Add(new AvailableDate(i, i.AddDays(duration)));
                }
            }

            return availableDate;
        }

    }
}