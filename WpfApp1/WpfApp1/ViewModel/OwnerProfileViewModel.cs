using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfApp1.Commands;
using WpfApp1.Domain.Models;
using WpfApp1.Domain.ServiceInterfaces;
using WpfApp1.Service;
using WpfApp1.Views;

namespace WpfApp1.ViewModel
{
    public class OwnerProfileViewModel : ViewModelBase
    {
        public Owner LoggedOwner { get; set; }
        private readonly IReservationService _reservationService;
        private readonly IOwnerRatingService _ownerRatingService;
        private readonly IGuestRatingService _guestRatingService;

        private Window _window;
        public RelayCommand SignInAccomodationCommand { get; set; }
        public RelayCommand ViewExpiredReservationCommand { get; set; }
        public RelayCommand ViewOwnerRatingsCommand { get; set; }
        public RelayCommand ReservationPostponementOverviewCommand { get; set; }
        public RelayCommand LogOutCommand { get; set; }
        public RelayCommand ConfirmCommand { get; set; }
        public List<OwnerRating> OwnerRatings { get; set; }
        public ObservableCollection<Reservation> ExpiredReservations { get; set; }
        public ObservableCollection<int> Grades { get; set; }
        public int Cleanness { get; set; }
        public int Damage { get; set; }
        public int Timeliness { get; set; }
        public int FollowingRules { get; set; }
        public int Noise { get; set; }
        private string _comment;
        public string Comment
        {
            get => _comment;
            set
            {
                if (_comment != value)
                {
                    _comment = value;
                    ConfirmCommand.RaiseCanExecuteChanged();
                }
            }
        }
        private Reservation _selectedReservation;
        public Reservation SelectedReservation
        {
            get => _selectedReservation;
            set
            {
                _selectedReservation = value;
                ConfirmCommand.RaiseCanExecuteChanged();
            }
        }
        public OwnerProfileViewModel(Owner owner)
        {
            _reservationService = InjectorService.CreateInstance<IReservationService>();
            _ownerRatingService = InjectorService.CreateInstance<IOwnerRatingService>();
            _guestRatingService = InjectorService.CreateInstance<IGuestRatingService>();
            InitCommand();
            Init(owner);
        //    FindNotification();
        }

        private void Init(Owner owner)
        {
            _window = Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.Name == "OwnerStart");
            LoggedOwner = owner;
            OwnerRatings = new List<OwnerRating>(_ownerRatingService.GetAllOwnerRewies(owner.Id));  
            Grades = new ObservableCollection<int>();
            Grades.Add(1);
            Grades.Add(2);
            Grades.Add(3);
            Grades.Add(4);
            Grades.Add(5);
            ExpiredReservations = new(_reservationService.GetUnratedById(LoggedOwner.Id));
            Cleanness = Damage = Timeliness = FollowingRules = Noise = 1;
            SelectedReservation = new();
        }

        private void InitCommand()
        {
            SignInAccomodationCommand = new RelayCommand(param => Execute_SignInAccomodation(), param => CanExecute_Command());
            ViewExpiredReservationCommand = new RelayCommand(param => Execute_ViewExpiredReservation(), param => CanExecute_Command());
            ViewOwnerRatingsCommand = new RelayCommand(param => Execute_ViewOwnerRatings(), param => CanExecute_Command());
            ReservationPostponementOverviewCommand = new RelayCommand(param => Execute_ReservationPostponementOverview(), param => CanExecute_Command());
            LogOutCommand = new RelayCommand(param => Execute_LogOut(), param => CanExecute_Command());
            ConfirmCommand = new RelayCommand(param => Execute_Confrim(), param => CanExecute_Confirm());
        }

        private void FindNotification()
        {
            int numberNotification = _reservationService.GetUnratedById(LoggedOwner.Id).Count;
            if (numberNotification == 0)
            {
                return;
            }
            string result = "Oslobodilo Vam se " + numberNotification.ToString() + " apartaman, ocenite goste";
            MessageBox.Show(result, "Obavestenje");
        }

        private void Execute_SignInAccomodation()
        {
            SignInAccommodation signInAccommodation = new SignInAccommodation();
        }

        private void Execute_ViewExpiredReservation()
        {
            ExpiredReservation expiredReservation = new ExpiredReservation(LoggedOwner);
            expiredReservation.Show();
        }

        private void Execute_ViewOwnerRatings()
        {
            OwnerRatingView ownerRatingView = new OwnerRatingView(LoggedOwner);
            ownerRatingView.Show();
        }

        private void Execute_ReservationPostponementOverview()
        {
            ReservationPostponementOverview reservationPostponementOverview = new ReservationPostponementOverview(LoggedOwner);
            reservationPostponementOverview.Show();
        }

        private void Execute_LogOut()
        {
            User user = MainWindow.LogInUser;
            user.Id = -1;
            MainWindow mw = new MainWindow();
            mw.Show();
            _window.Close();
        }

        private bool CanExecute_Command()
        {
            return true;
        }

        private void Execute_Confrim()
        {
            SelectedReservation.Status = Domain.Models.Enums.GuestRatingStatus.Rated;
            _reservationService.Update(SelectedReservation);
            GuestRating ratingGuest = new GuestRating(SelectedReservation, Comment, Cleanness, FollowingRules, Noise, Damage, Timeliness);
            _guestRatingService.Create(ratingGuest);
            _window.Close();

            //OBRISATI KASNIJE KAD URADIS NORMALNO
            OwnerRating ratingOwner = _ownerRatingService.GetByIdReservation(SelectedReservation.Id);
            if (ratingOwner == null)
            {
                MessageBox.Show("Korisnik Vas jos uvek nije ocenio");
            }
            else
            {
                MessageBox.Show(ratingOwner.ToString());
            }
        }


        private bool CanExecute_Confirm()
        {
            return !string.IsNullOrEmpty(Comment) && SelectedReservation != null;
        }

        }

    }
