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
    public class ReportForumRepository : IReportForumRepository
    {
        private const string _filePath = "../../../Resources/Data/forumReports.csv";
        private readonly Serializer<ReportForum> _serializer;
        private static IReportForumRepository _instance = null;
        private List<ReportForum> _reportForums;

        public static IReportForumRepository GetInstance()
        {
            if (_instance == null)
            {
                _instance = new ReportForumRepository();
            }
            return _instance;
        }

        private ReportForumRepository()
        {
            _serializer = new();
            _reportForums = new();
            _reportForums = _serializer.FromCSV(_filePath);
        }
        public ReportForum Create(ReportForum entity)
        {
            entity.Id = NextId();
            _reportForums.Add(entity);
            Save();
            return entity;
        }
        public ReportForum Delete(ReportForum entity)
        {
            _reportForums.Remove(entity);
            Save();
            return entity;
        }
        public ReportForum Get(int id)
        {
            return _reportForums.Find(a => a.Id == id);
        }
        public List<ReportForum> GetAll()
        {
            return _reportForums;
        }

        public int NextId()
        {
            if (_reportForums.Count == 0) return 0;
            int newId = _reportForums[_reportForums.Count() - 1].Id + 1;
            foreach (ReportForum a in _reportForums)
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
            _serializer.ToCSV(_filePath, _reportForums);
        }
        public ReportForum Update(ReportForum entity)
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
    }
}
