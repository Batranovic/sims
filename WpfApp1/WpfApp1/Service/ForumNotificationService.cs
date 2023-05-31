﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Domain.Models;
using WpfApp1.Domain.RepositoryInterfaces;
using WpfApp1.Domain.ServiceInterfaces;
using WpfApp1.Repository;

namespace WpfApp1.Service
{
    public class ForumNotificationService : IForumNotificationService
    {
        private readonly IForumNotificationRepository _notificationRepository;
        private readonly IForumRepository _forumRepository;
        private readonly IOwnerRepository _ownerRepository;
        public ForumNotificationService()
        {
            _notificationRepository = InjectorRepository.CreateInstance<IForumNotificationRepository>();
            //_forumRepository = InjectorRepository.CreateInstance<IForumRepository>();
            _ownerRepository = InjectorRepository.CreateInstance<IOwnerRepository>();
            BindOwner();
            //BindForum();
        }

        private void BindForum()
        {
            foreach (var item in GetAll())
            {
                item.Forum = _forumRepository.Get(item.Forum.Id);
            }
        }

        private void BindOwner()
        {
            foreach(var item in GetAll())
            {
                item.Owner = _ownerRepository.Get(item.Owner.Id);
            }
        }

        public List<ForumNotification> GetAll()
        {
            return _notificationRepository.GetAll();
        }
        public ForumNotification Get(int id)
        {
            return _notificationRepository.Get(id);
        }
        public void Create(ForumNotification entity)
        {
            _notificationRepository.Create(entity);
        }
        public void Delete(ForumNotification entity)
        {
            _notificationRepository.Delete(entity);
        }
        public ForumNotification Update(ForumNotification entity)
        {
            return _notificationRepository.Update(entity);
        }
        public void Save()
        {
            _notificationRepository.Save();
        }

        

    }
}
