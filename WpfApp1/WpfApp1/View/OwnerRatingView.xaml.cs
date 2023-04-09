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
using WpfApp1.Repository;
using WpfApp1.Service;

namespace WpfApp1.View
{
    /// <summary>
    /// Interaction logic for OwnerRatingView.xaml
    /// </summary>
    public partial class OwnerRatingView : Window
    {
        public ObservableCollection<OwnerRating> RatingOwners { get; set; }
        private readonly OwnerRatingService _ownerRatingService;

        public Owner LogInOwner { get; set; }
        public OwnerRatingView(Owner owner)
        {
            InitializeComponent();
            this.DataContext = this;

            _ownerRatingService = InjectorService.CreateInstance<OwnerRatingService>();

            RatingOwners = new ObservableCollection<OwnerRating>(_ownerRatingService.GetAllOwnerRewies(owner.Id));
            LogInOwner = owner;
        }


    }
}
