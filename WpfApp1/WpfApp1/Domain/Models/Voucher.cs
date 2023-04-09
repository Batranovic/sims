using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Serializer;

namespace WpfApp1.Model
{
    public class Voucher : ISerializable
    {
        private int _id;

        private string _name;

        private DateTime _expiration_date;

        public int Id
        {
            get => _id;
            set
            {
                if(value != null)
                {
                    _id = value;
                }
            }
        }

        public string Name
        {
            get => _name;
            set
            {
                if (value != null)
                {
                    _name = value;
                }
            }
        }



        public DateTime ExpirationDate
        {
            get => _expiration_date;
            set
            {
                if(value != null)
                {
                    _expiration_date = value;
                }
            }
        }


        public string[] ToCSV()
        {
            string[] csvValues =
                 {
                    Id.ToString(),
                    Name, 
                    ExpirationDate.ToString()
                };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            Name = values[1];
            ExpirationDate = DateTime.Parse(values[2]);
        }
    }
}
