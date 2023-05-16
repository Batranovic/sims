using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Domain.Models;
using WpfApp1.Domain.Models.Enums;
using WpfApp1.Serializer;

namespace WpfApp1.Domain.Models
{
    public class RequestNotification : WpfApp1.Serializer.ISerializable
    {
        public int Id { get; set; }
        public SimpleTourRequest SimpleTourRequest { get; set; }
        public bool IsDelivered { get; set; }

        public DateTime Date { get; set; }

        public RequestStatus RequestStatus { get; set; }

        public RequestNotification() { }

        public RequestNotification(int id, SimpleTourRequest simpleTourRequest, bool isDelivered, DateTime date, RequestStatus status)
        {
            Id = id;
            SimpleTourRequest = simpleTourRequest;
            IsDelivered = isDelivered;
            Date = date;
            RequestStatus = status;
        }
        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                SimpleTourRequest.Id.ToString(),
                Date.ToString(),
                RequestStatus.ToString(),
                IsDelivered.ToString()
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            SimpleTourRequest = new SimpleTourRequest() { Id = Convert.ToInt32(values[1]) };
            Date = DateTime.Parse(values[2]);
            RequestStatus = (RequestStatus)Enum.Parse(typeof(RequestStatus), values[3]);
            IsDelivered = Boolean.Parse(values[4]);
        }
    }
}
