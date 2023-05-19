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
        public User Tourist { get; set; }

        public Tour Tour { get; set; }
        public bool IsDelivered { get; set; }



        public NewTourNotification()
        {

        }
        

        public NewTourNotification(int id, User tourist, Tour tour, bool d)
        {
            Id = id;
            Tour = tour;    
            Tourist = tourist;
            IsDelivered = d;
        }


        public string[] ToCSV()
        {

            string[] csvValues =
            {
                Id.ToString(),
                Tourist.Id.ToString(),
                Tour.Id.ToString(),
                IsDelivered.ToString()

            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            Tourist = new User() { Id = Convert.ToInt32(values[1]) };
            Tour = new Tour { Id = int.Parse(values[2]) };
            IsDelivered = Boolean.Parse(values[3]);
        }
    }
    
}
