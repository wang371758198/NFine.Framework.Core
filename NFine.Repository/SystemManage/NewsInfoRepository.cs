using System;
using System.Collections.Generic;
using System.Text;
using NFine.Data;
using NFine.Domain.Entity.SystemManage;
using NFine.Domain.IRepository.SystemManage;

namespace NFine.Repository.SystemManage
{
    public class NewsInfoRepository : RepositoryBase<NewsInfoEntity>, INewsInfoRepository
    {
        public NewsInfoRepository(NFineDbContext dbContext) : base(dbContext)
        {

        }
    }
}
