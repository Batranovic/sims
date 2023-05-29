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

namespace WpfApp1.ViewModel
{
    public partial class AccommodationViewModel : ViewModelBase
    {
        private AccommodationDisplayViewModel _accommodationDisplayViewModel;
        private ViewModelBase _currentVM;

        public ViewModelBase CurrentVM
        {
            get => _currentVM;
            set
            {
                if(value != _currentVM)
                {
                    _currentVM = value;
                    OnPropertyChanged(nameof(CurrentVM));
                }
            }
        }

        public AccommodationViewModel(User user)
        {
            _accommodationDisplayViewModel = new AccommodationDisplayViewModel(user);
            CurrentVM = _accommodationDisplayViewModel;
        }
    }
}

