using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using WpfApp.Observer;
using WpfApp1.Controller;
using WpfApp1.Model;

namespace WpfApp1.View
{
    /// <summary>
    /// Interaction logic for ExpiredReservation.xaml
    /// </summary>
    public partial class ExpiredReservation : Window, INotifyPropertyChanged, IObserver
    {
        public ObservableCollection<Reservation> Reservations { get; set; } 
        public ReservationController ReservationController { get; set; }
        public Owner LogInOwner { get; set; }
        public Reservation SelectedReservation { get; set; }
        public ExpiredReservation(Owner owner)
        {
            InitializeComponent();
            this.DataContext = this;

            var app = Application.Current as App;
            ReservationController = app.ReservationController;

            ReservationController.Subscribe(this);

            LogInOwner = owner;
            Reservations = new ObservableCollection<Reservation>(ReservationController.GetUnratedById(LogInOwner.Id));
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void AddRatingToGuest(object sender, RoutedEventArgs e)
        {
            if(SelectedReservation == null)
            {
                return;
            }
            AddRatingGuest addRatingGuest = new AddRatingGuest(SelectedReservation);
            addRatingGuest.Show();
        }

        public void Update()
        {
            Reservations.Remove(SelectedReservation);
        }
    }
}
