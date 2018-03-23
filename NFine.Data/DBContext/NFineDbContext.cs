/*******************************************************************************
 * Copyright © 2016 NFine.Framework 版权所有
 * Author: NFine
 * Description: NFine快速开发平台
 * Website：http://www.nfine.cn
*********************************************************************************/
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Extensions;
using System;
using System.Linq;
using System.Reflection;
using System.Data.Common;
using NFine.Code;

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
            //Console.WriteLine("ConfigurationManager.ConnectionStrings[\"NFineDbContext\"]:{0}", ConfigurationManager.ConnectionStrings["NFineDbContext"]);
            optionsBuilder.UseSqlServer(ConfigurationManager.ConnectionStrings["NFineDbContext"],options=>options.UseRowNumberForPaging());
        }

        // public DbSet<UserEntity> UserEntities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            string currentAssembleFileName = Assembly.GetExecutingAssembly().CodeBase.ToString();
            //Console.WriteLine("currentAssembleFileName:" + currentAssembleFileName);
            string assembleFileName = currentAssembleFileName.Replace(".Data.", ".Mapping.").Replace("file:///","");

            //Console.WriteLine(" pre assembleFileName Path: " + assembleFileName);

            if (assembleFileName.IndexOf(":") == -1)
                assembleFileName = @"/" + assembleFileName;
            //Console.WriteLine("assembleFileName Path: " + assembleFileName);
            Assembly asm = Assembly.LoadFile(assembleFileName);
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
