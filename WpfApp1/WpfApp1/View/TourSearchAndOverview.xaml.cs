using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using WpfApp1.Domain.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using WpfApp1.Service;
using WpfApp1.Domain.Models;
using WpfApp1.Domain.ServiceInterfaces;
using WpfApp1.Service;

namespace WpfApp1.View
{
    /// <summary>
    /// Interaction logic for TourView.xaml
    /// </summary>
    public partial class TourSearchAndOverview : Window
    {
        private readonly ILocationService _locationService;

        private readonly ITourService _tourService;

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

            _locationService = InjectorService.CreateInstance<ILocationService>();
            _tourService = InjectorService.CreateInstance<ITourService>();   

            Tours = new ObservableCollection<Tour>(_tourService.GetAll());


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
            List<Tour> searchedTours = _tourService.TourSearch(State, City, Languages, MaxGuests, Duration);
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
