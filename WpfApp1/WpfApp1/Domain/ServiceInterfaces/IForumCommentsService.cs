using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp.Observer;
using WpfApp1.Domain.Models;

namespace WpfApp1.Domain.ServiceInterfaces
{
    public interface IForumCommentsService : IService<ForumComments>
    {
        void Create(ForumComments entity);
        void Delete(ForumComments entity);
        void Subscribe(IObserver observer);
        void Unsubscribe(IObserver observer);
    }
}
