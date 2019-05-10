using NFine.Domain.Entity.SystemManage;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace NFine.Mapping.SystemManage
{
    public class UserMap : EntityTypeConfiguration<UserEntity>
    {
        //public UserMap()
        //{
        //    this.ToTable("Sys_User");
        //    this.HasKey(t => t.F_Id);
        //}
        //public override void Map(EntityTypeBuilder<UserEntity> builder)
        //{
        //    builder.ToTable("Sys_User");
        //    builder.HasKey(_ => _.F_Id);
        //}

        public override void Map(ModelBuilder builder)
        {
            builder.Entity<UserEntity>().ToTable("Sys_User").HasKey(_ => _.F_Id);
            builder.Entity<UserEntity>().HasQueryFilter(_ => _.F_DeleteMark != true);
        }
    }
}
