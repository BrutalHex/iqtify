using QTF.Data.Abstraction;
using QTF.Domain.Entity.UserBundle;
using System;
using System.Collections.Generic;
using System.Text;

namespace QTF.RepositoryContract
{
 
    public interface IUserRepository : IGenericRepository<ApplicationUser, string>
    {

    }
}
