using NFine.Code;
using System;

namespace NFine.Domain
{
    public class IEntity<TEntity>
    {
        public void Create()
        {
            var entity = this as ICreationAudited;
            entity.F_Id = Common.GuId();
            var LoginInfo = OperatorProvider.Provider.GetCurrent();
            if (LoginInfo != null && !string.IsNullOrWhiteSpace(LoginInfo.UserName) && !string.IsNullOrWhiteSpace(LoginInfo.UserId))
            {
                entity.F_CreatorUserId = LoginInfo.UserId;
                entity.F_CreatorUserName = LoginInfo.UserName;
            }
            entity.F_CreatorTime = DateTime.Now;
            
        }
        public void Modify(string keyValue)
        {
            var entity = this as IModificationAudited;
            entity.F_Id = keyValue;
            var LoginInfo = OperatorProvider.Provider.GetCurrent();
            if (LoginInfo != null)
            {
                entity.F_LastModifyUserId = LoginInfo.UserId;
                entity.F_LastModifyUserName = LoginInfo.UserName;
            }
            entity.F_LastModifyTime = DateTime.Now;
        }
        public void Remove()
        {
            var entity = this as IDeleteAudited;
            var LoginInfo = OperatorProvider.Provider.GetCurrent();
            if (LoginInfo != null)
            {
                entity.F_DeleteUserId = LoginInfo.UserId;
                entity.F_DeleteUserName = LoginInfo.UserName;
            }
            entity.F_DeleteTime = DateTime.Now;
            entity.F_DeleteMark = true;
        }
    }
}
