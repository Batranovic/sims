using System;
using System.Collections.Generic;
using System.Linq;
using System.Printing;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Domain.Domain.Models.Enums;
using WpfApp1.Domain.Models;
using WpfApp1.Domain.Models.Enums;

namespace WpfApp1.Domain.Models
{
    public class Image : WpfApp1.Serializer.ISerializable
    {
        private int _id;
        private string _path;
        private int _externalId;        //cija je slika
        private ImageKind _imageKind;   //da li je od smestaja ili od ture

        public Image(string path, int externalId, ImageKind imageKind)
        {
            _path = path;
            _externalId = externalId;
            _imageKind = imageKind;
        }

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

        public string Path
        {
            get => _path;
            set
            {
                if (value != null)
                {
                    _path = value;
                }
            }
        }

        public int ExternalId
        {
            get => _externalId;
            set
            {
                if (value != null)
                {
                    _externalId = value;
                }
            }
        }

        public ImageKind ImageKind
        {
            get => _imageKind;
            set
            {
                if (value != null)
                {
                    _imageKind = value;
                }
            }
        }

        public Image() { }

        public string[] ToCSV()
        {
            string[] result =
            {
                Id.ToString(),
                Path,
                ExternalId.ToString(),
                ImageKind.ToString()
            };
            return result;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Path = values[1];
            ExternalId = Convert.ToInt32(values[2]);
            ImageKind = Enum.Parse<ImageKind>(values[3]);
        }
    }
}
