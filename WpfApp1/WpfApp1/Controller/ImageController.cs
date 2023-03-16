using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp.Observer;
using WpfApp1.Model;
using WpfApp1.Repository;

namespace WpfApp1.Controller
{
    public class ImageController
    {
        private readonly ImageDAO _images;

        public ImageController(ImageDAO imageDAO)
        {
            _images = imageDAO;
        }

        public Image Get(int id)
        {
            return _images.Get(id);
        }

        public List<Image> GetAll()
        {
            return _images.GetAll();
        }

        public void Create(Image location)
        {
            _images.Create(location);
        }

        public void Delete(Image location)
        {
            _images.Delete(location);
        }

        public void Update(Image image)
        {
            _images.Update(image);
        }

        public List<string> GetAccommodations()
        {
            return _images.GetAccommodations();  //vraca putanje samo do slika smestaja
        }

        public List<string> GetTour()
        {
            return _images.GetTour();    //vraca putanje samo do slika ture
        }

    }
}
