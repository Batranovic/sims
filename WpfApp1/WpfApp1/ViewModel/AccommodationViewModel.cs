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

using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp1.ViewModel
{
    public partial class AccommodationViewModel : ViewModelBase
    {
        //readonly Duration _animationDuration = new Duration(TimeSpan.FromSeconds(0.3));
        private App app;
        private AccommodationDisplayViewModel _accommodationDisplayViewModel;
        private UnratedReservationDisplay _unratedReservationDisplay;
        private OwnerReviewsDisplayViewModel _ownerReviewsDisplayViewModel;
        private ReservationDisplayViewModel _reservationDisplayViewModel;
        private ForumGuestOverviewViewModel _forumGuestOverviewViewModel;
        private int _tabPosition;
        private bool _checkLanguage;
        private bool _checkTheme;
        private string _color1;
        private string _color2;
        private string _color3;
        private string _color4;
        private string _color5;
        private string _color6;
        private string _color7;
        private string _currentLanguage;
        private ViewModelBase _currentVM;
        private Window _window;
        public Guest LoggedGuest { get; set; }

        public RelayCommand NavCommand { get; set; }
        public RelayCommand PopUpCommand { get; set; }
        public RelayCommand ShowCommand { get; set; }

        public RelayCommand ChangeLanguageCommand { get; set; }

        public RelayCommand ColorCommand { get; set; }


        public RelayCommand LogoutCommand { get; set; }

        public string Color1
        {
            get => _color1;
            set
            {
                _color1 = value;
                OnPropertyChanged(nameof(Color1));
            }
        }
        public string Color2
        {
            get => _color2;
            set
            {
                _color2 = value;
                OnPropertyChanged(nameof(Color2));
            }
        }
        public string Color3
        {
            get => _color3;
            set
            {
                _color3 = value;
                OnPropertyChanged(nameof(Color3));
            }
        }
        public string Color4
        {
            get => _color4;
            set
            {
                _color4 = value;
                OnPropertyChanged(nameof(Color4));
            }
        }
        public string Color5
        {
            get => _color5;
            set
            {
                _color5 = value;
                OnPropertyChanged(nameof(Color5));
            }
        }
        public string Color6
        {
            get => _color6;
            set
            {
                _color6 = value;
                OnPropertyChanged(nameof(Color6));
            }
        } 
        public string Color7
        {
            get => _color7;
            set
            {
                _color7 = value;
                OnPropertyChanged(nameof(Color7));
            }
        }
        public ForumGuestOverviewViewModel ForumGuestOverviewViewModel
        {
            get => _forumGuestOverviewViewModel;
            set
            {
                if(value != _forumGuestOverviewViewModel)
                {
                    _forumGuestOverviewViewModel = value;
                    OnPropertyChanged(nameof(ForumGuestOverviewViewModel));
                }
            }
        }
        public string CurrentLanguage
        {
            get { return _currentLanguage; }
            set
            {
                _currentLanguage = value;
            }
        }
        public bool CheckLanguage
        {
            get => _checkLanguage;
            set
            {
                if(value != _checkLanguage)
                {
                    _checkLanguage = value;
                    OnPropertyChanged(nameof(CheckLanguage));
                }
            }
        }

        public bool CheckTheme
        {
            get => _checkTheme;
            set
            {
                if(value != _checkTheme)
                {
                    _checkTheme = value;
                    OnPropertyChanged(nameof(CheckTheme));
                }
            }
        }
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
            _window = Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.Name == "AccommodationViewName");
            AccommodationDisplayViewModel = new AccommodationDisplayViewModel(user);
            CurrentVM = _accommodationDisplayViewModel;
            UnratedReservationDisplay = new UnratedReservationDisplay((Guest)user);
            OwnerReviewsDisplayViewModel = new OwnerReviewsDisplayViewModel((Guest)user);
            ReservationDisplayViewModel = new ReservationDisplayViewModel((Guest)user);
            ForumGuestOverviewViewModel = new ForumGuestOverviewViewModel((Guest)user);
            this.app = System.Windows.Application.Current as App;
            this.app = (App)System.Windows.Application.Current;
            this.CurrentLanguage = "en-US";

            ShowCommand = new(param => Execute_ShowCommand(), param => CanExecute());
            PopUpCommand = new(param => Execute_PopUpCommand(), param => CanExecute());
            LogoutCommand = new(param => Execute_LogoutCommand(), param => CanExecute());
            ChangeLanguageCommand = new(param => Execute_ChangeLanguageCommand(), param => CanExecute());
            ColorCommand = new(param => Execute_ColorCommand(), param => CanExecute());

            VisibilityPopUp = false;
            CheckTheme = false;
            CheckLanguage = false;
            LoggedGuest = (Guest)user;
            UserType = LoggedGuest.Super ? "Super guest" : "Basic guest";


            Color1 = "#85A392";
            Color2 = "#FFECC7";
            Color3 = "#FDD998";
            Color4 = "#F5F0BB";
            Color5 = "#C4DFAA";
            Color6 = "#F6EABE";
            Color7 = "#FAEDCD";


        }

        public void Execute_PopUpCommand()
        {
            VisibilityPopUp = !VisibilityPopUp;
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

        private void Execute_ChangeLanguageCommand()
        {
            CheckLanguage = !CheckLanguage;
            CurrentLanguage = CheckLanguage ? "sr-LATN" : "en-US";
            app.ChangeLanguage(CurrentLanguage);
        }

        public void Execute_LogoutCommand()
        {
            MainWindow mw = new();
            mw.Show();
            MainWindow.LogInUser = null;
            _window.Close();
        }

        public void Execute_ColorCommand()
        {
           // Color1 = "#C4DFDF";
           // Color2 = "#D2E9E9";
            if(Color1.Equals("#C4DFDF"))
            {
                Color1 = "#85A392";
                Color2 = "#FFECC7";
                Color3 = "#FDD998";
                Color4 = "#F5F0BB";
                Color5 = "#C4DFAA";
                Color6 = "#F6EABE";
                Color7 = "#FAEDCD";
            }
            else
            {
                Color1 = "#C4DFDF";
                Color2 = "#76BA99";
                Color3 = "#ADCF9F";
                Color4 = "#CED89E";
                Color5 = "#FFDCAE";
                Color6 = "#FDEFEF";
                Color7 = "#DAD0C2";
            }

        }

    }
}

