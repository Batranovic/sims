using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Security.AccessControl;
using System.Threading.Tasks;
using System.Windows;
using WpfApp1.Controller;
using WpfApp1.Model;
using WpfApp1.Repository;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public TouristController TouristController { get; set; }
        public TourBookingController TourBookingController { get; set; }
        public TourEventController TourEventController { get; set; }    
        public TourController TourController { get; set; }
  
        public App()
        {
            TouristController = new TouristController();
     /*
            AccommodationRepository.GetInstance().BindLocation();
            AccommodationRepository.GetInstance().BindOwner();
            AccommodationRepository.GetInstance().BindImage();
            ReservationRepository.GetInstance().BindAccommodation();
            ReservationRepository.GetInstance().BindGuest();
            OwnerRatingRepository.GetInstance().BindReservation();
            GuestRatingRepository.GetInstance().BindReservation();
            OwnerRepository.GetInsatnce().BindRating();
            OwnerRepository.GetInsatnce().CalculateAverageRating();
            OwnerRepository.GetInsatnce().SetKind();
            ReservationPostponementRepository.GetInstance().BindReservation();
     */
            TourRepository.GetInstance().BindLocation();
            TourBookingRepository.GetInstance().BindTourEvent();
            TourEventRepository.GetInstance().BindTour();

            TourBookingController = new TourBookingController();
            TourController = new TourController();
            TourEventController = new TourEventController();


        }

      

    }
}
