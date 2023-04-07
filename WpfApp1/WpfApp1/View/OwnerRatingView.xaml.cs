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
using WpfApp1.Controller;
using WpfApp1.Model;

namespace WpfApp1.View
{
    /// <summary>
    /// Interaction logic for OwnerRatingView.xaml
    /// </summary>
    public partial class OwnerRatingView : Window
    {
        public ObservableCollection<OwnerRating> RatingOwners { get; set; }
        public RatingOwnerController RatingOwnerController { get; set; }

        public Owner LogInOwner { get; set; }
        public OwnerRatingView(Owner owner)
        {
            InitializeComponent();
            this.DataContext = this;

            var app = Application.Current as App;
            RatingOwnerController = app.RatingOwnerController;

            RatingOwners = new ObservableCollection<OwnerRating>(RatingOwnerController.GetAllOwnerRewies(owner.Id));
            LogInOwner = owner;
        }


    }
}
