using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Domain.Models.Enums;
using WpfApp1.Views;

namespace WpfApp1.Domain.Models
{
    public class ComplexTourRequest : Serializer.ISerializable
    {
        public int Id { get; set; }
        public Tourist Tourist { get; set; }

        public RequestStatus RequestStatus { get; set; }

        public List<SimpleTourRequest> SimpleTourRequests { get; set; }

        public ComplexTourRequest() { 
            SimpleTourRequests = new List<SimpleTourRequest>();
        }

        public ComplexTourRequest(int id, Tourist tourist, RequestStatus requestStatus, List<SimpleTourRequest> simpleTourRequests)
        {
            Id = id;
            Tourist = tourist;
            RequestStatus = requestStatus;
            SimpleTourRequests = simpleTourRequests;
        }


        public string[] ToCSV()
        {

            string[] csvValues =
         {
                    Id.ToString(),
                    Tourist.Id.ToString(),
                    RequestStatus.ToString(),
                    GetSimpleTourRequestIds(),

                };
            return csvValues;
        }
        public void FromCSV(string[] values)
        {

            Id = int.Parse(values[0]);
            Tourist = new Tourist() { Id = Convert.ToInt32(values[1]) };
            RequestStatus = (RequestStatus)Enum.Parse(typeof(RequestStatus), values[2]);
            SimpleTourRequests = GetSimpleTourRequestsFromIds(values[1]);
        }

        private string GetSimpleTourRequestIds()
        {
            List<string> ids = new List<string>();
            foreach (var tourRequest in SimpleTourRequests)
            {
                ids.Add(tourRequest.Id.ToString());
            }
            return string.Join("|", ids);
        }

        private List<SimpleTourRequest> GetSimpleTourRequestsFromIds(string ids)
        {
            List<SimpleTourRequest> tourRequests = new List<SimpleTourRequest>();
            string[] idArray = ids.Split('|');
            foreach (var id in idArray)
            {
                int tourRequestId = Convert.ToInt32(id);
                foreach (SimpleTourRequest tourRequest in SimpleTourRequests)
                {
                    if (tourRequest.Id == tourRequestId)
                    {
                        tourRequests.Add(tourRequest);
                        break;
                    }
                }

            }
            return tourRequests;
        }

    }
}
