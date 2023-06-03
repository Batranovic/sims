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
using System.Diagnostics.Eventing.Reader;
using System.Drawing;

namespace WpfApp1.ViewModel
{
    public class RequestNewToursViewModel : ViewModelBase, IDataErrorInfo
    {
        private readonly ILocationService _locationService;
        private readonly ISimpleTourRequestService _simpleTourRequestService;
        private readonly IComplexTourRequestService _complexTourRequestService;

        public SimpleTourRequest SelectedTourRequest { get; set; }
        public ObservableCollection<string> States { get; set; }

        public ObservableCollection<string> Cities { get; set; }

        public ObservableCollection<SimpleTourRequest> PartOfRequest { get; set; }
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

        private int id;
        public int ComplexTourRequestId
        {
            get => id;
            set
            {
                if (value != id)
                {
                    id = value;
                    OnPropertyChanged("ComplexTourRequestId");
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
        public DateTime SelectedStartDate
        {
            get => _selectedStartDate;
            set
            {
                if(value != _selectedStartDate)
                {
                    _selectedStartDate = value;
                    OnPropertyChanged("SelectedStartDate");
                }
               
            }
        }

        private DateTime _selectedEndDate;
        public DateTime SelectedEndDate
        {
            get => _selectedEndDate;
            set
            {
                if (value != _selectedEndDate)
                {
                    _selectedEndDate = value;
                    OnPropertyChanged("SelectedEndDate");
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
            _complexTourRequestService = InjectorService.CreateInstance<IComplexTourRequestService>();
            _locationService = InjectorService.CreateInstance<ILocationService>();
            States = new ObservableCollection<string>(_locationService.GetStates());
            Cities = new ObservableCollection<string>();
            PartOfRequest = new ObservableCollection<SimpleTourRequest>();

            MaxGuests = "1";
            SelectedStartDate = DateTime.Today;
            SelectedEndDate = DateTime.Today;
            ComplexTourRequestId = _complexTourRequestService.NextId();

            MyProfileCommand = new RelayCommand(Execute_MyProfile, CanExecute_Command);
            AllToursCommand = new RelayCommand(Execute_AllTours, CanExecute_Command);
            BookedToursCommand = new RelayCommand(Execute_BookedTours, CanExecute_Command);
            RequestSimpleTourCommand = new RelayCommand(Execute_AddTour, CanExecute_Command);
            RequestComplexTourCommand = new RelayCommand(Execute_Finish, CanExecute_Command);
            IncrementCommand = new RelayCommand(Execute_Increment, CanExecute_Command);
            DecrementCommand = new RelayCommand(Execute_Decrement, CanExecute_Command);
            RequestListCommand = new RelayCommand(Execute_RequestList, CanExecute_Command);
            RemoveTourCommand = new RelayCommand(Execute_Remove,CanExecute_Command);
        }


         private void Execute_Remove(object sender)
        {

            if (SelectedTourRequest == null) return;
            PartOfRequest.Remove(SelectedTourRequest);
            _simpleTourRequestService.Delete(SelectedTourRequest);

            SelectedTourRequest = null;
        }

        private void Execute_AddTour(object sender)
        {
            IsSubmitClicked = true;
            if (IsValid)
            {
                int complexTourRequestId = -1;
                int maxG = int.Parse(MaxGuests);
                ComplexTourRequest complex = new ComplexTourRequest();
                Tourist tourist = (Tourist)MainWindow.LogInUser;
                Location location = _locationService.GetByCityAndState(SelectedCity, SelectedState);
                SimpleTourRequest simpleTour = new SimpleTourRequest(-1, location, Description, Language, maxG, SelectedStartDate, SelectedEndDate, tourist, RequestStatus.Pending, complex);
                PartOfRequest.Add(simpleTour);
                _simpleTourRequestService.Create(simpleTour);

                IsSubmitClicked = false;

                complexTourRequestId = ComplexTourRequestId; // Reset complexTourRequestId

                if (PartOfRequest.Count > 1)
                {
                    // Update ComplexTourRequestId for previously added SimpleTourRequests
                    foreach (SimpleTourRequest tour in PartOfRequest)
                    {
                        tour.ComplexTourRequestId.Id = complexTourRequestId;
                        _simpleTourRequestService.Update(tour);
                    }
                }
            }
          
        }


        private void Execute_Finish(object sender)
        {
            if (IsValid)
            {
                MessageBoxResult result = MessageBox.Show(
                    "   Are you finished?",
                    "Request will be sent",
                    MessageBoxButton.YesNo);
                if (result == MessageBoxResult.No)
                {
                    // Continue adding more tours
                }
                else if (result == MessageBoxResult.Yes)
                {
                    Tourist tourist = (Tourist)MainWindow.LogInUser;

                    if (PartOfRequest.Count > 1)
                    {
                        List<SimpleTourRequest> newSimpleTourRequest = new List<SimpleTourRequest>(PartOfRequest);
                        ComplexTourRequest complexTour = new ComplexTourRequest(-1, tourist, RequestStatus.Pending, newSimpleTourRequest);
                        _complexTourRequestService.Create(complexTour);
                        IsSubmitClicked = false;

                        SelectedState = "";
                        SelectedCity = "";
                        MaxGuests = "0";
                        Language = "";
                        Description = "";
                        SelectedStartDate = SelectedEndDate;
                        SelectedEndDate = DateTime.Now;
                        ComplexTourRequestId = _complexTourRequestService.NextId();
                        PartOfRequest.Clear();
                        
                    }
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


        private RelayCommand remove;
        public RelayCommand RemoveTourCommand
        {
            get => remove;
            set
            {
                if (value != remove)
                {
                    remove = value;
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
                if (IsSubmitClicked)
                {
                    if (columnName == "SelectedState")
                    {
                        if (string.IsNullOrEmpty(SelectedState))
                        {
                            return "Please choose a state.";
                        }
                    }

                    if (columnName == "SelectedCity")
                    {
                        if (string.IsNullOrEmpty(SelectedCity))
                        {
                            return "Please choose a location.";
                        }
                    }

                    if (columnName == "MaxGuests")
                    {
                        if (string.IsNullOrEmpty(MaxGuests) || MaxGuests == "0")
                        {
                            return "Please choose the number of people.";
                        }
                        else if (!int.TryParse(MaxGuests, out int maxGuests) || maxGuests <= 0)
                        {
                            return "Please enter a positive integer.";
                        }
                    }

                    if (columnName == "Language")
                    {
                        if (string.IsNullOrEmpty(Language))
                        {
                            return "Please enter a language.";
                        }
                        else if (!Regex.IsMatch(Language.TrimEnd(), "^[a-zA-Z]+(\\s)*$"))
                        {
                            return "Language must contain only letters.";
                        }
                    }

                    if (columnName == "Description")
                    {
                        if (string.IsNullOrEmpty(Description))
                        {
                            return "Please write a description.";
                        }
                    }

                    if (columnName == "SelectedStartDate")
                    {
                        if (SelectedStartDate <= DateTime.Today)
                        {
                            return "Please select a future date.";
                        }
                        else if (SelectedStartDate > DateTime.Today.AddYears(5))
                        {
                            return "Please select a date within the next 5 years.";
                        }
                    }

                    if (columnName == "SelectedEndDate")
                    {
                        if (SelectedEndDate <= SelectedStartDate)
                        {
                            return "Please select a future date.";
                        }
                        else if (SelectedEndDate > SelectedStartDate.AddDays(15))
                        {
                            return "Please select an end date within 15 days from the start date.";
                        }
                    }
                }

                return null;
            }
        }


        private bool _isSubmitClicked = false;

        public bool IsSubmitClicked
        {
            get { return _isSubmitClicked; }
            set
            {
                if (_isSubmitClicked != value)
                {
                    _isSubmitClicked = value;
                    OnPropertyChanged("IsSubmitClicked");
                    OnPropertyChanged("SelectedState");
                    OnPropertyChanged("SelectedCity");
                    OnPropertyChanged("MaxGuests");
                    OnPropertyChanged("Language");
                    OnPropertyChanged("Description");
                    OnPropertyChanged("SelectedStartDate");
                    OnPropertyChanged("SelectedEndDate");
                }
            }
        }


        private readonly string[] _validatedProperties = { "SelectedState", "SelectedCity", "MaxGuests", "Language", "Description", "SelectedStartDate", "SelectedEndDate" };

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
