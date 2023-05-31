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
        private readonly ITouristService _touristService;

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

        private DateTime _selectedStartDate { get; set; }
        public DateTime SelectedStartDate
        {
            get => _selectedStartDate;
            set
            {
                if (_selectedStartDate != value)
                {
                    _selectedStartDate = value;
                    OnPropertyChanged("SelectedStartDate");
                }
            }
        }

        private DateTime _selectedEndDate { get; set; }
        public DateTime SelectedEndDate
        {
            get => _selectedEndDate;
            set
            {
                if (_selectedEndDate != value)
                {
                    _selectedEndDate = value;
                    OnPropertyChanged("SelectedEndDate");
                }
            }
        }
        public TouristProfileViewModel() {
            _voucherService = InjectorService.CreateInstance<IVoucherService>();
            _tourBookingService = InjectorService.CreateInstance<ITourBookingService>();
            _touristService = InjectorService.CreateInstance<ITouristService>();
            Vouchers = new ObservableCollection<Voucher>(_voucherService.VoucherForTourist(MainWindow.LogInUser.Id));
            TourBookings = new ObservableCollection<TourBooking>(_tourBookingService.GetTourBookingsForTourist(MainWindow.LogInUser.Id));

            AllToursCommand = new RelayCommand(Execute_AllTours, CanExecute_Command);
            BookedToursCommand = new RelayCommand(Execute_BookedTours, CanExecute_Command);
            RequestTourCommand = new RelayCommand(Execute_RequestTour, CanExecute_Command);
            RequestListCommand = new RelayCommand(Execute_RequestList, CanExecute_Command);
            LogOutCommand = new RelayCommand(Execute_LogOut, CanExecute_Command);
            DownloadPDFCommand = new RelayCommand(Execute_PDF, CanExecute_Command);

            SelectedEndDate = DateTime.Today;
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
            Document document = new Document();

            // Set up the output stream
            string filePath = "tour_booking_report.pdf";
            FileStream fileStream = new FileStream(filePath, FileMode.Create);
            PdfWriter writer = PdfWriter.GetInstance(document, fileStream);

            // Open the document
            document.Open();

            // Add the title and subtitle
            Font titleFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 24);
            Paragraph title = new Paragraph("Tour Report", titleFont);
            title.Alignment = Element.ALIGN_CENTER;
            document.Add(title);

            Font subtitleFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 16);
            Paragraph subtitle = new Paragraph("Your tour bookings", subtitleFont);
            subtitle.Alignment = Element.ALIGN_CENTER;
            document.Add(subtitle);

            // Add a separator
            LineSeparator separator = new LineSeparator();
            document.Add(new Chunk(separator));

            Font infoFont = FontFactory.GetFont(FontFactory.HELVETICA, 12);
            Font boldFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12);
            Paragraph organizedBy = new Paragraph();
            organizedBy.Alignment = Element.ALIGN_LEFT;
            organizedBy.Add(new Chunk("Organized by:\n", boldFont));
            organizedBy.Add(new Chunk("TourAdvisor\n", infoFont));
            organizedBy.Add(new Chunk("Novi Sad, Serbia, 21000\n", infoFont));
            organizedBy.Add(new Chunk("touradvisor@gmail.com", infoFont));
            document.Add(organizedBy);
            // Add spacing after the "Organized by" section
            document.Add(new Paragraph("\n"));

            // Add the "Customer details" section
            Paragraph customerDetails = new Paragraph();
            customerDetails.Alignment = Element.ALIGN_RIGHT;
            customerDetails.Add(new Chunk("Customer details:\n", boldFont));

            // Retrieve the tourist object
            Tourist tourist = _touristService.getName(MainWindow.LogInUser.Id);
            if (tourist != null)
            {
                // Access the name and email properties of the tourist
                string touristName = tourist.Name + " " +  tourist.Surname;
                string touristEmail = tourist.Email;

                // Add the tourist name and email to the "Customer details" section
                customerDetails.Add(new Chunk(touristName + "\n", infoFont));
                customerDetails.Add(new Chunk(touristEmail + "\n", infoFont));
            }
            // Add the "Customer details" section above the "From: Start Date" section
            document.Add(customerDetails);

            // Add spacing before the "From: Start Date" section
            document.Add(new Paragraph("\n"));

            // Add the date range information
            Paragraph dateRange = new Paragraph();
            dateRange.Add(new Chunk("From: ", boldFont));
            dateRange.Add(new Chunk(SelectedStartDate.ToString("dd-MM-yyyy"), infoFont));
            dateRange.Add(new Chunk("\nTo: ", boldFont));
            dateRange.Add(new Chunk(SelectedEndDate.ToString("dd-MM-yyyy"), infoFont));
            document.Add(dateRange);

            // Add spacing after the date range
            document.Add(new Paragraph("\n"));

            // Add a new paragraph of text
            Paragraph paragraph = new Paragraph("Your tour booking report for the selected date range:", infoFont);
            document.Add(paragraph);

            // Add two rows of space
            document.Add(new Paragraph("\n\n"));

            // Create the table
            PdfPTable table = new PdfPTable(4);
            table.WidthPercentage = 100;

            // Set the column widths
            float[] columnWidths = { 2f, 2f, 2f, 2f };
            table.SetWidths(columnWidths);

            // Add table headers
            PdfPCell headerCell = new PdfPCell();
            headerCell.BackgroundColor = BaseColor.LIGHT_GRAY;
            headerCell.Padding = 5;
            headerCell.HorizontalAlignment = Element.ALIGN_CENTER;
            headerCell.VerticalAlignment = Element.ALIGN_MIDDLE;

            headerCell.Phrase = new Phrase("Location", infoFont);
            table.AddCell(headerCell);

            headerCell.Phrase = new Phrase("Start Date", infoFont);
            table.AddCell(headerCell);

            headerCell.Phrase = new Phrase("Language", infoFont);
            table.AddCell(headerCell);

            headerCell.Phrase = new Phrase("Number of People", infoFont);
            table.AddCell(headerCell);

            // Get the tour booking data for the selected date range
            List<TourBooking> tourBookings = TourBookings.Where(booking =>
                booking.TourEvent.StartTime >= SelectedStartDate && booking.TourEvent.StartTime <= SelectedEndDate).ToList();

            // Add tour booking data to the table
            foreach (TourBooking booking in tourBookings)
            {
                table.AddCell(new PdfPCell(new Phrase(booking.TourEvent.Tour.Location.State + " - " + booking.TourEvent.Tour.Location.City, infoFont)));
                table.AddCell(new PdfPCell(new Phrase(booking.TourEvent.StartTime.ToString("dd-MM-yyyy"), infoFont)));
                table.AddCell(new PdfPCell(new Phrase(booking.TourEvent.Tour.Languages, infoFont)));
                table.AddCell(new PdfPCell(new Phrase(booking.NumberOfGuests.ToString(), infoFont)));
            }

            // Add the table to the document
            document.Add(table);

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
