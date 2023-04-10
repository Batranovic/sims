using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp.Observer;
using WpfApp1.Models;
using WpfApp1.Repository;
using WpfApp1.Service;

namespace WpfApp1.Controller
{
    public class ImageController
    {
        private readonly ImageService _imageService;

        public ImageController()
        {
            _imageService = new ImageService();
        }

        public Image Get(int id)
        {
            return _imageService.Get(id);
        }

        public List<Image> GetAll()
        {
            return _imageService.GetAll();
        }

        public void Create(Image location)
        {
            _imageService.Create(location);
        }

        public void Delete(Image location)
        {
            _imageService.Delete(location);
        }

        public void Update(Image image)
        {
            _imageService.Update(image);
        }

        public List<Image> GetAccommodations()
        {
            return _imageService.GetAccommodations();  //vraca putanje samo do slika smestaja
        }

        public List<string> GetTour()
        {
            return _imageService.GetTour();    //vraca putanje samo do slika ture
        }

    }
}
