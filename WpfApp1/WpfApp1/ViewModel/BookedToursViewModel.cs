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
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace WpfApp1.ViewModel
{
    public class BookedToursViewModel : ViewModelBase
    {
        public ObservableCollection<TourEvent> TourEvents { get; set; }

        public ObservableCollection<TourPoint> TourPoints { get; set; }

        public Action CloseAction { get; set; }

        private readonly ITourEventService _tourEventService;
        private readonly ITourBookingService _tourBookingService;
        private readonly ITourPointService _tourPointService;


        private TourEvent _selectedTourEvent;

        public TourEvent SelectedTourEvent
        {
            get => _selectedTourEvent;
            set
            {
                if (_selectedTourEvent != value)
                {
                    _selectedTourEvent = value;
                    IsTourEventSelected = (_selectedTourEvent != null);
                    OnPropertyChanged("SelectedTourEvent");
                }
            }
        }


        public BookedToursViewModel() 
        {
            _tourBookingService = InjectorService.CreateInstance<ITourBookingService>();
            _tourEventService = InjectorService.CreateInstance<ITourEventService>();
            _tourPointService = InjectorService.CreateInstance<ITourPointService>();

            TourEvents = new ObservableCollection<TourEvent>(_tourBookingService.TouristTourEvents(MainWindow.LogInUser.Id));

            SelectedTourEvent = TourEvents[0];
           
            LeaveReviewCommand = new RelayCommand(Execute_LeaveReview, CanExecute_Command);
            AllToursCommand = new RelayCommand(Execute_AllTours, CanExecute_Command);
        
            MyProfileCommand = new RelayCommand(Execute_MyProfile, CanExecute_Command);
            RequestTourCommand = new RelayCommand(Execute_RequestTour, CanExecute_Command);
            RequestListCommand = new RelayCommand(Execute_RequestList, CanExecute_Command);


        }
        private RelayCommand requestListCommand;
        public RelayCommand RequestListCommand
        {
            get => requestListCommand;
            set
            {
                if (value != requestListCommand)
                {
                    requestListCommand = value;
                    OnPropertyChanged();
                }
            }
        }
        private void Execute_RequestList(object sender)
        {
            TourRequest tourRequestList = new TourRequest();
            tourRequestList.Show();
            CloseAction();


        }

     

        private void Execute_MyProfile(object sender)
        {
            TouristProfile profile = new TouristProfile();
            profile.Show();
            CloseAction();

        }
        private void Execute_RequestTour(object sender)
        {
            RequestNewTours requestNewTour = new RequestNewTours();
            requestNewTour.Show();
            CloseAction();
        }

    
        private void Execute_LeaveReview(object sender)
        {
            if (_selectedTourEvent.Status == Domain.Models.Enums.TourEventStatus.Finished)
            {
                AddRatingTourAndGuide rateWindow = new AddRatingTourAndGuide(_tourBookingService.GetTourBookingForTourEventAndUser(_selectedTourEvent.Id, MainWindow.LogInUser.Id));
                rateWindow.Show();
            }
            else
            {
                MessageBox.Show("You can only review tours with finished status");
            }

            return;
        }





        private void Execute_AllTours(object sender)
        {
            TourSearchAndOverview tourSearch = new TourSearchAndOverview();
            tourSearch.Show();
            CloseAction();
        }

        private bool CanExecute_Command(object parameter)
        {
            return true;
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
        private RelayCommand leaveReviewCommand;
        public RelayCommand LeaveReviewCommand
        {
            get => leaveReviewCommand;
            set
            {
                if (value != leaveReviewCommand)
                {
                    leaveReviewCommand = value;
                    OnPropertyChanged();
                }
            }
        }

        private RelayCommand selectCommand;
        public RelayCommand SelectCommand
        {
            get => selectCommand;
            set
            {
                if (value != selectCommand)
                {
                    selectCommand = value;
                    OnPropertyChanged();
                }
            }
        }

     

        private RelayCommand requestTourCommand;
        public RelayCommand RequestTourCommand
        {
            get => requestTourCommand;
            set
            {
                if (value != requestTourCommand)
                {
                    requestTourCommand = value;
                    OnPropertyChanged();
                }
            }
        }

        private RelayCommand myProfileCommand;
        public RelayCommand MyProfileCommand
        {
            get => myProfileCommand;
            set
            {
                if (value != myProfileCommand)
                {
                    myProfileCommand = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _isTourEventSelected;
        public bool IsTourEventSelected
        {
            get { return _isTourEventSelected; }
            set
            {
                _isTourEventSelected = value;
                OnPropertyChanged(nameof(IsTourEventSelected));
            }
        }

    }
}
