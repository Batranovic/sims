using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp.Observer;
using WpfApp1.Models;
using WpfApp1.Serializer;

namespace WpfApp1.Repository
{
    public class ImageRepository : IRepository<Image>
    {
        private const string _filePath = "../../../Resources/Data/images.csv";
 
        private readonly Serializer<Image> _serializer;

        private static ImageRepository _instance = null;
        
        private List<Image> _images;

        public static ImageRepository GetInsatnce()
        {
            if(_instance == null)
            {
                _instance = new ImageRepository();
            }
            return _instance;
        }
        private ImageRepository()
        {
            _images = new List<Image>();
            _serializer = new Serializer<Image>();
            _images = _serializer.FromCSV(_filePath);
        }
        public Image Create(Image entity)
        {
            entity.Id = NextId();
            _images.Add(entity);
            Save();
            return entity;
        }

        

        public Image Delete(Image entity)
        {
            _images.Remove(entity);
            Save();
            return entity;
        }

        public Image Get(int id)
        {
            return _images.Find(i => i.Id == id);
        }

        public List<Image> GetAll()
        {
            return _images;
        }

        public int NextId()
        {
            if (_images.Count == 0)
                return 0;
            int nextId = _images[_images.Count - 1].Id + 1;
            foreach (Image i in _images)
            {
                if (nextId == i.Id)
                {
                    nextId++;
                }
            }
            return nextId;
        }
      
        public Image Update(Image entity)
        {
            var oldEntity = Get(entity.Id);
            if (oldEntity == null)
            {
                return null;
            }
            oldEntity = entity;
            Save();
            return oldEntity;
        }


     

        public void Save()
        {
            _serializer.ToCSV(_filePath, _images);
        }


        public List<Image> GetAccommodations()
        {
            return _images.FindAll(i => i.ImageKind == Models.Enums.ImageKind.Accommodation).ToList();
        }

        public List<string> GetTour()
        {
            return _images.FindAll(i => i.ImageKind == Models.Enums.ImageKind.Tour).Select(i => i.Path).ToList();
        }



    }
}
