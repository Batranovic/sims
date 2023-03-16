using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
using WpfApp1.Controller;
using WpfApp1.Model;
using WpfApp1.Model.Enums;

namespace WpfApp1.View
{
    /// <summary>
    /// Interaction logic for AccommodationView.xaml
    /// </summary>
    public partial class AccommodationView : Window, INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;
        //public LocationController LocationController { get; set; }

        private readonly AccommodationController _accommodationController;
        public ObservableCollection<AccommodationKind> AccommodationKind { get; set; }
        public ObservableCollection<Accommodation> Accommodations { get; set; }

        

        public AccommodationView()
        {
            InitializeComponent();
            DataContext = this;

            var app = Application.Current as App;
            _accommodationController = app.AccommodationController;
            var kind = Enum.GetValues(typeof(AccommodationKind)).Cast<AccommodationKind>();
            AccommodationKind = new ObservableCollection<AccommodationKind>(kind);

            Accommodations = new ObservableCollection<Accommodation>(_accommodationController.GetAll());

        }

        public void Search(ObservableCollection<Accommodation> observe, string name, string city, string state, List<string> accommodationKind, string numberOfGuests, string minNumDaysOfReservation)
        {
            observe.Clear();

            foreach (Accommodation accommodation in _accommodationController.GetAll())
            {
                if ((accommodation.Name.ToLower().Contains(name.ToLower()) || name.Equals(""))
                    && (accommodation.Location.State.ToLower().Contains(state.ToLower()) || state.Equals(""))
                        && (isEnumTrue(accommodation, accommodationKind))
                           && (accommodation.Location.City.ToLower().Contains(city.ToLower()) || city.Equals(""))
                              && (numberOfGuests.Equals("") || int.Parse(numberOfGuests) <= accommodation.MaxGuests))
                {
                    if (minNumDaysOfReservation.Equals("") || int.Parse(minNumDaysOfReservation) >= accommodation.MinResevation)
                    {
                        observe.Add(accommodation);
                    }
                }
            }

        }

        public Boolean isEnumTrue(Accommodation accommodation, List<String> accommodationTypes)
        {
            Boolean info = false;

            if (accommodationTypes.Count == 0)
            {
                return true;
            }
            foreach (String type in accommodationTypes)
            {
                if (accommodation.AccommodationKind.ToString().Contains(type))
                {
                    info = true;
                    break;
                }
            }
            return info;
        }

        

    }
}
