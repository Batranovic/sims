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
using WpfApp1.Model;
using System.Collections.ObjectModel;
using WpfApp1.Controller;
using System.ComponentModel;

namespace WpfApp1.View
{
    /// <summary>
    /// Interaction logic for TourBookingWindow.xaml
    /// </summary>
    public partial class TourBookingWindow : Window, INotifyPropertyChanged
    {
        public ObservableCollection<TourEvent> TourEvents { get; set; }

        public TourBookingController TourBookingController;
        public TourEventController TourEventController;
        public LocationController LocationController { get; set; }

        private string _availableSpotsText { get; set; }
        private int _availableSpots { get; set; }

        private TourEvent _selectedTourEvent;

        public int NumberOfPeople { get; set; }

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



        public TourBookingWindow(Tour tour)
        {
            InitializeComponent();
            this.DataContext = this;

            var app = Application.Current as App;
            LocationController = app.LocationController;
            TourBookingController = app.TourBookingController;
            TourEventController = app.TourEventController;

            TourEvents = new ObservableCollection<TourEvent>(tour.TourEvents);

            // TourEventController = new TourEventController();
            //TourBookingController = new TourBookingController();

        }

        private void ReserveButton(object sender, RoutedEventArgs e)
        {
            if(AvailableSpots >= NumberOfPeople)
            {
                User user = new User() { Id = 1 };
                TourBooking tourBooking = new TourBooking(-1, NumberOfPeople, SelectedTourEvent, user);
                TourBookingController.Create(tourBooking);
                MessageBox.Show("Successful reservation!");

            }
            else
            {
                MessageBox.Show("Not enough available spots!")
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
            int reservedSpots = TourEventController.CheckAvailability(SelectedTourEvent);
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
            List<TourEvent> tourEventsForLocation = TourEventController.GetAvailableTourEventsForLocation(SelectedTourEvent.Tour.Location, NumberOfPeople);
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

    }

}
