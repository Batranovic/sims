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
    public class TourSearchAndOverviewViewModel : ViewModelBase, IDataErrorInfo
    {
        private readonly ILocationService _locationService;

        private readonly ITourService _tourService;

        public ObservableCollection<Tour> Tours { get; set; }

        public Action CloseAction { get; set; }
        public Tour SelectedTour { get; set; }

        public ObservableCollection<string> States { get; set; }

        public ObservableCollection<string> Cities { get; set; }
       
        private string _duration;

        public string Duration
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
        
        private string _languages;
        public string Languages
        {
            get => _languages;
            set
            {
                if (value != _languages)
                {
                    _languages = value;
                    OnPropertyChanged("Languages");
                }
            }
        }


        private void ChosenState()
        {
            Cities.Clear();
            foreach (string city in _locationService.GetCitiesFromStates(SelectedState))
            {
                Cities.Add(city);
            }
        }

        private string _maxGuests;
        public string MaxGuests
        {
            get { return _maxGuests; }
            set
            {
                _maxGuests = value;
                OnPropertyChanged("MaxGuests");
            }
        }



        private string _city;
        public string SelectedCity
        {
            get => _city;
            set
            {
                if (_city != value)
                {
                    _city = value;
                    OnPropertyChanged("SelectedCity");
                }
            }
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
                    OnPropertyChanged("SelectedState");
                }
            }
        }
        public IEnumerable<string> Statess { get; private set; }
        public TourSearchAndOverviewViewModel()
        {
            _locationService = InjectorService.CreateInstance<ILocationService>();
            _tourService = InjectorService.CreateInstance<ITourService>();

            Tours = new ObservableCollection<Tour>(_tourService.GetAll());

            States = new ObservableCollection<string>(_locationService.GetStates());
            Cities = new ObservableCollection<string>();

           // SelectedState = null;
            //SelectedCity = null;

            Languages = "";
            Duration = "";
            MaxGuests = "";

            SearchCommand = new RelayCommand(Execute_Search, CanExecute_Command);
            AllToursCommand = new RelayCommand(Execute_AllTours, CanExecute_Command);
            BookedToursCommand = new RelayCommand(Execute_BookedTours, CanExecute_Command);
            LogOutCommand = new RelayCommand(Execute_LogOut, CanExecute_Command);
            IncrementCommand = new RelayCommand(Execute_Increment, CanExecute_Command);
            DecrementCommand = new RelayCommand(Execute_Decrement, CanExecute_Command);
            ViewMoreCommand = new RelayCommand(Execute_ViewMore, CanExecute_Command);
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
        private RelayCommand searchCommand;
        public RelayCommand SearchCommand
        {
            get => searchCommand;
            set
            {
                if (value != searchCommand)
                {
                    searchCommand = value;
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

        private RelayCommand logOutCommand;
        public RelayCommand LogOutCommand
        {
            get => logOutCommand;
            set
            {
                if (value != logOutCommand)
                {
                    logOutCommand = value;
                    OnPropertyChanged();
                }
            }
        }

        private RelayCommand incrementCommand;
        public RelayCommand IncrementCommand
        {
            get => incrementCommand;
            set
            {
                if (value != incrementCommand)
                {
                    incrementCommand = value;
                    OnPropertyChanged();
                }
            }
        }

        private RelayCommand decrementCommand;
        public RelayCommand DecrementCommand
        {
            get => decrementCommand;
            set
            {
                if (value != decrementCommand)
                {
                    decrementCommand = value;
                    OnPropertyChanged();
                }
            }
        }


        private RelayCommand viewMoreCommand;
        public RelayCommand ViewMoreCommand
        {
            get => viewMoreCommand;
            set
            {
                if (value != viewMoreCommand)
                {
                    viewMoreCommand = value;
                    OnPropertyChanged();
                }
            }
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


        private void Execute_RequestTour(object sender)
        {
            RequestNewTours requestNewTour = new RequestNewTours();
            requestNewTour.Show();
            CloseAction();
        }
        private void Execute_ViewMore(object sender)
        {
            if (sender != null && sender is Tour tour)
            {
                TourBookings tourBookingWindow = new TourBookings(tour);
                tourBookingWindow.Show();
                CloseAction();
            }

        }

        private bool CanExecute_Command(object parameter)
        {
            return true;
        }

        private void RefreshTours(List<Tour> tours)
        {
            Tours.Clear();
            foreach (Tour tour in tours)
            {
                Tours.Add(tour);
            }
        }
        private void Execute_Increment(object sender)
        {
            int currentValue = 0;
            if (!string.IsNullOrEmpty(MaxGuests))
            {
                int.TryParse(MaxGuests, out currentValue);
            }
            MaxGuests = (currentValue + 1).ToString();
        }

        private void Execute_Decrement(object sender)
        {
            int currentValue;
            if (int.TryParse(MaxGuests, out currentValue))
            {
                if (currentValue > 0)
                {
                    MaxGuests = (currentValue - 1).ToString();
                }
            }
        }

        private void Execute_Search(object sender)
        {
            if (IsValid)
            {
                List<Tour> searchedTours = _tourService.TourSearch(SelectedState, SelectedCity, Languages, MaxGuests, Duration);
                RefreshTours(searchedTours);
            }

        }
        private void Execute_AllTours(object sender)
        {
            TourSearchAndOverview tourSearch = new TourSearchAndOverview();
            tourSearch.Show();
        }
        private void Execute_BookedTours(object sender)
        {

            BookedTours bookedTours = new BookedTours();
            bookedTours.Show();
            CloseAction();
            
        }

        private void Execute_LogOut(object sender)
        {
            MessageBox.Show("You are logging out!");
            MainWindow mw = new MainWindow();
            mw.Show();
            CloseAction();

        }

        public string Error => null;
        public string this[string columnName]
        {
            get
            {

                if (columnName == "SelectedCity")
                {
                    if (SelectedCity == null || SelectedCity=="City")
                    {
                        return "choose a city";

                    }
                }


                if (columnName == "SelectedState")
                {
                    if (SelectedState == null || SelectedState == "State")
                    {
                        return "choose a state";

                    }
                }

                if (columnName == "Duration")
                {
                    if (columnName == "Duration")
                    {
                        if (!string.IsNullOrEmpty(Duration) && !Regex.IsMatch(Duration, @"^\d+(\.\d+)?$") || Duration=="0")
                        {
                            return "only valid positive number.";
                        }
                        // Do something with the valid input
                    }


                }

                if (columnName == "MaxGuests")
                {
                    if (!string.IsNullOrEmpty(MaxGuests) && !Regex.IsMatch(MaxGuests, "^[1-9][0-9]*$"))
                    {
                        if (MaxGuests.StartsWith("-"))
                        {
                            return "please enter only positive numbers";
                        }
                        else
                        {
                            return "please enter only numbers";
                        }
                    }
                }


                if (columnName == "Languages")
                {
                    if (!string.IsNullOrEmpty(Languages))
                    {

                        if (!Regex.IsMatch(Languages.TrimEnd(), "^[a-zA-Z]+(\\s)*$"))
                        {
                            return "enter only letters";
                        }
                    }
                }

                return null;

            }

        }
        private readonly string[] _validatedProperties = { "Duration", "MaxGuests", "Languages","SelectedCity", "SelectedState"};

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

