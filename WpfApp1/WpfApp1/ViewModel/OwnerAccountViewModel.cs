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
        private Window _window;
        public RelayCommand SignInAccomodationCommand { get; set; }
        public RelayCommand ViewExpiredReservationCommand { get; set; }
        public RelayCommand ViewOwnerRatingsCommand { get; set; }
        public RelayCommand ReservationPostponementOverviewCommand { get; set; }
        public RelayCommand LogOutCommand { get; set; }
        public OwnerAccountViewModel(User user)
        {
            _reservationService = InjectorService.CreateInstance<IReservationService>();
              _window = Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.Name == "OwnerStart");

            InitCommand();
            LoggedOwner = (Owner)user;

            FindNotification();
        }

        private void InitCommand()
        {
            SignInAccomodationCommand = new RelayCommand(param => Execute_SignInAccomodation(), param => CanExecute_Command());
            ViewExpiredReservationCommand = new RelayCommand(param => Execute_ViewExpiredReservation(), param => CanExecute_Command());
            ViewOwnerRatingsCommand = new RelayCommand(param => Execute_ViewOwnerRatings(), param => CanExecute_Command());
            ReservationPostponementOverviewCommand = new RelayCommand(param => Execute_ReservationPostponementOverview(), param => CanExecute_Command());
            LogOutCommand = new RelayCommand(param => Execute_LogOut(), param => CanExecute_Command());
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

        private void Execute_SignInAccomodation()
        {
            SignInAccommodation signInAccommodation = new SignInAccommodation(LoggedOwner);
            signInAccommodation.Show();
        }

        private void Execute_ViewExpiredReservation()
        {
            ExpiredReservation expiredReservation = new ExpiredReservation(LoggedOwner);
            expiredReservation.Show();
        }

        private void Execute_ViewOwnerRatings()
        {
            OwnerRatingView ownerRatingView = new OwnerRatingView(LoggedOwner);
            ownerRatingView.Show();
        }

        private void Execute_ReservationPostponementOverview()
        {
            ReservationPostponementOverview reservationPostponementOverview = new ReservationPostponementOverview(LoggedOwner);
            reservationPostponementOverview.Show();
        }

        private void Execute_LogOut()
        {
            User user = MainWindow.LogInUser;
            user.Id = -1;
            MainWindow mw = new MainWindow();
            mw.Show();
            _window.Close();
        }

        private bool CanExecute_Command()
        {
            return true;
        }

    }
}
