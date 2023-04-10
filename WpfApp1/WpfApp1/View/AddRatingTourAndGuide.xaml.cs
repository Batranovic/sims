using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WpfApp1.Controller;
using WpfApp1.Domain.ServiceInterfaces;
using WpfApp1.Model;
using WpfApp1.Model.Enums;
using WpfApp1.Service;

namespace WpfApp1.View
{
    /// <summary>
    /// Interaction logic for AddRatingTourAndGuide.xaml
    /// </summary>
    public partial class AddRatingTourAndGuide : Window, INotifyPropertyChanged, IDataErrorInfo
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<int> Scores { get; set; }

        public TourBooking SelectedTourBooking { get; set; }

        public RatingTourAndGuideController RatingTourAndGuideController { get; set; }

        public TourEventController TourEventController { get; set; }

        private readonly IImageService _imageService;

        public Tourist LogInTourist { get; set; }

        public int SelectedKnowledge {get; set;}

        public int SelectedLanguage { get; set;}

        public int SelectedInterest { get; set;}

        public ObservableCollection<string> Images { get; set; }

        public string SelectedUrl { get; set; }



        public AddRatingTourAndGuide(TourBooking tourBooking)
        {
            InitializeComponent();
            this.DataContext = this;

            _imageService = InjectorService.CreateInstance<IImageService>();   

            var app = Application.Current as App;
            RatingTourAndGuideController = app.RatingTourAndGuideController;
            TourBookingController = app.TourBookingController;
            TourEventController = app.TourEventController;
            ImageController = app.ImageController;

            Images = new ObservableCollection<string>();

            Scores = new ObservableCollection<int>();
            Scores.Add(1);
            Scores.Add(2);
            Scores.Add(3);
            Scores.Add(4);
            Scores.Add(5);
            SelectedTourBooking = tourBooking;
            SelectedKnowledge = 0;
            SelectedLanguage = 0;
            SelectedInterest = 0;


            //List<string> a = new List<string>(Images);


        }

        private string _comment;

        public string Comment
        {
            get => _comment;
            set
            {
                if (value != _comment)
                {
                    _comment = value;
                    OnPropertyChanged("Comment");
                }
            }
        }

        private string _url;
        public string Url
        {
            get => _url;
            set
            {
                if (value != _url)
                {
                    _url = value;
                    OnPropertyChanged("Url");
                }
            }
        }


       
       

        private void SubmitImageButton(object sender, RoutedEventArgs e)
        {
            Images.Add(Url);
            Url = "";
        }
        private void RemoveImageButton(object sender, RoutedEventArgs e)
        {
            if (SelectedUrl == null) return;
            Images.Remove(SelectedUrl);
            SelectedUrl = null;
        }
        private void ConfirmButton(object sender, RoutedEventArgs e)
        {
            if (!IsValid)
            {
                MessageBox.Show("You didn't finish your review");
                return;
            }
            List<string> images = new List<string>(Images);
            RatingTourAndGuide ratingTourAndGuide = new RatingTourAndGuide(-1, SelectedKnowledge, SelectedLanguage, SelectedInterest, Comment,SelectedTourBooking.Id ,SelectedTourBooking ,images);
            RatingTourAndGuideController.Create(ratingTourAndGuide);
            MessageBox.Show("Your review has been sent!");
            this.Close();
        }

        private void RejectButton(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        public string Error => null;
        public string this[string columnName]
        {
            get
            {
                if (columnName == "SelectedKnowledge")
                {
                    if (SelectedKnowledge == 0)
                    {
                        return "missing review";
                    }
                }

                if (columnName == "SelectedLanguage")
                {
                    if (SelectedLanguage == 0)
                    {
                        return "missing review";

                    }
                }
                if (columnName == "SelectedInterest")
                {
                    if (SelectedInterest == 0)
                    {
                        return "missing review";
                    }
                }

                if (columnName == "Comment")
                {
                    if(Comment == null)
                    {
                        return "missing comment";
                    }
                }

                return null;


                
            }

        }

        private readonly string[] _validatedProperties = { "SelectedKnowledge", "SelectedLanguage", "SelectedInterest","Comment"};

        public bool IsValid
        {
            get
            {
                foreach (var property in _validatedProperties)
                {
                    if (this[property] != null)
                        return false;
                }

                return true;
            }
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

       
    }
}
