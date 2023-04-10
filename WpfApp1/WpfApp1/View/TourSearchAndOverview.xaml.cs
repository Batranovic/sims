using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using WpfApp1.Controller;
using WpfApp1.Models;

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

                this.Close();
            }
        }

     

        private void BookedToursButton(object sender, RoutedEventArgs e)
        {

            BookedTours bookedTours = new BookedTours();
            bookedTours.Show();

            this.Close();

        }

        private void LogOutButton(object sender, RoutedEventArgs e)
        {
           
            MessageBox.Show("You are logging out!");
            User user = MainWindow.LogInUser;
            user.Id = -1;
            MainWindow mw = new MainWindow();
            mw.Show();
            this.Close();
        }
    }
}
