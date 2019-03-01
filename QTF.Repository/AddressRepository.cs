using Microsoft.EntityFrameworkCore;
using QTF.Data.Infra;
using QTF.RepositoryContract;
using QTF.Domain.Entity.UserBundle;


namespace QTF.Repository
{
    public class AddressRepository : GenericRepository<AddressEntity,int>, IAddressRepository
    {

        public AddressRepository(DbContext context) : base(context)
        {
        }
      

    }
}


