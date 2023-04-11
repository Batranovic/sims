using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Security.Policy;
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
        private readonly GuestRatingService _guestRatingService;
        private readonly ReservationService _reservationService;
        private readonly OwnerRatingService _ownerRatingService; 
                
        public AddRatingGuest(Reservation reservation)
        {
            InitializeComponent();
            this.DataContext = this;

            _guestRatingService = InjectorService.CreateInstance<GuestRatingService>();
            _reservationService = InjectorService.CreateInstance<ReservationService>();
            _ownerRatingService = InjectorService.CreateInstance<OwnerRatingService>();

            Scores = new ObservableCollection<int>();
            Scores.Add(1);
            Scores.Add(2);
            Scores.Add(3);
            Scores.Add(4);
            Scores.Add(5);
            SelectedResevation = reservation;
            SelectedCleanness = 0;
            SelectedDamage = 0;
            SelectedNoise = 0;
            SelectedTimeliness = 0;
            SelectedFollowingRules = 0;
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
            SelectedResevation.Status =  Domain.Models.Enums.GuestRatingStatus.Rated;
            _reservationService.Update(SelectedResevation);
            GuestRating  ratingGuest = new GuestRating(SelectedResevation, Comment, SelectedCleanness, SelectedFollowingRules, SelectedNoise, SelectedDamage, SelectedTimeliness);
            _guestRatingService.Create(ratingGuest);        
            this.Close();

            //OBRISATI KASNIJE KAD URADIS NORMALNO
            OwnerRating ratingOwner = _ownerRatingService.GetByIdReservation(SelectedResevation.Id);
            if(ratingOwner == null)
            {
                MessageBox.Show("Korisnik Vas jos uvek nije ocenio");
            }
            else
            {
                MessageBox.Show(ratingOwner.ToString());
            }
        }

        private void Reject(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        public string Error => null;

        public string this[string columnName]
        {
            get
            { 
                if (columnName == "SelectedNoise")
                {
                    if (SelectedNoise == 0)
                    {
                        return "Nije dodata ocena";
                    }
                }

                if (columnName == "SelectedTimeliness")
                {
                    if (SelectedTimeliness == 0)
                    {
                        return "Nije dodata ocena";

                    }
                }
                if (columnName == "SelectedFollowingRules")
                {
                    if (SelectedFollowingRules == 0)
                    {
                        return "Nije dodata ocena";
                    }
                }
                if (columnName == "SelectedDamage")
                {
                    if (SelectedDamage == 0)
                    {
                        return "Nije dodata ocena";
                    }
                }
                if (columnName == "MaxGuests")
                {
                    if (SelectedCleanness == 0)
                    {
                        return "Nije dodata ocena";
                    }
                }
                
                return null;
            }
        }

        private readonly string[] _validatedProperties = { "SelectedNoise", "SelectedTimeliness", "SelectedFollowingRules", "SelectedDamage", "SelectedCleanness" };

        public bool IsValid
        {
            get
            {
                foreach (var property in _validatedProperties)
                {
                    if (this[property] != null)
                        return false;
                }

                return true;
            }
        }

    }
}
