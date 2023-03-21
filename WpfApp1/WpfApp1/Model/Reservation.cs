using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;
using WpfApp1.Model.Enums;
using WpfApp1.Repository;
using WpfApp1.Serializer;
using WpfApp1.Util;


namespace WpfApp1.Model
{
    public class Reservation : WpfApp1.Serializer.ISerializable
    {
        public int Id { get; set; }
        public int IdGuest { get; set; }
        public Guest Guest { get; set; }
        public Accommodation Accommodation { get; set; }
        public int IdAccommodation { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public RatingGuestStatus Status { get; set; }


        public Reservation()
        {

        }

        public Reservation(Guest guest, Accommodation accommodation, DateTime startDate, DateTime endDate, RatingGuestStatus status)
        {
            IdGuest = guest.Id;
            IdAccommodation = accommodation.Id;
            EndDate = endDate;
            Status = status;
            this.Guest = guest;
            this.Accommodation = accommodation;
            this.StartDate = startDate;

        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                DateHelper.DateToString(StartDate),
                DateHelper.DateToString(EndDate),
                IdAccommodation.ToString(),
                IdGuest.ToString(),
                Status.ToString()
            };
            return csvValues;
        }


        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            StartDate = DateHelper.StringToDate(values[1]);
            EndDate = DateHelper.StringToDate(values[2]);
            IdAccommodation = Convert.ToInt32(values[3]);
            IdGuest = Convert.ToInt32(values[4]);
            Status = Enum.Parse<RatingGuestStatus>(values[5]);
        }

    }
}
