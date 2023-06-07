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
    public class ReservationPostponementOverviewViewModel
    {
        public Owner LogInOwner { get; set; }
        public ObservableCollection<ReservationPostponement> ReservationPostponements { get; set; }
        private readonly IReservationPostponementService _reservationPostponementService;
        private readonly IReservationService _reservationService;
        public ReservationPostponement SelectedPostponements { get; set; }
        public bool CkeckAprove { get; set; }
        public bool CkeckReject { get; set; }
        
        public RelayCommand AproveCommand { get; set; }
        public RelayCommand RejectCommand { get; set; }
        public RelayCommand NotificationCommand { get; set; }
        public ReservationPostponementOverviewViewModel(Owner owner)
        {
            _reservationService = InjectorService.CreateInstance<IReservationService>();
            _reservationPostponementService = InjectorService.CreateInstance<IReservationPostponementService>();

            InitCommand();
            Init(owner);
        }

        private void InitCommand()
        {
            AproveCommand = new RelayCommand(param => Execute_Aprove(), param => CanExecute());
            RejectCommand = new RelayCommand(param => Execute_Reject(), param => CanExecute());
            NotificationCommand = new RelayCommand(param => Execute_Notification(), param => CanExecute_Notification());
        }
        private void Init(Owner owner)
        {
            CkeckReject = false;
            CkeckAprove = false;
            LogInOwner = owner;
            ReservationPostponements = new ObservableCollection<ReservationPostponement>(_reservationPostponementService.GetAllByOwnerIdAhead(LogInOwner.Id));
        }

        private void Execute_Aprove()
        {
            SelectedPostponements.Status = Domain.Models.Enums.ReservationPostponementStatus.Approved;
            SelectedPostponements.Reservation.StartDate = SelectedPostponements.StartDate;
            SelectedPostponements.Reservation.EndDate = SelectedPostponements.EndDate;
            _reservationService.Update(SelectedPostponements.Reservation);
            _reservationPostponementService.Update(SelectedPostponements);
        }

        private void Execute_Reject()
        {
            SelectedPostponements.Status = Domain.Models.Enums.ReservationPostponementStatus.Rejected;

            AddComment addComment = new AddComment(SelectedPostponements);
            addComment.Show();

            _reservationPostponementService.Update(SelectedPostponements);
        }

        private bool CanExecute()
        {
            if(SelectedPostponements == null)
            {
                return false;
            }
            return true;
        }

        public void Update()
        {
            ReservationPostponements.Clear();
            foreach (var r in _reservationPostponementService.GetAllByOwnerIdAhead(LogInOwner.Id))
            {
                ReservationPostponements.Add(r);
            }
        }

        private bool CanExecute_Notification()
        {
            if (SelectedPostponements == null)
            {
                return false;
            }
            return true;
        }
        private void Execute_Notification()
        {
            bool freeDate = _reservationService.IsDateFree(SelectedPostponements.Reservation.Accommodation.Id, SelectedPostponements.EndDate) && _reservationService.IsDateFree(SelectedPostponements.Reservation.Accommodation.Id, SelectedPostponements.StartDate);
            if (freeDate)
            {
                MessageBox.Show("Date is free", "Notification");
            }
            else
            {
                MessageBox.Show("Date is taken", "Notification");
            }
        }
    }
}
