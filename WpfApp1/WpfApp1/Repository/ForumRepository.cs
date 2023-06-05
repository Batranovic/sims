using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp.Observer;
using WpfApp1.Domain.Models;
using WpfApp1.Domain.RepositoryInterfaces;
using WpfApp1.Serializer;

namespace WpfApp1.Repository
{
    public class ForumRepository : IForumRepository
    {
        private const string _filePath = "../../../Resources/Data/forums.csv";
        private readonly List<IObserver> _observers;
        private readonly Serializer<Forum> _serializer;
        private static IForumRepository _instance = null;
        private List<Forum> _forums;

        public static IForumRepository GetInstance()
        {
            if(_instance == null)
            {
                _instance = new ForumRepository();
            }
            return _instance;
        }

        private ForumRepository()
        {
            _serializer = new Serializer<Forum>();
            _forums= new List<Forum>();
            _forums = _serializer.FromCSV(_filePath);
            _observers = new List<IObserver>();
        }
        public Forum Create(Forum entity)
        {
            entity.Id = NextId();
            _forums.Add(entity);
            Save();
            return entity;
        }
        public Forum Delete(Forum entity)
        {
            _forums.Remove(entity);
            Save();
            return entity;
        }
        public Forum Get(int id)
        {
            return _forums.Find(a => a.Id == id);
        }
        public List<Forum> GetAll()
        {
            return _forums;
        }

        public int NextId()
        {
            if (_forums.Count == 0) return 0;
            int newId = _forums[_forums.Count() - 1].Id + 1;
            foreach (Forum a in _forums)
            {
                if (newId == a.Id)
                {
                    newId++;
                }
            }
            return newId;
        }
        public void Save()
        {
            _serializer.ToCSV(_filePath, _forums);
        }
        public Forum Update(Forum entity)
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
        public void Subscribe(IObserver observer)
        {
            _observers.Add(observer);
        }
        public void Unsubscribe(IObserver observer)
        {
            _observers.Remove(observer);
        }
        public void NotifyObservers()
        {
            foreach (var observer in _observers)
            {
                observer.Update();
            }
        }
    }
}
