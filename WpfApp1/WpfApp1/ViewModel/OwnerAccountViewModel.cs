using iTextSharp.text.pdf.draw;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;
using WpfApp1.Commands;
using WpfApp1.Domain.Models;
using WpfApp1.Domain.ServiceInterfaces;
using WpfApp1.Service;
using WpfApp1.Views;
using PdfSharp.Drawing;
using System;
using iTextSharp.text.pdf.qrcode;
using System.Windows.Controls;
using System.Windows.Media.Media3D;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using PdfSharp.Pdf.IO;
using WpfApp1.DTO;

namespace WpfApp1.ViewModel
{
    public class OwnerAccountViewModel : ViewModelBase
    {
        private ViewModelBase _currentViewModel;
        private readonly IReservationService _reservationService;
        private OwnerProfileViewModel _ownerProfileViewModel;
        private SignInAccommodationViewModel _signInAccommodationViewModel;
        private ReservationOverviewViewModel _reservationOverviewViewModel;
        private ForumOverviewViewModel _forumViewModel;
        private RenovationOverviewViewModel _renovationOverviewViewModel;
        private readonly IOwnerService _ownerService;
        private INotificationAccommodationReleaseService _notificationService;
        private readonly IForumNotificationService _forumNotificationService;
        private readonly IAccommodationService _accommodationService;
        

        private string _haveNotification;
        public string HaveNotification
        {
            get => _haveNotification;
            set
            {
                _haveNotification = value;
                OnPropertyChanged(nameof(HaveNotification));
            }
        }

        public string OwnerAccountWizard = "Dear user,\n\n" +
        " You have a big section(buttons on top of window), when you select one section then " +
            "new window open up. In new window you have a vertical tabs with more features. " +
            "PDF icon is for creating PDF report, bell icon is for display notification " +
            "and profile icon is for displaying information of user, have a log out which bring back to log in page. " +
            "You have section for displaying  all reviews.";
        public string AccommodationWizard = "Overview section is used for displaying all accommodations.\n" +
            "Statistics section is used for displaying statistic of accommodation,\n" +
            " you can double tap on accommodation then open statistic for that accommdoation.\n" +
            "Create section is used for creat new accommodation.\n" +
            "Suggestion section is used displaying popular and unpopular location.";
        public string ReservationWizzard = "Overview section is used for display all reservations.\n" +
            "Request section is used for displaying all postponed request reservation,\n" +
            " double click used pop up new window with message showing is date free.";
        public string RenovationWizzard = "Overview section is used for displaying all future renovations. Schedulling section is used for chooseing accommdation and scheduling  renovation.";
        public string ForumWizzard = "Reviews section is used  for displaying all forums. With double click on one forum from all forums opens  all comments on that forum."; 

        private Window _window;
        //    private AccommodationRenovationViewModel _accommodationRenovationViewModel;
        //    private RenovationHistoryViewModel _renovationHistoryViewModel;

        private string _nameSurname;
        public string NameSurname
        {
            get => _nameSurname;
            set
            {
                _nameSurname = value;
                OnPropertyChanged(nameof(NameSurname));
            }
        }

        private string _wizardMessage;

        public string WizardMessage
        {
            get => _wizardMessage;
            set
            {
                _wizardMessage = value;
                OnPropertyChanged(nameof(WizardMessage));
            }
        }


        private bool _visibilityWizard;
        public bool VisibilityWizard
        {
            get => _visibilityWizard;
            set
            {
                _visibilityWizard = value;
                OnPropertyChanged(nameof(VisibilityWizard));
            }
        }
        public RelayCommand WizardCommand { get; set; }

        private bool _visibilityPopUp;
        public bool VisibilityPopUp
        {
            get => _visibilityPopUp;
            set
            {
                _visibilityPopUp = value;
                OnPropertyChanged(nameof(VisibilityPopUp));
            }
        }
        public string UserType { get; set; }
        public Owner LoggedOwner { get; set; }
        public ViewModelBase CurrentViewModel
        {
            get => _currentViewModel;
            set
            {
                if (_currentViewModel != value)
                {
                    _currentViewModel = value;
                    OnPropertyChanged(nameof(CurrentViewModel));
                }
            }
        }
        private bool _visibilityNotification;

        public bool VisibilityNotification
        {
            get => _visibilityNotification;
            set
            {
                _visibilityNotification = value;
                OnPropertyChanged(nameof(VisibilityNotification));
            }
        }

        private bool _visibilityPDF;
        public bool VisibilityPDF
        {
            get => _visibilityPDF;
            set
            {
                _visibilityPDF = value;
                OnPropertyChanged(nameof(VisibilityPDF));
            }
        }

        private DateTime _startDate;
        public DateTime StartDate
        {
            get => _startDate;
            set
            {
                _startDate = value;
                OnPropertyChanged(nameof(StartDate));
            }
        }

        private DateTime _endDate;
        public DateTime EndDate
        {
            get => _endDate;
            set
            {
                _endDate = value;
                OnPropertyChanged(nameof(EndDate));
            }
        }

        

        private bool _toolTipEnable;
        public  bool ToolTipEnable
        {
            get => _toolTipEnable;
            set
            {
                _toolTipEnable = value;
                OnPropertyChanged(nameof(ToolTipEnable));
            }
        }

       

        private string _toolTipStatus;
        public  string ToolTipStatus
        {
            get => _toolTipStatus;
            set
            {
                _toolTipStatus = value;
                OnPropertyChanged(nameof(ToolTipStatus));
            }
        }

        public RelayCommand NavCommand { get; set; }
        public RelayCommand ShowCommand { get; set; }
        public RelayCommand LogoutCommand { get; set; }
        public RelayCommand NotificationCommand { get; set; }
        public RelayCommand DeleteNotificationCommand { get; set; }
        public RelayCommand DeleteAllNotificationCommand { get; set; }
        public RelayCommand CreatePdfCommand { get; set; }
        public RelayCommand VisibilityPDFCommand { get; set; }
        public RelayCommand TogleCommand { get; set;  }


        public OwnerAccountViewModel(Owner owner)
        {
            _ownerService = InjectorService.CreateInstance<IOwnerService>();
            _reservationOverviewViewModel = new ReservationOverviewViewModel(owner);
            _ownerProfileViewModel = new OwnerProfileViewModel(owner);
            _signInAccommodationViewModel = new SignInAccommodationViewModel(owner);
            CurrentViewModel = new OwnerProfileViewModel(owner);
            _renovationOverviewViewModel = new(owner);
            _forumViewModel = new(owner);
            _reservationService = InjectorService.CreateInstance<IReservationService>();
            _notificationService = InjectorService.CreateInstance<INotificationAccommodationReleaseService>();
            _forumNotificationService = InjectorService.CreateInstance<IForumNotificationService>();
            _accommodationService = InjectorService.CreateInstance<IAccommodationService>();

            Init(owner);
            IntiCommand();
        }

        private void IntiCommand()
        {
            TogleCommand = new(param => Execute_TogleCommand(), param => CanExecute());
            NavCommand = new(Execute_NavCommand, CanExecute_NavCommand);
            ShowCommand = new(param => Execute_ShowCommand(), param => CanExecute());
            WizardCommand = new(param => Execute_WizardCommand(), param => CanExecute());
            LogoutCommand = new(param => Execute_LogoutCommand(), param => CanExecute());
            NotificationCommand = new(param => Execute_NotificationCommand(), param => CanExecute());
            DeleteNotificationCommand = new(param => Execute_DeleteNotificationCommand(), param => CanExecute_DeleteNotificationCommand());
            DeleteAllNotificationCommand = new(param => Execute_DeleteAllNotificationCommand(), param => CanExecute());
            CreatePdfCommand = new(Execute_PDF, param => CanExecute());
            VisibilityPDFCommand = new(param => Execute_VisibilityPDFCommand(), param => CanExecute());
        }
        public ObservableCollection<Accommodation> Accommodations { get; set; }
        private Accommodation _selectedAccommodation;
        public Accommodation SelectedAccommodation
        {
            get => _selectedAccommodation;
            set
            {
                _selectedAccommodation = value;
                OnPropertyChanged(nameof(SelectedAccommodation));
                SetYear();
            }
        }

        private void SetYear()
        {
            List<int> years = new();
            foreach(var r in InjectorService.CreateInstance<IReservationService>().GetAll().FindAll(re => re.Accommodation.Id == SelectedAccommodation.Id))
            {
                years.Add(r.StartDate.Year);
            }
            years = years.Distinct().ToList();
            Years.Clear();
            foreach(int i in years)
            {
                Years.Add(i);
            }
            if (years.Count == 0)
            {
                LabelVisibility = "Visible"; 
            }
            else
            {
                LabelVisibility = "Hidden";
            }
        }

        private ObservableCollection<int> _years;
        public ObservableCollection<int> Years
        {
            get => _years;
            set
            {
                _years = value;
                OnPropertyChanged(nameof(Years));
            }
        }

        private int _selectedYear;
        public int SelectedYear
        {
            get => _selectedYear;
            set
            {
                _selectedYear = value;
                OnPropertyChanged(nameof(SelectedAccommodation));
            }
        }

        public ObservableCollection<AccommodationStatisticDTO> AccommodationStatisticMonthDTOs { get; set; }

        private string _labelVisibility;
        public string  LabelVisibility
        {
            get => _labelVisibility;
            set
            {
                _labelVisibility = value;
                OnPropertyChanged(nameof(LabelVisibility));
            }
        }
        private void Init(Owner owner)
        {
            LabelVisibility = "Hidden";
            AccommodationStatisticMonthDTOs = new();
            Years = new();
            Accommodations = new(_accommodationService.GetAll());
            ToolTipStatus = "Enable";
            ToolTipEnable = true;
            WizardMessage = OwnerAccountWizard;
            StartDate = DateTime.Now;
            EndDate = DateTime.Now;
            _window = Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.Name == "OwnerStart");
            HaveNotification = owner.Notifications.Count == 0 ? "White" : "Green";
            VisibilityWizard = false;
            VisibilityPopUp = false;
            VisibilityNotification = false;
            LoggedOwner = owner;
            UserType = LoggedOwner.Super ? "Super owner" : "Basic owner";
            VisibilityPDF = false;
        }

        private void Execute_TogleCommand()
        {
            ToolTipEnable = !ToolTipEnable;
            ToolTipStatus = ToolTipEnable ? "Enabled" : "Disabled";
        }

        private void Execute_NotificationCommand()
        {
            VisibilityNotification = !VisibilityNotification;
        }

        private void Execute_VisibilityPDFCommand()
        {
            LabelVisibility = "Hidden";
            VisibilityPDF = !VisibilityPDF;
        }

        public bool CanExecute_NavCommand(object parameter)
        {
            if (parameter == null || !int.TryParse(parameter.ToString(), out int index))
            {
                return false;
            }

            return index >= 0 && index <= 4;
        }

        public void Execute_LogoutCommand()
        {
            MainWindow mw = new();
            mw.Show();
            MainWindow.LogInUser = null;
            _window.Close();
        }
        public void Execute_WizardCommand()
        {
            if(CurrentViewModel.GetType() == _ownerProfileViewModel.GetType())
            {
                WizardMessage = OwnerAccountWizard;
            }
            else  if(CurrentViewModel.GetType() == _signInAccommodationViewModel.GetType())
            {
                WizardMessage = AccommodationWizard;
            }
            else if(CurrentViewModel.GetType() == _reservationOverviewViewModel.GetType())
            {
                WizardMessage = ReservationWizzard;
            }
            else if(CurrentViewModel.GetType() == _renovationOverviewViewModel.GetType())
            {
                WizardMessage = RenovationWizzard;
            }
            else
            {
                WizardMessage = ForumWizzard;
            }
            VisibilityWizard = !VisibilityWizard;
        }
        public void Execute_NavCommand(object parameter)
        {
            int index = int.Parse(parameter.ToString());
            switch (index)
            {
                case 0:
                    CurrentViewModel = _ownerProfileViewModel;
                    Execute_ShowCommand();
                    break;
                case 1:
                    CurrentViewModel = _signInAccommodationViewModel;
                    break;
                case 2:
                    CurrentViewModel = _reservationOverviewViewModel;
                    break;
                case 3:
                    CurrentViewModel = _renovationOverviewViewModel;
                    break;
                case 4:
                    CurrentViewModel = _forumViewModel;
                    break;
            }
        }

        public void Execute_ShowCommand()
        {
            NameSurname = LoggedOwner.Name + " " + LoggedOwner.Surname;
            VisibilityPopUp = !VisibilityPopUp;
        }

        public bool CanExecute()
        {
            return true;
        }
        private NotificationBase _SelectedNotificationBase;
        public NotificationBase SelectedNotificationBase
        {
            get { return _SelectedNotificationBase; }
            set
            {
                _SelectedNotificationBase = value;
                OnPropertyChanged(nameof(SelectedNotificationBase));
                DeleteNotificationCommand.RaiseCanExecuteChanged();
            }
        }
        private bool CanExecute_DeleteNotificationCommand()
        {
            return SelectedNotificationBase != null;
        }

        private void Execute_DeleteNotificationCommand()
        {
            SelectedNotificationBase.IsDelivered = true;
            _notificationService.Update((NotificationAccommodationRelease)SelectedNotificationBase);
            LoggedOwner.Notifications.Clear();
            foreach (NotificationAccommodationRelease n in _notificationService.GetForOwner(LoggedOwner.Id))
            {
                LoggedOwner.Notifications.Add(n);
            }
            OnPropertyChanged(nameof(LoggedOwner.Notifications));
            HaveNotification = LoggedOwner.Notifications.Count == 0 ? "White" : "Green";

        }

        private void Execute_DeleteAllNotificationCommand()
        {
            foreach (var n in LoggedOwner.Notifications)
            {
                n.IsDelivered = true;
                if(n.GetType().Equals(typeof(NotificationAccommodationRelease)))
                {
                    _notificationService.Update((NotificationAccommodationRelease)n);
                }
                else
                {
                    _forumNotificationService.Update((ForumNotification)n);
                }
            }
            LoggedOwner.Notifications.Clear();
            HaveNotification = "White";
        }

        private void Execute_PDF(object sender)
        {
            Document document = new Document();

            // Set up the output stream
            string filePath = "accommodation.pdf";
            FileStream fileStream = new FileStream(filePath, FileMode.Create);
            PdfWriter writer = PdfWriter.GetInstance(document, fileStream);

            // Open the document
            document.Open();

            // Add the title and subtitle
            Font titleFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 24);
            Paragraph title = new Paragraph("Accommodation Report", titleFont);
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
            organizedBy.Add(new Chunk("Booking\n", infoFont));
            organizedBy.Add(new Chunk("Novi Sad, Serbia, 21000\n", infoFont));
            organizedBy.Add(new Chunk("booking@gmail.com", infoFont));
            document.Add(organizedBy);
            // Add spacing after the "Organized by" section
            document.Add(new Paragraph("\n"));

            // Add the "Customer details" section
            Paragraph customerDetails = new Paragraph();
            customerDetails.Alignment = Element.ALIGN_RIGHT;
            customerDetails.Add(new Chunk("Customer details:\n", boldFont));

            // Retrieve the tourist object
            Owner owner = _ownerService.Get(MainWindow.LogInUser.Id);
            if (owner != null)
            {
                // Access the name and email properties of the tourist
                string ownerName = owner.Name + " " + owner.Surname;
                string ownerEmail = owner.Email;

                // Add the tourist name and email to the "Customer details" section
                customerDetails.Add(new Chunk(ownerName + "\n", infoFont));
                customerDetails.Add(new Chunk(ownerEmail + "\n", infoFont));
            }
            // Add the "Customer details" section above the "From: Start Date" section
            document.Add(customerDetails);

            // Add spacing before the "From: Start Date" section
            document.Add(new Paragraph("\n"));

            // Add the date range information
            Paragraph dateRange = new Paragraph();
            dateRange.Add(new Chunk("From: ", boldFont));
            dateRange.Add(new Chunk(StartDate.ToString("dd-MM-yyyy"), infoFont));
            dateRange.Add(new Chunk("\nTo: ", boldFont));
            dateRange.Add(new Chunk(EndDate.ToString("dd-MM-yyyy"), infoFont));
            document.Add(dateRange);

            // Add spacing after the date range
            document.Add(new Paragraph("\n"));

            // Add a new paragraph of text
            string s = "Your reservation report for the selected accommodation " + SelectedAccommodation.Name + " in year " + SelectedYear.ToString() + ".";
            Paragraph paragraph = new Paragraph(s);
            document.Add(paragraph);

            // Add two rows of space
            document.Add(new Paragraph("\n\n"));

            // Create the table
            PdfPTable table = new PdfPTable(5);
            table.WidthPercentage = 100;

            // Set the column widths
            float[] columnWidths = { 1.5f, 1.5f, 1.5f, 1.5f, 1.5f };
            table.SetWidths(columnWidths);

            // Add table headers
            PdfPCell headerCell = new PdfPCell();
            headerCell.BackgroundColor = BaseColor.LIGHT_GRAY;
            headerCell.Padding = 5;
            headerCell.HorizontalAlignment = Element.ALIGN_CENTER;
            headerCell.VerticalAlignment = Element.ALIGN_MIDDLE;


            headerCell.Phrase = new Phrase("Month", infoFont);
            table.AddCell(headerCell);

            headerCell.Phrase = new Phrase("Reservation", infoFont);
            table.AddCell(headerCell);

            headerCell.Phrase = new Phrase("Cancelation", infoFont);
            table.AddCell(headerCell);

            headerCell.Phrase = new Phrase("Rescheduling", infoFont);
            table.AddCell(headerCell);

            headerCell.Phrase = new Phrase("Renovation advice", infoFont);
            table.AddCell(headerCell);


         
            // Get the tour booking data for the selected date range
            List<Reservation> resevations = _reservationService.GetAll().FindAll(r => r.StartDate >=  StartDate && r.EndDate <= EndDate);

            // Add tour booking data to the table
            AccommodationStatisticMonthDTOs.Clear();
            foreach (AccommodationStatisticDTO a in _accommodationService.StatisticByMonthForAccommodation(SelectedAccommodation.Id,SelectedYear))
            {
                table.AddCell(new PdfPCell(new Phrase(a.Month, infoFont)));
                table.AddCell(new PdfPCell(new Phrase(a.Renovations.ToString(), infoFont)));
                table.AddCell(new PdfPCell(new Phrase(a.Cancelations.ToString(), infoFont)));
                table.AddCell(new PdfPCell(new Phrase(a.Rescheduling.ToString(), infoFont)));
                table.AddCell(new PdfPCell(new Phrase(a.Renovations.ToString(), infoFont)));
            }
           
            // Add the table to the document
            document.Add(table);

            // Close the document
            document.Close();

            // Open the PDF document with the default application
            Process.Start(new ProcessStartInfo(filePath) { UseShellExecute = true });
            Execute_VisibilityPDFCommand();
            LabelVisibility = "Hidden";
        }


    }
}
