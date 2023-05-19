using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WpfApp1.Commands;
using WpfApp1.Domain.Models;
using WpfApp1.Domain.ServiceInterfaces;
using WpfApp1.Service;
using WpfApp1.Views;

namespace WpfApp1.ViewModel
{
    public class TouristProfileViewModel : ViewModelBase
    {
        public ObservableCollection<Voucher> Vouchers { get; set; }
        private readonly IVoucherService _voucherService;

        public Action CloseAction { get; set; }

        private Voucher _selectedVoucher;
        public Voucher SelectedVoucher
        {
            get => _selectedVoucher;
            set
            {
                if (_selectedVoucher != value)
                {
                    _selectedVoucher = value;
                    OnPropertyChanged("SelectedVoucher");
                }
            }
        }
        public TouristProfileViewModel() {
            _voucherService = InjectorService.CreateInstance<IVoucherService>();

            Vouchers = new ObservableCollection<Voucher>(_voucherService.VoucherForTourist(MainWindow.LogInUser.Id));

            AllToursCommand = new RelayCommand(Execute_AllTours, CanExecute_Command);
            BookedToursCommand = new RelayCommand(Execute_BookedTours, CanExecute_Command);
            RequestTourCommand = new RelayCommand(Execute_RequestTour, CanExecute_Command);
            RequestListCommand = new RelayCommand(Execute_RequestList, CanExecute_Command);
            LogOutCommand = new RelayCommand(Execute_LogOut, CanExecute_Command);
        }


        private RelayCommand logOutCommand;
        public RelayCommand LogOutCommand
        {
            get => logOutCommand;
            set
            {
                if (value != logOutCommand)
                {
                    logOutCommand = value;
                    OnPropertyChanged();
                }
            }
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

        private void Execute_LogOut(object sender)
        {
            MessageBoxResult result = MessageBox.Show(
                  "   Are you sure you want to log out?\n\n" ,
                  "Exit",
                  MessageBoxButton.YesNo);

            if (result == MessageBoxResult.Yes)
            {
                MainWindow mw = new MainWindow();
                mw.Show();
                CloseAction();
            }
            else if (result == MessageBoxResult.No)
            {

                TouristProfile parentWindow = Application.Current.Windows.OfType<TouristProfile>().FirstOrDefault(window => window.Name == "TouristProfile");
                if (parentWindow != null)
                {
                    parentWindow.Close();
                }

            }
        }
        private void Execute_RequestTour(object sender)
        {
            RequestNewTours requestNewTour = new RequestNewTours();
            requestNewTour.Show();
            CloseAction();
        }
        private void Execute_RequestList(object sender)
        {
            TourRequest tourRequestList = new TourRequest();
            tourRequestList.Show();
            CloseAction();


        }

        private bool CanExecute_Command(object parameter)
        {
            return true;
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
    }
}
