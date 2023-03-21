﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp.Observer;
using WpfApp1.Model;
using WpfApp1.Serializer;

namespace WpfApp1.Repository
{
    public class OwnerRepository : IRepository<Owner>, ISubject
    {
        private const string _filePath = "../../../Resources/Data/owners.csv";
        private readonly List<IObserver> _observers;
        private readonly Serializer<Owner> _serializer;

        private List<Owner> _owners;


        private static OwnerRepository _instance = null;

        public static OwnerRepository GetInsatnce()
        {
            if(_instance == null)
            {
                _instance = new OwnerRepository();
            }
            return _instance;
        }

        private OwnerRepository() 
        {
            _serializer = new Serializer<Owner>();
            _owners = new List<Owner>();
            _owners = _serializer.FromCSV(_filePath);
            _observers = new List<IObserver>();
        }


        public  Owner Get(int id)
        {
            return _owners.Find(o => o.Id == id);
        }

        public  List<Owner> GetAll()
        {
            return _owners;
        }

        public void NotifyObservers()
        {
            foreach (var observer in _observers)
            {
                observer.Update();
            }
        }

        public void Subscribe(IObserver observer)
        {
            _observers.Add(observer);
        }

        public void Unsubscribe(IObserver observer)
        {
            _observers.Remove(observer);
        }
    }
}
