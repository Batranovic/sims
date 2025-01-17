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
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfApp1.Domain.Models;
using WpfApp1.ViewModel;

namespace WpfApp1.Views
{
    /// <summary>
    /// Interaction logic for OwnerProfile.xaml
    /// </summary>
    public partial class OwnerProfile : UserControl
    {
        public OwnerProfile()
        {
            InitializeComponent();
            this.DataContext = new OwnerProfileViewModel((Owner)MainWindow.LogInUser);
        }
    }
}
