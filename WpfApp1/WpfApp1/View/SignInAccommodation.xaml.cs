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
    /// Interaction logic for SignInAccommodation.xaml
    /// </summary>
    public partial class SignInAccommodation : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<AccommodationKind> AccommodationKind { get; set; }
        public AccommodationKind SelectedAccommodationKind { get; set; }
        
        public LocationController LocationController { get; set; }
        public AccommodationController AccommodationController { get; set; }
        public Owner LogInOwner { get; set; }
        public SignInAccommodation(User user)
        {
            InitializeComponent();
            this.DataContext = this;

            var app = Application.Current as App;
            LocationController = app.LocationController;
            AccommodationController = app.AccommodationController;

            LogInOwner = (Owner)user;
            AccommodationKind = new ObservableCollection<AccommodationKind>(Enum.GetValues(typeof(AccommodationKind)).Cast<AccommodationKind>());

        }

        private string _name;
        public string NameAccommodation
        {
            get => _name;
            set
            {
                if (value != _name)
                {
                    _name = value;
                //    OnPropertyChanged("Name");
                }
            }
        }


        private string _city;
        public string City
        {
            get => _city;
            set
            {
                if(value != _city)
                {
                    _city = value;
                //    OnPropertyChanged("City");
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
                //    OnPropertyChanged("State");
                }
            }
        }

        private int _maxGuests;
        public int MaxGuests
        {
            get => _maxGuests;
            set
            {
                if(_maxGuests != value)
                {
                    _maxGuests = value;
                    OnPropertyChanged("MaxGuests");
                }
            }
        }
        private int _minResevation;
        public int MinResevation
        {
            get => _minResevation;
            set
            {
                if(value != _minResevation)
                {
                    _minResevation = value;
                    OnPropertyChanged("MinResevation\r\n");
                }
            }
        }
        private int _cancelDay = 1;
        public int CancelDay
        {
            get => _cancelDay;
            set
            {
                if(value != _cancelDay)
                {
                    _cancelDay = value;
                    OnPropertyChanged("CancelDay");
                }
            }
        }

        private void Confirm(object sender, RoutedEventArgs e)
        {
            Accommodation accommodation = new Accommodation();
            accommodation.Name = NameAccommodation; 
           
            Location location = new Location();
            location.City = City;
            location.State = State;
            LocationController.Create(location);
            accommodation.IdLocation = location.Id;
            accommodation.Location = location;

            accommodation.MaxGuests = MaxGuests;
            accommodation.CancelDay = CancelDay;
            accommodation.MinResevation = MinResevation;
            accommodation.AccommodationKind = SelectedAccommodationKind;
            accommodation.OwnerId = LogInOwner.Id;
            accommodation.Owner = LogInOwner;
            AccommodationController.Create(accommodation);
            LogInOwner.Accommodations.Add(accommodation);


        }

        private void Reject(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
