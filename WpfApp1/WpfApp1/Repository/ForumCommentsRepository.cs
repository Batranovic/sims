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
    public class ForumCommentsRepository : IForumCommentsRepository
    {
        private const string _filepath = "../../../Resources/Data/forumComments.csv";
        private readonly List<IObserver> _observers;
        private readonly Serializer<ForumComments> _serializer;
        private static IForumCommentsRepository _instance = null;
        private List<ForumComments> _forumComments;

        public static IForumCommentsRepository GetInstance()
        {
            if(_instance == null)
            {
                _instance = new ForumCommentsRepository();
            }
            return _instance;
        }

        private ForumCommentsRepository()
        {
            _serializer = new Serializer<ForumComments>();
            _forumComments = new List<ForumComments>();
            _forumComments = _serializer.FromCSV(_filepath);
            _observers = new List<IObserver>();
        }

        public ForumComments Create(ForumComments entity)
        {
            entity.Id = NextId();
            _forumComments.Add(entity);
            Save();
            return entity;
        }
        public ForumComments Delete(ForumComments entity)
        {
            _forumComments.Remove(entity);
            Save();
            return entity;
        }
        public ForumComments Get(int id)
        {
            return _forumComments.Find(a => a.Id == id);
        }
        public List<ForumComments> GetAll()
        {
            return _forumComments;
        }

        public int NextId()
        {
            if (_forumComments.Count == 0) return 0;
            int newId = _forumComments[_forumComments.Count() - 1].Id + 1;
            foreach (ForumComments a in _forumComments)
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
            _serializer.ToCSV(_filepath, _forumComments);
        }
        public ForumComments Update(ForumComments entity)
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
