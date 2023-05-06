using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using WpfApp1.Commands;
using WpfApp1.Domain.Models;
using WpfApp1.Domain.ServiceInterfaces;
using WpfApp1.Service;
using WpfApp1.Views;


namespace WpfApp1.ViewModel
{
    public class RequestNewToursViewModel : ViewModelBase
    {
        public Action CloseAction { get; set; }

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
        public RequestNewToursViewModel() {


            MaxGuests = "";

            LogOutCommand = new RelayCommand(Execute_LogOut, CanExecute_Command);
            AllToursCommand = new RelayCommand(Execute_AllTours, CanExecute_Command);
            BookedToursCommand = new RelayCommand(Execute_BookedTours, CanExecute_Command);
            RequestSimpleTourCommand = new RelayCommand(Execute_RequestSimpleTour, CanExecute_Command);
            RequestComplexTourCommand = new RelayCommand(Execute_RequestComplexTour, CanExecute_Command);
            IncrementCommand = new RelayCommand(Execute_Increment, CanExecute_Command);
            DecrementCommand = new RelayCommand(Execute_Decrement, CanExecute_Command);
        }

        private void Execute_RequestSimpleTour(object sender)
        {
            MessageBox.Show( "    Please wait for guide's answer. "  + Environment.NewLine +
                "    View status in REQUEST LIST" + Environment.NewLine +  
                "    \t     (F9)", "Request sent ");
        }



        private void Execute_RequestComplexTour(object sender)
        {
            MessageBoxResult result = MessageBox.Show(
                "   Want to add more than one tour to the list?\n\n" +
                "           Yes - \"MORE\"    No - \"SUBMIT\"",
                "Request sent",
                MessageBoxButton.YesNo);

            if (result == MessageBoxResult.Yes)
            {
                // Handle "Yes" button clicked
            }
            else if (result == MessageBoxResult.No)
            {
                // Handle "No" button clicked
            }
        }

        private void Execute_LogOut(object sender)
        {
            MessageBox.Show("You are logging out!");
            User user = MainWindow.LogInUser;
            user.Id = -1;
            MainWindow mw = new MainWindow();
            mw.Show();
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

        private bool CanExecute_Command(object parameter)
        {
            return true;
        }
    }
}
