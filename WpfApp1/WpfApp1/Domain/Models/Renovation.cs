using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Util;

namespace WpfApp1.Domain.Models
{
    public class Renovation : WpfApp1.Serializer.ISerializable
    {
        public int Id { get; set; }
        public int IdAccommodation { get; set; }
        public Accommodation Accommodation { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Description { get; set; }

        public bool IsCanceled { get; set; }

        public Renovation()
        {
            IsCanceled = false;
            Accommodation = new();
        }

        public Renovation(Accommodation accommodation, DateTime startDate, DateTime endDate, string description)
        {
            Accommodation = accommodation;
            StartDate = startDate;
            EndDate = endDate;
            Description = description;
            IsCanceled = false;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Accommodation.Id = Convert.ToInt32(values[1]);
            StartDate = DateHelper.StringToDate(values[2]);
            EndDate = DateHelper.StringToDate(values[3]);
            Description = values[4];
            IsCanceled = Convert.ToBoolean(values[5]);
        }

        public string[] ToCSV()
        {
            string[] values =
            {
                Id.ToString(),
                Accommodation.Id.ToString(),
                DateHelper.DateToString(StartDate),
                DateHelper.DateToString(EndDate),
                Description,
                IsCanceled.ToString()
            };
            return values;
        }
    }
}
