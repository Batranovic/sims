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
    /// Interaction logic for CreateNewTour.xaml
    /// </summary>
    public partial class CreateNewTour : Window
    {
        public CreateNewTour()
        {
            InitializeComponent();
            CreateNewTourViewModel newTour = new CreateNewTourViewModel();
            DataContext = newTour;

            if (newTour.CloseAction == null)
            {
                newTour.CloseAction = new Action(this.Close);
            }
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }
    }
}
