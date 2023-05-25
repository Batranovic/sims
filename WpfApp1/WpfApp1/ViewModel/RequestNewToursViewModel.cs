using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Linq;
using WpfApp1.Commands;
using WpfApp1.Domain.Models;
using WpfApp1.Domain.ServiceInterfaces;
using WpfApp1.Service;
using WpfApp1.Views;
using WpfApp1.Domain.Models.Enums;
using static System.Net.Mime.MediaTypeNames;


namespace WpfApp1.ViewModel
{
    public class RequestNewToursViewModel : ViewModelBase, IDataErrorInfo
    {
        private readonly ILocationService _locationService;

        private readonly ISimpleTourRequestService _simpleTourRequestService;
        public ObservableCollection<string> States { get; set; }

        public ObservableCollection<string> Cities { get; set; }
        public Action CloseAction { get; set; }


        private string _language;
        public string Language
        {
            get => _language;
            set
            {
                if(value != _language)
                {
                    _language = value;
                    OnPropertyChanged("Language");
                }
            }
        }

        private string _description;
        public string Description
        {
            get => _description;
            set
            {
                if (value != _description)
                {
                    _description = value;
                    OnPropertyChanged("Description");
                }
            }

        }
        private DateTime _selectedStartDate;
        public DateTime StartDate
        {
            get => _selectedStartDate;
            set
            {
                if(value != _selectedStartDate)
                {
                    _selectedStartDate = value;
                    OnPropertyChanged("StartDate");
                }
               
            }
        }

        private DateTime _selectedEndDate;
        public DateTime EndDate
        {
            get => _selectedEndDate;
            set
            {
                if (value != _selectedEndDate)
                {
                    _selectedEndDate = value;
                    OnPropertyChanged("EndDate");
                }

            }
        }

        private string _maxGuests;
        public string MaxGuests
        {
            get => _maxGuests;
            set
            {
                if (value != _maxGuests)
                {
                    _maxGuests = value;
                    OnPropertyChanged("MaxGuests");
                }
                
            }
        }
        private string _selectedCity;
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
        public string SelectedCity
        {
            get => _selectedCity;
            set
            {
                if (_selectedCity != value)
                {
                    _selectedCity = value;
                    OnPropertyChanged(_selectedCity);
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
        public RequestNewToursViewModel() {

            _simpleTourRequestService = InjectorService.CreateInstance<ISimpleTourRequestService>();
            _locationService = InjectorService.CreateInstance<ILocationService>();
            States = new ObservableCollection<string>(_locationService.GetStates());
            Cities = new ObservableCollection<string>();

           
            MaxGuests = "";
            StartDate = DateTime.Now;
            EndDate = DateTime.Now;

            MyProfileCommand = new RelayCommand(Execute_MyProfile, CanExecute_Command);
            AllToursCommand = new RelayCommand(Execute_AllTours, CanExecute_Command);
            BookedToursCommand = new RelayCommand(Execute_BookedTours, CanExecute_Command);
            RequestSimpleTourCommand = new RelayCommand(Execute_RequestSimpleTour, CanExecute_Command);
            RequestComplexTourCommand = new RelayCommand(Execute_RequestComplexTour, CanExecute_Command);
            IncrementCommand = new RelayCommand(Execute_Increment, CanExecute_Command);
            DecrementCommand = new RelayCommand(Execute_Decrement, CanExecute_Command);
            RequestListCommand = new RelayCommand(Execute_RequestList, CanExecute_Command);
        }



        private void Execute_RequestSimpleTour(object sender)
        {
            if (IsValid)
            {
                MessageBox.Show("Please wait for guide's answer." + Environment.NewLine +
                                "View status in REQUEST LIST" + Environment.NewLine +
                                "\t   (CTRL + R)", "Request sent ");
                int maxG = int.Parse(MaxGuests);
                Tourist tourist = (Tourist)MainWindow.LogInUser;
                SimpleTourRequest simpleTour = new SimpleTourRequest(-1, SelectedState, SelectedCity, Description, Language, maxG, StartDate, EndDate,tourist,RequestStatus.Pending);
                _simpleTourRequestService.Create(simpleTour);
                SelectedCity = null;
                SelectedState = null;
                Description = "";
                Language = "";
                MaxGuests = "";
                StartDate = DateTime.Today; 
                EndDate = DateTime.Today;
                

            }
            else
            {
                MessageBox.Show("Please fix the errors before submitting the request.", "Error");
            }

           
        }


        private void Execute_RequestList(object sender)
        {
            TourRequest tourRequestList = new TourRequest();
            tourRequestList.Show();
            CloseAction();


        }
        private void Execute_RequestComplexTour(object sender)
        {
            if (IsValid)
            {
                MessageBoxResult result = MessageBox.Show(
                    "   Want to add more than one tour to the list?\n\n" +
                    "           Yes - \"MORE\"    No - \"SUBMIT\"",
                    "Request sent",
                    MessageBoxButton.YesNo);

                if (result == MessageBoxResult.Yes)
                {
                   
                }
                else if (result == MessageBoxResult.No)
                {
                   
                    // Handle "No" button clicked
                }
            } else
            {
                MessageBox.Show("Please fix the errors before submitting the request.", "Error");
            }
        }

        private void Execute_MyProfile(object sender)
        {
           TouristProfile profile = new TouristProfile();   
           profile.Show();
           CloseAction();

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
        private RelayCommand requestSimpleTourCommand;
        public RelayCommand RequestSimpleTourCommand
        {
            get => requestSimpleTourCommand;
            set
            {
                if (value != requestSimpleTourCommand)
                {
                    requestSimpleTourCommand = value;
                    OnPropertyChanged();
                }
            }
        }

        private RelayCommand requestComplexTourCommand;
        public RelayCommand RequestComplexTourCommand
        {
            get => requestComplexTourCommand;
            set
            {
                if (value != requestComplexTourCommand)
                {
                    requestComplexTourCommand = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Error => null;
        public string this[string columnName]
        {
            get
            {
                if (columnName == "SelectedState")
                {
                    if (string.IsNullOrEmpty(SelectedState))
                    {
                        return "choose a state";
                    }
                }

                if (columnName == "SelectedCity")
                {
                    if (SelectedCity == null)
                    {
                        return "choose a location";

                    }
                }
                if (columnName == "MaxGuests")
                {
                    if (string.IsNullOrEmpty(MaxGuests) || MaxGuests=="0")
                    {
                        return "choose number of people";
                    }
                    else if (!Regex.IsMatch(MaxGuests, "^[0-9]+$"))
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

                if(columnName == "Language")
                {
                    if (string.IsNullOrEmpty(Language))
                    {
                        return "enter a language.";
                    }
                    else if (!Regex.IsMatch(Language.TrimEnd(), "^[a-zA-Z]+(\\s)*$"))
                    {
                        return "must contain only letters.";
                    }

                }

                if (columnName == "Description")
                {
                    if (string.IsNullOrEmpty(Description))
                    {
                        return "write a description";
                    }
                }

                if(columnName == "StartDate")
                { 
                    
                    if (StartDate < DateTime.Today)
                    {
                        return "Please select a future date.";
                    }
                     else if (StartDate > DateTime.Today.AddYears(5))
                    {
                         return "Please select a date within the next 5 years.";
                     }
                }

                if (columnName == "EndDate")
                {

                    if (EndDate < StartDate)
                    {
                        return "Please select a future date.";
                    }
                    else if (EndDate > StartDate.AddDays(15))
                    {
                        return "Add up to 15 days to start date";
                    }

                }


                return null;



            }

        }

        private readonly string[] _validatedProperties = { "SelectedState", "SelectedCity", "MaxGuests", "Language", "Description", "StartDate", "EndDate" };

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


        private bool CanExecute_Command(object parameter)
        {
            return true;
        }
    }
}
