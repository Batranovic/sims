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
using WpfApp1.Model;
using WpfApp1.Repository;
using WpfApp1.View;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        OwnerController OwnerController { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;

            var app = Application.Current as  App;
            OwnerController = app.OwnerController;

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
    }
}
