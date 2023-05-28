using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.draw;
using PdfSharp.Drawing;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Xml.Linq;
using WpfApp1.Commands;
using WpfApp1.Domain.Models;
using WpfApp1.Domain.Models.Enums;
using WpfApp1.Domain.ServiceInterfaces;
using WpfApp1.Service;
using WpfApp1.Views;

namespace WpfApp1.ViewModel
{
    public class TouristProfileViewModel : ViewModelBase
    {
        public ObservableCollection<Voucher> Vouchers { get; set; }
        public ObservableCollection<TourBooking> TourBookings { get; set; }
        private readonly IVoucherService _voucherService;
        private readonly ITourBookingService _tourBookingService;

        public Action CloseAction { get; set; }

        private Voucher _selectedVoucher;
        public Voucher SelectedVoucher
        {
            get => _selectedVoucher;
            set
            {
                if (_selectedVoucher != value)
                {
                    _selectedVoucher = value;
                    OnPropertyChanged("SelectedVoucher");
                }
            }
        }
        public TouristProfileViewModel() {
            _voucherService = InjectorService.CreateInstance<IVoucherService>();
            _tourBookingService = InjectorService.CreateInstance<ITourBookingService>();

            Vouchers = new ObservableCollection<Voucher>(_voucherService.VoucherForTourist(MainWindow.LogInUser.Id));
            TourBookings = new ObservableCollection<TourBooking>(_tourBookingService.GetTourBookingsForTourist(MainWindow.LogInUser.Id));

            AllToursCommand = new RelayCommand(Execute_AllTours, CanExecute_Command);
            BookedToursCommand = new RelayCommand(Execute_BookedTours, CanExecute_Command);
            RequestTourCommand = new RelayCommand(Execute_RequestTour, CanExecute_Command);
            RequestListCommand = new RelayCommand(Execute_RequestList, CanExecute_Command);
            LogOutCommand = new RelayCommand(Execute_LogOut, CanExecute_Command);
            DownloadPDFCommand = new RelayCommand(Execute_PDF, CanExecute_Command);
            ShowPopUpCommand = new RelayCommand(Execute_ShowPopUp, CanExecute_Command);
        }

        private bool _isPopupOpen;
        public bool IsPopupOpen
        {
            get { return _isPopupOpen; }
            set { _isPopupOpen = value; OnPropertyChanged(nameof(IsPopupOpen)); }
        }
        private RelayCommand logOutCommand;
        public RelayCommand LogOutCommand
        {
            get => logOutCommand;
            set
            {
                if (value != logOutCommand)
                {
                    logOutCommand = value;
                    OnPropertyChanged();
                }
            }
        }
        private RelayCommand requestListCommand;
        public RelayCommand RequestListCommand
        {
            get => requestListCommand;
            set
            {
                if (value != requestListCommand)
                {
                    requestListCommand = value;
                    OnPropertyChanged();
                }
            }
        }

        private RelayCommand showPopUpCommand;
        public RelayCommand ShowPopUpCommand
        {
            get => showPopUpCommand;
            set
            {
                if (value != showPopUpCommand)
                {
                    showPopUpCommand = value;
                    OnPropertyChanged();
                }
            }
        }
        private void Execute_ShowPopUp(object sender)
        {
            IsPopupOpen = true;
        }
        private void Execute_LogOut(object sender)
        {
            MessageBoxResult result = MessageBox.Show(
                  "   Are you sure you want to log out?\n\n" ,
                  "Exit",
                  MessageBoxButton.YesNo);

            if (result == MessageBoxResult.Yes)
            {
                MainWindow mw = new MainWindow();
                mw.Show();
                CloseAction();
            }
            else if (result == MessageBoxResult.No)
            {

                TouristProfile parentWindow = Application.Current.Windows.OfType<TouristProfile>().FirstOrDefault(window => window.Name == "TouristProfile");
                if (parentWindow != null)
                {
                    parentWindow.Close();
                }

            }
        }

        private void Execute_PDF(object sender)
        {
            // Create a new PDF document
            Document document = new Document();

            // Set up the output stream
            string filePath = "tour_booking_report.pdf";
            FileStream fileStream = new FileStream(filePath, FileMode.Create);
            PdfWriter writer = PdfWriter.GetInstance(document, fileStream);

            // Open the document
            document.Open();

            // Add a title to the document
            Font titleFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 16);
            Paragraph title = new Paragraph("Tour Booking Report", titleFont);
            title.Alignment = Element.ALIGN_CENTER;
            document.Add(title);

            // Get the tour booking data
            List<TourBooking> tourBookings = _tourBookingService.GetTourBookingsForTourist(MainWindow.LogInUser.Id);

            // Add tour booking data to the document
            Font contentFont = FontFactory.GetFont(FontFactory.HELVETICA, 12);
            foreach (TourBooking booking in tourBookings)
            {
                // Add spacing between entries
                document.Add(new Paragraph("\n"));

                // Add state and city
                Chunk locationChunk = new Chunk("Location: " + booking.TourEvent.Tour.Location.State + " - " + booking.TourEvent.Tour.Location.City);
                locationChunk.Font = contentFont;
                document.Add(new Paragraph(locationChunk));

                // Add start date
                Chunk startDateChunk = new Chunk("Start Date: " + booking.TourEvent.StartTime.ToString("dd-MM-yyyy"));
                startDateChunk.Font = contentFont;
                document.Add(new Paragraph(startDateChunk));

                // Add description
                Chunk descriptionChunk = new Chunk("Description: " + booking.TourEvent.Tour.Description);
                descriptionChunk.Font = contentFont;
                document.Add(new Paragraph(descriptionChunk));

                // Add language
                Chunk languageChunk = new Chunk("Language: " + booking.TourEvent.Tour.Languages);
                languageChunk.Font = contentFont;
                document.Add(new Paragraph(languageChunk));
                // Add number of people
                Chunk guestsChunk = new Chunk("Number of People: " + booking.NumberOfGuests);
                guestsChunk.Font = contentFont;
                document.Add(new Paragraph(guestsChunk));

                LineSeparator separator = new LineSeparator();
                document.Add(new Chunk(separator));
            }

            // Close the document
            document.Close();

            // Open the PDF document with the default application
            Process.Start(new ProcessStartInfo(filePath) { UseShellExecute = true });
        }



        private void Execute_RequestTour(object sender)
        {
            RequestNewTours requestNewTour = new RequestNewTours();
            requestNewTour.Show();
            CloseAction();
        }
        private void Execute_RequestList(object sender)
        {
            TourRequest tourRequestList = new TourRequest();
            tourRequestList.Show();
            CloseAction();


        }

        private bool CanExecute_Command(object parameter)
        {
            return true;
        }
        private void Execute_AllTours(object sender)
        {
            TourSearchAndOverview tourSearch = new TourSearchAndOverview();
            tourSearch.Show();
            CloseAction();
        }
        private void Execute_BookedTours(object sender)
        {

            BookedTours bookedTours = new BookedTours();
            bookedTours.Show();
            CloseAction();

        }
        private RelayCommand requestTourCommand;
        public RelayCommand RequestTourCommand
        {
            get => requestTourCommand;
            set
            {
                if (value != requestTourCommand)
                {
                    requestTourCommand = value;
                    OnPropertyChanged();
                }
            }
        }

        private RelayCommand allToursCommand;
        public RelayCommand AllToursCommand
        {
            get => allToursCommand;
            set
            {
                if (value != allToursCommand)
                {
                    allToursCommand = value;
                    OnPropertyChanged();
                }
            }
        }

        private RelayCommand bookedToursCommand;
        public RelayCommand BookedToursCommand
        {
            get => bookedToursCommand;
            set
            {
                if (value != bookedToursCommand)
                {
                    bookedToursCommand = value;
                    OnPropertyChanged();
                }
            }
        }


        private RelayCommand downloadPDFCommand;
        public RelayCommand DownloadPDFCommand
        {
            get => downloadPDFCommand;
            set
            {
                if (value != downloadPDFCommand)
                {
                    downloadPDFCommand = value;
                    OnPropertyChanged();
                }
            }
        }
    }
}
