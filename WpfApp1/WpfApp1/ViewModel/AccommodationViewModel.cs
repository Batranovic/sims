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
        private int _tabPosition;
        private ViewModelBase _currentVM;

        public RelayCommand NavCommand { get; set; }

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

        public AccommodationViewModel(User user)
        {
            AccommodationDisplayViewModel = new AccommodationDisplayViewModel(user);
            CurrentVM = _accommodationDisplayViewModel;
            UnratedReservationDisplay = new UnratedReservationDisplay((Guest)user);

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
                    break;
                case 1:
                    CurrentVM = _unratedReservationDisplay;
                    break;
                case 2:
                    CurrentVM = _unratedReservationDisplay;
                    break;
                case 3:
                    CurrentVM = _unratedReservationDisplay;
                    break;
            }
        }

    }
}

