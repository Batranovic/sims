using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using WpfApp.Observer;
using WpfApp1.Controller;
using WpfApp1.Domain.ServiceInterfaces;
using WpfApp1.Model;
using WpfApp1.Service;

namespace WpfApp1.View
{
    /// <summary>
    /// Interaction logic for ReservationPostponementOverview.xaml
    /// </summary>
    public partial class ReservationPostponementOverview : Window, IObserver
    {
        public Owner LogInOwner { get; set; }
        public ObservableCollection<ReservationPostponement> ReservationPostponements { get; set; }
        private readonly IReservationPostponementService _reservationPostponementService;
        private readonly IReservationService _reservationService;
        public ReservationPostponement SelectedPostponements { get; set; }

        public bool CkeckAprove { get; set; }
        public bool CkeckReject { get; set; }
        public ReservationPostponementOverview(Owner owner)
        {
            InitializeComponent();
            this.DataContext = this;    

            _reservationService = InjectorService.CreateInstance<IReservationService>(); 
            _reservationPostponementService = InjectorService.CreateInstance<IReservationPostponementService>();
            _reservationPostponementService.Subscribe(this);

            CkeckReject = false;
            CkeckAprove = false;
            LogInOwner = owner;
            ReservationPostponements = new ObservableCollection<ReservationPostponement>(_reservationPostponementService.GetAllByOwnerIdAhead(LogInOwner.Id));
        }
    

        private void Aprove(object sender, RoutedEventArgs e)
        {
            SelectedPostponements.Status = Model.Enums.ReservationPostponementStatus.Approved;
            SelectedPostponements.Reservation.StartDate = SelectedPostponements.StartDate;
            SelectedPostponements.Reservation.EndDate = SelectedPostponements.EndDate;
            _reservationService.Update(SelectedPostponements.Reservation);
            _reservationPostponementService.Update(SelectedPostponements);
        }

        private void Reject(object sender, RoutedEventArgs e)
        {
            SelectedPostponements.Status = Model.Enums.ReservationPostponementStatus.Rejected;

            AddComment addComment = new AddComment(SelectedPostponements);
            addComment.Show();

            _reservationPostponementService.Update(SelectedPostponements);
        }

       
        public void Update()
        {
            ReservationPostponements.Clear();
            foreach(var r in _reservationPostponementService.GetAllByOwnerIdAhead(LogInOwner.Id))
            {
                ReservationPostponements.Add(r);    
            }
        }

    }
}
