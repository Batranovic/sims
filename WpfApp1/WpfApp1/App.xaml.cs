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
        public OwnerController OwnerController {  get; set; }
        public GuestController GuestController { get; set; }
        public TouristController TouristController { get; set; }
        public LocationController LocationController { get; set; }
        public AccommodationController AccommodationController { get; set; }
        public ReservationController ReservationController { get; set; }
        public GuestRatingController RatingGuestController { get; set; }
        public RatingGuestController RatingGuestController { get; set; }

        public RatingTourAndGuideController RatingTourAndGuideController { get; set; }
        public TourBookingController TourBookingController { get; set; }
        public TourEventController TourEventController { get; set; }    
        public ImageController ImageController { get; set; }
        public TourController TourController { get; set; }
        public RatingOwnerController RatingOwnerController { get; set; }
       

        public VoucherController VoucherController { get; set; }

       
        public App()
        {
            TouristController = new TouristController();
            GuestController = new GuestController();
            OwnerController = new OwnerController();
            LocationController = new LocationController();
            AccommodationController = new AccommodationController();
            ImageController = new ImageController();
            ReservationController = new ReservationController();    
            RatingGuestController = new GuestRatingController();
            RatingOwnerController = new RatingOwnerController();

            AccommodationDAO.GetInstance().OwnerDAO = OwnerDAO.GetInsatnce();

            AccommodationDAO.GetInstance().BindLocation();
            AccommodationDAO.GetInstance().BindOwner();
            AccommodationDAO.GetInstance().BindImage();
            ReservationDAO.GetInstance().BindAccommodation();
            ReservationDAO.GetInstance().BindGuest();
            RatingOwnerDAO.GetInstance().BindReservation();
            GuestRatingDAO.GetInstance().BindReservation();
            OwnerDAO.GetInsatnce().BindRating();
            OwnerDAO.GetInsatnce().CalculateAverageRating();
            OwnerDAO.GetInsatnce().SetKind();
         

            TourDAO.GetInstance().BindLocation();
            TourBookingDAO.GetInstance().BindTourEvent();
            TourEventDAO.GetInstance().BindTour();
            RatingTourAndGuideDAO.GetInstance().BindTourBooking();

            //VoucherDAO.GetInsatnce();

            TourBookingController = new TourBookingController();
            TourController = new TourController();
            TourEventController = new TourEventController();
            RatingTourAndGuideController = new RatingTourAndGuideController();
            VoucherController = new VoucherController();


        }

      

    }
}
