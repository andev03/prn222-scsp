﻿using BusinessObject;
using BusinessObject.Models;
using DataAccess.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.IRepositories
{
    public interface IBadgeRepository : IGenericRepository<Badge>
    {
        Task<bool> IsExist(int id);

        List<Badge> ListBadgesUserReceived(User user);
    }
}
