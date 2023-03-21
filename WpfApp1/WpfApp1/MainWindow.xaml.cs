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
using System.Windows.Navigation;
using System.Windows.Shapes;
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
        OwnerRepository OwnerRepostiroy { get; set; }
        public MainWindow()
        {
            InitializeComponent();

           


            //UserView userView = new UserView();
            AccommodationView accommodationView = new AccommodationView();
            accommodationView.Show();

            User user = OwnerRepository.GetInsatnce().Get(0);
            OwnerAccount ownerAccount = new OwnerAccount(user);
            ownerAccount.Show();

         

            //UserView userView = new UserView();
           // userView.Show();
            TourSearchAndOverview a = new TourSearchAndOverview();
            a.Show();



           // UserView userView = new UserView();
           //userView.Show();


        }
    }
}
