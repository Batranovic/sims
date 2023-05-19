using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfApp1.Commands;
using WpfApp1.Domain.Models;
using WpfApp1.Domain.ServiceInterfaces;
using WpfApp1.Service;

namespace WpfApp1.ViewModel
{
    public partial  class AvailableDaysViewModel : ViewModelBase
    {
        public Dictionary<DateTime, DateTime> Range { get; set; }

        public readonly IReservationService _reservationService;

        public KeyValuePair<DateTime, DateTime> SelectedRange { get; set; }

        public Guest Guest { get; set; }

        public Accommodation Accommodation { get; set; }

        private Window _window;
        public RelayCommand ConfirmCommand { get; set; }

        public RelayCommand RejectCommand { get; set; }

        public RelayCommand CancelCommand { get; set; }
        public AvailableDaysViewModel(Dictionary<DateTime, DateTime> range, Accommodation accommodation, Guest guest)
        {

            _window = Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.Name == "AvailableDaysName");
            _reservationService = InjectorService.CreateInstance<IReservationService>();

            Range = range;
            Accommodation = accommodation;
            Guest = guest;

            ConfirmCommand = new RelayCommand(param => Execute_Confirm(), param => CanExecute());
            CancelCommand = new RelayCommand(param => Execute_Cancel(), param => CanExecute());
        }

        private void Execute_Confirm()
        {
            Reservation reservation = new Reservation(Guest, Accommodation, SelectedRange.Value, SelectedRange.Key, Domain.Models.Enums.GuestRatingStatus.Reserved, Domain.Domain.Models.Enums.AccommodationAndOwnerRatingStatus.Disabled);
            _reservationService.Create(reservation);
            this.Execute_Reject();
        }

        private void Execute_Cancel()
        {
            this.Execute_Reject();
        }

        private bool CanExecute()
        {
            return true;
        }

        private void Execute_Reject()
        {
            _window.Close();
        }


    }
}

