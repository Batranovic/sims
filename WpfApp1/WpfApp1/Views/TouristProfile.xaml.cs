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
using WpfApp1.ViewModel;

namespace WpfApp1.Views
{
    /// <summary>
    /// Interaction logic for TouristProfile.xaml
    /// </summary>
    public partial class TouristProfile : Window
    {
        public TouristProfile()
        {
            InitializeComponent();
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            TouristProfileViewModel profile = new TouristProfileViewModel();
            DataContext = profile;

            if (profile.CloseAction == null)
            {
                profile.CloseAction = new Action(this.Close);
            }
        }
    }
}
