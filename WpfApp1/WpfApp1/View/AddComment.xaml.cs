using System;
using System.Collections.Generic;
using System.Linq;
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
using WpfApp1.Domain.Models;
using WpfApp1.Service;

namespace WpfApp1.View
{
    /// <summary>
    /// Interaction logic for AddComment.xaml
    /// </summary>
    public partial class AddComment : Window
    {
        public string Comment { get; set; }
        private readonly IReservationPostponementService _reservationPostponementService;
        public ReservationPostponement ReservationPostponement { get; set; }
        public AddComment(ReservationPostponement reservationPostponement)
        {
            InitializeComponent();
            this.DataContext = this;
            ReservationPostponement = reservationPostponement;
            _reservationPostponementService = InjectorService.CreateInstance<ReservationPostponementService>(); 
        }

        private void Confirm(object sender, RoutedEventArgs e)
        {
            ReservationPostponement.OwnerComment = Comment;
            _reservationPostponementService.Update(ReservationPostponement);
            Close();
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
