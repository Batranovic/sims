using System;
using System.Collections.Generic;
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
using WpfApp1.Domain.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using WpfApp1.Domain.Domain.Models.Enums;
using WpfApp1.Repository;
using WpfApp1.Domain.ServiceInterfaces;
using WpfApp1.Service;


namespace WpfApp1.View
{
    /// <summary>
    /// Interaction logic for TourBookingWindow.xaml
    /// </summary>
    public partial class TourBookingWindow : Window, INotifyPropertyChanged
    {
        public ObservableCollection<TourEvent> TourEvents { get; set; }

        public ObservableCollection<Voucher> Vouchers { get; set; }

        private readonly IVoucherService _voucherService;
        private readonly ITourBookingService _tourBookingService;
        private readonly ITourEventService _tourEventService;
        private readonly ILocationService _locationService;

        private string _availableSpotsText { get; set; }
        private int _availableSpots { get; set; }


        private TourEvent _selectedTourEvent;

        private Voucher _selectedVoucher;

        public int NumberOfPeople { get; set; }
        public string Name { get; set; }
        public DateTime ExpirationDate { get; set; }

        public string AvailableSpotsText
        {
            get => _availableSpotsText;
            set
            {
                if (_availableSpotsText != value)
                {
                    _availableSpotsText = value;
                    OnPropertyChanged("AvailableSpotsText");
                }
            }
        }

        public int AvailableSpots
        {
            get => _availableSpots;
            set
            {
                if (_availableSpots != value)
                {
                    _availableSpots = value;
                    OnPropertyChanged("AvailableSpots");
                }
            }
        }


        public TourEvent SelectedTourEvent
        {
            get => _selectedTourEvent;
            set
            {
                if (_selectedTourEvent != value)
                {
                    _selectedTourEvent = value;
                    OnPropertyChanged("SelectedTourEvent");
                }
            }
        }

        public Voucher SelectedVoucher
        {
            get => _selectedVoucher;
            set
            {
                if(_selectedVoucher != value)
                {
                    _selectedVoucher = value;
                    OnPropertyChanged("SelectedVoucher");
                }
            }
        }



        public TourBookingWindow(Tour tour)
        {
            InitializeComponent();
            this.DataContext = this;

            _locationService = InjectorService.CreateInstance<ILocationService>();
            _tourEventService = InjectorService.CreateInstance<ITourEventService>();
            _voucherService = InjectorService.CreateInstance<IVoucherService>();
            _tourBookingService = InjectorService.CreateInstance<ITourBookingService>();

            TourEvents = new ObservableCollection<TourEvent>(_tourEventService.GetNotFinishedTourEvents(tour));

            Vouchers = new ObservableCollection<Voucher>(_voucherService.VoucherForTourist(MainWindow.LogInUser.Id));


        }

        
        private void ReserveButton(object sender, RoutedEventArgs e)
        {

            if (AvailableSpots >= NumberOfPeople)
            {
                TourBooking existingTourBooking = _tourBookingService.GetTourBookingForTourEventAndUser(SelectedTourEvent.Id, MainWindow.LogInUser.Id);
                if (existingTourBooking != null)
                {
                    MessageBox.Show("Already reserved this tour!");
                }
                else
                {
                    TourBooking tourBooking = new TourBooking(-1, NumberOfPeople, SelectedTourEvent, MainWindow.LogInUser, SelectedVoucher);
                    _tourBookingService.Create(tourBooking);

                    if(SelectedVoucher != null)
                    {
                        Vouchers.Remove(SelectedVoucher);
                        SelectedVoucher = null;
                    }

                    MessageBox.Show("Successful reservation!");
                }

            }
            else
            {
                MessageBox.Show("Not enough available spots!");
            } 
            
        }

        private void CancelButton(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void CheckAvailabilityButton(object sender, RoutedEventArgs e)
        {
            if (SelectedTourEvent == null)
            {
                return;
            }
            int reservedSpots = _tourEventService.CheckAvailability(SelectedTourEvent);
            AvailableSpots = SelectedTourEvent.Tour.MaxGuests - reservedSpots;
            if (AvailableSpots < NumberOfPeople)
            {
                AvailableSpotsText = "Not available";
            }
            else
            {
                AvailableSpotsText = "Available";
            }
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void SuggestMoreButton(object sender, RoutedEventArgs e)
        {
              if(SelectedTourEvent == null)
            {
                return;
            }
            List<TourEvent> tourEventsForLocation = _tourEventService.GetAvailableTourEventsForLocation(SelectedTourEvent.Tour.Location, NumberOfPeople);
            RefreshTours(tourEventsForLocation);
        }
        private void RefreshTours(List<TourEvent> tourEvents)
        {
            TourEvents.Clear();
            foreach (TourEvent tourEvent in tourEvents)
            {
                TourEvents.Add(tourEvent);
            }
        }

       

        private void AllToursButton(object sender, RoutedEventArgs e)
        {
            TourSearchAndOverview tourOverview = new TourSearchAndOverview();
            tourOverview.Show();

            this.Close();
        }

        private void BookedToursButton(object sender, RoutedEventArgs e)
        {

            BookedTours bookedTours = new BookedTours();
            bookedTours.Show();

            this.Close();

        }
    }

}
