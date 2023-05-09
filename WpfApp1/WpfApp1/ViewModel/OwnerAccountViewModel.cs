using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;
using WpfApp1.Commands;
using WpfApp1.Domain.Models;
using WpfApp1.Domain.ServiceInterfaces;
using WpfApp1.Service;
using WpfApp1.Views;

namespace WpfApp1.ViewModel
{
    public class OwnerAccountViewModel : ViewModelBase
    {
        private ViewModelBase _currentViewModel;
        private OwnerProfileViewModel _ownerProfileViewModel;
        private SignInAccommodationViewModel _signInAccommodationViewModel;
        private ReservationOverviewViewModel _reservationOverviewViewModel;
        //    private AccommodationRenovationViewModel _accommodationRenovationViewModel;
        //    private RenovationHistoryViewModel _renovationHistoryViewModel;

        private bool _visibilityWizard;
        public bool VisibilityWizard
        {
            get => _visibilityWizard;
            set
            {
                _visibilityWizard = value;
                OnPropertyChanged(nameof(Visibility));
            }
        }
        public string WizardText { get; set; }
        public RelayCommand WizardCommand { get; set; }

        private string _visibility;
        public string Visibility
        {
            get => _visibility;
            set
            {
                _visibility = value;
                OnPropertyChanged(nameof(Visibility));
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
        public string UserType { get; set; }
        public Owner LoggedOwner { get; set; }
        public ViewModelBase CurrentViewModel
        {
            get => _currentViewModel;
            set
            {
                if (_currentViewModel != value)
                {
                    _currentViewModel = value;
                    OnPropertyChanged(nameof(CurrentViewModel));
                }
            }
        }
        public RelayCommand NavCommand { get; set; }
        public RelayCommand ShowCommand { get; set; }

        public OwnerAccountViewModel(Owner owner)
        {
            _reservationOverviewViewModel = new ReservationOverviewViewModel(owner);
            _ownerProfileViewModel = new OwnerProfileViewModel(owner);
            _signInAccommodationViewModel = new SignInAccommodationViewModel(owner);
            CurrentViewModel = new OwnerProfileViewModel(owner);

            Init(owner);
            IntiCommand();
        }

        private void IntiCommand()
        {
            NavCommand = new RelayCommand(Execute_NavCommand, CanExecute_NavCommand);
            ShowCommand = new(param => Execute_ShowCommand(), param => CanExecute());
            WizardCommand = new(param => Execute_WizardCommand(), param  => CanExecute());
        }
        private void Init(Owner owner)
        {
            VisibilityWizard = false;
            VisibilityPopUp = false;
            Visibility = "Visible";
            LoggedOwner = owner;
            UserType = LoggedOwner.Super ? "Super" : "Basic";
            WizardText = "Hello dear user,\nYou have a big section(buttons on top of\n window), when you select one section then\nnew window open up. In new window you\nhave a  vertical tabs with more features.";
        }

        public void Execute_WizardCommand()
        {
            VisibilityWizard = !VisibilityWizard;
        }
        public bool CanExecute_NavCommand(object parameter)
        {
            if (parameter == null || !int.TryParse(parameter.ToString(), out int index))
            {
                return false;
            }

            return index >= 0 && index <= 2;
        }

        public void Execute_NavCommand(object parameter)
        {
            int index = int.Parse(parameter.ToString());
            switch (index)
            {
                case 0:
                    CurrentViewModel = _ownerProfileViewModel;
                    Execute_ShowCommand();
                    break;
                case 1:
                    CurrentViewModel = _signInAccommodationViewModel;
                    break;
                case 2:
                    CurrentViewModel = _reservationOverviewViewModel;
                    break;
            }
        }

        public void Execute_ShowCommand()
        {
            VisibilityPopUp = !VisibilityPopUp;
        }

        public bool CanExecute()
        {
            return true;
        }
    }
}
