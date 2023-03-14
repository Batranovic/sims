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
using WpfApp1.Model.Enums;
using WpfApp1.Model;
using System.Collections.ObjectModel;
using WpfApp1.Controller;
using System.ComponentModel;

namespace WpfApp1.View
{
    /// <summary>
    /// Interaction logic for TourView.xaml
    /// </summary>
    public partial class TourView : Window
    {
        public LocationController LocationController { get; set; }

        public TourController TourController { get; set; }

        public TourView()
        {
            InitializeComponent();
            this.DataContext = this;

            var app = Application.Current as App;
            LocationController = app.LocationController;
            
        }

        private void Search(object sender, RoutedEventArgs e)
        {

        }

        private void Restart(object sender, RoutedEventArgs e)
        {

        }
    }
}
