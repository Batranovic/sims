﻿using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp.Observer;
using WpfApp1.Controller;
using WpfApp1.Model;
using WpfApp1.Serializer;

namespace WpfApp1.Repository
{
    public class AccommodationDAO : IDAO<Accommodation>, ISubject
    {
        private const string _filePath = "../../../Resources/Data/accommodations.csv";
        private readonly List<IObserver> _observers;
        private readonly Serializer<Accommodation> _serializer;

        public ImageDAO ImageDAO { get; set; }

        private static AccommodationDAO _instance = null;

        private List<Accommodation> _accommododations;
        public LocationDAO LocationDAO { get; set; }
        public OwnerRepository OwnerRepository { get; set; }
        
        public static AccommodationDAO GetInstance()
        {
            if(_instance == null)
            {
                _instance = new AccommodationDAO();
            }
            return _instance;
        }
        
        private AccommodationDAO()

        {
            _serializer = new Serializer<Accommodation>();
            _accommodations = new List<Accommodation>();
            _accommodations = _serializer.FromCSV(_filePath);
            _observers = new List<IObserver>();
            OwnerRepository = OwnerRepository.GetInsatnce();
            LocationDAO = LocationDAO.GetInstance();
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
                a.Owner = OwnerRepository.Get(a.OwnerId);
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

        public List<Accommodation> SearchAccommodation(string name,string city,string state, string type, int guestsNumber, int reservationDays)
        {
            if (name == null) name = "";        
            return _accommodations.Where(a => a.Name.Contains(name) && a.Location.City.Contains(city) && a.Location.State.Contains(state) && a.AccommodationKind.ToString().Contains(type) && a.MaxGuests >= guestsNumber && a.MinResevation <= reservationDays).ToList();
        }

        //public List<Image> ShowImage(int id)
        //{
        //    return;
        //   return _accommodations.FindAll(a => a.Id == id);
        //}

        
    }
}
