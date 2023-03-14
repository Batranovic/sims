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
    public partial class TourView : Window, INotifyPropertyChanged

    {
        public event PropertyChangedEventHandler PropertyChanged;
        public LocationController LocationController { get; set; }

        public ObservableCollection<Tour> Tours { get; set; }
        public TourController TourController { get; set; }

        public TourView()
        {
            InitializeComponent();
            DataContext = this;

            var app = Application.Current as App;
            LocationController = app.LocationController;
            
            
        }

        private string _city;

        public string City
        {
            get => _city;

            set
            {
                if (value != _city)
                {
                    _city = value;
                    OnPropertyChanged("City");
                }
            }
        }

        private string _state;

        public string State
        {
            get => _state;

            set
            {
                if (value != _state)
                {
                    _state = value;
                    OnPropertyChanged("State");
                }
            }
        }

        private TimeSpan _duration;
        public TimeSpan Duration
        {
            get => _duration;

            set
            {
                if (value != _duration)
                {
                    _duration = value;
                    OnPropertyChanged("Duration");
                }
            }
        }

        private string _language;

        public string Language
        {
            get => _language;

            set
            {
                if (value != _language)
                {
                    _language = value;
                    OnPropertyChanged("Language");
                }
            }
        }

        private int _maxGuests;
        public int MaxGuests
        {
            get => _maxGuests;
            set
            {
                if (_maxGuests != value)
                {
                    _maxGuests = value;
                    OnPropertyChanged("MaxGuests");
                }
            }
        }
    



        private void Search(object sender, RoutedEventArgs e)
        {
           // Tours = new ObservableCollection<Tour>(TourController.Search(Location, Duration, Language, MaxGuests));
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


    }
}
