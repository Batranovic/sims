﻿using System;
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
using WpfApp1.Domain.Models;
using WpfApp1.Domain.ServiceInterfaces;
using WpfApp1.Service;
using WpfApp1.ViewModel;

namespace WpfApp1.Views
{
    /// <summary>
    /// Interaction logic for BookedTours.xaml
    /// </summary>
    public partial class BookedTours : Window
    {
     
        

        public BookedTours()
        {
            InitializeComponent();
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            BookedToursViewModel bookedToursViewModel = new BookedToursViewModel();
            DataContext = bookedToursViewModel;

            if (bookedToursViewModel.CloseAction == null)
            {
                bookedToursViewModel.CloseAction = new Action(this.Close);
            }

        }

    }
}
