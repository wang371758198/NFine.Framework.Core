using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NFine.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using NFine.Domain;
using NFine.Application.SystemManage;
using NFine.Code;
using NFine.Application.SystemSecurity;
using NFine.Domain.Entity.SystemSecurity;
using NFine.Application;

namespace NFine.Web.Controllers
{
    public class HomeController : BaseController
    {
        private UserApp userApp;
        private LogApp logApp;
        public HomeController(UserApp userApp, LogApp logApp)
        {
            this.userApp = userApp;
            this.logApp = logApp;
        }

        // [Authorize]
        public override ActionResult Index()
        {
            if (!string.IsNullOrWhiteSpace(HttpContext.Request.Query["ReturnUrl"].ToString()))
            {
                return Redirect(HttpContext.Request.Query["ReturnUrl"].ToString());
            }
            return View();
        }

        public IActionResult Default()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult ChangePwd() => View();

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> SubmitPwd(ChangePassWordReuqest query)
        {
            if (string.IsNullOrWhiteSpace(query.F_NewPawword))
                return Error("新密码不能为空");
            if (string.IsNullOrWhiteSpace(query.F_OldPawword))
                return Error("老密码不能为空");

            if (!userApp.ChangePassworld(OperatorProvider.Provider.GetCurrent().UserId, query.F_OldPawword, query.F_NewPawword))
                return Error("原密码不正确");

            logApp.WriteDbLog(new LogEntity
            {
                F_ModuleName = "系统登录",
                F_Type = DbLogType.Exit.ToString(),
                F_Account = OperatorProvider.Provider.GetCurrent().UserCode,
                F_NickName = OperatorProvider.Provider.GetCurrent().UserName,
                F_Result = true,
                F_Description = "安全退出系统",
            });
            await OperatorProvider.Provider.RemoveCurrent();
            return Success("密码修改成功，请重新登录");
        }

    }
}
