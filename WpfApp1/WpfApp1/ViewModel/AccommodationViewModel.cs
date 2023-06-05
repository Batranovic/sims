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
        private AccommodationDisplayViewModel _accommodationDisplayViewModel;
        private UnratedReservationDisplay _unratedReservationDisplay;
        private OwnerReviewsDisplayViewModel _ownerReviewsDisplayViewModel;
        private ReservationDisplayViewModel _reservationDisplayViewModel;
        private int _tabPosition;
        private ViewModelBase _currentVM;

        public RelayCommand NavCommand { get; set; }
        public RelayCommand PopUpCommand { get; set; }
        public RelayCommand ShowCommand { get; set; }

        public Guest LoggedGuest { get; set; }

        public ViewModelBase CurrentVM
        {
            get => _currentVM;
            set
            {
                if (value != _currentVM)
                {
                    _currentVM = value;
                    OnPropertyChanged(nameof(CurrentVM));
                }
            }
        }

        public int TabPosition
        {
            get => _tabPosition;
            set
            {
                if (value != null)
                {
                    _tabPosition = value;
                    OnPropertyChanged(nameof(TabPosition));
                    //Execute_NavCommand();
                }
            }
        }

        private string _nameSurname;
        public string NameSurname
        {
            get => _nameSurname;
            set
            {
                _nameSurname = value;
                OnPropertyChanged(nameof(NameSurname));
            }
        }

        private bool _visibilityPopUp;
        public bool VisibilityPopUp
        {
            get => _visibilityPopUp;
            set
            {
                _visibilityPopUp = value;
                OnPropertyChanged(nameof(VisibilityPopUp));
            }
        }

        public ReservationDisplayViewModel ReservationDisplayViewModel
        {
            get => _reservationDisplayViewModel;
            set
            {
                if(value != _reservationDisplayViewModel)
                {
                    _reservationDisplayViewModel = value;
                    OnPropertyChanged(nameof(ReservationDisplayViewModel));
                }
            }
        }
        public UnratedReservationDisplay UnratedReservationDisplay
        {
            get => _unratedReservationDisplay;
            set
            {
                if(value != _unratedReservationDisplay)
                {
                    _unratedReservationDisplay = value;
                    OnPropertyChanged(nameof(UnratedReservationDisplay));
                }
            }

        }

        public AccommodationDisplayViewModel AccommodationDisplayViewModel
        {
            get => _accommodationDisplayViewModel;
            set
            {
                if(value != _accommodationDisplayViewModel)
                {
                    _accommodationDisplayViewModel = value;
                    OnPropertyChanged(nameof(AccommodationDisplayViewModel));
                }
            }
        }
        public string UserType { get; set; }
        public OwnerReviewsDisplayViewModel OwnerReviewsDisplayViewModel
        {
            get => _ownerReviewsDisplayViewModel;
            set
            {
                if(value != _ownerReviewsDisplayViewModel)
                {
                    _ownerReviewsDisplayViewModel = value;
                    OnPropertyChanged(nameof(OwnerReviewsDisplayViewModel));
                }
            }
        }

        public AccommodationViewModel(User user)
        {
            AccommodationDisplayViewModel = new AccommodationDisplayViewModel(user);
            CurrentVM = _accommodationDisplayViewModel;
            UnratedReservationDisplay = new UnratedReservationDisplay((Guest)user);
            OwnerReviewsDisplayViewModel = new OwnerReviewsDisplayViewModel((Guest)user);
            ReservationDisplayViewModel = new ReservationDisplayViewModel((Guest)user);

            ShowCommand = new(param => Execute_ShowCommand(), param => CanExecute());

            VisibilityPopUp = false;
            LoggedGuest = (Guest)user;
            UserType = LoggedGuest.Super ? "Super owner" : "Basic owner";


        }

        public bool CanExecute_NavCommand(object parameter)
        {

            return TabPosition >= 0 && TabPosition <= 3;
        }

        public void Execute_NavCommand()
        {
            switch (TabPosition)
            {
                case 0:
                    CurrentVM = _accommodationDisplayViewModel;
                    Execute_ShowCommand();
                    break;
                case 1:
                    CurrentVM = _unratedReservationDisplay;
                    break;
                case 2:
                    CurrentVM = _ownerReviewsDisplayViewModel;
                    break;
                case 3:
                    CurrentVM = _reservationDisplayViewModel;
                    break;
            }
        }

        public void Execute_ShowCommand()
        {
            NameSurname = LoggedGuest.Name + " " + LoggedGuest.Surname;
            VisibilityPopUp = !VisibilityPopUp;
        }

        public bool CanExecute()
        {
            return true;
        }

       
    }
}

