using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using WpfApp1.Controller;
using WpfApp1.Model;
using System.Collections.ObjectModel;
using WpfApp1.Controller;
using System.ComponentModel;
using WpfApp1.Service;

namespace WpfApp1.View
{
    /// <summary>
    /// Interaction logic for TourView.xaml
    /// </summary>
    public partial class TourSearchAndOverview : Window
    {
        private readonly LocationService _locationService;

        public TourController TourController { get; set; }

        public ObservableCollection<Tour> Tours { get; set; }


        public Tour SelectedTour { get; set; }

        public string State { get; set; }

        public string City { get; set; }
        public string Languages { get; set; }
        public string Duration { get; set; }

        public string MaxGuests { get; set; }


        public TourSearchAndOverview()
        {
            InitializeComponent();
            this.DataContext = this;

            _locationService = new LocationService();

            var app = Application.Current as App;
            TourController = app.TourController;

            TourController = new TourController();
            Tours = new ObservableCollection<Tour>(TourController.GetAll());


            State = "";
            City = "";
            Languages = "";
            Duration = "";
            MaxGuests = "";



        }
        private void RefreshTours(List<Tour> tours)
        {
            Tours.Clear();
            foreach (Tour tour in tours)
            {
                Tours.Add(tour);
            }
        }
        private void SearchButton(object sender, RoutedEventArgs e)
        {
            List<Tour> searchedTours = TourController.TourSearch(State, City, Languages, MaxGuests, Duration);
            RefreshTours(searchedTours);
        }

        private void TourDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                SelectedTour = e.AddedItems[0] as Tour;
                TourBookingWindow tourBookingWindow = new TourBookingWindow(SelectedTour);
                tourBookingWindow.Show();
            }
        }
    }
}
