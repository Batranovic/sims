using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
using WpfApp1.Domain.Models;
using WpfApp1.Service;

namespace WpfApp1.View
{
    /// <summary>
    /// Interaction logic for GuestPostponementsOverview.xaml
    /// </summary>
    public partial class GuestPostponementsOverview : Window
    {

        public Guest LogInGuest { get; set; }

        public ObservableCollection<ReservationPostponement> ReservationPostponements { get; set; }

        private readonly IReservationPostponementService _reservationPostponementService;
        public GuestPostponementsOverview(Guest guest)
        {
            InitializeComponent();
            this.DataContext = this;

            _reservationPostponementService = InjectorService.CreateInstance<IReservationPostponementService>();
            LogInGuest = guest;
            ReservationPostponements = new ObservableCollection<ReservationPostponement>(_reservationPostponementService.GetAllByGuestId(LogInGuest.Id));
            
        }

        private void CloseWindow(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        public void Update()
        {
            ReservationPostponements.Clear();
            foreach (var r in _reservationPostponementService.GetAllByOwnerIdAhead(LogInGuest.Id))
            {
                ReservationPostponements.Add(r);
            }
        }

    }
}
