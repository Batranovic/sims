using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Domain.Models;

namespace WpfApp1.Domain.ServiceInterfaces
{
    public interface IOwnerService : IService<Owner>
    {

        Owner GetByUsernameAndPassword(string username, string password);

    }
}
