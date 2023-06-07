using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using WpfApp1.Commands;
using WpfApp1.Domain.Models;
using WpfApp1.Domain.ServiceInterfaces;
using WpfApp1.Service;
using WpfApp1.Views;

namespace WpfApp1.ViewModel
{
    public partial class ReservationViewModel : ViewModelBase
    {
        private readonly IReservationService _reservationService;
        private readonly IReservationPostponementService _reservationPostponementService;
        public ObservableCollection<Reservation> Reservations { get; set; }

        private Reservation _selectedReservation;
        public Reservation SelectedReservation
        {
            get => _selectedReservation;
            set
            {
                _selectedReservation = value;
                OnPropertyChanged(nameof(SelectedReservation));
                OwnerRatingCommand.RaiseCanExecuteChanged();
            }
        }

        private Window _window;

        public RelayCommand OwnerRatingCommand { get; set; }

        public RelayCommand ReservationPostponementCommand { get; set; }

        public RelayCommand CancelReservationCommand { get; set; }

        public RelayCommand UpdateCommand { get; set; }
        
        public RelayCommand RejectCommand { get; set; }

        public Guest LogInGuest { get; set; }
        public ReservationViewModel(Guest guest)
        {
            _window = Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.Name == "ReservationViewName");
            _reservationService = InjectorService.CreateInstance<IReservationService>();
            _reservationPostponementService = InjectorService.CreateInstance<IReservationPostponementService>();
            //_reservationService.Subscribe(this);

            LogInGuest = guest;
            Reservations = new ObservableCollection<Reservation>(_reservationService.GetFutureReseravtions(LogInGuest.Id));

            OwnerRatingCommand = new RelayCommand(param => Execute_OwnerRating(), param => CanExecuteOwnerRating());
            ReservationPostponementCommand = new RelayCommand(param => Execute_ReservationPostponement(), param => CanExecuteReservationPostponement());
            CancelReservationCommand = new RelayCommand(param => Execute_CancelReservation(), param => CanExecuteCancelReservation());
            UpdateCommand = new RelayCommand(param => Execute_Update(), param => CanExecute());

        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void Execute_OwnerRating()    //ime
        {
           
            AccommodationAndOwnerRating acoommodationAndOwnerRating = new AccommodationAndOwnerRating(SelectedReservation);
            acoommodationAndOwnerRating.Show();
        }

        private bool CanExecuteOwnerRating()
        {
            return SelectedReservation!= null && SelectedReservation.GuestReservationStatus == Domain.Domain.Models.Enums.AccommodationAndOwnerRatingStatus.Unrated;
        }


        public void Execute_ReservationPostponement()
        {
            

            ReservationPostponation reservationPostponation = new ReservationPostponation(SelectedReservation);
            reservationPostponation.Show();
        }

        private bool CanExecuteReservationPostponement()
        {
            return SelectedReservation != null;
        }

        public void Execute_CancelReservation()
        {
            _reservationService.Delete(SelectedReservation);
        }

        private bool CanExecuteCancelReservation()
        {
            
            return SelectedReservation != null && !(SelectedReservation.StartDate < DateTime.Now.AddDays(-SelectedReservation.Accommodation.CancelDay) && SelectedReservation.EndDate <= DateTime.Now);
        }

        public void Execute_Update()
        {
            Reservations.Clear();
            foreach (Reservation r in _reservationService.GetAll())
            {
                Reservations.Add(r);
            }
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
