using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp.Observer;
using WpfApp1.Commands;
using WpfApp1.Domain.Models;
using WpfApp1.Domain.ServiceInterfaces;
using WpfApp1.Service;
using WpfApp1.Views;

namespace WpfApp1.ViewModel
{
    public class ReservationOverviewViewModel : ViewModelBase, IObserver
    {
        private readonly IReservationService _reservationService;
        private readonly IReservationPostponementService _reservationPostponementService;

        private ReservationPostponement _selected;
        public ReservationPostponement SelectedPostponements
        {
            get => _selected;
            set
            {
                _selected = value;
                AnswerPostCommand.RaiseCanExecuteChanged();
            }
        }
        public Owner LoggedOwner { get; set; }
        public ObservableCollection<Reservation> Reservations { get; set; }
        
        public ObservableCollection<ReservationPostponement> ReservationPostponements { get; set; }

        private string _notification;
        public string NotificationReservation
        {
            get => _notification;
            set
            {
                _notification = value;
                OnPropertyChanged(nameof(NotificationReservation));
            }
        }
        public RelayCommand AnswerPostCommand { get; set; }
        public RelayCommand AproveCommand { get; set; }
        public RelayCommand RejectCommand { get; set; }
        private bool _visibiltyAnswer;
        public bool VisibiltyAnswer
        {
            get => _visibiltyAnswer;
            set
            {
                _visibiltyAnswer = value;
                OnPropertyChanged(nameof(VisibiltyAnswer));
            }
        }

        public ReservationOverviewViewModel(Owner owner)
        {
            _reservationPostponementService = InjectorService.CreateInstance<IReservationPostponementService>();
            _reservationService = InjectorService.CreateInstance<IReservationService>();
            Init(owner);
            InitCommand();
        }

        public void InitCommand()
        {
            AnswerPostCommand = new(param => Execute_VisibilityCommand(), param => CanExecute_VisibilityCommand());
            AproveCommand = new(param => Execute_Aprove(), param => CanExecute());
            RejectCommand = new(param => Execute_Reject(), param => CanExecute());
        }

        public void Init(Owner owner)
        {
            LoggedOwner = owner;
            _reservationService.GetUnratedById(owner.Id);
            Reservations = new (_reservationService.GetAll().FindAll(r => r.Accommodation.Owner.Id == owner.Id));
            ReservationPostponements = new (_reservationPostponementService.GetAllByOwnerIdAhead(LoggedOwner.Id));
        }

        private void Execute_VisibilityCommand()
        {
            bool freeDate = _reservationService.IsDateFree(SelectedPostponements.Reservation.Accommodation.Id, SelectedPostponements.EndDate) && _reservationService.IsDateFree(SelectedPostponements.Reservation.Accommodation.Id, SelectedPostponements.StartDate);
            NotificationReservation = freeDate ? "Date is free" : "Date is taken";
            VisibiltyAnswer = !VisibiltyAnswer;
        }

        private void Execute_Aprove()
        {
            SelectedPostponements.Status = Domain.Models.Enums.ReservationPostponementStatus.Approved;
            SelectedPostponements.Reservation.StartDate = SelectedPostponements.StartDate;
            SelectedPostponements.Reservation.EndDate = SelectedPostponements.EndDate;
            _reservationPostponementService.Update(SelectedPostponements);
            _reservationService.Update(SelectedPostponements.Reservation);
            Update();
            VisibiltyAnswer = false;
        }

        private void Execute_Reject()
        {
            SelectedPostponements.Status = Domain.Models.Enums.ReservationPostponementStatus.Rejected;

            AddComment addComment = new AddComment(SelectedPostponements);
            addComment.Show();

            _reservationPostponementService.Update(SelectedPostponements);
            VisibiltyAnswer = false;
        }

        private bool CanExecute_VisibilityCommand()
        {
            return SelectedPostponements != null;
        }

        private bool CanExecute()
        {
            return true;
        }

        public void Update()
        {
            ReservationPostponements.Clear();
            foreach(ReservationPostponement r in _reservationPostponementService.GetAllByOwnerIdAhead(LoggedOwner.Id))
            {
                ReservationPostponements.Add(r);
            }
            Reservations.Clear();
            foreach(Reservation r in _reservationService.GetAll().FindAll(r => r.Accommodation.Owner.Id == LoggedOwner.Id))
            {
                Reservations.Add(r);
            }
        }
    }
}
