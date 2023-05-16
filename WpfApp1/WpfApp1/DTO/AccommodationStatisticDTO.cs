using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.DTO
{
    public class AccommodationStatisticDTO
    {
        public int Year { get; set; }
        
        public int Reservations { get; set; }
        public int Cancelations { get; set; }
        public int Rescheduling { get; set; }
        public int Renovations { get; set; }

        public int IntMonth { get; set; }
        
        private string _month;
        public string Month
        {
            get => _month;
            set
            {
                switch(Convert.ToInt32(value))
                {
                    case 1:
                        _month = "January";
                        break;
                    case 2:
                        _month = "February";
                        break;
                    case 3:
                        _month = "March";
                        break;
                    case 4:
                        _month = "April";
                        break;
                    case 5:
                        _month = "May";
                        break;
                    case 6:
                        _month = "June";
                        break;
                    case 7:
                        _month = "July";
                        break;
                    case 8:
                        _month = "August";
                        break;
                    case 9:
                        _month = "September";
                        break;
                    case 10:
                        _month = "October";
                        break;
                    case 11:
                        _month = "November";
                        break;
                    case 12:
                        _month = "December";
                        break;
                }
            }
        }
         
        public AccommodationStatisticDTO() 
        {
            Cancelations = 0;
            Rescheduling = 0;
            Renovations = 0;
            Reservations = 0;
        }

        public AccommodationStatisticDTO(int reservations, int cancelations, int rescheduling, int renovations)
        {
            Reservations = reservations;
            Cancelations = cancelations;
            Rescheduling = rescheduling;
            Renovations = renovations;
        }
    }
}
