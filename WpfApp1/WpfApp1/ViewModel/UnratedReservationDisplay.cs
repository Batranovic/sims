using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfApp1.Commands;
using WpfApp1.Domain.Models;
using WpfApp1.Domain.ServiceInterfaces;
using WpfApp1.Service;
using WpfApp1.Views;

namespace WpfApp1.ViewModel
{
    public class UnratedReservationDisplay : ViewModelBase
    {
        private readonly IReservationService _reservationService;
        public ObservableCollection<Reservation> Reservations { get; set; }

        private Reservation _selectedReservation;
        public Reservation SelectedReservation
        {
            get => _selectedReservation;
            set
            {
                if(_selectedReservation!= value)
                {
                    _selectedReservation = value;
                   // OnPropertyChanged(nameof(SelectedReservation));
                    OwnerRatingCommand.RaiseCanExecuteChanged();
                }
            }
        }

        private Window _window;

        public RelayCommand OwnerRatingCommand { get; set; }

        public Guest LogInGuest { get; set; }

        public UnratedReservationDisplay(Guest guest)
        {
            _window = Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.Name == "ReservationViewName");
            _reservationService = InjectorService.CreateInstance<IReservationService>();

            LogInGuest = guest;
            Reservations = new ObservableCollection<Reservation>(_reservationService.GetUnratedById(LogInGuest.Id));

            OwnerRatingCommand = new RelayCommand(param => Execute_OwnerRating(), param => CanExecuteOwnerRating());
        }

        private void Execute_OwnerRating()    //ime
        {

            AccommodationAndOwnerRating acoommodationAndOwnerRating = new AccommodationAndOwnerRating(SelectedReservation);
            acoommodationAndOwnerRating.Show();
        }

        private bool CanExecuteOwnerRating()
        {
            //return true;
            return SelectedReservation != null;
        }
    }
}
