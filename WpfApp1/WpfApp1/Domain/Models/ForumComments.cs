using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Domain.Models.Enums;

namespace WpfApp1.Domain.Models
{
    public class ForumComments : WpfApp1.Serializer.ISerializable
    {
        private int _id;
        private string _comment;
        public Forum Forum { get; set; }
        public User Author { get; set; }

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

        public string Comment
        {
            get => _comment;
            set
            {
                if (value != null)
                {
                    _comment = value;
                }
            }
        }

        public ForumComments(int id, string comment, Forum forum, User author)
        {
            Id = id;
            Comment = comment;
            Forum = forum;
            Author = author;
        }

        public ForumComments()
        {
            Forum = new();
            Author = new User();
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                Comment.ToString(),
                Forum.Id.ToString(),
                Author.Id.ToString(),
                Author.UserKind.ToString()
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Comment = values[1];
            Forum.Id = Convert.ToInt32(values[2]);
            Author.Id = Convert.ToInt32(values[3]);
            Author.UserKind = Enum.Parse<UserKind>(values[4]);
        }

    }
}
