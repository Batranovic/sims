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
    public partial class GuestAccountViewModel : ViewModelBase
    {
        public Guest LogInGuest { get; set; }

        private readonly IReservationService _reservationService;

        private Window _window;

        public string SuperGuest { get; set; }
        public RelayCommand AccommodationViewCommand { get; set; }

        public RelayCommand ReservationViewCommand { get; set; }

        public RelayCommand PostponementsOverviewCommand { get; set; }

        public RelayCommand LogOutCommand { get; set; }

        public RelayCommand OwnerReviewsCommand { get; set; }
        
        public RelayCommand RejectCommand { get; set; }
        public GuestAccountViewModel(User user)
        {
            _window = Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.Name == "GuestAccountViewName");
            _reservationService = InjectorService.CreateInstance<IReservationService>();

            LogInGuest = (Guest)user;

            AccommodationViewCommand = new RelayCommand(param => Execute_AccommodationView(), param => CanExecute());
            ReservationViewCommand = new RelayCommand(param => Execute_ReservationView(), param => CanExecute());
            PostponementsOverviewCommand = new RelayCommand(param => Execute_PostponementsOverview(), param => CanExecute());
            LogOutCommand = new RelayCommand(param => Execute_LogOut(), param => CanExecute());
            OwnerReviewsCommand = new RelayCommand(param => Execute_OwnerReviewsCommand(),param => CanExecute());
            SuperGuest = LogInGuest.Super ? "Super guest" : "Basic guest";

        }

        private void Execute_AccommodationView()
        {
            AccommodationView accommodationView = new AccommodationView(LogInGuest);
            accommodationView.Show();
        }

        private void Execute_ReservationView()
        {
            ReservationView reservationView = new ReservationView(LogInGuest);
            reservationView.Show();
        }

        private void Execute_PostponementsOverview()
        {
            GuestPostponementsOverview guestPostponementsOverview = new GuestPostponementsOverview(LogInGuest);
            guestPostponementsOverview.Show();
        }

        private void Execute_LogOut()
        {
            User user = MainWindow.LogInUser;
            user.Id = -1;
            MainWindow mw = new MainWindow();
            mw.Show();
            this.Execute_Reject();
        }

        private void Execute_OwnerReviewsCommand()
        {
            OwnerReviews ownerReviews = new OwnerReviews(LogInGuest);
            ownerReviews.Show();
        }

        private bool CanExecute()
        {
            return true;
        }

        private void Execute_Reject()
        {
            _window.Close();
        }
    }
}

