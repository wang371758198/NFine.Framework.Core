using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Extensions;
using System;
using System.Linq;
using System.Reflection;
using System.Data.Common;
using NFine.Code;
using System.IO;

namespace NFine.Data
{
    public partial class NFineDbContext : DbContext
    {
        public NFineDbContext()
            : base()
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConfigurationManager.ConnectionStrings["NFineDbContext"],options=>options.UseRowNumberForPaging());
        }

        // public DbSet<UserEntity> UserEntities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            string executingAssemblyDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
           
            string mappingAssemblePath = Path.Combine(executingAssemblyDirectory, "NFine.Mapping.dll");

            if (!File.Exists(mappingAssemblePath))
                throw new Exception($"{mappingAssemblePath}文件不存在");

            Assembly asm = Assembly.LoadFile(mappingAssemblePath);

            var typesToRegister = asm.GetTypes()
            .Where(type => !String.IsNullOrEmpty(type.Namespace))
            .Where(type => type.BaseType != null && type.BaseType.IsGenericType && type.BaseType.GetGenericTypeDefinition() == typeof(EntityTypeConfiguration<>));
            foreach (var type in typesToRegister)
            {
                object configurationInstance = Activator.CreateInstance(type);
               
                modelBuilder.AddConfiguration(type, configurationInstance);
            }
            //modelBuilder.AddConfiguration(new UserMap());
            base.OnModelCreating(modelBuilder);
        }

    }
}
