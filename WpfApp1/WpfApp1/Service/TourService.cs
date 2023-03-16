﻿using System;
using System.Collections.Generic;
using WpfApp1.Model;
using WpfApp1.Repository;
using System.Linq;

namespace WpfApp1.Service
{
    public class TourService
    {
        private TourDAO _tourDAO;

        public TourService()
        {
            _tourDAO = TourDAO.GetInstance();
        }

        public List<Tour> GetAll()
        {
            return _tourDAO.GetAll();
        }

        public Tour Get(int id)
        {
            return _tourDAO.Get(id);
        }

        public void Save()  //proveri
        {
            _tourDAO.Save();
        }

        public void Delete(Tour tour)
        {
            _tourDAO.Delete(tour);
        }

        public Tour Update(Tour tour)
        {
            return _tourDAO.Update(tour);
        }

        public int NextId()
        {
            return _tourDAO.NextId();
        }

        public void Create(Tour tour)
        {
            _tourDAO.Create(tour);
        }

        private bool SearchCondition(Tour tour, string state, string city, string language, string numberOfPeople, string duration)
        {
            bool retVal = tour.Location.State.Contains(state) && tour.Location.City.Contains(city) && tour.Language.Contains(language);
            if (numberOfPeople != null && numberOfPeople != "")
            {
                int numberOfPeopleNum = Convert.ToInt32(numberOfPeople);
                retVal = retVal && tour.MaxGuests > numberOfPeopleNum;
            }
            if (duration != null && duration != "")
            {
                double durationNum = Convert.ToDouble(duration);
                retVal = retVal && tour.Duration > durationNum;
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
 

        private List<Tour> TourSearchLogic(string state, string city, string language, string numberOfPeople, string duration)
        {
            List<Tour> tours = new List<Tour>();

            foreach (Tour tour in _tourDAO.GetAll())
            {
                if (SearchCondition(tour, state, city, language, numberOfPeople, duration))
                    {
                    tours.Add(tour);
                    }
            }
            return tours;
        }

        public IEnumerable<Tour> TourSearchLINQ(string state, string city, string language, string numberOfPeople, string duration)
        {
            return _tourDAO.GetAll().Where(t => SearchCondition(t, state, city, language, numberOfPeople, duration));
        }
    }
}
