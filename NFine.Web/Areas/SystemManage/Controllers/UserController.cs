using NFine.Application.SystemManage;
using NFine.Code;
using NFine.Domain.Entity.SystemManage;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;


namespace NFine.Web.Areas.SystemManage.Controllers
{
    public class UserController : BaseController
    {
        private UserApp userApp;
        private UserLogOnApp userLogOnApp;
        private OrganizeApp organizeApp;
        private RoleApp roleApp;
        private DutyApp dutyApp;
        public UserController(UserApp userApp, UserLogOnApp userLogOnApp,OrganizeApp organizeApp, RoleApp roleApp,DutyApp dutyApp)
        {
            this.userApp = userApp;
            this.userLogOnApp = userLogOnApp;
            this.organizeApp = organizeApp;
            this.roleApp = roleApp;
            this.dutyApp = dutyApp;
        }

        [HttpGet]
        //[HandlerAjaxOnly]
        public ActionResult GetGridJson(Pagination pagination, string keyword)
        {
            var data = new
            {
                rows = userApp.GetList(pagination, keyword),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());
        }
        [HttpGet]
        //[HandlerAjaxOnly]
        public ActionResult GetFormJson(string keyValue)
        {
            var data = userApp.GetForm(keyValue);
            return Content(data.ToJson());
        }
        [HttpPost]
        //[HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public ActionResult SubmitForm(UserEntity userEntity, UserLogOnEntity userLogOnEntity, string keyValue)
        {
            userApp.SubmitForm(userEntity, userLogOnEntity, keyValue);
            return Success("操作成功。");
        }
        [HttpPost]
        //[HandlerAuthorize]
        //[HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteForm(string keyValue)
        {
            userApp.DeleteForm(keyValue);
            return Success("删除成功。");
        }
        [HttpGet]
        public ActionResult RevisePassword()
        {
            return View();
        }
        [HttpPost]
        //[HandlerAjaxOnly]
        //[HandlerAuthorize]
        [ValidateAntiForgeryToken]
        public ActionResult SubmitRevisePassword(string userPassword, string keyValue)
        {
            userLogOnApp.RevisePassword(userPassword, keyValue);
            return Success("重置密码成功。");
        }
        [HttpPost]
        //[HandlerAjaxOnly]
        //[HandlerAuthorize]
        [ValidateAntiForgeryToken]
        public ActionResult DisabledAccount(string keyValue)
        {
            UserEntity userEntity = new UserEntity();
            userEntity.F_Id = keyValue;
            userEntity.F_EnabledMark = false;
            userApp.UpdateForm(userEntity);
            return Success("账户禁用成功。");
        }
        [HttpPost]
        //[HandlerAjaxOnly]
        //[HandlerAuthorize]
        [ValidateAntiForgeryToken]
        public ActionResult EnabledAccount(string keyValue)
        {
            UserEntity userEntity = new UserEntity();
            userEntity.F_Id = keyValue;
            userEntity.F_EnabledMark = true;
            userApp.UpdateForm(userEntity);
            return Success("账户启用成功。");
        }

        [HttpGet]
        public ActionResult Info()
        {
            var user = OperatorProvider.Provider.GetCurrent();
            var entity = userApp.GetForm(user.UserId);

            string OrganizeName = string.Empty;



            ViewBag.OrganizeName =  organizeApp.GetForm(entity.F_OrganizeId)?.F_FullName;
            ViewBag.DepartmentName = organizeApp.GetForm(entity.F_DepartmentId)?.F_FullName;

            ViewBag.RoleName = string.IsNullOrWhiteSpace(entity.F_RoleId) == true ? "" : roleApp.GetForm(entity.F_RoleId)?.F_FullName;
            ViewBag.Duty = string.IsNullOrWhiteSpace(entity.F_DutyId) == true ? "" : dutyApp.GetForm(entity.F_DutyId)?.F_FullName;


            return View(entity);
        }
    }
}
