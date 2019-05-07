using NFine.Application.SystemManage;
using NFine.Code;
using NFine.Domain.Entity.SystemManage;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace NFine.Web.Areas.SystemManage.Controllers
{
    public class RoleController : BaseController
    {
        private RoleApp roleApp;
        private RoleAuthorizeApp roleAuthorizeApp;
        private ModuleApp moduleApp;
        private ModuleButtonApp moduleButtonApp;
        public RoleController(RoleApp roleApp, RoleAuthorizeApp roleAuthorizeApp, ModuleApp moduleApp, ModuleButtonApp moduleButtonApp)
        {
            this.roleApp = roleApp;
            this.roleAuthorizeApp = roleAuthorizeApp;
            this.moduleApp = moduleApp;
            this.moduleButtonApp = moduleButtonApp;
        }

        [HttpGet]
       // [HandlerAjaxOnly]
        public ActionResult GetGridJson(string keyword)
        {
            var data = roleApp.GetList(keyword);
            return Content(data.ToJson());
        }
        [HttpGet]
        //[HandlerAjaxOnly]
        public ActionResult GetFormJson(string keyValue)
        {
            var data = roleApp.GetForm(keyValue);
            return Content(data.ToJson());
        }
        [HttpPost]
        //[HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public ActionResult SubmitForm(RoleEntity roleEntity, string permissionIds, string keyValue)
        {
            roleApp.SubmitForm(roleEntity, permissionIds?.Split(','), keyValue);
            return Success("操作成功。");
        }
        [HttpPost]
        //[HandlerAjaxOnly]
        //[HandlerAuthorize]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteForm(string keyValue)
        {
            roleApp.DeleteForm(keyValue);
            return Success("删除成功。");
        }
    }
}
