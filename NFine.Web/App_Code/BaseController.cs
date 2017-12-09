using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using NFine.Code;

namespace NFine.Web
{
    public class Session
    {
        public string this[string name]
        {
            get
            {
               return NFine.Code.WebHelper.GetSession(name);
            }
            set { NFine.Code.WebHelper.WriteSession(name, value); }
        }
    }

    public class BaseController : Controller
    {
        public BaseController()
        {
            this.Session = new Session();
        }

        /// <summary>
        /// HttpContext的Session
        /// </summary>
        public Session Session { get; set; }

        //public Log FileLog
        //{
        //    get { return LogFactory.GetLogger(this.GetType().ToString()); }
        //}

        [HttpGet]
       // [HandlerAuthorize]
        public virtual ActionResult Index()
        {
            return View();
        }
        [HttpGet]
       // [HandlerAuthorize]
        public virtual ActionResult Form()
        {
            return View();
        }
        [HttpGet]
       // [HandlerAuthorize]
        public virtual ActionResult Details()
        {
            return View();
        }
        protected virtual ActionResult Success(string message)
        {
            return Content(new AjaxResult { state = ResultType.success.ToString(), message = message }.ToJson());
        }
        protected virtual ActionResult Success(string message, object data)
        {
            return Content(new AjaxResult { state = ResultType.success.ToString(), message = message, data = data }.ToJson());
        }
        protected virtual ActionResult Error(string message)
        {
            return Content(new AjaxResult { state = ResultType.error.ToString(), message = message }.ToJson());
        }

    }

}
