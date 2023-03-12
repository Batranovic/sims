﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Model;
using WpfApp1.Serializer;

namespace WpfApp1.Repository
{
    public class TourDAO
    {
        private const string _filePath = "../../../Resources/Data/tours.csv";

        private readonly Serializer<Tour> _serializer;

        private List<Tour> _tours;

        public LocationDAO LocationDAO { get; set; }

        public TourDAO()
        {
            _serializer = new Serializer<Tour>();
            _tours = new List<Tour>();
            _tours = _serializer.FromCSV(_filePath);
        }

        public void BindLocation()
        {
            foreach (Tour a in _tours)
            {
                a.Location = LocationDAO.Get(a.IdLocation);
            }
        }

        public Tour Get(int id)
        {
            return _tours.Find(a => a.Id == id);
        }

        public List<Tour> GetAll()
        {
            return _tours;
        }

        public List<Tour> Search(Location location, TimeSpan duration, string language, int maxGuests)
        {
            List<Tour> searchedTours = new List<Tour>();

            foreach (Tour tour in _tours)
            {
                if (string.Equals(tour.Location.City, location.City, StringComparison.OrdinalIgnoreCase)
                    && string.Equals(tour.Location.State, location.State, StringComparison.OrdinalIgnoreCase)
                    && tour.Duration == duration
                    && string.Equals(tour.Language, language, StringComparison.OrdinalIgnoreCase)
                    && tour.MaxGuests >= maxGuests)
                {
                    searchedTours.Add(tour);
                }
            }
            return searchedTours;
        }
    }
}