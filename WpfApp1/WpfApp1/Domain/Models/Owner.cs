using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Domain.Domain.Models.Enums;

namespace WpfApp1.Domain.Models
{
    public class Owner : User
    {
        public List<Accommodation> Accommodations { get; set; }
        public List<OwnerRating> Ratings { get; set; }
        private double _averageRating;
        private bool _super;

        public ObservableCollection<NotificationBase> Notifications { get; set; }
        public double AverageRating
        {
            get => _averageRating;
            set
            {
                if(value != null)
                {
                    _averageRating = value;
                }
            }
        }
        public bool Super
        {
            get => _super;
            set
            {
                if(value != null)
                {
                    _super = value;
                }
            }
        }
        public Owner() : base()
        {
            Accommodations = new();
            Ratings = new();
            Notifications = new();
        }
        public override string[] ToCSV() 
        {
            String[] result = base.ToCSV();
            Array.Resize(ref result, result.Length + 1);
            result[result.Length - 1] = Super.ToString();
            return result;
        }
        public override void FromCSV(string[] values)
        {
            base.FromCSV(values);
            Super = Convert.ToBoolean(values[values.Length-1]);
        }
    }
}
