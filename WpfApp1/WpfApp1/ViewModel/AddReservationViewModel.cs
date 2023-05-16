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
using System.Windows.Input;
using WpfApp1.Commands;
using WpfApp1.Domain.Domain.Models.Enums;
using WpfApp1.Domain.Models;
using WpfApp1.Domain.Models.Enums;
using WpfApp1.Domain.ServiceInterfaces;
using WpfApp1.Service;
using WpfApp1.Util;
using WpfApp1.Views;

namespace WpfApp1.ViewModel
{
    public partial class AddReservationViewModel : ViewModelBase
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private readonly IReservationService _reservationService;

        public Accommodation Accommodation { get; set; }

        public Guest Guest { get; set; }

        public int ReservationDays { get; set; }

        public int GuestsNumber { get; set; }


        public DateTime StartDateConverted { get; set; }

        public DateTime EndDateConverted { get; set; }

        private Window _window;

        public RelayCommand ConfirmCommand { get; set; }

        public RelayCommand CancelCommand { get; set; }


        public AddReservationViewModel(Accommodation accommodation, Guest guest)
        {
            _window = Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.Name == "AddReservationName");
            _reservationService = InjectorService.CreateInstance<IReservationService>();

            bool slobodan;
            Guest = guest;
            Accommodation = accommodation;
            StartlDay = DateTime.Now;
            EndDay = DateTime.Now;

            ConfirmCommand = new RelayCommand(param => Execute_AddReservation(), param => CanExecute());
            CancelCommand = new RelayCommand(param => Execute_CancelReservation(), param => CanExecute());
        }

        private DateTime _startDay;
        public DateTime StartlDay
        {
            get => _startDay;
            set
            {
                if (_startDay != value)
                {
                    _startDay = value;
                    OnPropertyChanged();
                }
            }
        }

        private DateTime _endDay;
        public DateTime EndDay
        {
            get => _endDay;
            set
            {
                if (_endDay != value)
                {
                    _endDay = value;
                    OnPropertyChanged();
                }
            }
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void Execute_AddReservation()
        {

            if (Accommodation.MinResevation > ReservationDays)
            {
                MessageBox.Show("You need to enter above " + Accommodation.MinResevation.ToString(), "Notification reservations");
                return;
            }

            if (Accommodation.MaxGuests < GuestsNumber)
            {
                MessageBox.Show("You need to enter below " + Accommodation.MaxGuests.ToString(), "Notification guests");
                return;
            }

            StartDateConverted = _reservationService.CheckAvailableDate(Accommodation.Id, StartlDay, EndDay, ReservationDays);
            if (DateTime.Compare(StartDateConverted, EndDay) == 0)
            {
                var range = _reservationService.GetAvailableDates(Accommodation.Id, EndDay, ReservationDays);
                AvailableDays availableDays = new AvailableDays(range, Accommodation, Guest);
                availableDays.Show();
                return;
            }

            string message = "Do you want to book this date?" + DateHelper.DateToString(StartDateConverted);

            MessageBoxResult result = MessageBox.Show(message, "Reservation", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {

                Reservation reservation = new Reservation(Guest, Accommodation, StartDateConverted, StartDateConverted.AddDays(ReservationDays), Domain.Models.Enums.GuestRatingStatus.Reserved, Domain.Domain.Models.Enums.AccommodationAndOwnerRatingStatus.Disabled);
                _reservationService.Create(reservation);
            }


            this.Execute_Reject();
        }

        private bool CanExecute()
        {
            return true;
        }

        private void Execute_CancelReservation()
        {
            this.Execute_Reject();
        }

        private void Execute_Reject()
        {
            _window.Close();
        }
    }
}

