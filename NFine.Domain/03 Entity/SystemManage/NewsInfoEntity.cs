using System;
using System.Collections.Generic;
using System.Text;

namespace NFine.Domain.Entity.SystemManage
{
    public class NewsInfoEntity : IEntity<NewsInfoEntity>, ICreationAudited, IDeleteAudited, IModificationAudited
    {
        public string F_Id { get; set; }
        public string F_CreatorUserId { get; set; }
        public DateTime? F_CreatorTime { get; set; }
        public string F_CreatorUserName { get; set; }
        public string F_LastModifyUserId { get; set; }
        public DateTime? F_LastModifyTime { get; set; }
        public string F_LastModifyUserName { get; set; }
        public bool? F_DeleteMark { get; set; } = false;
        public string F_DeleteUserId { get; set; }
        public DateTime? F_DeleteTime { get; set; }
        public string F_DeleteUserName { get; set; }

        public string F_Title { get; set; }

        public string F_Spokesman { get; set; }

        public string F_Content { get; set; }

        public bool F_Status { get; set; }

        public int F_Type { get; set; }
    }
}
