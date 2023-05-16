using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using WpfApp1.Domain.Models.Enums;

namespace WpfApp1.Domain.Models
{
    public class NewTourNotification : WpfApp1.Serializer.ISerializable
    {
        public int Id { get; set; }
        public Tourist Tourist { get; set; }
        public int TouristId { get; set; }

        public Tour Tour { get; set; }
        public bool IsDelivered { get; set; }



        public NewTourNotification()
        {

        }
        

        public NewTourNotification(int id, Tourist tourist, Tour tour, bool d)
        {
            Id = id;
            Tour = tour;    
            Tourist = tourist;
            TouristId = Tourist.Id;
            IsDelivered = d;
        }


        public string[] ToCSV()
        {

            string[] csvValues =
            {
                Id.ToString(),
                TouristId.ToString(),
                Tour.Id.ToString(),
                IsDelivered.ToString()

            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            TouristId = int.Parse(values[1]);
            Tour = new Tour { Id = int.Parse(values[2]) };
            IsDelivered = Boolean.Parse(values[3]);
        }
    }
    
}
