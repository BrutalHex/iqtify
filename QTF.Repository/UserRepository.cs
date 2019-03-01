using Microsoft.EntityFrameworkCore;
using QTF.Data.Infra;
using QTF.Domain.Entity.UserBundle;
using QTF.RepositoryContract;
using System;
using System.Collections.Generic;
using System.Text;

namespace QTF.Repository
{

    public class UserRepository : GenericRepository<ApplicationUser, string>, IUserRepository
    {

        public UserRepository(DbContext context) : base(context)
        {
        }


    }
}
