using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Model.Enums;

namespace WpfApp1.Model
{
    public class Owner : User
    {
        public List<Accommodation> Accommodations { get; set; }
        public List<OwnerRating> Ratings { get; set; }
        private double _averageRating;
        private bool _super;
        
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
            Accommodations = new List<Accommodation>();
            Ratings = new List<OwnerRating>();
        }
        public override string[] ToCSV() 
        {
            String[] result = base.ToCSV() ;
            result.Append(AverageRating.ToString());
            result.Append(Super.ToString());
            return result;
        }
        public override void FromCSV(string[] values)
        {
            base.FromCSV(values);
            AverageRating = Convert.ToDouble(values[values.Length-2]);
            Super = Convert.ToBoolean(values[values.Length-1]);
        }
    }
}
