using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfApp1.Commands;
using WpfApp1.Domain.Models;
using WpfApp1.Domain.ServiceInterfaces;
using WpfApp1.Service;

namespace WpfApp1.ViewModel
{
    public partial class AccommodationRenovationSuggestionViewModel : ViewModelBase
    {

        // private readonly IReservationService _reservationService;

        private readonly IAccommodationRenovationSuggestionService _accommodationRenovationSuggestionService;

        private readonly Window _window;

        private AccommodationRenovationSuggestion _accommodationRenovationSuggestion;

        private Dictionary<int, string> _options = new()
        {
            { 0,"False"},
            { 1,"False" },
            { 2,"False" },
            { 3,"False" },
            { 4,"False" }

        };

        public AccommodationRenovationSuggestion AccommodationRenovationSuggestion
        {
            get => _accommodationRenovationSuggestion;
            set
            {
                _accommodationRenovationSuggestion= value;
            }
        }
        public Dictionary<int,string> Options
        {
            get => _options;
            set
            {
                _options = value;
                OnPropertyChanged();
            }
        }
        public RelayCommand SelectedOptionCommand { get; set; }
        public RelayCommand ConfirmCommand { get;  set; }
        public RelayCommand CancelCommand { get; set; }
        public RelayCommand RejectCommand { get; set; }

        public AccommodationRenovationSuggestionViewModel(Reservation reservation)
        {
            //_reservationService = InjectorService.CreateInstance<IReservationService>();
            _window = Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.Name == "AccommodationRenovationSuggestionViewName");
            _accommodationRenovationSuggestionService = InjectorService.CreateInstance<IAccommodationRenovationSuggestionService>();
            AccommodationRenovationSuggestion = new();

            SelectedOptionCommand = new RelayCommand(Execute_SelectedOption, CanSelectedOptionCommandExecute);
            ConfirmCommand = new RelayCommand(param => Execute_Confirm(), param => CanExecute());
            CancelCommand = new RelayCommand(param => Execute_Cancel(), param => CanExecute());
        }

        private string _comment;

        public string Comment
        {
            get => _comment;
            set
            {
                if(_comment != value)
                {
                    _comment = value;
                    OnPropertyChanged("Comment");
                }
            }
        }

        private void Execute_Cancel()
        {
            this.Execute_Reject();
        }

        private bool CanExecute()
        {
            return true;
        }

        private void Execute_Confirm()
        {
            int selectedIndex = 0;

            for (int i = 0; i <= 4; ++i)
            {
                if(Options[i] == "True")
                {
                    selectedIndex = i;
                    break;
                }
            }

            AccommodationRenovationSuggestion.Comment = Comment;

            AccommodationRenovationSuggestion.UrgencyLevel = selectedIndex + 1;             //+1 zato sto indksiranje u recniku krece od 0

            _accommodationRenovationSuggestionService.Create(AccommodationRenovationSuggestion);

            Execute_Reject();
            
        }

        private bool CanSelectedOptionCommandExecute(object parameter)
        {
            
            if(parameter == null || !int.TryParse(parameter.ToString(),out int index))
            {
                return false;
            }

            return index >= 0 && index <= 5;
        }

        private void Execute_SelectedOption(object parameter)
        {
            int index = int.Parse(parameter.ToString());
            for(int i=0; i<=4; ++i)
            {
                if(i == index)
                {
                    Options[i] = "True";
                }
                else
                {
                    Options[i] = "False";
                }

                OnPropertyChanged(nameof(Options));
            }
        }

        private void Execute_Reject()
        {
            _window.Close();
        }
    }
}
