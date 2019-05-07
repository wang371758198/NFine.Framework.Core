using NFine.Code;
using NFine.Data;
using NFine.Domain.Entity.SystemManage;
using NFine.Domain.IRepository.SystemManage;
using NFine.Repository.SystemManage;
using System;

namespace NFine.Repository.SystemManage
{
    public class UserRepository : RepositoryBase<UserEntity>, IUserRepository
    {
        private IRepositoryBase repositoryBase;
        public UserRepository(NFineDbContext dbContext, IRepositoryBase repositoryBase) : base(dbContext)
        {
            this.repositoryBase = repositoryBase;
        }

        public void DeleteForm(string keyValue)
        {
            
            using (var db = this.repositoryBase .BeginTrans())
            {
                db.Delete<UserEntity>(t => t.F_Id == keyValue);
                db.Delete<UserLogOnEntity>(t => t.F_UserId == keyValue);
                db.Commit();
            }
        }
        public void SubmitForm(UserEntity userEntity, UserLogOnEntity userLogOnEntity, string keyValue)
        {
            using (var db = this.repositoryBase.BeginTrans())
            {
                if (!string.IsNullOrEmpty(keyValue))
                {
                    db.Update(userEntity);
                }
                else
                {
                    userLogOnEntity.F_Id = userEntity.F_Id;
                    userLogOnEntity.F_UserId = userEntity.F_Id;
                    userLogOnEntity.F_UserSecretkey = EncryptProvider.CreateDesKey();
                    userLogOnEntity.F_UserPassword = EncryptProvider.Md5(EncryptProvider.DESEncrypt(EncryptProvider.Md5(userLogOnEntity.F_UserPassword, Code.Internal.MD5Length.L32).ToLower(), userLogOnEntity.F_UserSecretkey).ToLower(), Code.Internal.MD5Length.L32).ToLower();
                    db.Insert(userEntity);
                    db.Insert(userLogOnEntity);
                }
                db.Commit();
            }
        }

        public void ChangeUserPassword(UserLogOnEntity userLogOnEntity, string password)
        {
            using (var db = this.repositoryBase)
            {
                userLogOnEntity.F_UserPassword = EncryptProvider.Md5(password, Code.Internal.MD5Length.L32).ToLower();
                userLogOnEntity.F_ChangePasswordDate = DateTime.Now;
                db.Update<UserLogOnEntity>(userLogOnEntity);
            }
        }

    }
}
