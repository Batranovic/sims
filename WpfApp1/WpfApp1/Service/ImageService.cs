using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Domain.RepositoryInterfaces;
using WpfApp1.Model;
using WpfApp1.Repository;

namespace WpfApp1.Service
{
    public class ImageService
    {
        private IImageRepository _imageRepository;

        public ImageService()
        {
            _imageRepository = ImageRepository.GetInsatnce();
        }

        public Image Get(int id)
        {
            return _imageRepository.Get(id);
        }

        public List<Image> GetAll()
        {
            return _imageRepository.GetAll();
        }

        public void Create(Image location)
        {
            _imageRepository.Create(location);
        }

        public void Delete(Image location)
        {
            _imageRepository.Delete(location);
        }

        public void Update(Image image)
        {
            _imageRepository.Update(image);
        }

        public List<Image> GetAccommodations()
        {
            return _imageRepository.GetAccommodations();
        }

        public List<string> GetTour()
        {
            return _imageRepository.GetTour();
        }

    }
}
