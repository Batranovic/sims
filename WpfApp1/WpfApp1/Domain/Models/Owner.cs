using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Models.Enums;

namespace WpfApp1.Models
{
    public class Owner : User
    {
        public List<Accommodation> Accommodations { get; set; }
        public List<RatingOwner> Ratings { get; set; }
        private double _averageRating;
        private OwnerKind _ownerKind;

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

        public OwnerKind OwnerKind
        {
            get => _ownerKind;
            set
            {
                if(_ownerKind != null)
                {
                    _ownerKind = value;
                }
            }
        }

        public Owner() : base()
        {
            Accommodations = new List<Accommodation>();
            Ratings = new List<RatingOwner>();
        }
        
        public override string[] ToCSV() 
        {
            String[] result = base.ToCSV() ;
            result.Append(AverageRating.ToString());
            result.Append(OwnerKind.ToString());
            return result;
        }

        public override void FromCSV(string[] values)
        {
            base.FromCSV(values);
            AverageRating = Convert.ToDouble(values[values.Length-2]);
            OwnerKind = Enum.Parse<OwnerKind>(values[values.Length-1]);
        }

      
    }
}
