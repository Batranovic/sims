using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Interop;
using WpfApp1.Util;

namespace WpfApp1.Domain.Models
{
    public class ForumNotification : NotificationBase
    {
        public string Message { get; set; }
        public DateTime Created { get; set; }
        public Forum Forum { get; set; }

        public Owner Owner { get; set; }
        
        public ForumNotification() : base()
        {
            Created = new();
            Forum = new();
            Message = "";
            Owner = new();
        }

        public ForumNotification(string message, DateTime created, Forum forum, Owner owner) : base()
        {
            Message = message;
            Created = created;
            Forum = forum;
            Owner = owner;
        }

        public override string[] ToCSV()
        {
            String[] result = base.ToCSV();
            Array.Resize(ref result, result.Length + 4);
            result[result.Length - 4] = Owner.Id.ToString();
            result[result.Length - 3] = Message;
            result[result.Length - 2] = DateHelper.DateToString(Created);
            result[result.Length - 1] = Forum.Id.ToString();
            return result;
        }
        public override void FromCSV(string[] values)
        {
            base.FromCSV(values);
            Owner.Id = Convert.ToInt32(values[values.Length - 4]);
            Message = values[values.Length - 3];
            Created = DateHelper.StringToDate(values[values.Length - 2]);
            Forum.Id = Convert.ToInt32(values[values.Length - 1]);
        }

    }
}
