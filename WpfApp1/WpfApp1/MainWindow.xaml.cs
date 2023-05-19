using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfApp1.Domain.ServiceInterfaces;
using WpfApp1.Domain.Models;
using WpfApp1.Repository;
using WpfApp1.Service;
using WpfApp1.Views;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IOwnerService _ownerService;
        private readonly IGuestService _guestService;
        private readonly ITouristService _touristService;
        private readonly INewTourNotificationService  _tourNotificationService;
        public static User LogInUser { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        private readonly INotificationService _notificationService;

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            _ownerService = InjectorService.CreateInstance<IOwnerService>();
            _guestService = InjectorService.CreateInstance<IGuestService>();
            _touristService = InjectorService.CreateInstance<ITouristService>();
            _notificationService = InjectorService.CreateInstance<INotificationService>();
            _tourNotificationService = InjectorService.CreateInstance<INewTourNotificationService>();



        }
        private void TourSearchAndOverview(object sender, RoutedEventArgs e)
        {
            TourSearchAndOverview a = new TourSearchAndOverview();
            a.Show();

        }
        private void AccommodationView(object sender, RoutedEventArgs e)
        {
            Guest guest = GuestRepository.GetInsatnce().Get(0);
            AccommodationView accommodationView = new AccommodationView(guest);
            accommodationView.Show();
        }
        private void LogIn(object sender, RoutedEventArgs e)
        {
            Password = passwordBox.Password;

            LogInUser = _ownerService.GetByUsernameAndPassword(Username, Password);   
            if(LogInUser != null)
            {
                OwnerAccount ownerAccount = new OwnerAccount();
                ownerAccount.Show();
                _ownerService.SetKind((Owner)LogInUser);
                Close();
                return;
            }
             LogInUser = _touristService.GetByUsernameAndPassword(Username, Password);
            if (LogInUser != null)
            {
                List<Notification> notifications = _notificationService.GetNotificationForUser(LogInUser.Id);
                foreach (Notification notification in notifications)
                {
                    string tourName = notification.TourBooking.TourEvent.Tour.Name;
                    MessageBoxResult result = MessageBox.Show(this, "You have been added to " + tourName);
                }

                List<NewTourNotification> newTourNotifications = _tourNotificationService.GetNotificationForUser(MainWindow.LogInUser.Id);
                foreach(NewTourNotification notification1 in newTourNotifications)
                {
                    string city = notification1.Tour.Location.City;
                    string language = notification1.Tour.Languages;
                    MessageBox.Show("New tour has been created in " + city + " in " + language);
                }

                TourSearchAndOverview tourSearchAndOverview = new TourSearchAndOverview();
                tourSearchAndOverview.Show();
                Close();
                return;
            }
            LogInUser = _guestService.GetByUsernameAndPassword(Username, Password);
            if(LogInUser != null)
            {
                GuestAccount guestAccount = new GuestAccount(LogInUser);
                guestAccount.Show();
                _guestService.ResetBonusPoints((Guest)LogInUser);
                Close();
            }
        }

    }
}
