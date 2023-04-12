using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Domain.Models
{
    public class TourBooking : WpfApp1.Serializer.ISerializable
    {
        private int _id;
        private int _numberOfGuests;
        public User Tourist { get; set; }
        public TourEvent TourEvent { get; set; }

        public Voucher Voucher { get; set; }

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

        public TourBooking(int id, int numberOfPeople,TourEvent tourEvent, User tourist, Voucher voucher)
        {
            Id = id;
            NumberOfGuests = numberOfPeople;
            TourEvent = tourEvent;
            Tourist = tourist;
            Voucher = voucher;

        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                NumberOfGuests.ToString(),
                TourEvent.Id.ToString(),
                Tourist.Id.ToString(),
                Voucher.Id.ToString()
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            NumberOfGuests = Convert.ToInt32(values[1]);
            TourEvent = new TourEvent() { Id = Convert.ToInt32(values[2]) };
            Tourist = new User() { Id = Convert.ToInt32(values[3]) };
            Voucher = new Voucher() { Id = Convert.ToInt32(values[4]) };
        }
    }
}
