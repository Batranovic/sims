using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Serializer;

namespace WpfApp1.Domain.Models
{
    public class Notification : ISerializable
    {
        public int Id { get; set; }
        public TourBooking TourBooking { get; set; }
        public bool IsDelivered { get; set; }

        public Notification() { }

        public Notification(int id, TourBooking tourBooking, bool isDelivered)
        {
            Id = id;
            TourBooking = tourBooking;
            IsDelivered = isDelivered;
        }
        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                TourBooking.Id.ToString(),
                IsDelivered.ToString()
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            TourBooking = new TourBooking() { Id = Convert.ToInt32(values[1]) };
            IsDelivered = Boolean.Parse(values[2]);
        }
       
    }
}
