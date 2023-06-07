using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp.Observer;
using WpfApp1.Domain.Models;
using WpfApp1.DTO;

namespace WpfApp1.Domain.ServiceInterfaces
{
    public interface IRenovationService : IService<Renovation>
    {
        void Create(Renovation entity);
        void Delete(Renovation entity);
        void Subscribe(IObserver observer);
        void Unsubscribe(IObserver observer);

        List<Renovation> GetAllForAccommodation(int idAccommodation);


        bool IsDateFree(DateTime date, int idAccommodation);
        List<AvailableDate> GetAvailableDate(DateTime start, DateTime end, int duration, int idAccommodation);
    }
}
