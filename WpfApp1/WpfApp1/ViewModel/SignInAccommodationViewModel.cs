using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using WpfApp1.Domain.Domain.Models.Enums;
using WpfApp1.Domain.Models.Enums;
using WpfApp1.Domain.Models;
using WpfApp1.Domain.ServiceInterfaces;
using WpfApp1.Service;
using WpfApp1.Commands;

namespace WpfApp1.ViewModel
{
    public class SignInAccommodationViewModel : ViewModelBase, IDataErrorInfo
    {
        public ObservableCollection<AccommodationKind> AccommodationKind { get; set; }
        public AccommodationKind SelectedAccommodationKind { get; set; }

        private readonly ILocationService _locationService;
        private readonly IAccommodationService _accommodationService;
        private readonly IImageService _imageService;
        private Window _window;

        public ObservableCollection<string> States { get; set; }
        public ObservableCollection<string> Cities { get; set; }

        public string SelectedCity { get; set; }
        public Owner LoggedOwner { get; set; }

        public RelayCommand ConfrimCommand { get; set; }
        public RelayCommand RejectCommand { get; set; } 
        public RelayCommand AddUrlCommand { get; set; }
        public SignInAccommodationViewModel(Owner owner)
        {
            _window = Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.Name == "AddAccommodation");
            _locationService = InjectorService.CreateInstance<ILocationService>();
            _accommodationService = InjectorService.CreateInstance<IAccommodationService>();
            _imageService = InjectorService.CreateInstance<IImageService>();

            Init(owner);
        }

        public void InitCommand()
        {
            ConfrimCommand = new RelayCommand(param => Execute_Confirm(), param => CanExecute_Confrim());
            RejectCommand = new RelayCommand(param => Execute_Reject(), param => CanExecute());
            AddUrlCommand = new RelayCommand(param => Execute_AddURL(), param => CanExecute());
        }

        public void Init(Owner owner)
        {
            States = new ObservableCollection<string>(_locationService.GetStates());
            Cities = new ObservableCollection<string>();

            LoggedOwner = (Owner)owner;
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
                    ChosenState();
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
                if (value != _url)
                {
                    _url = value;
                    OnPropertyChanged("Url");
                }
            }
        }


        private List<Domain.Models.Image> MakeImages(Accommodation accommodation)
        {
            List<Domain.Models.Image> images = new List<Domain.Models.Image>();
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
        private void Execute_Confirm()
        {
           
            Location location = _locationService.GetByCityAndState(SelectedCity, SelectedState);
            Accommodation accommodation = new Accommodation(NameA, location, SelectedAccommodationKind, MaxGuests, MinResevation, CancelDay, LoggedOwner);
            _accommodationService.Create(accommodation);
            accommodation.Images = MakeImages(accommodation);
            LoggedOwner.Accommodations.Add(accommodation);
            _window.Close();
        }

        private bool CanExecute_Confrim()
        {
            return IsValid;
        }

        private void Execute_Reject()
        {
            _window.Close();
        }

       private bool CanExecute()
        {
            return true;
        }

        private void ChosenState()
        {
            Cities.Clear();
            foreach (string city in _locationService.GetCitiesFromStates(SelectedState))
            {
                Cities.Add(city);
            }
        }

        private List<string> _urls = new List<string>();

        private void Execute_AddURL()
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
                if (columnName == "Url")
                {
                    if (_urls.Count == 0)
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
