using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using WpfApp1.Domain.Models;
using System.ComponentModel;
using WpfApp1.Service;
using WpfApp1.Domain.ServiceInterfaces;
using WpfApp1.Domain.Domain.Models.Enums;
using WpfApp1.Views;
using WpfApp1.ViewModel;

namespace WpfApp1.Views
{
    /// <summary>
    /// Interaction logic for TourView.xaml
    /// </summary>
    public partial class TourSearchAndOverview : Window, INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        public TourSearchAndOverview()
        {
            InitializeComponent();
           // this.DataContext = this;
            DataContext = new TourSearchAndOverviewViewModel();


        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
