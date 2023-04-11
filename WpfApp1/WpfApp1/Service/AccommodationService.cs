using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfApp.Observer;
using WpfApp1.Domain.RepositoryInterfaces;
using WpfApp1.Domain.ServiceInterfaces;
using WpfApp1.Model;
using WpfApp1.Repository;

namespace WpfApp1.Service
{
    public class AccommodationService : IAccommodationService
    {
        private readonly IAccommodationRepository _accommodationRepository;
        private readonly ILocationRepository _locationRepository;
        private readonly IOwnerRepository _ownerRepository;
        private readonly IImageRepository _imageRepository;
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
        public void Update(Accommodation entity)
        {
            _accommodationRepository.Update(entity);
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

    }
}
