using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Model
{
    public class Image : ISerializable
    {
        public int Id { get; set; }
        public string Url { get; set; }

        public int ResourceId { get; set; }

        public string Description { get; set; }

        public Image() { }

        public Image(string url, int resourceId, string description)
        {
            Url = url;
            ResourceId = resourceId;
            Description = description;
        }
        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(), 
                Url.ToString(), 
                Description
            };
            return csvValues;
        }
        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Url = values[1];
            ResourceId = Convert.ToInt32(values[2]);
            Description = values[3];
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            throw new NotImplementedException();
        }
    }
}
