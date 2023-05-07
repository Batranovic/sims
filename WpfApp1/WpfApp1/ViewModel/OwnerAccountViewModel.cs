using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfApp1.Commands;
using WpfApp1.Domain.Models;
using WpfApp1.Domain.ServiceInterfaces;
using WpfApp1.Service;
using WpfApp1.Views;

namespace WpfApp1.ViewModel
{
    public class OwnerAccountViewModel : ViewModelBase
    {
        public Owner LoggedOwner { get; set; }
        private readonly IReservationService _reservationService;

        public RelayCommand SignInAccomodationCommand { get; set; }
 
        public RelayCommand ViewExpiredReservationCommand { get; set; }
        public RelayCommand ViewOwnerRatingsCommand { get; set; }
        public RelayCommand ReservationPostponementOverviewCommand { get; set; }
        public RelayCommand LogOutCommand { get; set; }
        public OwnerAccountViewModel(User user)
        {
            _reservationService = InjectorService.CreateInstance<IReservationService>();

            SignInAccomodationCommand = ;
            ViewExpiredReservationCommand = ;
            ViewOwnerRatingsCommand = ;
            ReservationPostponementOverviewCommand = ;
            LogOutCommand = ;

            LoggedOwner = (Owner)user;

            FindNotification();
        }

        private void FindNotification()
        {
            int numberNotification = _reservationService.GetUnratedById(LoggedOwner.Id).Count;
            if (numberNotification == 0)
            {
                return;
            }
            string result = "Oslobodilo Vam se " + numberNotification.ToString() + " apartaman, ocenite goste";
            MessageBox.Show(result, "Obavestenje");
        }

        private void SignInAccomodation(object sender, RoutedEventArgs e)
        {
            SignInAccommodation signInAccommodation = new SignInAccommodation(LoggedOwner);
            signInAccommodation.Show();
        }

        private void ViewExpiredReservation(object sender, RoutedEventArgs e)
        {
            ExpiredReservation expiredReservation = new ExpiredReservation(LoggedOwner);
            expiredReservation.Show();
        }

        private void ViewOwnerRatings(object sender, RoutedEventArgs e)
        {
            OwnerRatingView ownerRatingView = new OwnerRatingView(LoggedOwner);
            ownerRatingView.Show();
        }

        private void ReservationPostponementOverview(object sender, RoutedEventArgs e)
        {
            ReservationPostponementOverview reservationPostponementOverview = new ReservationPostponementOverview(LoggedOwner);
            reservationPostponementOverview.Show();
        }

        private void LogOut(object sender, RoutedEventArgs e)
        {
            User user = MainWindow.LogInUser;
            user.Id = -1;
            MainWindow mw = new MainWindow();
            mw.Show();
       //     this.Close();
        }
    }
}
