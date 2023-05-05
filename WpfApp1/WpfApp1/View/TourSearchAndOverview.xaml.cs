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
using WpfApp1.Domain.Domain.Models.Enums;

namespace WpfApp1.View
{
    /// <summary>
    /// Interaction logic for TourView.xaml
    /// </summary>
    public partial class TourSearchAndOverview : Window, INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        private readonly ILocationService _locationService;

        private readonly ITourService _tourService;

        public ObservableCollection<Tour> Tours { get; set; }


        public Tour SelectedTour { get; set; }

        public ObservableCollection<string> States { get; set; }

        public ObservableCollection<string> Cities { get; set; }
        public string Languages { get; set; }
        public string Duration { get; set; }
        // public string MaxGuests { get; set; }

        private string _state;

        private string _maxGuests = "0";
        public string MaxGuests
        {
            get { return _maxGuests; }
            set
            {
                _maxGuests = value;
                OnPropertyChanged(nameof(MaxGuests));
            }
        }

        public string SelectedCity { get; set; }
        public string SelectedState
        {
            get => _state;
            set
            {
                if (_state != value)
                {
                    _state = value;
                    OnPropertyChanged("SelectedState");
                }
            }
        }
        public TourSearchAndOverview()
        {
            InitializeComponent();
            this.DataContext = this;

            _locationService = InjectorService.CreateInstance<ILocationService>();
            _tourService = InjectorService.CreateInstance<ITourService>();

            Tours = new ObservableCollection<Tour>(_tourService.GetAll());


            foreach (var state in _locationService.GetStates())
            {
                cbChoseState.Items.Add(state.ToString());
            }



            Languages = "";
            Duration = "";
            MaxGuests = "0";

        }


        private void ChosenState(object sender, SelectionChangedEventArgs e)
        {
            SelectedState = (string)cbChoseState.SelectedItem;
            cbChoseCity.Items.Clear();
            foreach (string city in _locationService.GetCitiesFromStates(SelectedState))
            {
                cbChoseCity.Items.Add(city);
            }
        }

        private void IncrementButton_Click(object sender, RoutedEventArgs e)
        {
            int currentValue;
            if (int.TryParse(MaxGuests, out currentValue))
            {
                MaxGuests = (currentValue + 1).ToString();
            }
        }

        private void DecrementButton_Click(object sender, RoutedEventArgs e)
        {
            int currentValue;
            if (int.TryParse(MaxGuests, out currentValue))
            {
                MaxGuests = (currentValue - 1).ToString();
            }
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
            List<Tour> searchedTours = _tourService.TourSearch(SelectedState, SelectedCity, Languages, MaxGuests, Duration);
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

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
