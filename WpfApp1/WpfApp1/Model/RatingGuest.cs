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
        private int _idReservation;
        private Reservation _reservation;
        private string _comment;
        private int _cleanness;
        private int _followingRules;

        public RatingGuest(Reservation reservation, string comment, int cleanness, int followingRules)
        {
            IdReservation = reservation.Id;
            Reservation = reservation;
            Comment = comment;
            Cleanness = cleanness;
            FollowingRules = followingRules;
        }

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

        public Reservation Reservation
        {
            get => _reservation;    
            set
            {
                if(_reservation != null)
                {
                    _reservation = value;
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
            IdReservation = Convert.ToInt32(values[1]);
            Comment = values[2];
            Cleanness = Convert.ToInt32(values[3]);
            FollowingRules = Convert.ToInt32(values[4]);
        }
    }
}
