using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Domain.Models
{
    internal class Guide : User
    {
            
        public List<Tour> Tours { get; set; }

        public Guide() : base()
        {
            Tours = new List<Tour>();
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
