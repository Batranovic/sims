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
using WpfApp1.Domain.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using WpfApp1.Domain.Domain.Models.Enums;
using WpfApp1.Repository;
using WpfApp1.Domain.ServiceInterfaces;
using WpfApp1.Service;
using WpfApp1.ViewModel;


namespace WpfApp1.Views
{
    /// <summary>
    /// Interaction logic for TourBookingWindow.xaml
    /// </summary>
    public partial class TourBookings : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public TourBookings(Tour tour)
        {
            InitializeComponent();

            TourBookingsViewModel tourBooking = new TourBookingsViewModel(tour);
            DataContext = tourBooking;

            if (tourBooking.CloseAction == null)
            {
                tourBooking.CloseAction = new Action(this.Close);
            }
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        
       
    }

}
