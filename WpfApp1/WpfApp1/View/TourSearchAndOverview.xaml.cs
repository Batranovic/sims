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
using WpfApp1.Model.Enums;
using WpfApp1.Model;
using System.Collections.ObjectModel;
using WpfApp1.Controller;
using System.ComponentModel;

namespace WpfApp1.View
{
    /// <summary>
    /// Interaction logic for TourView.xaml
    /// </summary>
    public partial class TourSearchAndOverview : Window
    {
        public LocationController LocationController { get; set; }

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

            var app = Application.Current as App;
            LocationController = app.LocationController;
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
