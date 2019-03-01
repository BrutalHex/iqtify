using AutoMapper;
using QTF.Domain.Entity.UserBundle;
using QTF.Dtos.UserBundle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Traveller.Service.Infra
{
    public class AutoMapperConfiguration : Profile
    {
        public AutoMapperConfiguration()
        {
            CreateMap<AddressDto, AddressEntity>();
            CreateMap<AddressEntity, GetAddressDto>().ForMember(dst => dst.Username,dst=>dst.MapFrom(d=>d.User.UserName));
            
        }
     
      
    }
}
