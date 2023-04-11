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
using WpfApp1.Domain.Domain.Models.Enums;
using WpfApp1.Domain.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using WpfApp.Observer;
using static System.Net.Mime.MediaTypeNames;
using System.Xml.Linq;
using Application = System.Windows.Application;
using WpfApp1.Service;
using WpfApp1.Domain.ServiceInterfaces;
using WpfApp1.Domain.Models.Enums;

namespace WpfApp1.View
{
    /// <summary>
    /// Interaction logic for SignInAccommodation.xaml
    /// </summary>
    public partial class SignInAccommodation : Window, INotifyPropertyChanged, IDataErrorInfo
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<AccommodationKind> AccommodationKind { get; set; }
        public AccommodationKind SelectedAccommodationKind { get; set; }

        private readonly ILocationService _locationService;
        private readonly IAccommodationService _accommodationService;
        private readonly IImageService _imageService;
   
        public ObservableCollection<string> States { get; set; }
        public ObservableCollection<string> Cities { get; set; }

        public string SelectedCity { get; set; }
        public Owner LogInOwner { get; set; }
        public SignInAccommodation(Owner owner)
        {
            InitializeComponent();
            this.DataContext = this;

            _locationService = InjectorService.CreateInstance<ILocationService>();
            _accommodationService = InjectorService.CreateInstance<IAccommodationService>(); 
            _imageService = InjectorService.CreateInstance<IImageService>(); 
                
            SelectCity.IsEnabled = false;

            States = new ObservableCollection<string>(_locationService.GetStates());
            Cities = new ObservableCollection<string>();

            LogInOwner = (Owner)owner;
            AccommodationKind = new ObservableCollection<AccommodationKind>(Enum.GetValues(typeof(AccommodationKind)).Cast<AccommodationKind>());

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


        private List<Domain.Models.Image> MakeImages(Accommodation accommodation)
        {
            List< Domain.Models.Image > images = new List<Domain.Models.Image>();
            foreach (string s in _urls)
            {
                images.Add(new Domain.Models.Image(s, accommodation.Id, ImageKind.Accommodation));
            }
            foreach (Domain.Models.Image image in images)
            {
                _imageService.Create(image);
            }
            return images;
        }
        private void Confirm(object sender, RoutedEventArgs e)
        {
            if(!IsValid)
            {
                return;
            }
            Location location = _locationService.GetByCityAndState(SelectedCity, SelectedState);
            Accommodation accommodation = new Accommodation(NameA, location, SelectedAccommodationKind, MaxGuests, MinResevation, CancelDay, LogInOwner);
            _accommodationService.Create(accommodation);
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
            SelectCity.IsEnabled = true;
            Cities.Clear();
            foreach (string city in _locationService.GetCitiesFromStates(SelectedState))
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

        public string Error => null;

        public string this[string columnName]
        {
            get
            {
                if (columnName == "NameA")
                {
                    if (NameA == null)
                    {
                        return "Nije dodato ime smestaja";
                    }
                }

                if (columnName == "SelectedState")
                {
                    if (SelectedState == null)
                    {
                        return "Nije izabrana drzava";
                    }
                }
                if (columnName == "SelectedCity")
                {
                    if (SelectedCity == null)
                    {
                        return "Nije izabran grad";
                    }
                }
                if (columnName == "SelectedAccommodationKind")
                {
                    if (SelectedAccommodationKind == null)
                    {
                        return "Nije izabrana vrsta smestaja";
                    }
                }
                if (columnName == "MaxGuests")
                {
                    if (MaxGuests == 0)
                    {
                        return "Nije postavljen broj gostiju";
                    }
                }
                if (columnName == "MinReservation")
                {
                    if (MinResevation == 0)
                    {
                        return "Nije postavljen najmanja rezervaciaj";
                    }
                }
                if (columnName == "CancelDay")
                {
                    if (CancelDay == 0)
                    {
                        return "Nije postavljen najmanja rezervaciaj";
                    }
                }
                if(columnName == "Url")
                {
                    if(_urls.Count == 0)
                    {
                        return "Nije postavljen ni jedan link";
                    }
                }
                return null;
            }
        }
      
        private readonly string[] _validatedProperties = { "NameA", "SelectedState", "SelectedCity", "SelectedAccommodationKind", "MaxGuests", "MinResevation", "CancelDay", "_urls" };

        public bool IsValid
        {
            get
            {
                foreach (var property in _validatedProperties)
                {
                    if (this[property] != null)
                        return false;
                }

                return true;
            }
        }

    }
}
