using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using WpfApp.Observer;
using WpfApp1.Commands;
using WpfApp1.Domain.Models;
using WpfApp1.Domain.ServiceInterfaces;
using WpfApp1.DTO;
using WpfApp1.Service;

namespace WpfApp1.ViewModel
{
    class RenovationOverviewViewModel : ViewModelBase, IObserver
    {
        private readonly IRenovationService _renovationService;
        private readonly IAccommodationService _accommodationService;
        public Owner LoggedOwner { get; set; }

        private Accommodation _selectedAccommodation;
        public  Accommodation SelectedAccommodation
        {
            get => _selectedAccommodation;
            set
            {
                _selectedAccommodation = value;
                 OnPropertyChanged(nameof(SelectedAccommodation));
                ChosenAccommodationCommand.RaiseCanExecuteChanged();
            }
        }

        private string _message;
        public string Message
        {
            get => _message;
            set
            {
                _message = value;
                OnPropertyChanged(nameof(Message));
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

        private bool _visibilityNotificationCancled;
        public bool VisibilityNotificationCancled
        {
            get => _visibilityNotification;
            set
            {
                _visibilityNotification = value;
                OnPropertyChanged(nameof(VisibilityNotificationCancled));
            }
        }

        private bool _visibilityChooseAccommodation;
        public bool VisibilityChooseAccommodation
        {
            get => _visibilityChooseAccommodation;
            set
            {
                _visibilityChooseAccommodation = value;
                OnPropertyChanged(nameof(VisibilityChooseAccommodation));   
            }
        }


        private ObservableCollection<Renovation> _renovations;
        public ObservableCollection<Renovation> Renovations
        {
            get => _renovations;
            set
            {
                _renovations = value;
                OnPropertyChanged(nameof(Renovations));
            }
        }

        private Renovation _selectedRenovation;
        public Renovation SelectedRenovation
        {
            get => _selectedRenovation;
            set
            {
                _selectedRenovation = value;
                OnPropertyChanged(nameof(SelectedRenovation));
                NotificationCommand.RaiseCanExecuteChanged();
            }
        }

        private int _tabPosition;
        public int TabPosition
        {
            get => _tabPosition;
            set
            {
                _tabPosition = value;
                OnPropertyChanged(nameof(TabPosition));
                VisibilityChooseAccommodation =  _tabPosition == 1 ? true : false;
            }
        }

        public RelayCommand ConfirmCommand { get; set; }
        public RelayCommand SearchCommand { get; set; }
        public RelayCommand NotificationCommand { get; set; }
        public RelayCommand ShutDownNotificationCommand { get; set; }
        public RelayCommand CancelCommand { get; set; }
        public RelayCommand ChosenAccommodationCommand { get; set; }
        public RelayCommand CloseChooseAccommodationCommand { get; set; }

        public ObservableCollection<Accommodation> Accommodations { get; set; }
        public RenovationOverviewViewModel(Owner owner)
        {
            _accommodationService = InjectorService.CreateInstance<IAccommodationService>(); 
            _renovationService = InjectorService.CreateInstance<IRenovationService>();
            _renovationService.Subscribe(this);
            Init(owner);
            InitCommand();
        }

        public void InitCommand()
        {
            ConfirmCommand = new(param => Execute_ConfirmCommand(), param => CanExecute());
            SearchCommand = new(param => Execute_SearchCommandd(), param => CanExecute());
            NotificationCommand = new(param => Execute_NotificationCommand(), param => CanExecute_NotificationCommand());
            ShutDownNotificationCommand = new(param => Execute_ShutDownNotificationCommand(), param => CanExecute());
            CancelCommand = new(param => Execute_CancelCommand(), param => CanExecute());
            ChosenAccommodationCommand = new(param => Execute_ChosenAccommodationCommand(), param => CanExecute_ChosenAccommodationCommand());
            CloseChooseAccommodationCommand = new(param => Execute_CloseChooseAccommodationCommand(), param => CanExecute());
        }

        public void Init(Owner owner)
        {
            Accommodations = new(_accommodationService.GetAll().FindAll(a => a.Owner.Id == owner.Id));
            VisibilityChooseAccommodation = false;
            LoggedOwner = owner;
            StartDate = DateTime.Now;
            EndDate = DateTime.Now;
            Renovations = new(_renovationService.GetAll().FindAll(r => r.Accommodation.Owner.Id == owner.Id));
            AvailableDays = new();
        }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Description { get; set; }
        public int Duration { get; set; }

        
        private void Execute_ConfirmCommand()
        {
            Renovation renovation = new(SelectedAccommodation, SelectedDate.StartDate, SelectedDate.EndDate, Description);
            _renovationService.Create(renovation);
            Renovations.Clear();
            foreach (var r in _renovationService.GetAllForAccommodation(SelectedAccommodation.Id))
            {
                Renovations.Add(r);
            }
        }

        private bool CanExecute()
        {
            return true;
        }

        private ObservableCollection<AvailableDate> _availableDays;
        public ObservableCollection<AvailableDate> AvailableDays
        {
            get => _availableDays;
            set
            {
                _availableDays = value;
                OnPropertyChanged(nameof(AvailableDays));
            }
        }
        public AvailableDate SelectedDate { get; set; }
        private void Execute_SearchCommandd()
        {
            AvailableDays.Clear();
            foreach (var k in _renovationService.GetAvailableDate(StartDate, EndDate, Duration, 0))
            {
                AvailableDays.Add(k);
            }
        }

        private void Execute_NotificationCommand()
        {
            if (SelectedRenovation.EndDate.Date <= DateTime.Now.Date)
            {
                Message = "Renovation is finished";
                VisibilityNotification = true;
            }
            else if (SelectedRenovation.StartDate.AddDays(5).Date < DateTime.Now.Date)
            {
                Message = "Renovation can not be cancel because deadline is passed.";
                VisibilityNotification = true;
            }
            else if (SelectedRenovation.StartDate.AddDays(5).Date < DateTime.Now.Date)
            {
                Message = "You can cancel renovation.";
                VisibilityNotificationCancled = true;
            }
        }

        private bool CanExecute_NotificationCommand()
        {
            return SelectedRenovation != null;
        }

        private void Execute_ShutDownNotificationCommand()
        {
            VisibilityNotificationCancled = false;
            VisibilityNotification = false;
        }

        private void Execute_CancelCommand()
        {
            _renovationService.Delete(SelectedRenovation);
            VisibilityNotificationCancled = false;
            VisibilityNotification = false;
        }

        public void Update()
        {
            Renovations.Clear();
            foreach(Renovation r in _renovationService.GetAll().FindAll(r => !(r.IsCanceled)))
            {
                Renovations.Add(r);
            }
        }

        private bool CanExecute_ChosenAccommodationCommand()
        {
            return SelectedAccommodation != null;
        }

        private void Execute_ChosenAccommodationCommand()
        {
            VisibilityChooseAccommodation = false;
        }

        private void Execute_CloseChooseAccommodationCommand()
        {
            TabPosition = 0;
            VisibilityChooseAccommodation = false;
        }


    }
}
