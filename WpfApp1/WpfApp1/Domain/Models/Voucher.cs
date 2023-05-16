using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Serializer;

namespace WpfApp1.Domain.Models
{
    public class Voucher : ISerializable
    {
        private int _id;

        private string _name;

        private DateTime _expirationDate;

        public Tourist Tourist { get; set; }
        public int TouristId { get; set; }

        private Boolean _isUsed;

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

        public Boolean IsUsed
        {
            get => _isUsed;
            set
            {
                if (value != null)
                {
                    _isUsed = value;
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
        public Voucher()
        {

        }
        public Voucher(int id, int name, DateTime date, Tourist tourist, Boolean isUsed)
        {
           Id = id;
           Name = Name;
           ExpirationDate = date;   
           Tourist = tourist;
           TouristId = Tourist.Id;
           IsUsed = isUsed;
        }

        public DateTime ExpirationDate
        {
            get => _expirationDate;
            set
            {
                if(value != null)
                {
                    _expirationDate = value;
                }
            }
        }


        public string[] ToCSV()
        {
            string[] csvValues =
                 {
                    Id.ToString(),
                    Name, 
                    ExpirationDate.ToString(),
                    TouristId.ToString(),
                    IsUsed.ToString()
                };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            Name = values[1];
            ExpirationDate = DateTime.Parse(values[2]);
            TouristId = int.Parse(values[3]);
            IsUsed = Convert.ToBoolean(values[4]);
        }
    }
}
