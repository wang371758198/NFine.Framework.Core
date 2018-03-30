using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

    /// <summary>
    /// NotSupportedHandler 的摘要说明
    /// </summary>
    public class NotSupportedHandler : Handler
    {
        public override void Process()
        {
            WriteJson(new
            {
                ///action 参数为空或者 action 不被支持。
                state = "action 参数为空或者 action 不被支持。"
            });
        }
    }

