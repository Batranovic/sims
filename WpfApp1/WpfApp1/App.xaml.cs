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
        public OwnerRepository OwnerRepository {  get; set; }
        public GuestRepository GuestRepository { get; set; }
        public LocationController LocationController { get; set; }
        public AccommodationController AccommodationController { get; set; }
        public ReservationController ReservationController { get; set; }
        public RatingGuestController RatingGuestController { get; set; }

        public TourBookingController TourBookingController { get; set; }

        public TourEventController TourEventController { get; set; }    

        public ImageController ImageController { get; set; }
        public TourController TourController { get; set; }

       
        public App()
        {

            LocationController = new LocationController();
            AccommodationController = new AccommodationController();
            ImageController = new ImageController();
            ReservationController = new ReservationController();    
            RatingGuestController = new RatingGuestController();
            
            ReservationDAO.GetInstance().BindAccommodation();
            AccommodationDAO.GetInstance().BindLocation();
            AccommodationDAO.GetInstance().BindOwner();
            AccommodationDAO.GetInstance().BindImage();
            ReservationDAO.GetInstance().BindGuest();
            RatingGuestDAO.GetInstance().BindReservation();


            TourDAO.GetInstance().BindLocation();
            TourBookingDAO.GetInstance().BindTourEvent();
            TourEventDAO.GetInstance().BindTour();

            TourBookingController = new TourBookingController();
            TourController = new TourController();
            TourEventController = new TourEventController();


        }

      

    }
}
