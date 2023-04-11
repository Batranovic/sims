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
using WpfApp1.Controller;
using WpfApp1.Domain.ServiceInterfaces;
using WpfApp1.Model;
using WpfApp1.Repository;
using WpfApp1.Service;

namespace WpfApp1.View
{
    /// <summary>
    /// Interaction logic for AvailableDays.xaml
    /// </summary>
    public partial class AvailableDays : Window
    {

        public Dictionary<DateTime, DateTime> Range { get; set; }

        public readonly IReservationService _reservationService;

        public KeyValuePair<DateTime,DateTime> SelectedRange { get; set; }

        public Guest Guest { get; set; }

        public Accommodation Accommodation { get; set; }
        public AvailableDays(Dictionary<DateTime, DateTime> range, Accommodation accommodation, Guest guest)
        {
            InitializeComponent();
            this.DataContext = this;

            _reservationService = InjectorService.CreateInstance<IReservationService>();

            Range = range;
            Accommodation = accommodation;
            Guest = guest;
        }

        private void Confirm(object sender, RoutedEventArgs e)
        {
            Reservation reservation = new Reservation(Guest, Accommodation, SelectedRange.Value, SelectedRange.Key, Model.Enums.GuestRatingStatus.Reserved, Domain.Models.Enums.AccommodationAndOwnerRatingStatus.Disabled);
            _reservationService.Create(reservation);
            this.Close();
        }


    }
}
