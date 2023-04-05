using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Model;
using WpfApp1.Repository;

namespace WpfApp1.Service
{
    public class ImageService
    {
        private ImageRepository _imageDAO;

        public ImageService()
        {
            _imageDAO = ImageRepository.GetInsatnce();
        }

        public Image Get(int id)
        {
            return _imageDAO.Get(id);
        }

        public List<Image> GetAll()
        {
            return _imageDAO.GetAll();
        }

        public void Create(Image location)
        {
            _imageDAO.Create(location);
        }

        public void Delete(Image location)
        {
            _imageDAO.Delete(location);
        }

        public void Update(Image image)
        {
            _imageDAO.Update(image);
        }

        public List<Image> GetAccommodations()
        {
            return _imageDAO.GetAccommodations();
        }

        public List<string> GetTour()
        {
            return _imageDAO.GetTour();
        }

    }
}
