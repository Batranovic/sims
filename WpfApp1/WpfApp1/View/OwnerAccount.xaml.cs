﻿using System;
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
using WpfApp1.Controller;
using WpfApp1.Model;

namespace WpfApp1.View
{
    /// <summary>
    /// Interaction logic for OwnerAccount.xaml
    /// </summary>
    public partial class OwnerAccount : Window
    {
        public Owner LogInOwner { get; set; }

        public ReservationController ReservationController { get; set; }
        public OwnerAccount(User user)
        {
            InitializeComponent();
            this.DataContext = this;

            var app = Application.Current as App;
            ReservationController = app.ReservationController;
            LogInOwner = (Owner)user;

            FindNotification();
        }

        private void FindNotification()
        {
            int numberNotification = ReservationController.GetUnratedById(LogInOwner.Id).Count;
            if (numberNotification == 0)
            {
                btnRatingGuest.IsEnabled = false;
                return;
            }
            string result = "Oslobodilo Vam se " + numberNotification.ToString() + " apartaman, ocenite goste";
            MessageBox.Show(result, "Obavestenje");
            btnRatingGuest.IsEnabled = true;
        }

        private void SignInAccomodation(object sender, RoutedEventArgs e)
        {
            SignInAccommodation signInAccommodation = new SignInAccommodation(LogInOwner);
            signInAccommodation.Show();
        }

        private void ViewExpiredReservation(object sender, RoutedEventArgs e)
        {
            ExpiredReservation expiredReservation = new ExpiredReservation(LogInOwner);
            expiredReservation.Show();
        }
    }
}