using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp.Observer;
using WpfApp1.Domain.Models;
using WpfApp1.Domain.RepositoryInterfaces;
using WpfApp1.Domain.ServiceInterfaces;

namespace WpfApp1.Service
{
    public class ForumCommentsService : IForumCommentsService
    {
        private readonly IForumCommentsRepository _forumCommentsRepository;
        private readonly IForumRepository _forumRepository;
        private readonly IOwnerRepository _ownerRepository;
        private readonly IGuestRepository _guestRepository;

        public ForumCommentsService()
        {
            _forumCommentsRepository = InjectorRepository.CreateInstance<IForumCommentsRepository>();
            _forumRepository = InjectorRepository.CreateInstance<IForumRepository>();
            _ownerRepository = InjectorRepository.CreateInstance<IOwnerRepository>();
            _guestRepository = InjectorRepository.CreateInstance<IGuestRepository>();
            BindForum();
            BindGuest();
            BindOwner();
        }

       
        private void BindOwner()
        {
            foreach(ForumComments f in GetAll().FindAll(k => k.Author.UserKind == Domain.Models.Enums.UserKind.Owner))
            {
                f.Author = _ownerRepository.Get(f.Author.Id);  
            }
        }

        private void BindGuest()
        {
            foreach(ForumComments f in GetAll().FindAll(k => k.Author.UserKind == Domain.Models.Enums.UserKind.Guest))
            {
                f.Author = _guestRepository.Get(f.Author.Id);
            }
        }

        private void BindForum()
        {
            foreach(ForumComments f in GetAll())
            {
                f.Forum = _forumRepository.Get(f.Forum.Id);
                f.Forum.Comments.Add(f);
                f.Forum.FirstComment = f.Forum.Comments[0]; 
            }
        }
        public List<ForumComments> GetAll()
        {
            return _forumCommentsRepository.GetAll();
        }
        public ForumComments Get(int id)
        {
            return _forumCommentsRepository.Get(id);
        }
        public void Create(ForumComments entity)
        {
            _forumCommentsRepository.Create(entity);
        }
        public void Delete(ForumComments entity)
        {
            _forumCommentsRepository.Delete(entity);
        }
        public ForumComments Update(ForumComments entity)
        {
            return _forumCommentsRepository.Update(entity);
        }
        public void Save()
        {
            _forumCommentsRepository.Save();
        }
        public void Subscribe(IObserver observer)
        {
            _forumCommentsRepository.Subscribe(observer);
        }
        public void Unsubscribe(IObserver observer)
        {
            _forumCommentsRepository.Unsubscribe(observer);
        }
    }
}
