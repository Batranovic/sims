using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace WpfApp1.Model
{
    public class Tour : WpfApp1.Serializer.ISerializable
    {
        private int _id;
        private string _name;
        private int _idLocation;
        private Location _location;
        private string _description;
        private TimeSpan _duration;
        private string _language;        //Da li da se napravi kao enum?
        private int _maxGuests;
        public List<Location> KeyPoints { get; set; }
        public List<Image> Images { get; set; }
        public List<DateTime> Date { get; set; }


        public User Guide { get; set; }
        public Tour()
        {
            KeyPoints = new List<Location>();
            Images = new List<Image>();
            Date = new List<DateTime>();
        }

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

        public int IdLocation
        {
            get => _idLocation;
            set
            {
                if (value != null)
                {
                    _idLocation = value;
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

        public TimeSpan Duration
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

        public string Language
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

        public Tour(int id, string name, int idLocation, string description, TimeSpan duration, string  language, int maxGuests, List<Location> keyPoints, List<DateTime> date)
        {
            Id = id;
            Name = name;
            IdLocation = idLocation;
            Description = description;
            Duration = duration;
            Language = language;
            MaxGuests = maxGuests;
            KeyPoints = keyPoints;
            Date = date;
        }

        public string[] ToCSV()
        {
            string[] csvValues =
           {
                    Id.ToString(),
                    Name.ToString(),
                    IdLocation.ToString(), 
                    Description.ToString(),
                    Duration.ToString(),
                    Language,
                    MaxGuests.ToString(),
                };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            Name = values[1];
            IdLocation = int.Parse(values[2]);
            Description = values[3];
            Duration = TimeSpan.Parse(values[4]);
            Language = values[5];
            MaxGuests = int.Parse(values[6]);
        }
    }
}
