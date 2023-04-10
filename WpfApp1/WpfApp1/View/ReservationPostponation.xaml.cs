using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
using WpfApp1.Domain.ServiceInterfaces;
using WpfApp1.Model;
using WpfApp1.Service;

namespace WpfApp1.View
{
    /// <summary>
    /// Interaction logic for ReservationPostponation.xaml
    /// </summary>
    public partial class ReservationPostponation : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private readonly IReservationPostponementService _reservationPostponementService;

        public ReservationPostponement ReservationPostponement { get; set; }

       // public DateTime StartDateNew { get; set; }
       // public DateTime EndDateNew { get; set; }
        public ReservationPostponation(Reservation reservation)
        {
            InitializeComponent();
            this.DataContext = this;

            _reservationPostponementService = InjectorService.CreateInstance<IReservationPostponementService>();
            StartedDay = DateTime.Now;
            EndedDay = DateTime.Now;
            ReservationPostponement = new ReservationPostponement();
            ReservationPostponement.Reservation = reservation;
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

        private void Confirm(object sender, RoutedEventArgs e)
        {
            ReservationPostponement.StartDate = StartedDay;
            ReservationPostponement.EndDate = EndedDay;
            ReservationPostponement.Status = Model.Enums.ReservationPostponementStatus.Waiting;
            _reservationPostponementService.Create(ReservationPostponement);
            this.Close();
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
