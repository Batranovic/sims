using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Domain.Domain.Models.Enums;
using WpfApp1.Util;

namespace WpfApp1.Domain.Models
{
    public class Accommodation : WpfApp1.Serializer.ISerializable
    {
        private int _id;
        private string _name;
        private Location _location;
        private AccommodationKind _accommodationKind;
        private int _maxGuests;
        private int _minResevation;
        private int _cancelDay = 1;
        private Owner _owner;
        
        public Image MainImage { get; set; }
        public List<Image> Images { get; set; }
        public bool IsRenovated { get; set; }

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
                if (value != null)
                {
                    _location = value;
                }
            }
        }

        public AccommodationKind AccommodationKind
        {
            get => _accommodationKind;
            set
            {
                if(value != null)
                {
                    _accommodationKind = value;
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

        public int MinResevation
        {
            get => _minResevation;
            set
            {
                if (value != null)
                {
                    _minResevation = value;
                }
            }
        }

        public int CancelDay
        {
            get => _cancelDay;
            set
            {
                if (value != null)
                {
                    _cancelDay = value;
                }
            }
        }

        public Owner Owner
        {
            get => _owner;
            set
            {
                if(value != _owner)
                {
                    _owner = value;
                }    
            }
        }

        public Accommodation(string name, Location location, AccommodationKind apartmentKind, int maxGuests, int minResevation, int cancelDay,  Owner owner)
        {
            Name = name;
            Location.Id = location.Id;
            Location = location;
            AccommodationKind = apartmentKind;
            MaxGuests = maxGuests;
            MinResevation = minResevation;
            CancelDay = cancelDay;
            Images = new List<Image>();
            Owner.Id = owner.Id;
            Owner = owner;
        }

        public Accommodation()
        {
            Images = new List<Image>();
            Location = new();
            Owner = new();
        }

        public string ToString()
        {
            return Name + " " + Location.City + " " + Location.State + " " + AccommodationKind;
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                Name,
                Location.Id.ToString(),
                AccommodationKind.ToString(),
                MaxGuests.ToString(),
                MinResevation.ToString(),
                CancelDay.ToString(),
                Owner.Id.ToString(),
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Name = values[1];
            Location.Id = Convert.ToInt32(values[2]);
            AccommodationKind = Enum.Parse<AccommodationKind>(values[3]);
            MaxGuests = Convert.ToInt32(values[4]);
            MinResevation = Convert.ToInt32(values[5]);
            CancelDay = Convert.ToInt32(values[6]);
            Owner.Id = Convert.ToInt32(values[7]);
        }
    }
}
