using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
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
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WpfApp1.Domain.ServiceInterfaces;
using WpfApp1.Domain.Models;
using WpfApp1.Domain.Domain.Models.Enums;
using WpfApp1.Service;
using WpfApp1.ViewModel;

namespace WpfApp1.Views
{
    /// <summary>
    /// Interaction logic for AddRatingTourAndGuide.xaml
    /// </summary>
    public partial class AddRatingTourAndGuide : Window
    {

        public AddRatingTourAndGuide(TourBooking tourBooking)
        {
            InitializeComponent();
            AddRatingTourAndGuideViewModel addRating = new AddRatingTourAndGuideViewModel(tourBooking);
            DataContext = addRating;

            if (addRating.CloseAction == null)
            {
                addRating.CloseAction = new Action(this.Close);
            }
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }


    }
}
