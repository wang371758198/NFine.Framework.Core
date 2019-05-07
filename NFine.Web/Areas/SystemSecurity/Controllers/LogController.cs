using NFine.Application.SystemSecurity;
using NFine.Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNetCore.Mvc;

namespace NFine.Web.Areas.SystemSecurity.Controllers
{
    public class LogController : BaseController
    {
        private LogApp logApp;
        public LogController(LogApp logApp)
        {
            this.logApp = logApp;
        }

        [HttpGet]
        public ActionResult RemoveLog()
        {
            return View();
        }
        [HttpGet]
       // [HandlerAjaxOnly]
        public ActionResult GetGridJson(Pagination pagination, string queryJson)
        {
            var data = new
            {
                rows = logApp.GetList(pagination, queryJson),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());
        }
        [HttpPost]
        //[HandlerAjaxOnly]
        //[HandlerAuthorize]
        [ValidateAntiForgeryToken]
        public ActionResult SubmitRemoveLog(string keepTime)
        {
            logApp.RemoveLog(keepTime);
            return Success("清空成功。");
        }
    }
}
