using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using QTF.Dtos;
using QTF.Dtos.UserBundle;

namespace QTF.ServiceContract
{
    public interface IAddressService
    {
        ValidationInformation Add(AddressDto item);
        ValidationInformation Edit(AddressDto item);
        AddressDto Get(int key);
        IEnumerable<GetAddressDto> GetAll();
        void Remove(AddressDto entity);
        void Remove(int key);
        int SaveChanges();
        Task<int> SaveChangesAsync();
    }
}
