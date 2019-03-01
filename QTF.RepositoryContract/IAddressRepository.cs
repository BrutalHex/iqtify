using QTF.Data.Abstraction;
using System;
using System.Collections.Generic;
using System.Text;
using QTF.Domain.Entity;
using QTF.Domain.Entity.UserBundle;

namespace QTF.RepositoryContract
{
    public interface IAddressRepository : IGenericRepository<AddressEntity,int>
    {
        
    }
}
