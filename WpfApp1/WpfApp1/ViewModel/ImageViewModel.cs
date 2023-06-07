using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfApp1.Commands;
using WpfApp1.Domain.Models;
using WpfApp1.Service;

namespace WpfApp1.ViewModel
{
    public partial class ImageViewModel
    {
        public Accommodation Accommodation { get; set; }
        // public Image Image { get; set; }

        private readonly ImageService _imageService;
        private Accommodation selectedAccommodation;

        private Window _window;

        public RelayCommand CloseCommand { get; set; }

        public RelayCommand RejectCommand { get; set; }

        public List<Image> Images { get; set; }


        //public List<Image> Images { get; set; } 
        public ImageViewModel(Accommodation accommodation)
        {
            _window = Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.Name == "ImageViewName");
            Images = new List<Image>(accommodation.Images);

            CloseCommand = new RelayCommand(param => Execute_CloseCommand(), param => CanExecute());

        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void Execute_CloseCommand()
        {
            this.Execute_Reject();
        }

        private bool CanExecute()
        {
            return true;
        }

        private void Execute_Reject()
        {
            _window.Close();
        }
    }
}
