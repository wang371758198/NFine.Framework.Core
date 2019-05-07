using NFine.Data;
using NFine.Domain.Entity.SystemManage;
using NFine.Domain.IRepository.SystemManage;
using NFine.Repository.SystemManage;

namespace NFine.Repository.SystemManage
{
    public class RoleAuthorizeRepository : RepositoryBase<RoleAuthorizeEntity>, IRoleAuthorizeRepository
    {
        public RoleAuthorizeRepository(NFineDbContext dbContext):base(dbContext)
        {

        }
    }
}
