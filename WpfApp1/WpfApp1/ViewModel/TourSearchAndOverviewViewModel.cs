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

namespace WpfApp1.ViewModel  
{
    public class TourSearchAndOverviewViewModel : ViewModelBase
    {
        private readonly ILocationService _locationService;

        private readonly ITourService _tourService;

        public ObservableCollection<Tour> Tours { get; set; }


        public Tour SelectedTour { get; set; }

        public ObservableCollection<string> States { get; set; }

        public ObservableCollection<string> Cities { get; set; }
        public string Languages { get; set; }
        public string Duration { get; set; }
        // public string MaxGuests { get; set; }

        private string _state;

        private string _maxGuests = "0";
        public string MaxGuests
        {
            get { return _maxGuests; }
            set
            {
                _maxGuests = value;
                OnPropertyChanged(nameof(MaxGuests));
            }
        }

        public string SelectedCity { get; set; }
        public string SelectedState
        {
            get => _state;
            set
            {
                if (_state != value)
                {
                    _state = value;
                    OnPropertyChanged("SelectedState");
                }
            }
        }

        private RelayCommand searchCommand;
        public RelayCommand SearchlCommand
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


        private RelayCommand chosenStateCommand;
        public RelayCommand ChosenStateCommand
        {
            get => chosenStateCommand;
            set
            {
                if (value != chosenStateCommand)
                {
                    chosenStateCommand = value;
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
        public TourSearchAndOverviewViewModel()
        {
            _locationService = InjectorService.CreateInstance<ILocationService>();
            _tourService = InjectorService.CreateInstance<ITourService>();

            Tours = new ObservableCollection<Tour>(_tourService.GetAll());

            /*
            foreach (var state in _locationService.GetStates())
            {
                cbChoseState.Items.Add(state.ToString());
            }
            */


            Languages = "";
            Duration = "";
            MaxGuests = "0";

            SearchlCommand = new RelayCommand(Execute_Search, CanExecute_Command);
            AllToursCommand = new RelayCommand(Execute_AllTours, CanExecute_Command);
            BookedToursCommand = new RelayCommand(Execute_BookedTours, CanExecute_Command);
            LogOutCommand = new RelayCommand(Execute_LogOut, CanExecute_Command);
            IncrementCommand = new RelayCommand(Execute_Increment, CanExecute_Command); 
            DecrementCommand = new RelayCommand(Execute_Decrement, CanExecute_Command);
            //ChosenStateCommand = new RelayCommand(Execute_ChoseState, CanExecute_Command);
            ViewMoreCommand = new RelayCommand(Execute_ViewMore, CanExecute_Command);
        }
        /*
        private void Execute_ChoseState(object sender)
        {

            SelectedState = (string)cbChoseState.SelectedItem;
            cbChoseCity.Items.Clear();
            foreach (string city in _locationService.GetCitiesFromStates(SelectedState))
            {
                cbChoseCity.Items.Add(city);
            }
        }*/


        private void Execute_ViewMore(object sender)
        {
            if (sender != null && sender is Tour tour)
            {
                TourBookings tourBookingWindow = new TourBookings(tour);
                tourBookingWindow.Show();
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
            int currentValue;
            if (int.TryParse(MaxGuests, out currentValue))
            {
                MaxGuests = (currentValue + 1).ToString();
            }
        }

        private void Execute_Decrement(object sender)
        {
            int currentValue;
            if (int.TryParse(MaxGuests, out currentValue))
            {
                MaxGuests = (currentValue - 1).ToString();
            }
        }

        private void Execute_Search(object sender)
        {

            List<Tour> searchedTours = _tourService.TourSearch(SelectedState, SelectedCity, Languages, MaxGuests, Duration);
            RefreshTours(searchedTours);
        }
        private void Execute_AllTours(object sender)
        {

        }
        private void Execute_BookedTours(object sender)
        {

            BookedTours bookedTours = new BookedTours();
            bookedTours.Show();
            
        }

        private void Execute_LogOut(object sender)
        {
            MessageBox.Show("You are logging out!");
            User user = MainWindow.LogInUser;
            user.Id = -1;
            MainWindow mw = new MainWindow();
            mw.Show();

        }


       
    

}
}

