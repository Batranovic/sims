using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Serializer;

namespace WpfApp1.Model
{
    public class RatingGuest : ISerializable
    {
        private int _id;
        private int _idAccommodation;
        private Accommodation _accommodation;
        private int _idReservation;
        private Reservation _reservation;
        private string _comment;
        private int _cleanness;
        private int _followingRules;

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
        
        public int IdAccommodation
        {
            get => _idAccommodation;
            set
            {
                if (value != null)
                {
                    _idAccommodation = value;
                }
            }
        }
        public Accommodation Accommodation
        {
            get => _accommodation;
            set
            {
                if (value != null)
                {
                    _accommodation = value;
                }
            }
        }

        public int IdReservation
        {
            get => _idReservation;
            set
            {
                if(value != null)
                {
                    _idReservation = value;
                }
            }
        }

        public string Comment
        {
            get => _comment;
            set
            {
                if (value != null)
                {
                    _comment = value;
                }
            }
        }
        public int Cleanness
        {
            get => _cleanness;
            set
            {
                if (value != null)
                {
                    _cleanness = value;
                }
            }
        }

        public int FollowingRules
        {
            get => _followingRules;
            set
            {
                if(value != null)
                {
                    _followingRules = value;
                }
            }
        }    
        public RatingGuest()
        {

        }

        public string[] ToCSV()
        {
            string[] result =
            {
                Id.ToString(),
                IdAccommodation.ToString(),
                IdReservation.ToString(),
                Comment,
                Cleanness.ToString(),
                FollowingRules.ToString()
            };
            return result;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            IdAccommodation = Convert.ToInt32(values[1]);
            IdReservation = Convert.ToInt32(values[2]);
            Comment = values[3];
            Cleanness = Convert.ToInt32(values[4]);
            FollowingRules = Convert.ToInt32(values[5]);
        }
    }
}
