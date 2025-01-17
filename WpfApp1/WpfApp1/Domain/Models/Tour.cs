﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace WpfApp1.Domain.Models
{
    public class Tour : WpfApp1.Serializer.ISerializable
    {
        private int _id;
        private string _name;
        private Location _location;
        private string _description;
        private double _duration;
        private string _language;        
        private int _maxGuests;
        public List<string> KeyPoints { get; set; }
        public string Image { get; set; }
        public List<DateTime> Date { get; set; }
        public List<TourEvent> TourEvents { get; set; }

        public List<TourPoint> TourPoints { get; set; }

        public int Id
        {
            get => _id;

            set
            {
                if (value != null)
                {
                    _id = value;
                }
            }
        }
        public string Name
        {
            get => _name;

            set
            {
                if (value != null)
                {
                    _name = value;
                }
            }
        }


        public Location Location
        {
            get => _location;
            set
            {
                if(value != null)
                {
                    _location = value;
                }
            }
        }

        public string Description
        {
            get => _description;

            set
            {
                if (value != null)
                {
                    _description = value;
                }
            }
        }

        public double Duration
        {
            get => _duration;
            set
            {
                if (value != null)
                {
                    _duration = value;
                }
            }
        }

        public string Languages
        {
            get => _language;
            set
            {
                if (value != null)
                {
                    _language = value;
                }
            }
        }

        public int MaxGuests
        {
            get => _maxGuests;
            set
            {
                if (value != null)
                {
                    _maxGuests = value;
                }
            }
        }
        public Tour()
        {
            TourEvents = new List<TourEvent>();
            TourPoints = new List<TourPoint>();
        }

        public Tour(int id, string name, Location idLocation, string description, double duration, string  language, int maxGuests, List<string> keyPoints, List<DateTime> date, string image)
        {
            Id = id; 
            Name = name;
            Location = idLocation;
            Description = description;
            Duration = duration;
            Languages = language;
            MaxGuests = maxGuests;
            KeyPoints = keyPoints;
            Date = date;
            Image = image;
            TourEvents = new List<TourEvent>();
            TourPoints = new List<TourPoint>();


        }

        public string[] ToCSV()
        {
            string[] csvValues =
           {
                    Id.ToString(),
                    Name,
                    Location.Id.ToString(), 
                    Description.ToString(),
                    Duration.ToString(),
                    Languages,
                    MaxGuests.ToString(),
                    Image,
                };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            Name = values[1];
            Location = new Location() { Id = Convert.ToInt32(values[2]) };
            Description = values[3];
            Duration = Double.Parse(values[4]);
            Languages = values[5];
            MaxGuests = int.Parse(values[6]);
            Image = values[7];
        }
    }
}
