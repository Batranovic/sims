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

        public Owner() : base()
        {
            Accommodations = new List<Accommodation>();
        }
        
        public override string[] ToCSV() 
        {
            return base.ToCSV();
        }

        public override void FromCSV(string[] values)
        {
            base.FromCSV(values);
        }

      
    }
}
