using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Domain.Domain.Models.Enums;
using WpfApp1.Domain.Models.Enums;
using WpfApp1.Util;

namespace WpfApp1.Domain.Models
{
    public class ReservationPostponement : WpfApp1.Serializer.ISerializable
    {
        public int Id { get; set; }

        public Reservation Reservation { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
        
        public string OwnerComment { get; set; }

        public ReservationPostponementStatus Status { get; set; }
        public bool Deleted { get; set; }

        public ReservationPostponement()
        {
            Deleted = false;
            Reservation = new();
        }

        public ReservationPostponement(int id, Reservation reservation, DateTime startDate, DateTime endDate, string ownerComment, ReservationPostponementStatus status)
        {
            Id = id;
            Reservation = reservation;
            StartDate = startDate;
            EndDate = endDate;
            OwnerComment = ownerComment;
            Status = status;
            Deleted = false;
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                Reservation.Id.ToString(),
                DateHelper.DateToString(StartDate),
                DateHelper.DateToString(EndDate),
                OwnerComment,
                Status.ToString(),
                Deleted.ToString()
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Reservation.Id = Convert.ToInt32(values[1]);
            StartDate = DateHelper.StringToDate(values[2]);
            EndDate = DateHelper.StringToDate(values[3]);
            OwnerComment = values[4];
            Status = Enum.Parse<ReservationPostponementStatus>(values[5]);
            Deleted = Convert.ToBoolean(values[6]);
        }
    }
}
