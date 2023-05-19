using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Domain.Domain.Models.Enums;
using WpfApp1.Util;

namespace WpfApp1.Domain.Models
{
    public class Guest : User
    {
        public List<Reservation> Reservations { get; set; }
        public List<GuestRating> Ratings{ get; set; }

        private bool _super;

        private int _bonusPoints;

        private DateTime _superGuestExpirationDate;

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

        public int BonusPoints
        {
            get => _bonusPoints;
            set
            {
                if(value != null)
                {
                    _bonusPoints = value;
                }
            }
        }

        public DateTime SuperGuestExpirationDate
        {
            get => _superGuestExpirationDate;
            set
            {
                if(value != null)
                {
                    _superGuestExpirationDate = value;
                }
            }
        }

        public Guest() : base()
        {
            Reservations = new List<Reservation>();
            Ratings = new List<GuestRating>();
        }

        public override string[] ToCSV()
        {
            String[] result = base.ToCSV();
            Array.Resize(ref result, result.Length + 3);
            result[result.Length - 3] = Super.ToString();
            result[result.Length - 2] = DateHelper.DateToString(SuperGuestExpirationDate);
            result[result.Length - 1] = BonusPoints.ToString();
            return result;
        }

        public override void FromCSV(string[] values)
        {
            base.FromCSV(values);
            Super = Convert.ToBoolean(values[values.Length - 3]);
            SuperGuestExpirationDate = DateHelper.StringToDate(values[values.Length - 2]);
            BonusPoints = Convert.ToInt32(values[values.Length - 1]);
        }

    }
}
