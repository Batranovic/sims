﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Domain.Models;

namespace WpfApp1.Domain.ServiceInterfaces
{
    public interface IGuestService : IService<Guest>
    {
        Guest GetByUsernameAndPassword(string username, string password);
        bool CanUseBonusPoints(Guest guest);
        void ResetBonusPoints(Guest guest);
    }
}
