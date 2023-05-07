using System;
using System.Collections.Generic;
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
using WpfApp1.Domain.Models;
using WpfApp1.ViewModel;

namespace WpfApp1.Views
{
    /// <summary>
    /// Interaction logic for RequestNewTours.xaml
    /// </summary>
    public partial class RequestNewTours : Window,  INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;


        public RequestNewTours()
        {
            InitializeComponent();
            RequestNewToursViewModel requestNewToursViewModel = new RequestNewToursViewModel();
            DataContext = requestNewToursViewModel;

            if (requestNewToursViewModel.CloseAction == null)
            {
                requestNewToursViewModel.CloseAction = new Action(this.Close);
            }
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;

        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
