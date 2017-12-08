/*******************************************************************************
 * Copyright © 2016 NFine.Framework 版权所有
 * Author: NFine
 * Description: NFine快速开发平台
 * Website：http://www.nfine.cn
*********************************************************************************/
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
        }
    }
}
