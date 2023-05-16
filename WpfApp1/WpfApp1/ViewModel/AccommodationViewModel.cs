using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WpfApp1.Commands;
using WpfApp1.Domain.Domain.Models.Enums;
using WpfApp1.Domain.Models;
using WpfApp1.Domain.Models.Enums;
using WpfApp1.Domain.ServiceInterfaces;
using WpfApp1.Service;
using WpfApp1.Views;

namespace WpfApp1.ViewModel
{
    public partial class AccommodationViewModel : ViewModelBase
    {

        public event PropertyChangedEventHandler PropertyChanged;

        private readonly IGuestService _guestService;
        private readonly IAccommodationService _accommodationService;
        private readonly ILocationService _locationService;
       
        public ObservableCollection<AccommodationKind> AccommodationKind { get; set; }

        public AccommodationKind? SelectedAccommodationKind { get; set; }
        public ObservableCollection<Accommodation> Accommodations { get; set; }

        public Accommodation SelectedAccommodation { get; set; }
        public ObservableCollection<string> States { get; set; }

        public ObservableCollection<string> Cities { get; set; }

        public string SelectedCity { get; set; }

        public Guest LogInGuest { get; set; }

        private Window _window;

        public RelayCommand SearchCommand { get; set; }

        public RelayCommand ReserveCommand { get; set; }

        public RelayCommand ShowImageCommand { get; set; }

        public RelayCommand RejectCommand { get; set; }


        public AccommodationViewModel(User user)
        {
            _window = Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.Name == "AccommodationViewName");
            _locationService = InjectorService.CreateInstance<ILocationService>();
            _accommodationService = InjectorService.CreateInstance<IAccommodationService>();
            _guestService = InjectorService.CreateInstance<IGuestService>();

            var kind = Enum.GetValues(typeof(AccommodationKind)).Cast<AccommodationKind>();
            AccommodationKind = new ObservableCollection<AccommodationKind>(kind);

            LogInGuest = (Guest)user;

            //foreach (var state in _locationService.GetStates())
            //{
            //    cbChoseState.Items.Add(state.ToString());
            //}

            //cbChoseType.Items.Add(string.Empty);

            //foreach (var type in AccommodationKind)
            //{
            //    cbChoseType.Items.Add(type.ToString());
            //}

            States = new ObservableCollection<string>(_locationService.GetStates());
            Cities = new ObservableCollection<string>();
            

            SearchCommand = new RelayCommand(param => Execute_Search(), param => CanExecute());
            ReserveCommand = new RelayCommand(Execute_Reserve, param => CanExecute());
            ShowImageCommand = new RelayCommand(Execute_ShowImage, param => CanExecute());


            Accommodations = new ObservableCollection<Accommodation>(_accommodationService.GetSortedListBySuperOwner());

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
                    OnPropertyChanged("SelectedState");
                    ChosenState();
                }
            }
        }

        private string _name;

        public string NameE
        {
            get => _name;
            set
            {
                if (value != _name)
                {
                    _name = value;
                    OnPropertyChanged("NameE");
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

        private int _reservationDays;

        public int ReservationDays
        {
            get => _reservationDays;
            set
            {
                if (_reservationDays != value)
                {
                    _reservationDays = value;
                    OnPropertyChanged("ReservationDays");
                }
            }
        }




        private void Execute_Search()
        {
            Accommodations.Clear();
            foreach (Accommodation a in _accommodationService.SearchAccommodation(NameE, SelectedCity, SelectedState, SelectedAccommodationKind.ToString(), MaxGuests, ReservationDays))
            {
                Accommodations.Add(a);
            }

        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void ChosenState()
        {
            Cities.Clear();
            foreach (string city in _locationService.GetCitiesFromStates(SelectedState))
            {
                Cities.Add(city);
            }
        }

        private void Execute_Reserve(object sender)
        {
            if(_guestService.CanUseBonusPoints(LogInGuest))
            {
                LogInGuest.BonusPoints--;
                _guestService.Update(LogInGuest);
            }

            if (sender != null && sender is Accommodation accommodation)
            {
                AddReservation addReservation = new AddReservation(accommodation, LogInGuest);
                addReservation.Show();

            }
        }


        private void Execute_ShowImage(object sender)
        {
            if (sender != null && sender is Accommodation accommodation)
            {
                ImageView p = new ImageView(accommodation);
                p.Show();
            }
        }

        private bool CanExecute_ShowImage()
        {
            return SelectedAccommodation != null;
        }

        private bool CanExecute()
        {
            return true;
        }

        private void Execute_Reject()
        {
            _window.Close();
        }
    }
}

