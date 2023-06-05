using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Serializer;

namespace WpfApp1.Domain.Models
{
    public class AcceptedRequestGuide : ISerializable
    {
        public int Id { get; set; }

        public User Guide { get; set; }

        public DateTime? Appointment { get; set; }

        public AcceptedRequestGuide() { }

        public AcceptedRequestGuide(int id, User guide, DateTime? appointment)
        {
            Id = id;
            Guide = guide;
            Appointment = appointment;
        }

        public string[] ToCSV()
        {


            string[] csvValues =
            {
               Id.ToString(),
               Guide.Id.ToString(),
               Appointment.ToString()
            };

            return csvValues;

        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Guide = new User() { Id = Convert.ToInt32(values[1]) };
            Appointment = DateTime.Parse(values[2]);
        }

    }
}
