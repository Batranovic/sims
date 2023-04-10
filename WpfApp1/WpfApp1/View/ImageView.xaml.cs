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
using WpfApp1.Controller;
using WpfApp1.Models;

namespace WpfApp1.View
{
    /// <summary>
    /// Interaction logic for ImageView.xaml
    /// </summary>
    public partial class ImageView : Window, INotifyPropertyChanged
    {
        
        public Accommodation Accommodation { get; set; }
       // public Image Image { get; set; }

        public ImageController ImageController { get; set; }
        
        //public List<Image> Images { get; set; } 
        public ImageView(Accommodation accommodation)
        {
            InitializeComponent();
            this.DataContext = this;

            ListImages.ItemsSource = accommodation.Images;

        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void Close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }    
     
}
