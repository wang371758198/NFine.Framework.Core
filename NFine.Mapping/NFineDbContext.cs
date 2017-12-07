using Microsoft.EntityFrameworkCore;
using NFine.Code;
using System;
using System.Collections.Generic;
using System.Text;
using NFine.Mapping.SystemManage;

namespace NFine.Mapping
{
    public partial class NFineDbContext : DbContext
    {
        public NFineDbContext()
            : base()
        {

        }

        public DbSet<UserMap> UserMap { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
