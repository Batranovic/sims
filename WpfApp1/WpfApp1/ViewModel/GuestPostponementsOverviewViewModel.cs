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

namespace WpfApp1.ViewModel
{
    public partial class GuestPostponementsOverviewViewModel
    {
        public Guest LogInGuest { get; set; }

        public ObservableCollection<ReservationPostponement> ReservationPostponements { get; set; }

        private readonly IReservationPostponementService _reservationPostponementService;

        private Window _window;

        public RelayCommand CloseWindowCommand { get; set; }

        public RelayCommand UpdateCommand { get; set; }

        public RelayCommand RejectCommand { get; set; }
        public GuestPostponementsOverviewViewModel(Guest guest)
        {
            _window = Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.Name == "GuestPostponementsOverviewName");
            _reservationPostponementService = InjectorService.CreateInstance<IReservationPostponementService>();
            LogInGuest = guest;
            ReservationPostponements = new ObservableCollection<ReservationPostponement>(_reservationPostponementService.GetAllByGuestId(LogInGuest.Id));

            CloseWindowCommand = new RelayCommand(param => Execute_CloseWindow(), param => CanExecute());
            UpdateCommand = new RelayCommand(param => Execute_Update(), param => CanExecute());
        }

        private void Execute_CloseWindow()
        {
            this.Execute_Reject();
        }

        public void Execute_Update()
        {
            ReservationPostponements.Clear();
            foreach (var r in _reservationPostponementService.GetAllByOwnerIdAhead(LogInGuest.Id))
            {
                ReservationPostponements.Add(r);
            }
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
