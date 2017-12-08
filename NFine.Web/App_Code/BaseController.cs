using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace NFine.Web.Controllers
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



    }

}
