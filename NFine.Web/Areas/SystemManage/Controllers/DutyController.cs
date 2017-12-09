using NFine.Application.SystemManage;
using NFine.Code;
using NFine.Domain.Entity.SystemManage;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace NFine.Web.Areas.SystemManage.Controllers
{
    public class DutyController : BaseController
    {
        private DutyApp dutyApp = new DutyApp();

        [HttpGet]
        //[HandlerAjaxOnly]
        public ActionResult GetGridJson(string keyword)
        {
            var data = dutyApp.GetList(keyword);
            return Content(data.ToJson());
        }
        [HttpGet]
       // [HandlerAjaxOnly]
        public ActionResult GetFormJson(string keyValue)
        {
            var data = dutyApp.GetForm(keyValue);
            return Content(data.ToJson());
        }
        [HttpPost]
        //[HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public ActionResult SubmitForm(RoleEntity roleEntity, string keyValue)
        {
            dutyApp.SubmitForm(roleEntity, keyValue);
            return Success("操作成功。");
        }
        [HttpPost]
       // [HandlerAjaxOnly]
       // [HandlerAuthorize]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteForm(string keyValue)
        {
            dutyApp.DeleteForm(keyValue);
            return Success("删除成功。");
        }
    }
}
