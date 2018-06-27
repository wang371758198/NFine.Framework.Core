using NFine.Data;
using NFine.Domain.Entity.SystemManage;

namespace NFine.Domain.IRepository.SystemManage
{
    public interface IUserRepository : IRepositoryBase<UserEntity>
    {
        void DeleteForm(string keyValue);
        void SubmitForm(UserEntity userEntity, UserLogOnEntity userLogOnEntity, string keyValue);

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="userLogOnEntity"></param>
        /// <param name="password"></param>
        void ChangeUserPassword(UserLogOnEntity userLogOnEntity, string password);
    }
}
