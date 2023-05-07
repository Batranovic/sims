using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfApp1.Commands;
using WpfApp1.Domain.Models;
using WpfApp1.Domain.ServiceInterfaces;
using WpfApp1.Service;

namespace WpfApp1.ViewModel
{
    public class AddCommentViewModel
    {
        public string Comment { get; set; }
        private readonly IReservationPostponementService _reservationPostponementService;
        private Window _window;
        public ReservationPostponement ReservationPostponement { get; set; }
        public RelayCommand ConfrimCommand { get; set; }
        public RelayCommand CancelCommand { get; set;  }
        public AddCommentViewModel(ReservationPostponement reservationPostponement)
        {
            _window = Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.Name == "AddCommentName");
            ReservationPostponement = reservationPostponement;
            _reservationPostponementService = InjectorService.CreateInstance<IReservationPostponementService>();
            InitCommand();
        }

        private void InitCommand()
        {
            ConfrimCommand = new RelayCommand(param => Execute_Confirm(), param => CanExecute());
            CancelCommand = new RelayCommand(param => Execute_Cancel(), param => CanExecute());
        }

        private void Execute_Confirm()
        {
            ReservationPostponement.OwnerComment = Comment;
            _reservationPostponementService.Update(ReservationPostponement);
            _window.Close();
        }

        private void Execute_Cancel()
        {
            _window.Close();
        }

        private bool CanExecute()
        {
            return true;
        }

    }
}
