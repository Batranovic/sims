using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using WpfApp1.Controller;
using WpfApp1.Models;

namespace WpfApp1.View
{
    /// <summary>
    /// Interaction logic for BookedTours.xaml
    /// </summary>
    public partial class BookedTours : Window, INotifyPropertyChanged
    {
        public ObservableCollection<TourEvent> TourEvents { get; set; }

        public ObservableCollection<TourPoint> TourPoints { get; set; }

        private TourEvent _selectedTourEvent;

        public TourEventController TourEventController { get; set; }

        public TourBookingController TourBookingController { get; set; }

        public TourPointController TourPointController { get; set; }


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

        public BookedTours()
        {
            InitializeComponent();

            this.DataContext = this;

            var app = Application.Current as App;
            TourEventController = app.TourEventController;
            TourPointController = app.TourPointController;
            TourBookingController = app.TourBookingController;


            TourEvents = new ObservableCollection<TourEvent>(TourBookingController.TouristTourEvents(MainWindow.LogInUser.Id));


        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;

         
        private void AllToursButton(object sender, RoutedEventArgs e)
        {
            TourSearchAndOverview allTours = new TourSearchAndOverview();
            allTours.Show();

            this.Close();

        }

        private void LeaveReviewButton(object sender, RoutedEventArgs e)
        {
            if (_selectedTourEvent.Status  == Models.Enums.TourEventStatus.Finished)
            {
                AddRatingTourAndGuide rateWindow = new AddRatingTourAndGuide(TourBookingController.GetTourBookingForTourEventAndUser(_selectedTourEvent.Id, MainWindow.LogInUser.Id));
                rateWindow.Show();
            } else
            {
                MessageBox.Show("You can only review tours with finished status");
            }
          
            return;
        }
    }
}
