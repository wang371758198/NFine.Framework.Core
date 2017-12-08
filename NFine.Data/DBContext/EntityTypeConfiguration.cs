using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Microsoft.EntityFrameworkCore
{
    public abstract class EntityTypeConfiguration<TEntity>
        where TEntity : class
    {
        //  public abstract void Map(EntityTypeBuilder<TEntity> builder);
        public abstract void Map(ModelBuilder modelBuilder);
    }

    public static class ModelBuilderExtensions
    {
        //public static void AddConfiguration<TEntity>(this ModelBuilder modelBuilder, EntityTypeConfiguration<TEntity> configuration)
        //    where TEntity : class
        //{
        //    configuration.Map(modelBuilder.Entity<TEntity>());
        //}
        public static void AddConfiguration(this ModelBuilder modelBuilder, System.Type type, object configuration)
        {
            var mapMethod = type.GetMethod("Map");
            mapMethod.Invoke(configuration, new object[] { modelBuilder });
        }
    }
}
