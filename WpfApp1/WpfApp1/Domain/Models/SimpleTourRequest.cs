using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Domain.Models.Enums;

namespace WpfApp1.Domain.Models
{
    public class SimpleTourRequest  : WpfApp1.Serializer.ISerializable
    {
        public int Id { get; set; }
        public int IdLocation { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public Location Location { get; set; }
        public string Description { get; set; }
        public string Languages { get; set; }
        public int MaxGuests { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Tourist Tourist { get; set; }
        public RequestStatus RequestStatus { get; set; }

        public SimpleTourRequest()
        {
           
        }

        public SimpleTourRequest(int id, string state, string city, string description, string language, int maxGuests, DateTime start, DateTime end, Tourist tourist, RequestStatus r)
        {
            Id = id;
            State = state;
            City = city;
            Description = description;
            Languages = language;
            MaxGuests = maxGuests;
            StartDate = start;
            EndDate = end;
            Tourist = tourist;
            RequestStatus = r;

        }

        public string[] ToCSV()
        {
            string[] csvValues =
           {
                    Id.ToString(),
                    State,
                    City,
                    Description.ToString(),
                    Languages,
                    MaxGuests.ToString(),
                    StartDate.ToString(),
                    EndDate.ToString(),
                    Tourist.Id.ToString(),
                    RequestStatus.ToString(),

                };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            State = values[1];
            City = values[2];
            Description = values[3];
            Languages = values[4];
            MaxGuests = int.Parse(values[5]);
            StartDate = DateTime.Parse(values[6]);
            EndDate = DateTime.Parse(values[7]);
            Tourist = new Tourist() { Id = Convert.ToInt32(values[8]) };
            RequestStatus = (RequestStatus)Enum.Parse(typeof(RequestStatus), values[9]);
        }
    }
}
