﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp.Observer;
using WpfApp1.Domain.Models;
using WpfApp1.Repository;

namespace WpfApp1.Domain.RepositoryInterfaces
{
    public interface IReportForumRepository : IRepository<ReportForum>
    { 
        ReportForum Create(ReportForum entity);
        int NextId();
        ReportForum Delete(ReportForum entity);
    }
}