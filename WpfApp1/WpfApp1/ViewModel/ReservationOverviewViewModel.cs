using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Domain.Models;
using WpfApp1.Domain.ServiceInterfaces;
using WpfApp1.Service;

namespace WpfApp1.ViewModel
{
    public class ReservationOverviewViewModel : ViewModelBase
    {
        private readonly IReservationService _reservationService;
        private readonly IReservationPostponementService _reservationPostponementService;

        public Owner LoggedOwner { get; set; }
        public List<Reservation> Reservations { get; set; }
        public List<ReservationPostponement> ReservationPostponements { get; set; }
        public ReservationOverviewViewModel(Owner owner)
        {
            _reservationPostponementService = InjectorService.CreateInstance<IReservationPostponementService>();
            _reservationService = InjectorService.CreateInstance<IReservationService>();
            Init(owner);
        }

        public void Init(Owner owner)
        {
            LoggedOwner = owner;
            Reservations = new List<Reservation>(_reservationService.GetAll().FindAll(r => r.Accommodation.OwnerId == owner.Id));
            ReservationPostponements = new List<ReservationPostponement>(_reservationPostponementService.GetAllByOwnerIdAhead(LoggedOwner.Id));
        }

    }
}
