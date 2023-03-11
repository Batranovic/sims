using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Model
{
    public class Location : WpfApp1.Serializer.ISerializable
    {
        private int _id;
        private string _city;
        private string _state;

        public int Id
        {
            get => _id;
            set
            {
                if (value != null)
                {
                    _id = value;
                }
            }
        }

        public string City
        {
            get => _city;
            set
            {
                if (value != null)
                {
                    _city = value;
                }
            }
        }

        public string State
        {
            get => _state;
            set
            {
                if (value != null)
                {
                    _state = value;
                }
            }
        }

        public Location()
        {

        }

        public Location(string city, string state)
        {
            City = city;
            State = state;
        }

        public string[] ToCSV()
        {
            string[] result =
            {
                Id.ToString(),
                City,
                State
            };
            return result;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            City = values[1];
            State = values[2];
        }
    }
}
