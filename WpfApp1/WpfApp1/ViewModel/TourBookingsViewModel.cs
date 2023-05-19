using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Domain.Models;
using WpfApp1.Domain.ServiceInterfaces;
using WpfApp1.Views;
using WpfApp1.Service;
using WpfApp1.Commands;
using System.Xml.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Reflection.Metadata;
using System.Text.RegularExpressions;
using System.ComponentModel;

namespace WpfApp1.ViewModel
{
    public class TourBookingsViewModel : ViewModelBase, IDataErrorInfo
    {
        public Action CloseAction { get; set; }
        public ObservableCollection<TourEvent> TourEvents { get; set; }

        public ObservableCollection<Voucher> Vouchers { get; set; }

        private readonly IVoucherService _voucherService;
        private readonly ITourBookingService _tourBookingService;
        private readonly ITourEventService _tourEventService;
        private readonly ILocationService _locationService;

        private string _availableSpotsText { get; set; }
        private int _availableSpots { get; set; }


        private TourEvent _selectedTourEvent;

        private Voucher _selectedVoucher;

        private int _numberOfPeople;
        public string Name { get; set; }
        public DateTime ExpirationDate { get; set; }

        public string AvailableSpotsText
        {
            get => _availableSpotsText;
            set
            {
                if (_availableSpotsText != value)
                {
                    _availableSpotsText = value;
                    OnPropertyChanged("AvailableSpotsText");
                }
            }
        }
        public int NumberOfPeople
        {
            get => _numberOfPeople;
            set
            {
                if (_numberOfPeople != value)
                {
                    _numberOfPeople = value;
                    OnPropertyChanged("NumberOfPeople");
                }
            }
        }


        public int AvailableSpots
        {
            get => _availableSpots;
            set
            {
                if (_availableSpots != value)
                {
                    _availableSpots = value;
                    OnPropertyChanged("AvailableSpots");
                }
            }
        }


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

        public Voucher SelectedVoucher
        {
            get => _selectedVoucher;
            set
            {
                if (_selectedVoucher != value)
                {
                    _selectedVoucher = value;
                    OnPropertyChanged("SelectedVoucher");
                }
            }
        }


        public TourBookingsViewModel(Tour tour)
        {


            _locationService = InjectorService.CreateInstance<ILocationService>();
            _tourEventService = InjectorService.CreateInstance<ITourEventService>();
            _voucherService = InjectorService.CreateInstance<IVoucherService>();
            _tourBookingService = InjectorService.CreateInstance<ITourBookingService>();

            TourEvents = new ObservableCollection<TourEvent>(_tourEventService.GetNotFinishedTourEvents(tour));

            Vouchers = new ObservableCollection<Voucher>(_voucherService.VoucherForTourist(MainWindow.LogInUser.Id));

            
            SelectedTourEvent = TourEvents[0];
            

            AllToursCommand = new RelayCommand(Execute_AllTours, CanExecute_Command);
            BookedToursCommand = new RelayCommand(Execute_BookedTours, CanExecute_Command);
            CheckAvailabilityCommand = new RelayCommand(Execute_CheckAvailability, CanExecute_Command);
            ReserveCommand = new RelayCommand(Execute_Reserve, CanExecute_Command);
            MyProfileCommand = new RelayCommand(Execute_MyProfile, CanExecute_Command);
            RequestTourCommand = new RelayCommand(Execute_RequestTour, CanExecute_Command);
            RequestListCommand = new RelayCommand(Execute_RequestList, CanExecute_Command);


        }

        private RelayCommand requestListCommand;
        public RelayCommand RequestListCommand
        {
            get => requestListCommand;
            set
            {
                if (value != requestListCommand)
                {
                    requestListCommand = value;
                    OnPropertyChanged();
                }
            }
        }
        private void Execute_RequestList(object sender)
        {
            TourRequest tourRequestList = new TourRequest();
            tourRequestList.Show();
            CloseAction();


        }

        private void Execute_MyProfile(object sender)
        {   
            TouristProfile profile = new TouristProfile();
            profile.Show();
            CloseAction();
        }
        private void Execute_RequestTour(object sender)
        {
            RequestNewTours requestNewTour = new RequestNewTours();
            requestNewTour.Show();
            CloseAction();
        }
        private bool CanExecute_Command(object parameter)
        {
            return true;
        }
        private void Execute_AllTours(object sender)
        {
            TourSearchAndOverview tourSearch = new TourSearchAndOverview();
            tourSearch.Show();
            CloseAction();
        }
        private void Execute_BookedTours(object sender)
        {

            BookedTours bookedTours = new BookedTours();
            bookedTours.Show();
            CloseAction();

        }


        private RelayCommand requestTourCommand;
        public RelayCommand RequestTourCommand
        {
            get => requestTourCommand;
            set
            {
                if (value != requestTourCommand)
                {
                    requestTourCommand = value;
                    OnPropertyChanged();
                }
            }
        }

        private RelayCommand myProfileCommand;
        public RelayCommand MyProfileCommand
        {
            get => myProfileCommand;
            set
            {
                if (value != myProfileCommand)
                {
                    myProfileCommand = value;
                    OnPropertyChanged();
                }
            }
        }

        private RelayCommand allToursCommand;
        public RelayCommand AllToursCommand
        {
            get => allToursCommand;
            set
            {
                if (value != allToursCommand)
                {
                    allToursCommand = value;
                    OnPropertyChanged();
                }
            }
        }

        private RelayCommand bookedToursCommand;
        public RelayCommand BookedToursCommand
        {
            get => bookedToursCommand;
            set
            {
                if (value != bookedToursCommand)
                {
                    bookedToursCommand = value;
                    OnPropertyChanged();
                }
            }
        }


        private RelayCommand checkAvailabilityCommand;
        public RelayCommand CheckAvailabilityCommand
        {
            get => checkAvailabilityCommand;
            set
            {
                if (value != checkAvailabilityCommand)
                {
                    checkAvailabilityCommand = value;
                    OnPropertyChanged();
                }
            }
        }

        private RelayCommand reserveCommand;
        public RelayCommand ReserveCommand
        {
            get => reserveCommand;
            set
            {
                if (value != reserveCommand)
                {
                    reserveCommand = value;
                    OnPropertyChanged();
                }
            }
        }


        private void Execute_Reserve(object sender)
        {

            if (AvailableSpots >= NumberOfPeople && IsValid)
            {
                TourBooking existingTourBooking = _tourBookingService.GetTourBookingForTourEventAndUser(SelectedTourEvent.Id, MainWindow.LogInUser.Id);
                if (existingTourBooking != null)
                {
                    MessageBox.Show("Already reserved this tour!");
                }
                else
                {
                    TourBooking tourBooking = new TourBooking(-1, NumberOfPeople, SelectedTourEvent, MainWindow.LogInUser, SelectedVoucher);
                    _tourBookingService.Create(tourBooking);

                    if (SelectedVoucher != null)
                    {
                        Vouchers.Remove(SelectedVoucher);
                        SelectedVoucher = null;
                    }

                    MessageBox.Show("          Successfully reserved! " + Environment.NewLine +
                "    View tour in BOOKED TOURS!" + Environment.NewLine +
                "    \t     (F7)", "Success ");
                }

            }
            else
            {
                MessageBox.Show("Not enough available spots!");
            }

        }


        private void Execute_CheckAvailability(object sender)
        {
            if (SelectedTourEvent == null)
            {
                return;
            }
            int reservedSpots = _tourEventService.CheckAvailability(SelectedTourEvent);
            AvailableSpots = SelectedTourEvent.Tour.MaxGuests - reservedSpots;

            if (AvailableSpots < NumberOfPeople)
            {
                List<TourEvent> tourEventsForLocation = _tourEventService.GetAvailableTourEventsForLocation(SelectedTourEvent.Tour.Location, NumberOfPeople);
                RefreshTours(tourEventsForLocation);
               
            }
            else
            {
                
            }
        }

    
        private void RefreshTours(List<TourEvent> tourEvents)
        {
            TourEvents.Clear();
            foreach (TourEvent tourEvent in tourEvents)
            {
                TourEvents.Add(tourEvent);
            }
        }

        public string Error => null;
        public string this[string columnName]
        {
            get
            {
                if (columnName == "NumberOfPeople")

                {
                    if (NumberOfPeople <= 0)
                    {
                        return "positive number";
                    }
                    if(NumberOfPeople >= 50)
                    {
                        return "choose a smaller number";
                    }
                }
                return null;
            }
        }
        private readonly string[] _validatedProperties = { "NumberOfPeople" };

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
