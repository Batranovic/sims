using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Model.Enums;

namespace WpfApp1.Model
{
    public class Accommodation : WpfApp1.Serializer.ISerializable
    {
        private int _id;
        private string _name;
        private string _city;
        private string _state;
        private AccommodationKind _accommodationKind; 
        private int _maxGuests;
        private int _minResevation;
        private int _cancelDay = 1;
        private int _ownerId;
        private Owner _owner;
        public List<string> Images { get; set; }

        public int Id
        {
            get => _id;
            set
            {
                if(value != null) 
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

        public string City
        {
            get => _city;
            set
            {
                if (value != null)
                {
                    _city = value;
                }
            }
        }

        public string State
        {
            get => _state;
            set
            {
                if(value != null)
                {
                    _state = value;
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

        public int OwnerId
        {
            get => _ownerId;
            set
            {
                if(value != _ownerId)
                {
                    _ownerId = value;
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

        public Accommodation(int id, string name, string city, string state, AccommodationKind apartmentKind, int maxGuests, int minResevation, int cancelDay, List<string> images, int ownerId, Owner owner)
        {
            Id = id;
            Name = name;
            City = city;
            State = state;
            AccommodationKind = apartmentKind;
            MaxGuests = maxGuests;
            MinResevation = minResevation;
            CancelDay = cancelDay;
            Images = images;
            OwnerId = ownerId;
            Owner = owner;
        }

        public Accommodation()
        {

        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                Name,
                City,
                AccommodationKind.ToString(),
                MaxGuests.ToString(),
                MinResevation.ToString(),
                CancelDay.ToString(),
                OwnerId.ToString()
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Name = values[1];
            City = values[2];
            AccommodationKind = Enum.Parse<AccommodationKind>(values[3]);
            MaxGuests = Convert.ToInt32(values[4]);
            MinResevation = Convert.ToInt32(values[5]);
            CancelDay = Convert.ToInt32(values[6]);
            OwnerId = Convert.ToInt32(values[7]);
        }
    }
}
