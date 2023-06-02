using System;
using System.Collections.Generic;
using System.Text;
using WpfApp1.Domain.Models.Enums;

namespace WpfApp1.Domain.Models
{
    public class ComplexTourRequest : Serializer.ISerializable
    {
        public int Id { get; set; }
        public Tourist Tourist { get; set; }

        public RequestStatus RequestStatus { get; set; }

        public List<SimpleTourRequest> SimpleTourRequests { get; set; }

        public ComplexTourRequest()
        {
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
            StringBuilder requestList = new StringBuilder();
            foreach (SimpleTourRequest tourRequest in SimpleTourRequests)
            {
                requestList.Append(tourRequest.Id.ToString());
                requestList.Append("|");
            }
            if (requestList.Length > 0)
            {
                requestList.Remove(requestList.Length - 1, 1);
            }
            string[] csvValues =
            {
                Id.ToString(),
                Tourist.Id.ToString(),
                RequestStatus.ToString(),
                requestList.ToString(),

            };
            return csvValues;
        }




        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            Tourist = new Tourist() { Id = Convert.ToInt32(values[1]) };
            RequestStatus = (RequestStatus)Enum.Parse(typeof(RequestStatus), values[2]);
            string[] tourRequestIds = values[3].Split("|");
            SimpleTourRequests = new List<SimpleTourRequest>();

            foreach (string tourRequestId in tourRequestIds)
            {
                int requestId = int.Parse(tourRequestId.Trim());
                SimpleTourRequest tourRequest = new SimpleTourRequest { Id = requestId };
                SimpleTourRequests.Add(tourRequest);
            }


        }





    }
}
