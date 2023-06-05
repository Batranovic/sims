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
        public Location Location { get; set; }
        public string Description { get; set; }
        public string Languages { get; set; }
        public int MaxGuests { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Tourist Tourist { get; set; }
    
        public RequestStatus RequestStatus { get; set; }
        public ComplexTourRequest ComplexTourRequestId { get; set; }    
        public AcceptedRequestGuide AcceptedRequestGuide { get; set; }

        public SimpleTourRequest()
        {
          
        }

        public SimpleTourRequest(int id, Location location, string description, string language, int maxGuests, DateTime start, DateTime end, Tourist tourist, RequestStatus r, ComplexTourRequest complexTourRequestId, AcceptedRequestGuide acceptedRequestGuide)
        {
            Id = id;
            Location = location;
            Description = description;
            Languages = language;
            MaxGuests = maxGuests;
            StartDate = start;
            EndDate = end;
            Tourist = tourist;
            RequestStatus = r;
            ComplexTourRequestId = complexTourRequestId;
            AcceptedRequestGuide = acceptedRequestGuide;
        }

        public string[] ToCSV()
        {
            string[] csvValues =
           {
                    Id.ToString(),
                    Location.Id.ToString(),
                    Location.City.ToString(),
                    Location.State.ToString(),
                    Description.ToString(),
                    Languages,
                    MaxGuests.ToString(),
                    StartDate.ToString(),
                    EndDate.ToString(),
                    Tourist.Id.ToString(),
                    RequestStatus.ToString(),
                    ComplexTourRequestId.Id.ToString(),
                    AcceptedRequestGuide.Id.ToString(),

                };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            Location = new Location() { Id = Convert.ToInt32(values[1]) };
            Location.City = values[2];
            Location.State = values[3];
            Description = values[4];
            Languages = values[5];
            MaxGuests = int.Parse(values[6]);
            StartDate = DateTime.Parse(values[7]);
            EndDate = DateTime.Parse(values[8]);
            Tourist = new Tourist() { Id = Convert.ToInt32(values[9]) };
            RequestStatus = (RequestStatus)Enum.Parse(typeof(RequestStatus), values[10]);
            ComplexTourRequestId = new ComplexTourRequest() { Id = Convert.ToInt32(values[11]) };
            AcceptedRequestGuide = new AcceptedRequestGuide() { Id = Convert.ToInt32(values[12]) };
        }
    }
}
