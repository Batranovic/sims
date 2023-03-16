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
using WpfApp.Observer;
using static System.Net.Mime.MediaTypeNames;
using System.Xml.Linq;
using Application = System.Windows.Application;

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

        public ImageController ImageController { get; set; }

        public ObservableCollection<string> States { get; set; }
        public ObservableCollection<string> Cities { get; set; }

        public string SelectedCity { get; set; }
        public Owner LogInOwner { get; set; }
        public SignInAccommodation(User user)
        {
            InitializeComponent();
            this.DataContext = this;

            InitialisationControllers();


            States = new ObservableCollection<string>(LocationController.GetStates());
            Cities = new ObservableCollection<string>();

            LogInOwner = (Owner)user;
            AccommodationKind = new ObservableCollection<AccommodationKind>(Enum.GetValues(typeof(AccommodationKind)).Cast<AccommodationKind>());

        }

        private void InitialisationControllers()
        {
            var app = Application.Current as App;
            LocationController = app.LocationController;
            AccommodationController = app.AccommodationController;
            ImageController = app.ImageController;
        }

        private string _state;
        public string SelectedState
        {
            get => _state;
            set
            {
                if (_state != value)
                {
                    _state = value;
                    OnPropertyChanged(_state);
                }
            }
        }

        private string _name;
        public string NameA
        {
            get => _name;
            set
            {
                if (value != _name)
                {
                    _name = value;
                    OnPropertyChanged("NameA");
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
        private int _minResevation;
        public int MinResevation
        {
            get => _minResevation;
            set
            {
                if (value != _minResevation)
                {
                    _minResevation = value;
                    OnPropertyChanged("MinResevation");
                }
            }
        }
        private int _cancelDay = 1;
        public int CancelDay
        {
            get => _cancelDay;
            set
            {
                if (value != _cancelDay)
                {
                    _cancelDay = value;
                    OnPropertyChanged("CancelDay");
                }
            }
        }

        private string _url;
        public string Url
        {
            get => _url;
            set
            {
                if(value != _url)
                {
                    _url = value;
                    OnPropertyChanged("Url");
                }
            }
        }


        private List<Model.Image> MakeImages(Accommodation accommodation)
        {
            List<Model.Image> images = new List<Model.Image>();
            foreach (string s in _urls)
            {
                images.Add(new Model.Image(s, accommodation.Id, ImageKind.accommodation));
            }
            foreach (Model.Image image in images)
            {
                ImageController.Create(image);
            }
            return images;
        }
        private void Confirm(object sender, RoutedEventArgs e)
        {
            Location location = LocationController.GetByCityAndState(SelectedCity, SelectedState);
            Accommodation accommodation = new Accommodation(NameA, location, SelectedAccommodationKind, MaxGuests, MinResevation, CancelDay, LogInOwner);
            AccommodationController.Create(accommodation);
            accommodation.Images = MakeImages(accommodation);
            LogInOwner.Accommodations.Add(accommodation);
            this.Close();
        }

        private void Reject(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

      

        private void ChosenState(object sender, SelectionChangedEventArgs e)
        {
            SelectedState = (string)SelectState.SelectedItem;
            Cities.Clear();
            foreach (string city in LocationController.GetCitiesFromStates(SelectedState))
            {
                Cities.Add(city);
            }
        }

        private List<string> _urls = new List<string>();

        private void AddURL(object sender, RoutedEventArgs e)
        {
            _urls.Add(Url);
            Url = "";
        }
    }
}
