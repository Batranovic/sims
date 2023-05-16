using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
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
using WpfApp1.Service;
using WpfApp1.ViewModel;

namespace WpfApp1.Views
{
    /// <summary>
    /// Interaction logic for ImageView.xaml
    /// </summary>
    public partial class ImageView : Window
    {
        public ImageView(Accommodation accommodation)
        {
            InitializeComponent();
            this.DataContext = new ImageViewModel(accommodation);
        }
    }    
     
}
