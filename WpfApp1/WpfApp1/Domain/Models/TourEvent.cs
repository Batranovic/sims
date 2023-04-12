using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Serializer;
using WpfApp1.Domain.Domain.Models.Enums;
using WpfApp1.Domain.Models.Enums;

namespace WpfApp1.Domain.Models
{
    public class TourEvent : ISerializable
    {

        public int Id { get; set; }
        public Tour Tour { get; set; }
        public DateTime StartTime { get; set; }

        public TourEventStatus Status { get; set; }

        public TourEvent() { }

        public TourEvent(int id, Tour tour, DateTime startTime, TourEventStatus status)
        {
            Id = id;
            Tour = tour;
            StartTime = startTime;
            Status = status;
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
               Id.ToString(),
               Tour.Id.ToString(),
               StartTime.ToString(),
               Status.ToString(),
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Tour = new Tour() { Id = Convert.ToInt32(values[1]) };
            StartTime = DateTime.Parse(values[2]);
            Status = (TourEventStatus)Enum.Parse(typeof(TourEventStatus), values[3]);
        }
    }
}
