using AutoMapper;
using QTF.Data.Abstraction;
using QTF.Domain.Entity.UserBundle;
using QTF.RepositoryContract;
using QTF.Service.Exceptions;
using QTF.ServiceContract;
using System;
using System.Collections.Generic;
using System.Text;

namespace QTF.Service
{
    internal class UserService : IUserService
    {
     
        private readonly IUserRepository _userRepository;
  
        public UserService(    IUserRepository userRepository    )
        {
 
           
            _userRepository = userRepository;
        }
        public IEnumerable<ApplicationUser> GetAll()
        {

            var result = _userRepository.GetAll();

            return result;
        }

        public bool CheckUserKey(string userKey)
        {
            var user = _userRepository.Get(userKey);
            if (user == null)
                throw new EntityNotFound(user.GetType().Name, userKey);

            return true;
        }


        public ApplicationUser Get(string key)
        {
            var entity = _userRepository.Get(key);
           
            return entity;
        }



    }
}
