using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NFine.Code;
using NFine.Domain.Entity.SystemSecurity;
using NFine.Domain.Entity.SystemManage;
using NFine.Application;
using NFine.Application.SystemManage;
using NFine.Application.SystemSecurity;

namespace NFine.Web.Controllers
{
    public class LoginController : Controller
    {
        private UserApp userApp;
        private LogApp logApp;
        public LoginController(UserApp userApp,LogApp logApp)
        {
            this.userApp = userApp;
            this.logApp = logApp;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetAuthCode()
        {
            return File(new VerifyCode().GetVerifyCode(), @"image/Gif");
        }

        [HttpPost]
        public async Task<ActionResult> CheckLogin(string username, string password, string code)
        {
            LogEntity logEntity = new LogEntity();
            logEntity.F_ModuleName = "系统登录";
            logEntity.F_Type = DbLogType.Login.ToString();
            try
            {
                if (NFine.Code.WebHelper.GetSession("nfine_session_verifycode").IsEmpty() || EncryptProvider.Md5(code.ToLower(), Code.Internal.MD5Length.L16) != NFine.Code.WebHelper.GetSession("nfine_session_verifycode").ToString())
                {
                    throw new Exception("验证码错误，请重新输入");
                }

                UserEntity userEntity = userApp.CheckLogin(username, password);
                if (userEntity != null)
                {
                    OperatorModel operatorModel = new OperatorModel();
                    operatorModel.UserId = userEntity.F_Id;
                    operatorModel.UserCode = userEntity.F_Account;
                    operatorModel.UserName = userEntity.F_RealName;
                    operatorModel.CompanyId = userEntity.F_OrganizeId;
                    operatorModel.DepartmentId = userEntity.F_DepartmentId;
                    operatorModel.RoleId = userEntity.F_RoleId;
                    operatorModel.LoginIPAddress = Net.Ip;
                    operatorModel.LoginIPAddressName = Net.GetLocation(operatorModel.LoginIPAddress);
                    operatorModel.LoginTime = DateTime.Now;
                    operatorModel.LoginToken = EncryptProvider.DESEncrypt(Guid.NewGuid().ToString());
                    if (userEntity.F_Account == "admin")
                    {
                        operatorModel.IsSystem = true;
                    }
                    else
                    {
                        operatorModel.IsSystem = false;
                    }
                   await  OperatorProvider.Provider.AddCurrent(operatorModel);
                    logEntity.F_Account = userEntity.F_Account;
                    logEntity.F_NickName = userEntity.F_RealName;
                    logEntity.F_Result = true;
                    logEntity.F_Description = "登录成功";
                    logEntity.F_CreatorUserId = operatorModel.UserId;
                    logEntity.F_CreatorUserName = operatorModel.UserName;
                    logApp.WriteDbLog(logEntity);
                }
                return Content(new AjaxResult { state = ResultType.success.ToString(), message = "登录成功。" }.ToJson());
            }
            catch (Exception ex)
            {
                logEntity.F_Account = username;
                logEntity.F_NickName = username;
                logEntity.F_Result = false;
                logEntity.F_Description = "登录失败，" + ex.Message;
                logEntity.F_CreatorUserId = username;
                logEntity.F_CreatorUserName = username;
                logApp.WriteDbLog(logEntity);
                return Content(new AjaxResult { state = ResultType.error.ToString(), message = ex.Message }.ToJson());
            }
        }

        [HttpGet]
        public async Task<ActionResult> OutLogin()
        {
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
            return RedirectToAction("Index", "Login");
        }

        public IActionResult GetConfig()
        {
            OperatorModel operatorModel = new OperatorModel();
            var str = EncryptProvider.DESEncrypt(operatorModel.ToJson());
            //WebHelper.WriteSession("nfine_loginuserkey_2016", str);
           //   HttpContext.Session.TryGetValue("nfine_loginuserkey_2016", out byte[] bytes);

           // var encryptString = System.Text.Encoding.UTF8.GetString(bytes);
            var decryptString = EncryptProvider.DESDecrypt(str);
            return Content("encrypt:  "+str +" \n decrypt: "+decryptString,"application/json");
        }

    }
}
