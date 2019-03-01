using QTF.Domain.Entity.UserBundle;
using System;
using System.Collections.Generic;
using System.Text;

namespace QTF.ServiceContract
{
    public interface IUserService
    {
        IEnumerable<ApplicationUser> GetAll();
        ApplicationUser Get(string key);
        /// <summary>
        /// checks user existance
        /// </summary>
        /// <param name="userKey">the primary key of user</param>
        /// <returns>true if user is existed</returns>
        bool CheckUserKey(string userKey);
    }
}
