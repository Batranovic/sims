using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using WpfApp1.Model;
using WpfApp1.Repository;

namespace WpfApp1.View
{
    /// <summary>
    /// Interaction logic for UserView.xaml
    /// </summary>
    public partial class UserView : Window
    {
        private readonly OwnerRepository _owner;

        public ObservableCollection<Owner> Owners { get; set; }
        public UserView()
        {
            InitializeComponent();
            this.DataContext = this;
            var app = Application.Current as App;

            _owner = app.OwnerRepository;

            Owners = new ObservableCollection<Owner>(_owner.GetAll());

        }
    }
}
