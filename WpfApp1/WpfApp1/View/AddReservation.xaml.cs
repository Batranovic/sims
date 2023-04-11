using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WpfApp1.Controller;
using WpfApp1.Domain.ServiceInterfaces;
using WpfApp1.Model;
using WpfApp1.Service;
using WpfApp1.Util;
using WpfApp1.View;

namespace WpfApp1.View
{
    /// <summary>
    /// Interaction logic for AddReservation.xaml
    /// </summary>
    public partial class AddReservation : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private readonly IReservationService _reservationService;

        public Accommodation Accommodation { get; set; }

        public Guest Guest { get; set; }

        public int ReservationDays { get; set; }

        public int GuestsNumber { get; set; }


        public DateTime StartDateConverted { get; set; }

        public DateTime EndDateConverted { get; set; }




        public AddReservation(Accommodation accommodation, Guest guest)
        {
            InitializeComponent();
            this.DataContext = this;

            _reservationService = InjectorService.CreateInstance<IReservationService>();

            bool slobodan;
            Guest = guest;
            Accommodation = accommodation;
            StartlDay = DateTime.Now;
            EndDay = DateTime.Now;

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

        private void Confirm(object sender, RoutedEventArgs e)
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

            MessageBoxResult result = MessageBox.Show(message, "Confirmation", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                
                Reservation reservation = new Reservation(Guest,Accommodation, StartDateConverted, StartDateConverted.AddDays(ReservationDays), Model.Enums.GuestRatingStatus.Reserved, Domain.Models.Enums.AccommodationAndOwnerRatingStatus.Disabled);
                _reservationService.Create(reservation);
            }


            this.Close();
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
