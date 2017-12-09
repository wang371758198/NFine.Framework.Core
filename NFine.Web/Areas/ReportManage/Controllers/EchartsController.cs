using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNetCore.Mvc;

namespace NFine.Web.Areas.ReportManage.Controllers
{
    public class EchartsController : BaseController
    {
        //
        // GET: /ReportManage/Echarts/

        public override ActionResult Index()
        {
            return View();
        }

    }
}
