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
        void SetKind();
        double GetAverageRating(List<OwnerRating> ratings);
        void CalculateAverageRating();
        Owner GetByUsernameAndPassword(string username, string password);

    }
}
