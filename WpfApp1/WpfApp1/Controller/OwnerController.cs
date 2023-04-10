﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp.Observer;
using WpfApp1.Models;
using WpfApp1.Repository;
using WpfApp1.Service;

namespace WpfApp1.Controller
{
    public class OwnerController
    {
        private readonly OwnerService _ownerService;

    
        public OwnerController()
        {
            _ownerService = new OwnerService();
        }

        public Owner Get(int id)
        {
            return _ownerService.Get(id);
        }

        public List<Owner> GetAll()
        {
            return _ownerService.GetAll();
        }

        public void Subscribe(IObserver observer)
        {
            _ownerService.Subscribe(observer);
        }

        public void Unsubscribe(IObserver observer)
        {
            _ownerService.Unsubscribe(observer);
        }

        public Owner GetByUsernameAndPassword(string username, string password)
        {
            return _ownerService.GetByUsernameAndPassword(username, password);
        }

    }
}
