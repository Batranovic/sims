using System;
using System.Windows;
using WpfApp1.Commands;
using WpfApp1.Domain.Models;
using WpfApp1.Views;
using WpfApp1.Domain.ServiceInterfaces;
using WpfApp1.Service;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace WpfApp1.ViewModel
{
    public class TourRequestViewModel : ViewModelBase
    {
        public ObservableCollection<SimpleTourRequest> SimpleTourRequests { get; set; }
        public ObservableCollection<SimpleTourRequest> PartsOfComplexTourRequests { get; set; }
        public ObservableCollection<ComplexTourRequest> ComplexTourRequests { get; set; }
        public ObservableCollection<SimpleTourRequest> AcceptedRequests { get; set; }

        private readonly ISimpleTourRequestService _simpleTourRequestSrvice;
        private readonly IComplexTourRequestService _complexTourRequestService;
        private readonly IRequestNotifactionService _requestNotifactionSrvice;

      
        public Action CloseAction { get; set; }



        public TourRequestViewModel()
        {
            _requestNotifactionSrvice = InjectorService.CreateInstance<IRequestNotifactionService>();
            _simpleTourRequestSrvice = InjectorService.CreateInstance<ISimpleTourRequestService>();
            _complexTourRequestService = InjectorService.CreateInstance<IComplexTourRequestService>();
        

            SimpleTourRequests = new ObservableCollection<SimpleTourRequest>(_simpleTourRequestSrvice.RequestsForTourist(MainWindow.LogInUser.Id));
            PartsOfComplexTourRequests = new ObservableCollection<SimpleTourRequest>(_complexTourRequestService.DeniedSimpleTourRequests(MainWindow.LogInUser.Id));
            AcceptedRequests = new ObservableCollection<SimpleTourRequest>(_simpleTourRequestSrvice.AcceptedRequestsForTourist(MainWindow.LogInUser.Id));


            AllToursCommand = new RelayCommand(Execute_AllTours, CanExecute_Command);
            BookedToursCommand = new RelayCommand(Execute_BookedTours, CanExecute_Command);
            MyProfileCommand = new RelayCommand(Execute_MyProfile, CanExecute_Command);
            RequestTourCommand = new RelayCommand(Execute_RequestTour, CanExecute_Command);
            StatisticsCommand = new RelayCommand(Execute_Statistics, CanExecute_Command);
            RefreshToursCommand = new RelayCommand(Execute_Refresh, CanExecute_Command);
            CreateNewTourCommand = new RelayCommand(Execute_CreateNewTour, CanExecute_Command);

            ShowNotifications();
        }

        public void ShowNotifications()
        {
            List<RequestNotification> notifications = _requestNotifactionSrvice.GetNotificationForUser(MainWindow.LogInUser.Id);
            foreach (RequestNotification notification in notifications)
            {
                string status = notification.RequestStatus.ToString();
                string city = notification.SimpleTourRequest.Location.City;
                MessageBoxResult result = MessageBox.Show("Your request for " + city + "has been " + status);
            }
        }
    
        public void Execute_CreateNewTour(object sender)
        {
            CreateNewTour create = new CreateNewTour();
            create.Show();
        }

        private RelayCommand createNew;
        public RelayCommand CreateNewTourCommand
        {
            get => createNew;
            set
            {
                if (value != createNew)
                {
                    createNew = value;
                    OnPropertyChanged();
                }
            }
        }

        private RelayCommand refreshCommand;
        public RelayCommand RefreshToursCommand
        {
            get => refreshCommand;
            set
            {
                if (value != refreshCommand)
                {
                    refreshCommand = value;
                    OnPropertyChanged();
                }
            }
        }

        private RelayCommand statisticsCommand;
        public RelayCommand StatisticsCommand
        {
            get => statisticsCommand;
            set
            {
                if (value != statisticsCommand)
                {
                    statisticsCommand = value;
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
        private void Execute_Refresh(object sender)
        {
            AcceptedRequests.Clear();
            SimpleTourRequests.Clear();

            var simpleRequests = _simpleTourRequestSrvice.RequestsForTourist(MainWindow.LogInUser.Id);
            foreach (var request in simpleRequests)
            {
                SimpleTourRequests.Add(request);
            }

            var acceptedRequests = _simpleTourRequestSrvice.AcceptedRequestsForTourist(MainWindow.LogInUser.Id);
            foreach (var request in acceptedRequests)
            {
                AcceptedRequests.Add(request);
            }
        }
        private void Execute_Statistics(object sender)
        {
            Execute_Refresh(sender);
            RequestStatistics request = new RequestStatistics();
            request.Show();
        }
        private void Execute_RequestTour(object sender)
        {
            RequestNewTours requestNewTour = new RequestNewTours();
            requestNewTour.Show();
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

        private void Execute_MyProfile(object sender)
        {
            TouristProfile profile = new TouristProfile();
            profile.Show();
            CloseAction();

        }


        private bool CanExecute_Command(object parameter)
        {
            return true;
        }

    }
}
