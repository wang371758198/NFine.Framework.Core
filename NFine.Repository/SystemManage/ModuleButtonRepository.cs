using NFine.Data;
using NFine.Domain.Entity.SystemManage;
using NFine.Domain.IRepository.SystemManage;
using NFine.Repository.SystemManage;
using System.Collections.Generic;

namespace NFine.Repository.SystemManage
{
    public class ModuleButtonRepository : RepositoryBase<ModuleButtonEntity>, IModuleButtonRepository
    {
        private IRepositoryBase repositoryBase;
        public ModuleButtonRepository(NFineDbContext dbContext,IRepositoryBase repositoryBase):base(dbContext)
        {
            this.repositoryBase = repositoryBase;
        }

        public void SubmitCloneButton(List<ModuleButtonEntity> entitys)
        {
            using (var db = repositoryBase.BeginTrans())
            {
                foreach (var item in entitys)
                {
                    db.Insert(item);
                }
                db.Commit();
            }
        }
    }
}
