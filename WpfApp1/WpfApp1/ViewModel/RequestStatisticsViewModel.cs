using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Commands;
using WpfApp1.Domain.Models;
using WpfApp1.Domain.Models.Enums;
using WpfApp1.Domain.ServiceInterfaces;
using WpfApp1.Service;

namespace WpfApp1.ViewModel
{
    public class RequestStatisticsViewModel : ViewModelBase
    {
        private readonly ISimpleTourRequestService simpleTourRequestService;
        public Action CloseAction { get; set; }

        public ObservableCollection<string> Years { get; set; }

        private string _selectedYear;
        public string SelectedYear
        {
            get => _selectedYear;
            set
            {
                if (_selectedYear != value)
                {
                    _selectedYear = value;
                    UpdateStatistics();
                    OnPropertyChanged("SelectedYear");
                }
            }
        }
        private void UpdateStatistics()
        {
            RequestsByLocation = simpleTourRequestService.CountRequestsByLocation(MainWindow.LogInUser.Id);
            RequestsByLanguage = simpleTourRequestService.CountRequestsByLanguage(MainWindow.LogInUser.Id);
            AcceptedNumber = simpleTourRequestService.GetAcceptedRequestsCount(SelectedYear);
            DeniedNumber = simpleTourRequestService.GetDeniedRequestsCount(SelectedYear);
            AverageNumber = simpleTourRequestService.GetAverageMaxGuests(SelectedYear);
        }

        private string _acceptedNumber;
        public string AcceptedNumber
        {
            get => _acceptedNumber;
            set
            {
                if (_acceptedNumber != value)
                {
                    _acceptedNumber = value;
                    OnPropertyChanged("AcceptedNumber");
                }
            }
        }


        private string _deniedNumber;
        public string DeniedNumber
        {
            get => _deniedNumber;
            set
            {
                if (_deniedNumber != value)
                {
                    _deniedNumber = value;
                    OnPropertyChanged("DeniedNumber");
                }
            }
        }

        private int _averageNumber;
        public int AverageNumber
        {
            get => _averageNumber;
            set
            {
                if (_averageNumber != value)
                {
                    _averageNumber = value;
                    OnPropertyChanged("AverageNumber");
                }
            }
        }

      


        private Dictionary<string, int> _requestsByLanguage;
        public Dictionary<string, int> RequestsByLanguage
        {
            get => _requestsByLanguage;
            set
            {
                if (_requestsByLanguage != value)
                {
                    _requestsByLanguage = value;
                    OnPropertyChanged("RequestsByLanguage");
                }
            }
        }


        private Dictionary<string, int> _requestsByLocation;
        public Dictionary<string, int> RequestsByLocation
        {
            get => _requestsByLocation;
            set
            {
                if (_requestsByLocation != value)
                {
                    _requestsByLocation = value;
                    OnPropertyChanged("RequestsByLocation");
                }
            }
        }


        public RequestStatisticsViewModel() {

            simpleTourRequestService = InjectorService.CreateInstance<ISimpleTourRequestService>();
            Years = new ObservableCollection<string>(simpleTourRequestService.GetAllYears());
            RequestsByLocation = simpleTourRequestService.CountRequestsByLocation(MainWindow.LogInUser.Id);
            RequestsByLanguage = simpleTourRequestService.CountRequestsByLanguage(MainWindow.LogInUser.Id);
            CloseCommand = new RelayCommand(Execute_Close, CanExecute_Command);
        }


        private void Execute_Close(object sender)
        {
            CloseAction();
        }
        private bool CanExecute_Command(object parameter)
        {
            return true;
        }

        private RelayCommand closeCommand;
        public RelayCommand CloseCommand
        {
            get => closeCommand;
            set
            {
                if (value != closeCommand)
                {
                    closeCommand = value;
                    OnPropertyChanged();
                }
            }
        }
    }
}
