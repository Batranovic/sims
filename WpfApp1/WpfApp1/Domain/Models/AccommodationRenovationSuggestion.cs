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

        public int IdReservation { get; set; }

        public Reservation Reservation { get; set; }

        public int UrgencyLevel { get; set; }

        public string Comment { get; set; }

        public AccommodationRenovationSuggestion()
        {

        }

        public AccommodationRenovationSuggestion(int id, int idReservation, Reservation reservation, int urgencyLevel, string comment)
        {
            Id = id;
            IdReservation = idReservation;
            Reservation = reservation;
            UrgencyLevel = urgencyLevel;
            Comment = comment;
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                IdReservation.ToString(),
                UrgencyLevel.ToString(),
                Comment
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            IdReservation = Convert.ToInt32(values[1]);
            UrgencyLevel = Convert.ToInt32(values[2]);
            Comment = values[3];
        }
    }
}
