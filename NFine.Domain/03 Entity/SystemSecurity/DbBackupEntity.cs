using System;

namespace NFine.Domain.Entity.SystemSecurity
{
    public class FilterIPEntity : IEntity<FilterIPEntity>, ICreationAudited, IDeleteAudited, IModificationAudited
    {
        public string F_Id { get; set; }
        public bool? F_Type { get; set; }
        public string F_StartIP { get; set; }
        public string F_EndIP { get; set; }
        public int? F_SortCode { get; set; }
        public bool? F_DeleteMark { get; set; }
        public bool? F_EnabledMark { get; set; }
        public string F_Description { get; set; }
        public DateTime? F_CreatorTime { get; set; }
        public string F_CreatorUserId { get; set; }
        public DateTime? F_LastModifyTime { get; set; }
        public string F_LastModifyUserId { get; set; }
        public DateTime? F_DeleteTime { get; set; }
        public string F_DeleteUserId { get; set; }

        public string F_CreatorUserName { get; set; }

        public string F_DeleteUserName { get; set; }

        public string F_LastModifyUserName { get; set; }
    }
}
