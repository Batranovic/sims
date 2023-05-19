using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Domain.Models
{
    public class AccommodationRenovationSuggestion : WpfApp1.Serializer.ISerializable
    {
        public int Id { get; set; }

        public Reservation Reservation { get; set; }

        public int UrgencyLevel { get; set; }

        public string Comment { get; set; }

        public AccommodationRenovationSuggestion()
        {
            Reservation = new();
        }

        public AccommodationRenovationSuggestion(int id, Reservation reservation, int urgencyLevel, string comment)
        {
            Id = id;
            Reservation.Id = reservation.Id;
            Reservation = reservation;
            UrgencyLevel = urgencyLevel;
            Comment = comment;
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                Reservation.Id.ToString(),
                UrgencyLevel.ToString(),
                Comment
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Reservation.Id = Convert.ToInt32(values[1]);
            UrgencyLevel = Convert.ToInt32(values[2]);
            Comment = values[3];
        }
    }
}
