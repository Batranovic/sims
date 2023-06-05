using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp1.Domain.Models
{
    public class NotificationAccommodationRelease : NotificationBase
    {
        public string Message { get; set; } 
        public Accommodation Accommodation { get; set; }

        public NotificationAccommodationRelease() : base()
        {
            //   Message = "The " + Accommodation.Name +  " has been released.";
            Accommodation = new();
        }

        public NotificationAccommodationRelease(Accommodation accommodation) : base()
        {
            Accommodation = accommodation;
            Message = "The " + Accommodation.Name + " has been released.";
        }

        public override string[] ToCSV()
        {
            String[] result = base.ToCSV();
            Array.Resize(ref result, result.Length + 2);
            result[result.Length - 2] = Message;
            result[result.Length - 1] = Accommodation.Id.ToString();
            return result;
        }
        public override void FromCSV(string[] values)
        {
            base.FromCSV(values);
            Message = values[values.Length - 2];
            Accommodation.Id = Convert.ToInt32(values[values.Length - 1]);
        }

    }
}
