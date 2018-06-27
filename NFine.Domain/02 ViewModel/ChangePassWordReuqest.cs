using System;
using System.Collections.Generic;
using System.Text;

namespace NFine.Domain
{
    /// <summary>
    /// 修改手机号请求参数
    /// </summary>
    public class ChangePassWordReuqest
    {
        /// <summary>
        /// 老手机号
        /// </summary>
        public string F_OldPawword { get; set; }

        /// <summary>
        /// 新手机号
        /// </summary>
        public string F_NewPawword { get; set; }
    }
}
