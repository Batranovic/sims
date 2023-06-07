using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Domain.Models;
using WpfApp1.Domain.RepositoryInterfaces;
using WpfApp1.Domain.ServiceInterfaces;
using WpfApp1.Repository;
using WpfApp1.Service;

namespace WpfApp1.Service
{
    public class ReportForumService : IReportForumService
    {
        private readonly IReportForumRepository _reportForumRepository;
        private readonly IForumCommentsRepository _forumRepository;
        private readonly IOwnerRepository _ownerRepository;
        private readonly IGuestRepository _guestRepository;
        public ReportForumService()
        {
            _reportForumRepository = InjectorRepository.CreateInstance<IReportForumRepository>();
            _forumRepository = InjectorRepository.CreateInstance<IForumCommentsRepository>();
            _ownerRepository = InjectorRepository.CreateInstance<IOwnerRepository>();
            _guestRepository = InjectorRepository.CreateInstance<IGuestRepository>();
            BindForum();
            BindAuthor();
        }
        private void BindAuthor()
        {
            foreach(var rf in GetAll())
            {
                if(rf.Author.UserKind == Domain.Models.Enums.UserKind.Guest)
                {
                    rf.Author = _guestRepository.Get(rf.Author.Id);
                }
                else
                {
                    rf.Author = _ownerRepository.Get(rf.Author.Id);
                }
            }
        }
        private void BindForum()
        {
            foreach(var  rf in GetAll())
            {
                rf.ForumComment =  _forumRepository.Get(rf.ForumComment.Id);
                rf.ForumComment.ForumReports.Add(rf);
            }
        }

        public List<ReportForum> GetAll()
        {
            return _reportForumRepository.GetAll();
        }
        public ReportForum Get(int id)
        {
            return _reportForumRepository.Get(id);
        }
        public void Create(ReportForum entity)
        {
            _reportForumRepository.Create(entity);
        }
        public void Delete(ReportForum entity)
        {
            _reportForumRepository.Delete(entity);
        }
        public ReportForum Update(ReportForum entity)
        {
            return _reportForumRepository.Update(entity);
        }

        public void Save()
        {
            throw new NotImplementedException();
        }
    }
}
