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

        public TourBookingController tourBookingController;
        public TourEventController _tourEventController;

        public LocationController LocationController { get; set; }

        private string _availableSpotsText { get; set; }
        private int _availableSpots { get; set; }


        private TourEvent _selectedTourEvent;

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

        public int NumberOfPeople { get; set; }



        public TourBookingWindow(Tour tour)
        {
            InitializeComponent();
            this.DataContext = this;

            var app = Application.Current as App;
            LocationController = app.LocationController;


            //NumberOfPeople = "";
            _tourEventController = new TourEventController();
            tourBookingController = new TourBookingController();
            TourEvents = new ObservableCollection<TourEvent>(tour.TourEvents);
        }

        private void Reserve_Click(object sender, RoutedEventArgs e)
        {
           User user = new User() { Id = 1 };
            TourBooking tourBooking = new TourBooking(-1, NumberOfPeople, SelectedTourEvent, user);
            tourBookingController.Create(tourBooking);

        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Check_Availability_Button_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedTourEvent == null)
            {
                return;
            }
            int reservedSpots = _tourEventController.CheckAvailability(SelectedTourEvent);
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

        private void Suggest_More_Button_Click(object sender, RoutedEventArgs e)
        {
              if(SelectedTourEvent == null)
            {
                return;
            }
            List<TourEvent> tourEventsForLocation = _tourEventController.GetAvailableTourEventsForLocation(SelectedTourEvent.Tour.Location, NumberOfPeople);
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
