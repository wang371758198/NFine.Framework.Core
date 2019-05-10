using NFine.Domain.Entity.SystemManage;
using Microsoft.EntityFrameworkCore;


namespace NFine.Mapping.SystemManage
{
    public class ModuleButtonMap : EntityTypeConfiguration<ModuleButtonEntity>
    {
        public override void Map(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ModuleButtonEntity>().ToTable("Sys_ModuleButton").HasKey(_ => _.F_Id);
            modelBuilder.Entity<ModuleButtonEntity>().HasQueryFilter(_ => _.F_DeleteMark != true);
        }
    }
}
