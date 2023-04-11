using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Serializer;

namespace WpfApp1.Domain.Models
{
    public class TourPoint : ISerializable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Tour Tour { get; set; }
        public int Order { get; set; }
        public Boolean Active { get; set; }

        public TourPoint() { }

        public TourPoint(int id, string name, Tour tour, int order, Boolean active)
        {
            Id = id;
            Name = name;
            Tour = tour;
            Order = order;
            Active = active;
        }

        public string[] ToCSV()
        {


            string[] csvValues =
           {
               Id.ToString(),
               Name,
               Tour.Id.ToString(),
               Order.ToString(),
               Active.ToString(),

           };

            return csvValues;

        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Name = values[1];
            Tour = new Tour() { Id = Convert.ToInt32(values[2]) };
            Order = Convert.ToInt32(values[3]);
            Active = Boolean.Parse(values[4]);

        }


    }
}
