using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using WpfApp1.Domain.ServiceInterfaces;
using WpfApp1.Domain.Models;
using WpfApp1.Domain.Domain.Models.Enums;
using WpfApp1.Service;
using WpfApp1.Domain.Models.Enums;
using WpfApp1.ViewModel;

namespace WpfApp1.Views
{
    /// <summary>
    /// Interaction logic for AccommodationAndOwnerRating.xaml
    /// </summary>
    public partial class AccommodationAndOwnerRating : Window
    {



        public AccommodationAndOwnerRating(Reservation reservation)
        {
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            InitializeComponent();
            this.DataContext = new AccommodationAndOwnerRatingViewModel(reservation);

        }

    }       
}
