using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp.Observer;
using WpfApp1.Domain.Models;
using WpfApp1.Domain.RepositoryInterfaces;
using WpfApp1.Repository;

namespace WpfApp1.Service
{
    public class ForumService : IForumService
    {
        private readonly IForumRepository _forumRepository;
        private readonly ILocationRepository _locationRepository;

        public ForumService()
        {
            _forumRepository = InjectorRepository.CreateInstance<IForumRepository>();
            _locationRepository = InjectorRepository.CreateInstance<ILocationRepository>();
            BindLocation();
        }

        private void BindLocation()
        {
            foreach(Forum f in GetAll())
            {
                f.Location = _locationRepository.Get(f.Location.Id);
            }
        }

        public List<Forum> GetAll()
        {
            return _forumRepository.GetAll();
        }
        public Forum Get(int id)
        {
            return _forumRepository.Get(id);
        }
        public void Create(Forum entity)
        {
            _forumRepository.Create(entity);
        }
        public void Delete(Forum entity)
        {
            _forumRepository.Delete(entity);
        }
        public Forum Update(Forum entity)
        {
            return _forumRepository.Update(entity);
        }
        public void Save()
        {
            _forumRepository.Save();
        }
        public void Subscribe(IObserver observer)
        {
            _forumRepository.Subscribe(observer);
        }
        public void Unsubscribe(IObserver observer)
        {
            _forumRepository.Unsubscribe(observer);
        }
    }
}
