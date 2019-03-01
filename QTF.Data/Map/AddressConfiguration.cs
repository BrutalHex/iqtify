using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTF.Domain.Entity.UserBundle;
using System.ComponentModel.DataAnnotations.Schema;

namespace QTF.Data.Map
{

    public class AddressConfiguration : IEntityTypeConfiguration<AddressEntity>
    {
        public void Configure(EntityTypeBuilder<AddressEntity> builder)
        {

            builder.ToTable("Address");

            builder.HasKey(o => o.Key);
       

            builder.Property(a => a.Key).HasColumnName("Address_Key");

            builder.Property(a => a.PostalCode).HasColumnName("Address_PostalCode");
            builder.Property(a => a.Path).HasColumnName("Address_Path").HasMaxLength(500);
          
            builder.Property(a => a.UserKey).HasColumnName("Address_UserKey");

            builder.HasOne(a => a.User).WithMany(a => a.AddressEntities).HasForeignKey(a => a.UserKey);
        }
    }
}
