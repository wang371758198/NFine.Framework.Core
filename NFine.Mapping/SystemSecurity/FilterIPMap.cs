using NFine.Domain.Entity.SystemSecurity;
using Microsoft.EntityFrameworkCore;


namespace NFine.Mapping.SystemSecurity
{
    public class FilterIPMap : EntityTypeConfiguration<FilterIPEntity>
    {
        //public FilterIPMap()
        //{
        //    this.ToTable("Sys_FilterIP");
        //    this.HasKey(t => t.F_Id);
        //}
        public override void Map(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FilterIPEntity>().ToTable("Sys_FilterIP").HasKey(_ => _.F_Id);
        }
    }
}
