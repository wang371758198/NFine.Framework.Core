using NFine.Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNetCore.Mvc;

namespace NFine.Web.Areas.ExampleManage.Controllers
{
    public class SendMailController : BaseController
    {

        [HttpPost]
        [ValidateAntiForgeryToken]
       
        public ActionResult SendMail(string account, string title, string content)
        {
            MailHelper mail = new MailHelper();
            mail.MailServer = Configs.GetValue("MailHost");
            mail.MailUserName = Configs.GetValue("MailUserName");
            mail.MailPassword = Configs.GetValue("MailPassword");
            mail.MailName = "NFine快速开发平台";
            mail.Send(account, title, content);
            return Success("发送成功。");
        }
    }
}
