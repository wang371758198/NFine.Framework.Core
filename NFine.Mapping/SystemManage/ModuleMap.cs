using NFine.Domain.Entity.SystemManage;
using Microsoft.EntityFrameworkCore;


namespace NFine.Mapping.SystemManage
{
    public class ModuleMap : EntityTypeConfiguration<ModuleEntity>
    {
        //public ModuleMap()
        //{
        //    this.ToTable("Sys_Module");
        //    this.HasKey(t => t.F_Id);
        //}
        public override void Map(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ModuleEntity>().ToTable("Sys_Module").HasKey(_ => _.F_Id);
            modelBuilder.Entity<ModuleEntity>().HasQueryFilter(_ => _.F_DeleteMark != true);
        }
    }
}
