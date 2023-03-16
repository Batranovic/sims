using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Model
{
    public class TourBooking : WpfApp1.Serializer.ISerializable
    {
        private int _id;

        private int _numberOfGuests;

        public User Tourist { get; set; }

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

        public int NumberOfGuests
        {
            get => _numberOfGuests;
            set
            {
                if (value != null)
                {
                    _numberOfGuests = value;
                }
            }
        }

        public TourBooking()
        {

        }

        public TourBooking(int id, int numberOfPeople,  User tourist)
        {
            Id = id;
            NumberOfGuests = numberOfPeople;
            Tourist = tourist;

        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                NumberOfGuests.ToString(),
                Tourist.Id.ToString()
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            NumberOfGuests = Convert.ToInt32(values[1]);
           // Tourist = new User() { Id = Convert.ToInt32(values[3]) };
        }
    }
}
