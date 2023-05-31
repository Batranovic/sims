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
        private ForumOverviewViewModel _forumOverviewViewModel;
        private INotificationAccommodationReleaseService _notificationService;

        private string _haveNotification;
        public string HaveNotification
        {
            get => _haveNotification;
            set
            {
                _haveNotification = value;
                OnPropertyChanged(nameof(HaveNotification));
            }
        }

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
        private bool _visibilityNotification;

        public bool VisibilityNotification
        {
            get => _visibilityNotification;
            set
            {
                _visibilityNotification = value;
                OnPropertyChanged(nameof(VisibilityNotification));
            }
        }


        public RelayCommand NavCommand { get; set; }
        public RelayCommand ShowCommand { get; set; }
        public RelayCommand LogoutCommand { get; set; }
        public RelayCommand NotificationCommand { get; set; }
        public RelayCommand DeleteNotificationCommand { get; set; }
        public RelayCommand DeleteAllNotificationCommand { get; set; }
        public OwnerAccountViewModel(Owner owner)
        {
            _reservationOverviewViewModel = new ReservationOverviewViewModel(owner);
            _ownerProfileViewModel = new OwnerProfileViewModel(owner);
            _signInAccommodationViewModel = new SignInAccommodationViewModel(owner);
            CurrentViewModel = new OwnerProfileViewModel(owner);
            _forumOverviewViewModel = new ForumOverviewViewModel(owner);
            _renovationOverviewViewModel = new(owner);
            _reservationService = InjectorService.CreateInstance<IReservationService>();
            _notificationService = InjectorService.CreateInstance<INotificationAccommodationReleaseService>();

            Init(owner);
            IntiCommand();
        }

        private void IntiCommand()
        {
            NavCommand = new(Execute_NavCommand, CanExecute_NavCommand);
            ShowCommand = new(param => Execute_ShowCommand(), param => CanExecute());
            WizardCommand = new(param => Execute_WizardCommand(), param  => CanExecute());
            LogoutCommand = new(param => Execute_LogoutCommand(), param => CanExecute());
            NotificationCommand = new(param => Execute_NotificationCommand(), param => CanExecute());
            DeleteNotificationCommand = new(param => Execute_DeleteNotificationCommand(), param => CanExecute_DeleteNotificationCommand());
            DeleteAllNotificationCommand = new(param => Execute_DeleteAllNotificationCommand() , param => CanExecute());
        }
        private void Init(Owner owner)
        {
            _window  = Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.Name == "OwnerStart");
            HaveNotification = owner.Notifications.Count == 0 ? "White" :  "Green";
            VisibilityWizard = false;
            VisibilityPopUp = false;
            VisibilityNotification = false; 
            LoggedOwner = owner;
            UserType = LoggedOwner.Super ? "Super owner" : "Basic owner";
        }

       
        private void Execute_NotificationCommand()
        {
            VisibilityNotification = !VisibilityNotification;
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

            return index >= 0 && index <= 4;
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
                case 4:
                    CurrentViewModel = _forumOverviewViewModel;
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
        private NotificationBase _SelectedNotificationBase;
        public NotificationBase SelectedNotificationBase
        {
            get { return _SelectedNotificationBase; }
            set
            {
                _SelectedNotificationBase = value;
                OnPropertyChanged(nameof(SelectedNotificationBase));
                DeleteNotificationCommand.RaiseCanExecuteChanged();
            }
        }
        private bool CanExecute_DeleteNotificationCommand()
        {
            return SelectedNotificationBase != null;
        }

        private void Execute_DeleteNotificationCommand()
        {
            SelectedNotificationBase.IsDelivered = true;
            _notificationService.Update((NotificationAccommodationRelease)SelectedNotificationBase);
            LoggedOwner.Notifications.Clear();
            foreach(NotificationAccommodationRelease n in _notificationService.GetForOwner(LoggedOwner.Id))
            {
                LoggedOwner.Notifications.Add(n);
            }
            OnPropertyChanged(nameof(LoggedOwner.Notifications));
            HaveNotification = LoggedOwner.Notifications.Count == 0 ? "White" : "Green"; 
           
        }

        private void Execute_DeleteAllNotificationCommand()
        {
            foreach(var n in LoggedOwner.Notifications)
            {
                n.IsDelivered = true;
                _notificationService.Update((NotificationAccommodationRelease)n);
            }
            LoggedOwner.Notifications.Clear();
            HaveNotification = "White";
        }

    }
}
