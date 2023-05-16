using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfApp1.Domain.Models.Enums;
using WpfApp1.Domain.Models;
using WpfApp1.Domain.ServiceInterfaces;
using WpfApp1.Service;
using WpfApp1.Commands;
using WpfApp1.Views;

namespace WpfApp1.ViewModel
{
    public partial class AccommodationAndOwnerRatingViewModel : ViewModelBase
    {
        public ObservableCollection<int> Grades { get; set; }

        public int SelectedCleanness { get; set; }

        public int SelectedOwnerCorectness { get; set; }

        public int SelectedTimeliness { get; set; }

        public Reservation SelectedReservation { get; set; }

        private readonly IOwnerRatingService _ownerRatingService;

        private readonly IImageService _imageService;

        private readonly IReservationService _reservationService;
        private readonly IOwnerService _ownerService;

        private Window _window;


        public RelayCommand ConfirmCommand { get; set; }

        public RelayCommand CancelCommand { get; set; }

        public RelayCommand AddUrlCommand { get; set; }
        public RelayCommand RejectCommand { get; set; }


        public AccommodationAndOwnerRatingViewModel(Reservation reservation)
        {
            _ownerRatingService = InjectorService.CreateInstance<IOwnerRatingService>();
            _imageService = InjectorService.CreateInstance<IImageService>();
            _reservationService = InjectorService.CreateInstance<IReservationService>();
            _ownerService = InjectorService.CreateInstance<IOwnerService>();

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

            ConfirmCommand = new RelayCommand(param => Execute_Confirm(), param => CanExecute());
            CancelCommand = new RelayCommand(param => Execute_Cancel(), param => CanExecute());
            AddUrlCommand = new RelayCommand(param => Execute_AddUrl(), param => CanExecute());

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

        private void Execute_AddUrl()
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

        private void Execute_Confirm()
        {
            OwnerRating rating = new OwnerRating(SelectedReservation, Comment, SelectedCleanness, SelectedOwnerCorectness, SelectedTimeliness);
            _ownerRatingService.Create(rating);
            rating.Reservation.Accommodation.Images = AddImage(rating);
            rating.Reservation.GuestReservationStatus = Domain.Domain.Models.Enums.AccommodationAndOwnerRatingStatus.Rated;
            _reservationService.Update(rating.Reservation);
            _ownerService.CalculateAverageRating();

            string message = "Do you want to add renovation suggestion?";
            
            MessageBoxResult result = MessageBox.Show(message, "Renovation suggestion", MessageBoxButton.YesNo);
            if(result == MessageBoxResult.Yes)
            {
                AccommodationRenovationSuggestionView a = new(SelectedReservation);
                a.Show();
                return;
            }


            this.Execute_Reject();
        }
        private void Execute_Cancel()
        {
            this.Execute_Reject();
        }

        private bool CanExecute()
        {
            return true;
        }

        private void Execute_Reject()
        {
            _window.Close();
        }
    }
}

