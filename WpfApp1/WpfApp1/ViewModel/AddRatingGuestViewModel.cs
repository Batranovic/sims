using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WpfApp1.Commands;
using WpfApp1.Domain.Models;
using WpfApp1.Domain.ServiceInterfaces;
using WpfApp1.Service;

namespace WpfApp1.ViewModel
{
    public class AddRatingGuestViewModel : ViewModelBase
    {
        public ObservableCollection<int> Scores { get; set; }
        public int SelectedCleanness { get; set; }
        public int SelectedFollowingRules { get; set; }
        public int SelectedNoise { get; set; }
        public int SelectedDamage { get; set; }
        public int SelectedTimeliness { get; set; }

        public Reservation SelectedResevation { get; set; }
        private readonly IGuestRatingService _guestRatingService;
        private readonly IReservationService _reservationService;
        private readonly IOwnerRatingService _ownerRatingService;

        private Window _window;

        public RelayCommand ConfrimCommand { get; set; }
        public RelayCommand RejectCommand { get; set; }

        public AddRatingGuestViewModel(Reservation reservation)
        {
            _window = Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.Name == "RatingGuest");
            _guestRatingService = InjectorService.CreateInstance<IGuestRatingService>();
            _reservationService = InjectorService.CreateInstance<IReservationService>();
            _ownerRatingService = InjectorService.CreateInstance<IOwnerRatingService>();

            ConfrimCommand = new RelayCommand(param => Execute_Confrim(), param => CanExecute());
        //    RejectCommand = new RelayCommand();

            Init(reservation);
        }

        private void Init(Reservation reservation)
        {
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

        private void Execute_Confrim()
        {
            SelectedResevation.Status = Domain.Models.Enums.GuestRatingStatus.Rated;
            _reservationService.Update(SelectedResevation);
            GuestRating ratingGuest = new GuestRating(SelectedResevation, Comment, SelectedCleanness, SelectedFollowingRules, SelectedNoise, SelectedDamage, SelectedTimeliness);
            _guestRatingService.Create(ratingGuest);
            _window.Close();

            //OBRISATI KASNIJE KAD URADIS NORMALNO
            OwnerRating ratingOwner = _ownerRatingService.GetByIdReservation(SelectedResevation.Id);
            if (ratingOwner == null)
            {
                MessageBox.Show("Korisnik Vas jos uvek nije ocenio");
            }
            else
            {
                MessageBox.Show(ratingOwner.ToString());
            }
        }

        
        private void Execute_Reject()
        {
            _window.Close();
        }

        private bool CanExecute()
        {
            return true;
        }

       
    }
}

