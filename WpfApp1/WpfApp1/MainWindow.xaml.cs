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
using WpfApp1.Controller;
using WpfApp1.Models;
using WpfApp1.Repository;
using WpfApp1.View;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public OwnerController OwnerController { get; set; }
        public GuestController GuestController { get; set; }
        public TouristController TouristController { get; set; }  
        public NotificationController NotificationController { get; set; }
        public static User LogInUser { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;

            var app = Application.Current as  App;
            OwnerController = app.OwnerController;
            GuestController = app.GuestController;
            TouristController = app.TouristController;
            NotificationController = app.NotificationController;

        }

        private void TourSearchAndOverview(object sender, RoutedEventArgs e)
        {
            TourSearchAndOverview a = new TourSearchAndOverview();
            a.Show();

        }

        private void OwnerProfile(object sender, RoutedEventArgs e)
        {
            User user = OwnerController.Get(0);
            OwnerAccount ownerAccount = new OwnerAccount(user);
            ownerAccount.Show();
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

            LogInUser = OwnerController.GetByUsernameAndPassword(Username, Password);   
            if(LogInUser != null)
            {
                OwnerAccount ownerAccount = new OwnerAccount(LogInUser);
                ownerAccount.Show();
                this.Close();
                return;
            }
            LogInUser = TouristController.GetByUsernameAndPassword(Username, Password);
            if(LogInUser != null)
            {
                List<Notification> notifications = NotificationController.GetNotificationForUser(LogInUser.Id);
                foreach (Notification notification in notifications)
                {
                    string tourName = notification.TourBooking.TourEvent.Tour.Name;
                    MessageBoxResult result = MessageBox.Show(this, "You have been added to " + tourName);
                }
                TourSearchAndOverview tourSearchAndOverview = new TourSearchAndOverview();
                tourSearchAndOverview.Show();
                this.Close();
                return;
            }
            LogInUser = GuestController.GetByUsernameAndPassword(Username, Password);
            if(LogInUser != null)
            {
                AccommodationView accommodationView = new AccommodationView(LogInUser);
                accommodationView.Show();
                this.Close();

            }
        }
    }
}
