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
                    GetDeniedRequests();
                    OnPropertyChanged("SelectedYear");
                }
            }
        }

        private int _acceptedNumber;
        public int AcceptedNumber
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


        private int _deniedNumber;
        public int DeniedNumber
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

        
        private void GetDeniedRequests()
        {
            IEnumerable<SimpleTourRequest> requests = simpleTourRequestService.GetAll();

            if (SelectedYear != "All Years" && int.TryParse(SelectedYear, out int year))
            {
                requests = requests.Where(r => r.StartDate.Year == year);
            }

            DeniedNumber = requests.Count(r => r.RequestStatus == RequestStatus.Denied);
            AcceptedNumber = requests.Count(r => r.RequestStatus == RequestStatus.Accepted);

            int acceptedCount = requests.Count(r => r.RequestStatus == RequestStatus.Accepted);
            if (acceptedCount == 0)
            {
                AverageNumber = 0;
            }
            else
            {
                AverageNumber = (int)Math.Round(requests.Where(r => r.RequestStatus == RequestStatus.Accepted).Average(r => r.MaxGuests));
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

        private Dictionary<string, int> CountRequestsByLanguage()
        {
            var languagesCount = new Dictionary<string, int>();
            var allRequests = simpleTourRequestService.GetAll();
            foreach (var request in allRequests)
            {
                if (request.Tourist.Id == MainWindow.LogInUser.Id)
                {
                    var languages = request.Languages.Split(',');
                    foreach (var language in languages)
                    {
                        if (languagesCount.ContainsKey(language))
                        {
                            languagesCount[language]++;
                        }
                        else
                        {
                            languagesCount[language] = 1;
                        }
                    }
                }
            }
            return languagesCount;
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

        private Dictionary<string, int> CountRequestsByLocation()
        {
            var locationsCount = new Dictionary<string, int>();
            var allRequests = simpleTourRequestService.GetAll();
            foreach (var request in allRequests)
            {
                if (request.Tourist.Id == MainWindow.LogInUser.Id)
                {
                    var location = request.City;
                    if (locationsCount.ContainsKey(location))
                    {
                        locationsCount[location]++;
                    }
                    else
                    {
                        locationsCount[location] = 1;
                    }
                }
            }
            return locationsCount;
        }



        public RequestStatisticsViewModel() {

            simpleTourRequestService = InjectorService.CreateInstance<ISimpleTourRequestService>();
            Years = new ObservableCollection<string>(simpleTourRequestService.GetAllYears());
            RequestsByLocation = CountRequestsByLocation();
            RequestsByLanguage = CountRequestsByLanguage();
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
