using NFine.Domain.Entity.SystemSecurity;
using Microsoft.EntityFrameworkCore;


namespace NFine.Mapping.SystemSecurity
{
    public class LogMap : EntityTypeConfiguration<LogEntity>
    {
        //public LogMap()
        //{
        //    this.ToTable("Sys_Log");
        //    this.HasKey(t => t.F_Id);
        //}

        public override void Map(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LogEntity>().ToTable("Sys_Log").HasKey(_ => _.F_Id);
        }
    }
}
