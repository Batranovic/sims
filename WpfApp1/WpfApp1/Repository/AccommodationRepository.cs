using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp.Observer;
using WpfApp1.Controller;
using WpfApp1.Models;
using WpfApp1.Serializer;

namespace WpfApp1.Repository
{
    public class AccommodationRepository : IRepository<Accommodation>, ISubject
    {
        private const string _filePath = "../../../Resources/Data/accommodations.csv";
        private readonly List<IObserver> _observers;
        private readonly Serializer<Accommodation> _serializer;

        public ImageRepository ImageDAO { get; set; }

        private static AccommodationRepository _instance = null;

        private List<Accommodation> _accommodations;
        public LocationRepository LocationDAO { get; set; }
        public OwnerRepository OwnerDAO { get; set; } //Setuje se App.xaml.cs jer se inace desi beskonacni poziv konstruktora
        
        public static AccommodationRepository GetInstance()
        {
            if(_instance == null)
            {
                _instance = new AccommodationRepository();
            }
            return _instance;
        }
        
        private AccommodationRepository()

        {
            _serializer = new Serializer<Accommodation>();
            _accommodations = new List<Accommodation>();
            _accommodations = _serializer.FromCSV(_filePath);
            _observers = new List<IObserver>();
            LocationDAO = LocationRepository.GetInstance();
            ImageDAO = ImageRepository.GetInsatnce();
        }

        public void BindLocation()
        {
            foreach(Accommodation a in _accommodations)
            {
                a.Location = LocationDAO.Get(a.IdLocation);
            }
        }

        public void BindOwner()
        {
            foreach(Accommodation a in _accommodations)
            {
                a.Owner = OwnerDAO.Get(a.OwnerId);
                a.Owner.Accommodations.Add(a);
            }
        }

        public void BindImage()
        {
            foreach (Image i in ImageDAO.GetAccommodations())
            {
                    Accommodation a = Get(i.ExternalId);
                    a.Images.Add(i);
            }
        }
        public Accommodation Create(Accommodation entity)
        {
            entity.Id = NextId();
            _accommodations.Add(entity);
            Save();
            return entity;
        }

        public Accommodation Delete(Accommodation entity)
        {
            _accommodations.Remove(entity);
            Save();
            return entity;
        }

        public Accommodation Get(int id)
        {
            return _accommodations.Find(a => a.Id == id); 
        }

        public List<Accommodation> GetAll()
        {
            return _accommodations;
        }

        public int NextId()
        {
            if (_accommodations.Count == 0) return 0;
            int newId =  _accommodations[_accommodations.Count() - 1].Id + 1;
            foreach(Accommodation a in _accommodations)
            {
                if(newId == a.Id)
                {
                    newId++;
                }
            }
            return newId;
        }

        public void Save()
        {
            _serializer.ToCSV(_filePath, _accommodations);
        }

        public Accommodation Update(Accommodation entity)
        {
            var oldEntity = Get(entity.Id);
            if(oldEntity == null)
            {
                return null;
            }
            oldEntity = entity;
            Save();
            return oldEntity;
        }

        public void Subscribe(IObserver observer)
        {
            _observers.Add(observer);
        }

        public void Unsubscribe(IObserver observer)
        {
            _observers.Remove(observer);
        }

        public void NotifyObservers()
        {
            foreach (var observer in _observers)
            {
                observer.Update();
            }
        }



        
    }
}
