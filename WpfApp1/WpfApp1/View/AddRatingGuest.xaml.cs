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
using WpfApp1.Controller;
using WpfApp1.Model;

namespace WpfApp1.View
{
    /// <summary>
    /// Interaction logic for AddRatingGuest.xaml
    /// </summary>
    public partial class AddRatingGuest : Window, INotifyPropertyChanged
    {
        public ObservableCollection<int> Scores { get; set; }
        public int SelectedCleanness { get; set; }
        public int SelectedFollowingRules { get; set; }
        public int SelectedNoise { get; set; }
        public int SelectedDamage { get; set; }
        public int SelectedTimeliness { get; set; }
        public Reservation SelectedResevation { get; set; }
        public RatingGuestController RatingGuestController { get; set; }
        public ReservationController ReservationController { get; set; }
        public AddRatingGuest(Reservation reservation)
        {
            InitializeComponent();
            this.DataContext = this;

            var app = Application.Current as App;
            RatingGuestController = app.RatingGuestController;
            ReservationController = app.ReservationController;

            Scores = new ObservableCollection<int>();
            Scores.Add(1);
            Scores.Add(2);
            Scores.Add(3);
            Scores.Add(4);
            Scores.Add(5);
            SelectedResevation = reservation;
        }

        private string _comment;

        public string Comment
        {
            get => _comment;
            set
            {
                if (_comment != value)
                {
                    _comment = value;
                    OnPropertyChanged("Comment");
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


       
        private void Confrim(object sender, RoutedEventArgs e)
        {
            SelectedResevation.Status = Model.Enums.RatingGuestStatus.rated;
            ReservationController.Update(SelectedResevation);
            RatingGuest  ratingGuest = new RatingGuest(SelectedResevation, Comment, SelectedCleanness, SelectedFollowingRules, SelectedNoise, SelectedDamage, SelectedTimeliness);
            RatingGuestController.Create(ratingGuest);        
            this.Close();
        }

        private void Reject(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
