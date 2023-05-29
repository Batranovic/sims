using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Serializer;

namespace WpfApp1.Domain.Models
{
    public class NotificationBase : ISerializable
    {
        public int Id { get; set; }
        public bool IsDelivered { get; set; }

        public NotificationBase()
        {
            IsDelivered = false;
        }

        public NotificationBase(int id)
        {
            Id = id;
            IsDelivered = false;
        }

        public virtual string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                IsDelivered.ToString()
            };
            return csvValues;
        }

        public virtual void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            IsDelivered = Boolean.Parse(values[1]);
        }

    }
}