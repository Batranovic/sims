using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Model
{
    public class RatingOwner : WpfApp1.Serializer.ISerializable
    {
        private int _id;
        private int _idAccommodation;
        private string _comment;
        private Accommodation _accommodation;
        private int _cleanliness;
        private int _ownerCorrectness;
        private int _timeliness;
        //dodati jos nesto po potrebi(timeliness vec dodat)


        public RatingOwner(Accommodation accommodation, string comment, int cleanliness, int ownerCorrectness, int timeliness)
        {
            IdAccommodation = accommodation.Id;
            Accommodation = accommodation;
            Comment = comment;
            Cleanliness = cleanliness;
            OwnerCorrectness = ownerCorrectness;
            Timeliness = timeliness;
        }
        
        public RatingOwner() 
        {

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

        public string[] ToCSV()
        {
            string[] result =
            {
                Id.ToString(),
                IdAccommodation.ToString(),
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
            IdAccommodation = Convert.ToInt32(values[1]);
            Comment = values[2];
            Cleanliness = Convert.ToInt32(values[3]);
            OwnerCorrectness = Convert.ToInt32(values[4]);
            Timeliness = Convert.ToInt32(values[5]);
        }
    }
}
