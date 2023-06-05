using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Domain.Models
{
    public class OwnerRating : WpfApp1.Serializer.ISerializable
    {
        private int _id;
        private int _idReservation;
        private string _comment;
        private Reservation _reservation;
        private int _cleanliness;
        private int _ownerCorrectness;
        private int _timeliness;
        //dodati jos nesto po potrebi(timeliness vec dodat)


        public OwnerRating(Reservation reservation, string comment, int cleanliness, int ownerCorrectness, int timeliness)
        {
            Reservation.Id = reservation.Id;
            Reservation = reservation;
            Comment = comment;
            Cleanliness = cleanliness;
            OwnerCorrectness = ownerCorrectness;
            Timeliness = timeliness;
        }
        
        public OwnerRating() 
        {
            Reservation = new();
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

        public int IdReservation
        {
            get => _idReservation;
            set
            {
                if (value != null)
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
                if (value != null)
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

        public int Cleanliness
        {
            get => _cleanliness;
            set
            {
                if (value != null)
                {
                    _cleanliness = value;
                }
            }
        }

        public int OwnerCorrectness
        {
            get => _ownerCorrectness;
            set
            {
                if (value != null)
                {
                    _ownerCorrectness = value;
                }
            }
        }
        public int Timeliness
        {
            get => _timeliness;
            set
            {
                if (value != null)
                {
                    _timeliness = value;
                }
            }
        }

        public string ToString()
        {
            String result = Reservation.Accommodation.ToString();
            result += " " + Timeliness + " " + OwnerCorrectness + " " + Cleanliness + " ";
            return result;
        }

        public string[] ToCSV()
        {
            string[] result =
            {
                Id.ToString(),
                Reservation.Id.ToString(),
                Comment,
                Cleanliness.ToString(),
                OwnerCorrectness.ToString(),
                Timeliness.ToString()

            };
            return result;
        }
        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Reservation.Id = Convert.ToInt32(values[1]);
            Comment = values[2];
            Cleanliness = Convert.ToInt32(values[3]);
            OwnerCorrectness = Convert.ToInt32(values[4]);
            Timeliness = Convert.ToInt32(values[5]);
        }
    }
}
