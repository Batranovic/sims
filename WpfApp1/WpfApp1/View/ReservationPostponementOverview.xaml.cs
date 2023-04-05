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
using WpfApp1.Model;

namespace WpfApp1.View
{
    /// <summary>
    /// Interaction logic for ReservationPostponementOverview.xaml
    /// </summary>
    public partial class ReservationPostponementOverview : Window, IObserver
    {
        public Owner LogInOwner { get; set; }
        public ObservableCollection<ReservationPostponement> ReservationPostponements { get; set; }
        public ReservationPostponementController ReservationPostponementController { get; set; }
        public ReservationPostponement SelectedPostponements { get; set; }

        public bool CkeckAprove { get; set; }
        public bool CkeckReject { get; set; }
        public ReservationPostponementOverview(Owner owner)
        {
            InitializeComponent();
            this.DataContext = this;    

            var app = Application.Current as App;
            ReservationPostponementController = app.ReservationPostponementController;
            ReservationPostponementController.Subscribe(this);

            CkeckReject = false;
            CkeckAprove = false;
            LogInOwner = owner;
            ReservationPostponements = new ObservableCollection<ReservationPostponement>(ReservationPostponementController.GetAllByOwnerIdAhead(LogInOwner.Id));
        }
        private void AprovePostponement(object sender, RoutedEventArgs e)
        {
            SelectedPostponements.Status = Model.Enums.ReservationPostponementStatus.Approved;
            CkeckAprove = true;
            CkeckReject = false;
        }

        private void RejectPostponement(object sender, RoutedEventArgs e)
        {
            SelectedPostponements.Status = Model.Enums.ReservationPostponementStatus.Rejected; 
            CkeckAprove = false;
            CkeckReject = true;
        }

        private void Aprove(object sender, RoutedEventArgs e)
        {
            SelectedPostponements.Status = Model.Enums.ReservationPostponementStatus.Approved;
            ReservationPostponementController.Update(SelectedPostponements);
        }

        private void Reject(object sender, RoutedEventArgs e)
        {
            SelectedPostponements.Status = Model.Enums.ReservationPostponementStatus.Rejected;
            ReservationPostponementController.Update(SelectedPostponements);
        }

       
        public void Update()
        {
            ReservationPostponements.Clear();
            foreach(var r in ReservationPostponementController.GetAllByOwnerIdAhead(LogInOwner.Id))
            {
                ReservationPostponements.Add(r);    
            }
        }

    }
}
