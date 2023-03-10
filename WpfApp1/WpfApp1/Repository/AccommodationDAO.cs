using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Model;
using WpfApp1.Serializer;

namespace WpfApp1.Repository
{
    public class AccommodationDAO : IDAO<Accommodation>
    {
        private const string _filePath = "../../../Resources/Data/accommodations.csv";

        private readonly Serializer<Accommodation> _serializer;

        private List<Accommodation> _accommododations;

        public AccommodationDAO()
        {
            _serializer = new Serializer<Accommodation>();
            _accommododations = new List<Accommodation>();
            _accommododations = _serializer.FromCSV(_filePath);
        }

        public Accommodation Create(Accommodation entity)
        {
            entity.Id = NextId();
            _accommododations.Add(entity);
            Save();
            return entity;
        }

        public void Delete(Accommodation entity)
        {
            _accommododations.Remove(entity);
        }

        public Accommodation Get(int id)
        {
            return _accommododations.Find(a => a.Id == id); 
        }

        public List<Accommodation> GetAll()
        {
            return _accommododations;
        }

        public int NextId()
        {
            if (_accommododations.Count == 0) return 0;
            int newId =  _accommododations[_accommododations.Count() - 1].Id + 1;
            foreach(Accommodation a in _accommododations)
            {
                if(newId == a.Id)
                {
                    newId++;
                }
            }
            return newId;
        }

        private void Save()
        {
            _serializer.ToCSV(_filePath, _accommododations);
        }

        public Accommodation Update(Accommodation entity)
        {
            var oldEntity = Get(entity.Id);
            if(oldEntity == null)
            {
                return null;
            }
            oldEntity = entity;
            Save();
            return oldEntity;
        }

      
    }
}
