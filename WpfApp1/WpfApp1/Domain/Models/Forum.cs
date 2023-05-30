using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Domain.Models
{
    public class Forum : WpfApp1.Serializer.ISerializable
    {
        private int _id;
        private Location _location;
        private string _firstComment;
        private List<string> _comments;
        private bool _isOpen;
        private Guest _guest;

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

        public Location Location
        {
            get => _location;
            set
            {
                if (value != null)
                {
                    _location = value;
                }
            }
        }

        public string FirstComment
        {
            get => _firstComment;
            set
            {
                if(value != null)
                {
                    _firstComment = value;
                }
            }
        }

        public List<string> Comments
        {
            get => _comments;
            set
            {
                if(value != null)
                {
                    _comments = value;
                }
            }
        }

        public bool IsOpen
        {
            get => _isOpen;
            set
            {
                if(value != null)
                {
                    _isOpen = value;
                }
            }
        }

        public Guest Guest
        {
            get => _guest;
            set
            {
                if(value != _guest)
                {
                    _guest = value;
                }
            }
        }

        public Forum(int id, Location location, string firstComment, List<string> comments, bool isOpen, Guest guest)
        {
            Id = id;
            Location = location;
            FirstComment = firstComment;
            Comments = comments;
            IsOpen = isOpen;
            Guest = guest;
        }

        public Forum()
        {
            Comments = new List<string>();
            Guest = new();
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                FirstComment.ToString(),
                IsOpen.ToString()
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            FirstComment = values[1];
            IsOpen = Convert.ToBoolean(values[2]);
        }
    }
}
