using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Domain.RepositoryInterfaces;
using WpfApp1.Domain.ServiceInterfaces;
using WpfApp1.Domain.Models;
using WpfApp1.Repository;

namespace WpfApp1.Service
{
    public class ImageService : IImageService
    {
        private readonly IImageRepository _imageRepository;

        public ImageService()
        {
            _imageRepository = InjectorRepository.CreateInstance<IImageRepository>();
        }

        public Image Get(int id)
        {
            return _imageRepository.Get(id);
        }

        public List<Image> GetAll()
        {
            return _imageRepository.GetAll();
        }

        public void Create(Image entity)
        {
            _imageRepository.Create(entity);
        }

        public void Delete(Image entity)
        {
            _imageRepository.Delete(entity);
        }

        public Image Update(Image entity)
        {
            return _imageRepository.Update(entity);
        }

        public void Save()
        {
            _imageRepository.Save();
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
