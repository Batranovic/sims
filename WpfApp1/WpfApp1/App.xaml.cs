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

        public TourController TourController { get; set; }
        public App()
        {
            OwnerRepository = new OwnerRepository();
            LocationDAO locationDAO = new LocationDAO();
            AccommodationDAO accommodationDAO = new AccommodationDAO();
            accommodationDAO.LocationDAO = locationDAO;
            accommodationDAO.OwnerRepository = OwnerRepository;
            accommodationDAO.BindLocation();
            accommodationDAO.BindOwner();

            TourDAO tourDAO = new TourDAO();
            TourController = new TourController(tourDAO);

            LocationController = new LocationController(locationDAO);

            AccommodationController = new AccommodationController(accommodationDAO);

        }

    }
}
