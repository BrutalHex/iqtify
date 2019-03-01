using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using QTF.ServiceContract;
using QTF.Service;

namespace QTF.Service.Infra
{
    
    public class RegisterServices
    {
        private readonly IServiceCollection _service;
        public RegisterServices(IServiceCollection services)
        {
            _service = services;
            Register();
        }

        private void Register()
        {
            _service.AddScoped<IAddressService, AddressService>();
            _service.AddScoped<IUserService, UserService>();
            
        }
    }
}
