using NFine.Domain.Entity.SystemManage;
using Microsoft.EntityFrameworkCore;


namespace NFine.Mapping.SystemManage
{
    public class RoleAuthorizeMap : EntityTypeConfiguration<RoleAuthorizeEntity>
    {
        //public RoleAuthorizeMap()
        //{
        //    this.ToTable("Sys_RoleAuthorize");
        //    this.HasKey(t => t.F_Id);
        //}

        public override void Map(ModelBuilder builder)
        {
            builder.Entity<RoleAuthorizeEntity>().ToTable("Sys_RoleAuthorize").HasKey(_ => _.F_Id);
        }
    }
}
