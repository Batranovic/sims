using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace WpfApp1.Domain.Models
{
    public class Tourist : User
    {
        
        //public List<Tour> Tours { get; set; }

        public List<Voucher> Vouchers { get; set; }

        public Tourist() : base()
        {
           // Tours = new List<Tour>();
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
