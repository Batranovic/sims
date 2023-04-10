using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Security.AccessControl;
using System.Threading.Tasks;
using System.Windows;
using WpfApp1.Controller;
using WpfApp1.Models;
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

        public RatingTourAndGuideController RatingTourAndGuideController { get; set; }
        public TourBookingController TourBookingController { get; set; }
        public TourEventController TourEventController { get; set; }    
        public ImageController ImageController { get; set; }
        public TourController TourController { get; set; }
        public RatingOwnerController RatingOwnerController { get; set; }

        public TourPointController TourPointController { get; set; }
       
        public NotificationController NotificationController { get; set; }
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

            AccommodationRepository.GetInstance().OwnerDAO = OwnerRepository.GetInsatnce();

            AccommodationRepository.GetInstance().BindLocation();
            AccommodationRepository.GetInstance().BindOwner();
            AccommodationRepository.GetInstance().BindImage();
            ReservationRepository.GetInstance().BindAccommodation();
            ReservationRepository.GetInstance().BindGuest();
            RatingOwnerRepository.GetInstance().BindReservation();
            GuestRatingRepository.GetInstance().BindReservation();
            OwnerRepository.GetInsatnce().BindRating();
            OwnerRepository.GetInsatnce().CalculateAverageRating();
            OwnerRepository.GetInsatnce().SetKind();
         

            TourRepository.GetInstance().BindLocation();
            TourBookingRepository.GetInstance().BindTourEvent();
            TourBookingRepository.GetInstance().BindVoucher();
            TourEventRepository.GetInstance().BindTour();
            RatingTourAndGuideRepository.GetInstance().BindTourBooking();
            NotificationRepository.GetInstance().BindTourBooking();
            TourPointRepository.GetInstance().BindTourPointTour();

            VoucherRepository.GetInstance();

            TourBookingController = new TourBookingController();
            TourController = new TourController();
            TourEventController = new TourEventController();
            RatingTourAndGuideController = new RatingTourAndGuideController();
            VoucherController = new VoucherController();
            TourPointController = new TourPointController();
            NotificationController = new NotificationController();


        }

      

    }
}
