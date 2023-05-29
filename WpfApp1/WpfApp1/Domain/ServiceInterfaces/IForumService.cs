using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp.Observer;
using WpfApp1.Domain.Models;
using WpfApp1.Domain.ServiceInterfaces;

namespace WpfApp1.Service
{
    public interface IForumService : IService<Forum>
    {
        void Create(Forum entity);
        void Delete(Forum entity);
        void Subscribe(IObserver observer);
        void Unsubscribe(IObserver observer);
    }
}
