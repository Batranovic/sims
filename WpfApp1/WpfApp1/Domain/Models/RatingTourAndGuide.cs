using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Serializer;

namespace WpfApp1.Model
{
    public class RatingTourAndGuide : ISerializable
    {

        private int _id;
        private int _knowledge;
        private int _language;
        private int _interest;
        private string _comment;
        private string _url;

        private int _idTourBooking;
        private TourBooking _tourBooking;

        public List<Image> Images { get; set; }

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

        public string Url
        {
            get => _url;
            set
            {
                if(value != null)
                {
                    _url = value;
                }
            }
        }

        public int IdTourBooking
        {
            get => _idTourBooking;
            set
            {
                if (value != null)
                {
                    _idTourBooking = value;
                }
            }
        }

        public int Knowledge
        {
            get => _knowledge;
            set
            {
                if(value != null)
                {
                    _knowledge = value;
                }
            }
        }

        public int Language
        {
            get => _language;
            set
            {
                if(value != null)
                {
                    _language = value;
                }
            }
        }

        public int Interest
        {
            get => _interest;
            set
            {
                if(value != null)
                {
                    _interest = value;
                }
            }
        }

        public string Comment
        {
            get => _comment;
            set
            {
                if(value != null)
                {
                    _comment = value;
                }
            }
        }

        public TourBooking TourBooking
        {
            get => _tourBooking;
            set
            {
                if (value != null)
                {
                    _tourBooking = value;
                }
            }
        }

        public RatingTourAndGuide()
        {

        }

        public RatingTourAndGuide(TourBooking tourBooking,  int knowledge, int language,int interest, string comment, string url)
        {
            IdTourBooking= tourBooking.Id;
            TourBooking = tourBooking;
            Knowledge = knowledge;
            Language = language;
            Interest = interest;
            Comment = comment;
            Images = new List<Image>();
            Url = url;
        }

        public string[] ToCSV()
        {
            string[] result =
            {
                Id.ToString(),
                IdTourBooking.ToString(),
                Knowledge.ToString(),
                Language.ToString(),
                Interest.ToString(),
                Comment,
                Url

            };
            return result;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            IdTourBooking = Convert.ToInt32(values[1]);
            Knowledge = Convert.ToInt32(values[2]);
            Language = Convert.ToInt32(values[3]);
            Interest = Convert.ToInt32(values[4]);
            Comment = values[5];
            Url = values[6];
        }
    }
}
