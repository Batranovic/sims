using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfApp1.Commands;
using WpfApp1.Domain.Models;
using WpfApp1.Domain.ServiceInterfaces;
using WpfApp1.Service;
using WpfApp1.Views;

namespace WpfApp1.ViewModel
{
    public class CreateNewTourViewModel : ViewModelBase
    {  
        public Action CloseAction { get; set; }
        public ObservableCollection<string> States { get; set; }
        public ObservableCollection<string> Cities { get; set; }


        private readonly ILocationService _locationService;
        private readonly ITourService _tourService;
        private readonly ISimpleTourRequestService _simpleTourRequestService;
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

        public CreateNewTourViewModel()
        {
            _locationService = InjectorService.CreateInstance<ILocationService>();
            _tourService = InjectorService.CreateInstance<ITourService>();
            _simpleTourRequestService = InjectorService.CreateInstance<ISimpleTourRequestService>();
            States = new ObservableCollection<string>(_locationService.GetStates());
            Cities = new ObservableCollection<string>();

            CreateCommand = new RelayCommand(Execute_Create, CanExecute_Command);
        }

        private void Execute_Create(object sender)
        {
            Location location = _locationService.GetByCityAndState(SelectedCity, SelectedState);
            Tour newTour = new Tour(-1, "", location.Id, "", -1, Language, -1, new List<string>(),new List<DateTime>(), "");
            newTour.Location = location;
            _tourService.Create(newTour);
            MessageBox.Show("New tour has been created!");
            _simpleTourRequestService.NewTourFromStatistics(newTour);
            CloseAction();
            

        }

        private bool CanExecute_Command(object parameter)
        {
            return true;
        }

        private RelayCommand createCmmand;
        public RelayCommand CreateCommand
        {
            get => createCmmand;
            set
            {
                if (value != createCmmand)
                {
                    createCmmand = value;
                    OnPropertyChanged();
                }
            }
        }
    }
}
