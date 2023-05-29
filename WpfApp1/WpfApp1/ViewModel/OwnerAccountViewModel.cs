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
using WpfApp.Observer;
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
        private readonly IReservationService _reservationService;
        private OwnerProfileViewModel _ownerProfileViewModel;
        private SignInAccommodationViewModel _signInAccommodationViewModel;
        private ReservationOverviewViewModel _reservationOverviewViewModel;
        private RenovationOverviewViewModel _renovationOverviewViewModel;

        private Window _window;
        //    private AccommodationRenovationViewModel _accommodationRenovationViewModel;
        //    private RenovationHistoryViewModel _renovationHistoryViewModel;

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

        private bool _visibilityWizard;
        public bool VisibilityWizard
        {
            get => _visibilityWizard;
            set
            {
                _visibilityWizard = value;
                OnPropertyChanged(nameof(VisibilityWizard));
            }
        }
        public RelayCommand WizardCommand { get; set; }
      
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
        public RelayCommand LogoutCommand { get; set; }

        public OwnerAccountViewModel(Owner owner)
        {
            _reservationOverviewViewModel = new ReservationOverviewViewModel(owner);
            _ownerProfileViewModel = new OwnerProfileViewModel(owner);
            _signInAccommodationViewModel = new SignInAccommodationViewModel(owner);
            CurrentViewModel = new OwnerProfileViewModel(owner);
            _renovationOverviewViewModel = new(owner);
            _reservationService = InjectorService.CreateInstance<IReservationService>();

            Init(owner);
            IntiCommand();
        }

        private void FindNotification()
        {
            int numberNotification = _reservationService.GetUnratedById(LoggedOwner.Id).Count;
            if (numberNotification == 0)
            {
              //  btnRatingGuest.IsEnabled = false;
                return;
            }
            string result = "Oslobodilo Vam se " + numberNotification.ToString() + " apartaman, ocenite goste";
            MessageBox.Show(result, "Obavestenje");
          //  btnRatingGuest.IsEnabled = true;
        }
        private void IntiCommand()
        {
            NavCommand = new RelayCommand(Execute_NavCommand, CanExecute_NavCommand);
            ShowCommand = new(param => Execute_ShowCommand(), param => CanExecute());
            WizardCommand = new(param => Execute_WizardCommand(), param  => CanExecute());
            LogoutCommand = new(param => Execute_LogoutCommand(), param => CanExecute());
        }
        private void Init(Owner owner)
        {
            _window = _window = Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.Name == "OwnerStart");
            VisibilityWizard = false;
            VisibilityPopUp = false;
            LoggedOwner = owner;
            UserType = LoggedOwner.Super ? "Super owner" : "Basic owner";
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

            return index >= 0 && index <= 3;
        }

        public void Execute_LogoutCommand()
        {
            MainWindow mw = new();
            mw.Show();
            MainWindow.LogInUser = null;
            _window.Close();
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
                case 3:
                    CurrentViewModel = _renovationOverviewViewModel;
                    break;
            }
        }

        public void Execute_ShowCommand()
        {
            NameSurname = LoggedOwner.Name + " " + LoggedOwner.Surname;
            VisibilityPopUp = !VisibilityPopUp;
        }

        public bool CanExecute()
        {
            return true;
        }

      
    }
}
