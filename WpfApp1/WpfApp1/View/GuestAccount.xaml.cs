using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WpfApp1.Domain.ServiceInterfaces;
using WpfApp1.Model;
using WpfApp1.Service;

namespace WpfApp1.View
{
    /// <summary>
    /// Interaction logic for GuestAccount.xaml
    /// </summary>
    public partial class GuestAccount : Window
    {
        public Guest LogInGuest { get; set; }

        private readonly IReservationService _reservationService;
        public GuestAccount(User user)
        {
            InitializeComponent();
            this.DataContext = this;

            _reservationService = InjectorService.CreateInstance<IReservationService>();

            LogInGuest = (Guest)user;
        }

        private void AccommodationView(object sender, RoutedEventArgs e)
        {
            AccommodationView accommodationView = new AccommodationView(LogInGuest);
            accommodationView.Show();
        }

        private void ReservationView(object sender, RoutedEventArgs e)
        {
            ReservationView reservationView = new ReservationView(LogInGuest);
            reservationView.Show();
        }

        private void PostponementsOverview(object sender, RoutedEventArgs e)
        {
            GuestPostponementsOverview guestPostponementsOverview = new GuestPostponementsOverview(LogInGuest);
            guestPostponementsOverview.Show();
        }
    }
}
