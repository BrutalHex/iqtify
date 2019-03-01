using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using QTF.RepositoryContract;

namespace QTF.Repository.Infra
{
    public  class RegisterRepositories
    {
        private readonly IServiceCollection _service;
        public RegisterRepositories(IServiceCollection services)
        {
            _service = services;
            RegisterServices();
        }

        private void RegisterServices()
        {

            _service.AddScoped<IAddressRepository, AddressRepository>();
            _service.AddScoped<IUserRepository, UserRepository>();
        }
    }
}
