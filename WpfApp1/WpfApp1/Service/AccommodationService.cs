using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfApp.Observer;
using WpfApp1.Domain.RepositoryInterfaces;
using WpfApp1.Domain.ServiceInterfaces;
using WpfApp1.Domain.Models;
using WpfApp1.Repository;
using WpfApp1.DTO;

namespace WpfApp1.Service
{
    public class AccommodationService : IAccommodationService
    {
        private readonly IAccommodationRepository _accommodationRepository;
        private readonly ILocationRepository _locationRepository;
        private readonly IOwnerRepository _ownerRepository;
        private readonly IImageRepository _imageRepository;
        private  IReservationService _reservationService;
        public AccommodationService()
        {
            _accommodationRepository = InjectorRepository.CreateInstance<IAccommodationRepository>();
            _locationRepository = InjectorRepository.CreateInstance<ILocationRepository>();
            _ownerRepository = InjectorRepository.CreateInstance<IOwnerRepository>();
            _imageRepository = InjectorRepository.CreateInstance<IImageRepository>();
            BindLocation();
            BindOwner();
            BindImage();
        }

        private void BindLocation()
        {
            foreach (Accommodation a in GetAll())
            {
                a.Location = _locationRepository.Get(a.IdLocation);
            }
        }
        private void BindOwner()
        {
            foreach (Accommodation a in GetAll())
            {
                a.Owner = _ownerRepository.Get(a.OwnerId);
                a.Owner.Accommodations.Add(a);
            }
        }
        private void BindImage()
        {
            foreach (Image i in _imageRepository.GetAccommodations())
            {
                Accommodation a = Get(i.ExternalId);
                a.Images.Add(i);
                a.MainImage = a.Images.ElementAt(0);
            }
        }
        public List<Accommodation> GetAll()
        {
            return _accommodationRepository.GetAll();
        }
        public Accommodation Get(int id)
        {
            return _accommodationRepository.Get(id);
        }
        public void Create(Accommodation entity)
        {
            _accommodationRepository.Create(entity);
        }
        public void Delete(Accommodation entity)
        {
            _accommodationRepository.Delete(entity);
        }
        public Accommodation Update(Accommodation entity)
        {
            return _accommodationRepository.Update(entity);
        }
        public void Save()
        {
            _accommodationRepository.Save();
        }
        public void Subscribe(IObserver observer)
        {
            _accommodationRepository.Subscribe(observer);
        }
        public void Unsubscribe(IObserver observer)
        {
            _accommodationRepository.Unsubscribe(observer);
        }
        public List<Accommodation> SearchAccommodation(string name, string city, string state, string type, int guestsNumber, int reservationDays)
        {
            name ??= "";
            city ??= "";
            state ??= "";
            type ??= "";
            return _accommodationRepository.GetAll().Where(a => a.Name.Contains(name) && a.Location.City.Contains(city) && a.Location.State.Contains(state) && a.AccommodationKind.ToString().Contains(type) && a.MaxGuests >= guestsNumber && a.MinResevation <= reservationDays).ToList();
        }
        public List<Accommodation> GetSortedListBySuperOwner()
        {
            return GetAll().OrderByDescending(a => a.Owner.AverageRating).ToList();
        }

       
        private void StatisticFromReservation(List<AccommodationStatisticDTO> accommodationStatisticDTOs, int idAccommodation)
        {
            _reservationService = InjectorService.CreateInstance<IReservationService>();
            foreach (Reservation r in _reservationService.GetAllWithDeleted().FindAll(r => r.IdAccommodation == idAccommodation))
            {
                AccommodationStatisticDTO accommodationStatisticDTO = accommodationStatisticDTOs.Find(a => a.Year == r.StartDate.Year);

                if (accommodationStatisticDTO == null)
                {
                    accommodationStatisticDTO = new();
                    accommodationStatisticDTO.Year = r.StartDate.Year;
                    accommodationStatisticDTO.Reservations++;
                    if (r.Deleted)
                    {
                        accommodationStatisticDTO.Cancelations++;
                    }
                    accommodationStatisticDTOs.Add(accommodationStatisticDTO);
                }
                else
                {
                    accommodationStatisticDTO.Reservations++;
                    if (r.Deleted)
                    {
                        accommodationStatisticDTO.Cancelations++;
                    }
                }

            }
        }

        private void StatisticFromPostoponement(List<AccommodationStatisticDTO> accommodationStatisticDTOs, int idAccommodation)
        {
            foreach (ReservationPostponement r in InjectorService.CreateInstance<IReservationPostponementService>().GetByAccommodation(idAccommodation))
            {
                accommodationStatisticDTOs.Find(a => a.Year == r.Reservation.StartDate.Year).Rescheduling++;
            }
        }

        public List<AccommodationStatisticDTO> StatisticByYearForAccommodation(int idAccommodation)
        {
            List<AccommodationStatisticDTO> accommodationStatisticDTOs = new();
            StatisticFromReservation(accommodationStatisticDTOs, idAccommodation);
            StatisticFromPostoponement(accommodationStatisticDTOs, idAccommodation);
            return accommodationStatisticDTOs;
        }

        private void StatisticFromReservationMonthly(List<AccommodationStatisticDTO> accommodationStatisticDTOs, int idAccommodation, int year)
        {
            foreach (Reservation r in _reservationService.GetAllWithDeleted().FindAll(r => r.IdAccommodation == idAccommodation && r.StartDate.Year == year))
            {
                AccommodationStatisticDTO accommodationStatisticDTO = accommodationStatisticDTOs.Find(a => a.IntMonth == r.StartDate.Month);

                if (accommodationStatisticDTO == null)
                {
                    accommodationStatisticDTO = new();
                    accommodationStatisticDTO.Year = r.StartDate.Year;
                    accommodationStatisticDTO.IntMonth = r.StartDate.Month;
                    accommodationStatisticDTO.Month = r.StartDate.Month.ToString();
                    accommodationStatisticDTO.Reservations++;
                    if (r.Deleted)
                    {
                        accommodationStatisticDTO.Cancelations++;
                    }
                    accommodationStatisticDTOs.Add(accommodationStatisticDTO);
                }
                else
                {
                    accommodationStatisticDTO.Reservations++;
                    if (r.Deleted)
                    {
                        accommodationStatisticDTO.Cancelations++;
                    }
                }

            }
        }

        private void StatisticFromPostoponementMonthly(List<AccommodationStatisticDTO> accommodationStatisticDTOs, int idAccommodation, int year)
        {
            foreach (ReservationPostponement r in InjectorService.CreateInstance<IReservationPostponementService>().GetByAccommodation(idAccommodation))
            {
                accommodationStatisticDTOs.Find(a => a.Year == r.Reservation.StartDate.Year && a.IntMonth == r.Reservation.StartDate.Month).Rescheduling++;
            }
        }

        public List<AccommodationStatisticDTO> StatisticByMonthForAccommodation(int idAccommodation, int year)
        {
            List<AccommodationStatisticDTO> accommodationStatisticDTOs = new();
            StatisticFromReservationMonthly(accommodationStatisticDTOs,  idAccommodation,  year);
            StatisticFromPostoponementMonthly(accommodationStatisticDTOs, idAccommodation, year);
            return accommodationStatisticDTOs;
        }

    }
}
