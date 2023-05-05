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

namespace WpfApp1.ViewModel
{
    public class BookedToursViewModel : ViewModelBase
    {
        public ObservableCollection<TourEvent> TourEvents { get; set; }

        public ObservableCollection<TourPoint> TourPoints { get; set; }


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
                    OnPropertyChanged("SelectedTourEvent");
                }
            }
        }
        private bool _isPopupOpen;
        public bool IsPopupOpen
        {
            get { return _isPopupOpen; }
            set { _isPopupOpen = value; OnPropertyChanged(nameof(IsPopupOpen)); }
        }


        public BookedToursViewModel() 
        {
            _tourBookingService = InjectorService.CreateInstance<ITourBookingService>();
            _tourEventService = InjectorService.CreateInstance<ITourEventService>();
            _tourPointService = InjectorService.CreateInstance<ITourPointService>();

            TourEvents = new ObservableCollection<TourEvent>(_tourBookingService.TouristTourEvents(MainWindow.LogInUser.Id));
           
            LeaveReviewCommand = new RelayCommand(Execute_LeaveReview, CanExecute_Command);
            AllToursCommand = new RelayCommand(Execute_AllTours, CanExecute_Command);
            ShowPopUpCommand = new RelayCommand(Execute_ShowPopUp, CanExecute_Command);

        }

        private void Execute_ShowPopUp(object sender)
        {
            IsPopupOpen = true;
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

        private RelayCommand showPopUpCommand;
        public RelayCommand ShowPopUpCommand
        {
            get => showPopUpCommand;
            set
            {
                if (value != showPopUpCommand)
                {
                    showPopUpCommand = value;
                    OnPropertyChanged();
                }
            }
        }


    }
}
