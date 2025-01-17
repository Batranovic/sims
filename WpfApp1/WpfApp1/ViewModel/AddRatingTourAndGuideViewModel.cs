﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WpfApp1.Commands;
using WpfApp1.Domain.Models;
using WpfApp1.Domain.ServiceInterfaces;
using WpfApp1.Service;
using WpfApp1.Views;

namespace WpfApp1.ViewModel
{
    public  class AddRatingTourAndGuideViewModel : ViewModelBase, IDataErrorInfo
    {
        public Action CloseAction { get; set; }
        public ObservableCollection<int> Scores { get; set; }
        public ObservableCollection<string> Images { get; set; }

        private readonly ITourBookingService _tourBookingService;
        private readonly IRatingTourAndGuideService _ratingTourAndGuideService;
        private readonly ITourEventService _tourEventService;
        private readonly IImageService _imageService;

        public Tourist LogInTourist { get; set; }

        public TourBooking SelectedTourBooking { get; set; }
        
        public string SelectedUrl { get; set; }


        private int _selectedKnowledge;
        public int SelectedKnowledge
        {
            get => _selectedKnowledge;
            set
            {
                if (value != _selectedKnowledge)
                {
                    _selectedKnowledge = value;
                    OnPropertyChanged();
                }
            }
        }

        private int _selectedLanguage;
        public int SelectedLanguage
        {
            get => _selectedLanguage;
            set
            {
                if (value != _selectedLanguage)
                {
                    _selectedLanguage = value;
                    OnPropertyChanged();
                }
            }
        }


        private int _selectedInterest;
        public int SelectedInterest
        {
            get => _selectedInterest;
            set
            {
                if (value != _selectedInterest)
                {
                    _selectedInterest = value;
                    OnPropertyChanged();
                }
            }
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

    


        public AddRatingTourAndGuideViewModel(TourBooking tourBooking) {

            _imageService = InjectorService.CreateInstance<IImageService>();
            _ratingTourAndGuideService = InjectorService.CreateInstance<IRatingTourAndGuideService>();
            _tourBookingService = InjectorService.CreateInstance<ITourBookingService>();
            _tourEventService = InjectorService.CreateInstance<ITourEventService>();

            Images = new ObservableCollection<string>();
            Scores = new ObservableCollection<int>();


            SelectedTourBooking = tourBooking;
            SelectedKnowledge = 3;
            SelectedLanguage = 3;
            SelectedInterest = 3;
            Comment = "";

            SubmitImageCommand = new RelayCommand(Execute_SubmitImage, CanExecute_Command);
            RemoveImageCommand = new RelayCommand(Execute_RemoveImage, CanExecute_Command);
            ConfirmCommand = new RelayCommand(Execute_Confirm, CanExecute_Command);
            RejectCommand = new RelayCommand(Execute_Reject, CanExecute_Command);

        }



        private bool CanExecute_Command(object parameter)
        {
            return true;
        }
        private void Execute_Confirm(object sender)
        {
            IsSubmitClicked = true; 
            List<RatingTourAndGuide> ratedTourBookings = _ratingTourAndGuideService.GetReviewFromTourist(SelectedTourBooking.Id, MainWindow.LogInUser.Id);
            if (ratedTourBookings.Count > 0)
            {
                MessageBox.Show("You have already rated this tour booking");
                this.CloseAction();
            }

            if (!IsValid )
            { 
                return;
            }
            List<string> images = new List<string>(Images);
            RatingTourAndGuide ratingTourAndGuide = new RatingTourAndGuide(-1, SelectedKnowledge, SelectedLanguage, SelectedInterest, Comment, SelectedTourBooking, images);
            _ratingTourAndGuideService.Create(ratingTourAndGuide);
            MessageBox.Show("Your review has been sent!");
            this.CloseAction();
        }

        private void Execute_Reject(object sender)
        {
            CloseAction();
        }

        private void Execute_SubmitImage(object sender)
        {
            Images.Add(Url);
            Url = "";
        }

        private void Execute_RemoveImage(object sender)
        {
            if (SelectedUrl == null) return;
            Images.Remove(SelectedUrl);
            SelectedUrl = null;
        }

        private RelayCommand submitImageCommand;
        public RelayCommand SubmitImageCommand
        {
            get => submitImageCommand;
            set
            {
                if (value != submitImageCommand)
                {
                    submitImageCommand = value;
                    OnPropertyChanged();
                }
            }
        }


        private RelayCommand removeImageCommand;
        public RelayCommand RemoveImageCommand
        {
            get => removeImageCommand;
            set
            {
                if (value != removeImageCommand)
                {
                    removeImageCommand = value;
                    OnPropertyChanged();
                }
            }
        }


        private RelayCommand confirmCommand;
        public RelayCommand ConfirmCommand
        {
            get => confirmCommand;
            set
            {
                if (value != confirmCommand)
                {
                    confirmCommand = value;
                    OnPropertyChanged();
                }
            }
        }

        private RelayCommand rejectCommand;
        public RelayCommand RejectCommand
        {
            get => rejectCommand;
            set
            {
                if (value != rejectCommand)
                {
                    rejectCommand = value;
                    OnPropertyChanged();
                }
            }
        }




        public string Error => null;
        public string this[string columnName]
        {
            get
            {
                if (IsSubmitClicked)
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
                        if (Comment == "")
                        {
                            return "missing review";
                        }
                    }
                }
                return null;

            }

        }


        private bool _isSubmitClicked = false;

        public bool IsSubmitClicked
        {
            get { return _isSubmitClicked; }
            set
            {
                if (_isSubmitClicked != value)
                {
                    _isSubmitClicked = value;
                    OnPropertyChanged("IsSubmitClicked");
                    OnPropertyChanged("Comment");
                }
            }
        }

        private readonly string[] _validatedProperties = { "SelectedKnowledge", "SelectedLanguage", "SelectedInterest", "SelectedInterest", "Comment" };

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


    }
}
