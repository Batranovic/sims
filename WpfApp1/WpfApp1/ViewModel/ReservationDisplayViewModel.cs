using iTextSharp.text.pdf.draw;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Schema;
using WpfApp1.Commands;
using WpfApp1.Domain.Models;
using WpfApp1.Domain.ServiceInterfaces;
using WpfApp1.Service;
using WpfApp1.Views;

namespace WpfApp1.ViewModel
{
    public class ReservationDisplayViewModel : ViewModelBase
    {
        private readonly IReservationService _reservationService;
        private readonly IReservationPostponementService _reservationPostponementService;
        public ObservableCollection<Reservation> Reservations { get; set; }

        private Reservation _selectedReservation;

        private DateTime _start;
        private DateTime _end;

        private string _color;

        private Dictionary<int, string> _options = new()
        {
            { 0,"False"},
            { 1,"False" }

        };

        public Dictionary<int, string> Options
        {
            get => _options;
            set
            {
                _options = value;
                OnPropertyChanged();
            }
        }

        public string Color
        {
            get => _color;
            set
            {
                _color = value;
                OnPropertyChanged(nameof(Color));
            }
        }
        public Reservation SelectedReservation
        {
            get => _selectedReservation;
            set
            {
                _selectedReservation = value;
                OnPropertyChanged(nameof(SelectedReservation));
                ReservationPostponementCommand.RaiseCanExecuteChanged();
                CancelReservationCommand.RaiseCanExecuteChanged();
            }
        }

        public DateTime Start
        {
            get => _start;
            set
            {
                if (_start != value)
                {
                    _start = value;
                    OnPropertyChanged(nameof(Start));
                }
            }
        }

        public DateTime End
        {
            get => _end;
            set
            {
                if (_end != value)
                {
                    _end = value;
                    OnPropertyChanged(nameof(End));
                }
            }
        }

        private Window _window;

        public RelayCommand OwnerRatingCommand { get; set; }

        public RelayCommand ReservationPostponementCommand { get; set; }

        public RelayCommand CancelReservationCommand { get; set; }

        public RelayCommand UpdateCommand { get; set; }

        public RelayCommand RejectCommand { get; set; }

        public RelayCommand GenerateReportCommand { get; set; }

        public RelayCommand SelectedOptionCommand { get; set; }

        public Guest LogInGuest { get; set; }
        public ReservationDisplayViewModel(Guest guest)
        {
            _window = Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.Name == "ReservationViewName");
            _reservationService = InjectorService.CreateInstance<IReservationService>();
            _reservationPostponementService = InjectorService.CreateInstance<IReservationPostponementService>();
            //_reservationService.Subscribe(this);

            LogInGuest = guest;
            Reservations = new ObservableCollection<Reservation>(_reservationService.GetFutureReseravtions(LogInGuest.Id));
            Color = "#FAEDCD";

            OwnerRatingCommand = new RelayCommand(param => Execute_OwnerRating(), param => CanExecuteOwnerRating());
            ReservationPostponementCommand = new RelayCommand(param => Execute_ReservationPostponement(), param => CanExecuteReservationPostponement());
            CancelReservationCommand = new RelayCommand(param => Execute_CancelReservation(), param => CanExecuteCancelReservation());
            SelectedOptionCommand = new RelayCommand(Execute_SelectedOption, CanSelectedOptionCommandExecute);
            UpdateCommand = new RelayCommand(param => Execute_Update(), param => CanExecute());
            //GenerateReportCommand = new RelayCommand(param => Execute_GenerateReportCommand(), param => CanExecuteGenerateReportCommand());

        }

        private bool CanSelectedOptionCommandExecute(object parameter)
        {

            if (parameter == null || !int.TryParse(parameter.ToString(), out int index))
            {
                return false;
            }

            return index >= 0 && index <= 5;
        }

        private void Execute_SelectedOption(object parameter)
        {
            int index = int.Parse(parameter.ToString());
            for (int i = 0; i <= 4; ++i)
            {
                if (i == index)
                {
                    Options[i] = "True";
                }
                else
                {
                    Options[i] = "False";
                }

                OnPropertyChanged(nameof(Options));
            }
        }

        private void Execute_OwnerRating()    //ime
        {

            AccommodationAndOwnerRating acoommodationAndOwnerRating = new AccommodationAndOwnerRating(SelectedReservation);
            acoommodationAndOwnerRating.Show();
        }

        private bool CanExecuteOwnerRating()
        {
            return SelectedReservation != null && SelectedReservation.GuestReservationStatus == Domain.Domain.Models.Enums.AccommodationAndOwnerRatingStatus.Unrated;
        }


        public void Execute_ReservationPostponement()
        {


            ReservationPostponation reservationPostponation = new ReservationPostponation(SelectedReservation);
            reservationPostponation.Show();
        }

        private bool CanExecuteReservationPostponement()
        {
            return SelectedReservation != null;
        }

        public void Execute_CancelReservation()
        {

            _reservationService.Delete(SelectedReservation);
            Reservations.Clear();
            foreach(Reservation reservation in _reservationService.GetFutureReseravtions(LogInGuest.Id))
            {
                Reservations.Add(reservation); 
            }

        }

        private bool CanExecuteCancelReservation()
        {

            return SelectedReservation != null && !(SelectedReservation.StartDate < DateTime.Now.AddDays(-SelectedReservation.Accommodation.CancelDay) && SelectedReservation.EndDate <= DateTime.Now);
        }

        public void Execute_Update()
        {
            Reservations.Clear();
            foreach (Reservation r in _reservationService.GetAll())
            {
                Reservations.Add(r);
            }
        }

        private bool CanExecute()
        {
            return true;
        }

        private void Execute_Reject()
        {
            _window.Close();
        }
        //private void Execute_PDF(object sender)
        //{
        //    Document document = new Document();

        //    // Set up the output stream
        //    string filePath = "tour_booking_report.pdf";
        //    FileStream fileStream = new FileStream(filePath, FileMode.Create);
        //    PdfWriter writer = PdfWriter.GetInstance(document, fileStream);

        //    // Open the document
        //    document.Open();

        //    // Add the title and subtitle
        //    Font titleFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 24);
        //    Paragraph title = new Paragraph("Tour Report", titleFont);
        //    title.Alignment = Element.ALIGN_CENTER;
        //    document.Add(title);

        //    Font subtitleFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 16);
        //    Paragraph subtitle = new Paragraph("Your tour bookings", subtitleFont);
        //    subtitle.Alignment = Element.ALIGN_CENTER;
        //    document.Add(subtitle);

        //    // Add a separator
        //    LineSeparator separator = new LineSeparator();
        //    document.Add(new Chunk(separator));

        //    Font infoFont = FontFactory.GetFont(FontFactory.HELVETICA, 12);
        //    Font boldFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12);
        //    Paragraph organizedBy = new Paragraph();
        //    organizedBy.Alignment = Element.ALIGN_LEFT;
        //    organizedBy.Add(new Chunk("Organized by:\n", boldFont));
        //    organizedBy.Add(new Chunk("TourAdvisor\n", infoFont));
        //    organizedBy.Add(new Chunk("Novi Sad, Serbia, 21000\n", infoFont));
        //    organizedBy.Add(new Chunk("touradvisor@gmail.com", infoFont));
        //    document.Add(organizedBy);
        //    // Add spacing after the "Organized by" section
        //    document.Add(new Paragraph("\n"));

        //    // Add the "Customer details" section
        //    Paragraph customerDetails = new Paragraph();
        //    customerDetails.Alignment = Element.ALIGN_RIGHT;
        //    customerDetails.Add(new Chunk("Customer details:\n", boldFont));

        //    // Retrieve the tourist object
        //    Tourist tourist = _touristService.getName(MainWindow.LogInUser.Id);
        //    if (tourist != null)
        //    {
        //        // Access the name and email properties of the tourist
        //        string touristName = tourist.Name + " " + tourist.Surname;
        //        string touristEmail = tourist.Email;

        //        // Add the tourist name and email to the "Customer details" section
        //        customerDetails.Add(new Chunk(touristName + "\n", infoFont));
        //        customerDetails.Add(new Chunk(touristEmail + "\n", infoFont));
        //    }
        //    // Add the "Customer details" section above the "From: Start Date" section
        //    document.Add(customerDetails);

        //    // Add spacing before the "From: Start Date" section
        //    document.Add(new Paragraph("\n"));

        //    // Add the date range information
        //    Paragraph dateRange = new Paragraph();
        //    dateRange.Add(new Chunk("From: ", boldFont));
        //    dateRange.Add(new Chunk(SelectedStartDate.ToString("dd-MM-yyyy"), infoFont));
        //    dateRange.Add(new Chunk("\nTo: ", boldFont));
        //    dateRange.Add(new Chunk(SelectedEndDate.ToString("dd-MM-yyyy"), infoFont));
        //    document.Add(dateRange);

        //    // Add spacing after the date range
        //    document.Add(new Paragraph("\n"));

        //    // Add a new paragraph of text
        //    Paragraph paragraph = new Paragraph("Your tour booking report for the selected date range:", infoFont);
        //    document.Add(paragraph);

        //    // Add two rows of space
        //    document.Add(new Paragraph("\n\n"));

        //    // Create the table
        //    PdfPTable table = new PdfPTable(4);
        //    table.WidthPercentage = 100;

        //    // Set the column widths
        //    float[] columnWidths = { 2f, 2f, 2f, 2f };
        //    table.SetWidths(columnWidths);

        //    // Add table headers
        //    PdfPCell headerCell = new PdfPCell();
        //    headerCell.BackgroundColor = BaseColor.LIGHT_GRAY;
        //    headerCell.Padding = 5;
        //    headerCell.HorizontalAlignment = Element.ALIGN_CENTER;
        //    headerCell.VerticalAlignment = Element.ALIGN_MIDDLE;

        //    headerCell.Phrase = new Phrase("Location", infoFont);
        //    table.AddCell(headerCell);

        //    headerCell.Phrase = new Phrase("Start Date", infoFont);
        //    table.AddCell(headerCell);

        //    headerCell.Phrase = new Phrase("Language", infoFont);
        //    table.AddCell(headerCell);

        //    headerCell.Phrase = new Phrase("Number of People", infoFont);
        //    table.AddCell(headerCell);

        //    // Get the tour booking data for the selected date range
        //    List<TourBooking> tourBookings = TourBookings.Where(booking =>
        //        booking.TourEvent.StartTime >= SelectedStartDate && booking.TourEvent.StartTime <= SelectedEndDate).ToList();

        //    // Add tour booking data to the table
        //    foreach (TourBooking booking in tourBookings)
        //    {
        //        table.AddCell(new PdfPCell(new Phrase(booking.TourEvent.Tour.Location.State + " - " + booking.TourEvent.Tour.Location.City, infoFont)));
        //        table.AddCell(new PdfPCell(new Phrase(booking.TourEvent.StartTime.ToString("dd-MM-yyyy"), infoFont)));
        //        table.AddCell(new PdfPCell(new Phrase(booking.TourEvent.Tour.Languages, infoFont)));
        //        table.AddCell(new PdfPCell(new Phrase(booking.NumberOfGuests.ToString(), infoFont)));
        //    }

        //    // Add the table to the document
        //    document.Add(table);

        //    // Close the document
        //    document.Close();

        //    // Open the PDF document with the default application
        //    Process.Start(new ProcessStartInfo(filePath) { UseShellExecute = true });
        //}
    }
}
