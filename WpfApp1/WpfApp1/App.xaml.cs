using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
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
        public LocationController LocationController { get; set; }
        public AccommodationController AccommodationController { get; set; }

        public TourBookingController TourBookingController { get; set; }

        public TourEventController TourEventController { get; set; }    

        public ImageController ImageController { get; set; }
        public TourController TourController { get; set; }
        public App()
        {
            OwnerRepository = new OwnerRepository();
            LocationDAO locationDAO = new LocationDAO();
            AccommodationDAO accommodationDAO = new AccommodationDAO();
            ImageDAO imageDAO = new ImageDAO();
            accommodationDAO.LocationDAO = locationDAO;
            accommodationDAO.OwnerRepository = OwnerRepository;
            accommodationDAO.BindLocation();
            accommodationDAO.BindOwner();

            TourDAO.GetInstance().LocationDAO = locationDAO;
            TourDAO.GetInstance().BindLocation();

            TourBookingDAO.GetInstance().BindTourEvent();
            TourEventDAO.GetInstance().BindTour();

            TourBookingController = new TourBookingController();
            TourController = new TourController();
            TourEventController = new TourEventController();


            ImageController = new ImageController(imageDAO);

            LocationController = new LocationController(locationDAO);

            AccommodationController = new AccommodationController(accommodationDAO);

        }

    }
}
