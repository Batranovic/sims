using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Domain.Models.Enums;

namespace WpfApp1.Domain.Models
{
    public class ReportForum : WpfApp1.Serializer.ISerializable
    {
        public int Id { get; set; }
        public ForumComments ForumComment { get; set;  }
        public User Author { get; set; }

        public ReportForum(int id, ForumComments forum, User author)
        {
            Id = id;
            ForumComment = forum;
            Author = author;
        }

        public ReportForum()
        {
            ForumComment = new();
            Author = new();
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                ForumComment.Id.ToString(),
                Author.Id.ToString(),
                Author.UserKind.ToString()
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            ForumComment.Id = Convert.ToInt32(values[1]);
            Author.Id = Convert.ToInt32(values[2]);
            Author.UserKind = Enum.Parse<UserKind>(values[3]);
        }

    }
}
