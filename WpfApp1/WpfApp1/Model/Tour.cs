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
        private int _idLocation;
        private Location _location;
        private TimeSpan _duration;
        private string _language;        //Da li da se napravi kao enum?
        private int _maxGuests;

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

        public Tour()
        {

        }

        public Tour(int id, int idLocation, TimeSpan duration, string  language, int  maxGuests)
        {
            Id = id;
            IdLocation = idLocation;
            Duration = duration;
            Language = language;
            MaxGuests = maxGuests;
        }

        public string[] ToCSV()
        {
            string[] csvValues =
           {
                    Id.ToString(),
                    IdLocation.ToString(),   
                    Duration.ToString(),
                    Language,
                    MaxGuests.ToString()

                };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            IdLocation = int.Parse(values[1]);
            Duration = TimeSpan.Parse(values[2]);
            Language = values[3];
            MaxGuests = int.Parse(values[4]);

            
        }
    }
}
