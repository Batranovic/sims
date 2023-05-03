using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
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
using WpfApp.Observer;
using WpfApp1.Domain.ServiceInterfaces;
using WpfApp1.Domain.Models;
using WpfApp1.Service;

namespace WpfApp1.View
{
    /// <summary>
    /// Interaction logic for ReservationView.xaml
    /// </summary>
    public partial class ReservationView : Window, INotifyPropertyChanged, IObserver
    {
        private readonly IReservationService _reservationService;
        private readonly IReservationPostponementService _reservationPostponementService;
        public ObservableCollection<Reservation> Reservations { get; set; }

        public Reservation SelectedReservation { get; set; }

        public Guest LogInGuest { get; set; }
        public ReservationView(Guest guest)
        {
            InitializeComponent();
            this.DataContext = this;

            _reservationService = InjectorService.CreateInstance<IReservationService>();
            _reservationPostponementService = InjectorService.CreateInstance<IReservationPostponementService>();
            _reservationService.Subscribe(this);

            LogInGuest = guest;
            Reservations = new ObservableCollection<Reservation>(_reservationService.GetGuestReservations(LogInGuest.Id));

        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void OwnerRating(object sender, RoutedEventArgs e)    //ime
        {
            if (SelectedReservation == null || SelectedReservation.GuestReservationStatus != Domain.Domain.Models.Enums.AccommodationAndOwnerRatingStatus.Unrated)
            {
                return;
            }
            AccommodationAndOwnerRating acoommodationAndOwnerRating = new AccommodationAndOwnerRating(SelectedReservation);
            acoommodationAndOwnerRating.Show();
        }


        public void ReservationPostponement(object sender, RoutedEventArgs e)
        {
            if (SelectedReservation == null)
            {
                return;
            }

            ReservationPostponation reservationPostponation = new ReservationPostponation(SelectedReservation);
            reservationPostponation.Show();
        }

        public void CancelReservation(object sender, RoutedEventArgs e)
        {
            if (SelectedReservation.StartDate < DateTime.Now.AddDays(-SelectedReservation.Accommodation.CancelDay) && SelectedReservation.EndDate <= DateTime.Now)
            {
                return;
            }
            _reservationService.Delete(SelectedReservation);
        }

        public void Update()
        {
            Reservations.Clear();
            foreach (Reservation r in _reservationService.GetAll())
            {
                Reservations.Add(r);
            }
        }
    }
}
