using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;
using WpfApp1.Domain.Domain.Models.Enums;
using WpfApp1.Domain.Models.Enums;
using WpfApp1.Repository;
using WpfApp1.Serializer;
using WpfApp1.Util;


namespace WpfApp1.Domain.Models
{
    public class Reservation : WpfApp1.Serializer.ISerializable
    {
        public int Id { get; set; }
        public int IdGuest { get; set; }
        public Guest Guest { get; set; }
        public Accommodation Accommodation { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public bool Deleted { get; set; }
        public GuestRatingStatus Status { get; set; }

        public AccommodationAndOwnerRatingStatus GuestReservationStatus { get; set; }


        public Reservation()
        {
            Accommodation = new();
        }

        public Reservation(Guest guest, Accommodation accommodation, DateTime startDate, DateTime endDate, GuestRatingStatus status, AccommodationAndOwnerRatingStatus guestReservationStatus)
        {
            IdGuest = guest.Id;
            EndDate = endDate;
            Status = status;
            GuestReservationStatus = guestReservationStatus;
            Guest = guest;
            Accommodation = accommodation;
            StartDate = startDate;
            Deleted = false;
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                DateHelper.DateToString(StartDate),
                DateHelper.DateToString(EndDate),
                Accommodation.Id.ToString(),
                IdGuest.ToString(),
                Status.ToString(),
                GuestReservationStatus.ToString(),
                Deleted.ToString()

            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            StartDate = DateHelper.StringToDate(values[1]);
            EndDate = DateHelper.StringToDate(values[2]);
            Accommodation.Id = Convert.ToInt32(values[3]);
            IdGuest = Convert.ToInt32(values[4]);
            Status = Enum.Parse<GuestRatingStatus>(values[5]);
            GuestReservationStatus = Enum.Parse<AccommodationAndOwnerRatingStatus>(values[6]);
            Deleted = Convert.ToBoolean(values[7]);
        }

    }
}
