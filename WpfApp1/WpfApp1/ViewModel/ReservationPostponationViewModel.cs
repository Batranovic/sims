using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfApp1.Commands;
using WpfApp1.Domain.Models;
using WpfApp1.Domain.ServiceInterfaces;
using WpfApp1.Service;

namespace WpfApp1.ViewModel
{
    public partial class ReservationPostponationViewModel : ViewModelBase
    {
        
        public event PropertyChangedEventHandler PropertyChanged;

        private readonly IReservationPostponementService _reservationPostponementService;
        public ReservationPostponement ReservationPostponement { get; set; }

        // public DateTime StartDateNew { get; set; }
        // public DateTime EndDateNew { get; set; }

        private Window _window;

        public RelayCommand ConfirmCommand { get; set; }

        public RelayCommand CancelCommand { get; set; }

        public RelayCommand RejectCommand { get; set; }
        public ReservationPostponationViewModel(Reservation reservation)
        {
            _window = Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.Name == "ReservationPostponationName");
            _reservationPostponementService = InjectorService.CreateInstance<IReservationPostponementService>();
            StartedDay = DateTime.Now;
            EndedDay = DateTime.Now;
            ReservationPostponement = new ReservationPostponement();
            ReservationPostponement.Reservation = reservation;
            ReservationPostponement.Reservation.Id = reservation.Id;

            ConfirmCommand = new RelayCommand(param => Execute_Confirm(), param => CanExecute());
            CancelCommand = new RelayCommand(param => Execute_Cancel(), param => CanExecute());
        }

        private DateTime _startedDay;
        public DateTime StartedDay
        {
            get => _startedDay;
            set
            {
                if (_startedDay != value)
                {
                    _startedDay = value;
                    OnPropertyChanged();
                }
            }
        }

        private DateTime _endedDay;
        public DateTime EndedDay
        {
            get => _endedDay;
            set
            {
                if (_endedDay != value)
                {
                    _endedDay = value;
                    OnPropertyChanged();
                }
            }
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void Execute_Confirm()
        {
            ReservationPostponement.StartDate = StartedDay;
            ReservationPostponement.EndDate = EndedDay;
            ReservationPostponement.Status = Domain.Models.Enums.ReservationPostponementStatus.Waiting;
            _reservationPostponementService.Create(ReservationPostponement);
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

