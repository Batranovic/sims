using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
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
using System.Windows.Shapes;
using WpfApp1.Domain.ServiceInterfaces;
using WpfApp1.Domain.Models;
using WpfApp1.Domain.Domain.Models.Enums;
using WpfApp1.Service;
using WpfApp1.Domain.Models.Enums;

namespace WpfApp1.View
{
    /// <summary>
    /// Interaction logic for AccommodationAndOwnerRating.xaml
    /// </summary>
    public partial class AccommodationAndOwnerRating : Window, INotifyPropertyChanged
    {
        public ObservableCollection<int> Grades { get; set; }
        
        public int SelectedCleanness { get; set; }

        public int SelectedOwnerCorectness { get; set; }

        public int SelectedTimeliness { get; set; }

        public Reservation SelectedReservation { get; set; }

        private readonly IOwnerRatingService _ownerRatingService;

        private readonly IImageService _imageService;

        private readonly IReservationService _reservationService;


        public AccommodationAndOwnerRating(Reservation reservation)
        {
            InitializeComponent();
            this.DataContext = this;

            _ownerRatingService = InjectorService.CreateInstance<IOwnerRatingService>();
            _imageService = InjectorService.CreateInstance<IImageService>();
            _reservationService = InjectorService.CreateInstance<IReservationService>();

            Grades = new ObservableCollection<int>();
            Grades.Add(1);
            Grades.Add(2);
            Grades.Add(3);
            Grades.Add(4);
            Grades.Add(5);
            SelectedReservation = reservation;
            SelectedCleanness = 0;
            SelectedOwnerCorectness = 0;
            SelectedTimeliness = 0;

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

        private string _url;
        public string Url
        {
            get => _url;
            set
            {
                if (value != _url)
                {
                    _url = value;
                    OnPropertyChanged("Url");
                }
            }
        }

        private List<string> _urls = new List<string>();

        private void AddURL(object sender, RoutedEventArgs e)
        {
            _urls.Add(Url);
            Url = "";
        }

        private List<Domain.Models.Image> AddImage(OwnerRating rating)
        {
            List<Domain.Models.Image> images = new List<Domain.Models.Image>();
            foreach (string s in _urls)
            {
                images.Add(new Domain.Models.Image(s, rating.Reservation.Accommodation.Id, ImageKind.ReviewAccommodation));
            }
            foreach (Domain.Models.Image image in images)
            {
                _imageService.Create(image);
            }
            return images;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void Confirm(object sender, RoutedEventArgs e)
        {
            OwnerRating rating = new OwnerRating(SelectedReservation, Comment, SelectedCleanness, SelectedOwnerCorectness, SelectedTimeliness);
            _ownerRatingService.Create(rating);
            rating.Reservation.Accommodation.Images = AddImage(rating);
            rating.Reservation.GuestReservationStatus = Domain.Domain.Models.Enums.AccommodationAndOwnerRatingStatus.Rated;
            _reservationService.Update(rating.Reservation);
            this.Close();
        }
        private void Reject(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
