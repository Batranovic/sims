﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Sources;
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

        private readonly AccommodationController accommodationController;
        public ObservableCollection<AccommodationKind> AccommodationKind { get; set; }

        public AccommodationKind SelectedAccommodationKind { get; set; }
        public ObservableCollection<Accommodation> Accommodations { get; set; }

        public Accommodation SelectedAccommodation { get; set; }

        public LocationController LocationController { get; set; }

        public ObservableCollection<string> States { get; set; }

        public ObservableCollection<string> Cities { get; set; }

        public string SelectedCity { get; set; }
        

        public AccommodationView()
        {
            InitializeComponent();
            this.DataContext = this;

            var app = Application.Current as App;
            accommodationController = app.AccommodationController;
            LocationController= app.LocationController; 

            var kind = Enum.GetValues(typeof(AccommodationKind)).Cast<AccommodationKind>();
            AccommodationKind = new ObservableCollection<AccommodationKind>(kind);

            //States = new ObservableCollection<string>(LocationController.GetStates());
            //Cities = new ObservableCollection<string>();

            cbChoseState.Items.Add("");
            cbChoseCity.Items.Add("");
            cbChoseType.Items.Add("");

            foreach(var state in LocationController.GetStates())
            {
                cbChoseState.Items.Add(state.ToString());
            }

            foreach(var type in AccommodationKind)
            {
                cbChoseType.Items.Add(type.ToString());
            }


            Accommodations = new ObservableCollection<Accommodation>(accommodationController.GetAll());

        }


        
        private string _state;

        public string SelectedState
        {
            get => _state;
            set
            {
                if(_state != value)
                {
                    _state = value;
                    OnPropertyChanged(_state);
                }
            }
        }

        private string _name;

        public string NameE
        {
            get => _name;
            set
            {
                if(value != _name)
                {
                    _name = value;
                    OnPropertyChanged("NameE");
                }
            }
        }

        private int _maxGuests;

        public int MaxGuests
        {
            get => _maxGuests;
            set
            {
                if (_maxGuests != value)
                {
                    _maxGuests = value;
                    OnPropertyChanged("MaxGuests");
                }
            }
        }

        private int _reservationDays;

        public int ReservationDays
        {
            get => _reservationDays;
            set
            {
                if (_reservationDays != value)
                {
                    _reservationDays = value;
                    OnPropertyChanged("ReservationDays");
                }
            }
        }


        

        private void Search(object sender, RoutedEventArgs e)
        {
            Accommodations.Clear();
            foreach(Accommodation a in accommodationController.SearchAccommodation(NameE, SelectedCity, SelectedState, SelectedAccommodationKind.ToString(), MaxGuests, ReservationDays))
            {
                Accommodations.Add(a);
            }

        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void ChosenState(object sender, SelectionChangedEventArgs e)
        {
            SelectedState = (string)cbChoseState.SelectedItem;
            //Cities.Clear();
            foreach (string city in LocationController.GetCitiesFromStates(SelectedState))
            {
            //    Cities.Add(city);
                  cbChoseCity.Items.Add(city);
            }
        }


        private void ShowImage(object sender, RoutedEventArgs e)
        {
            if(SelectedAccommodation != null)
            { 
                ImageView p = new ImageView(SelectedAccommodation);
                p.Show();
            }
        }
    }
}
