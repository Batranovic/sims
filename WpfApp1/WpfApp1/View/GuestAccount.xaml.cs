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
using WpfApp1.Model.Enums;
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

       // private IReservationPostponementService _reservationPostponementService;

        //private ReservationPostponementStatus States { get; set; }
        public GuestAccount(User user)
        {
            InitializeComponent();
            this.DataContext = this;

            _reservationService = InjectorService.CreateInstance<IReservationService>();
           // _reservationPostponementService = InjectorService.CreateInstance<IReservationPostponementService>();
            //States = (ReservationPostponementStatus)_reservationService.Get(LogInGuest.Id).Status;

            LogInGuest = (Guest)user;
           // FindNotification();
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

        //private void FindNotification()
        //{
        //    ReservationPostponementStatus state = (ReservationPostponementStatus)_reservationService.Get(LogInGuest.Id).Status;
        //    if(state != States)
        //    {
        //        string result = "Oslobodilo Vam se " + state.ToString() + " apartaman, ocenite goste";
        //        MessageBox.Show(result, "Obavestenje");

        //    }
        //}

        //private void FindNotification()
        //{
        //    foreach(ReservationPostponement r in  _reservationPostponementService.GetAll().FindAll(b => b.Reservation.IdGuest == LogInGuest.Id))
        //    {
        //        if(r.Status != ReservationPostponementStatus.Waiting && r.Status != ReservationPostponementStatus.Notification)
        //        {
        //            string result = "Doslo je do promene statusa rezervacije";
        //            if(r.Status == ReservationPostponementStatus.Approved)
        //            {
        //                MessageBox.Show
        //            }
        //            r.Status = ReservationPostponementStatus.Notification;
        //        }
        //    }
        //}
    }
}
