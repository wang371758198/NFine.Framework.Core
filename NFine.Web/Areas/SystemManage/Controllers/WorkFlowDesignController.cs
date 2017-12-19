using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace NFine.Web.Areas.SystemManage.Controllers
{
    public class WorkFlowDesignController : Controller
    {
        /// <summary>
        /// 工作流设计
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult stop()
        {
            return Content("");
        }

    }
}
