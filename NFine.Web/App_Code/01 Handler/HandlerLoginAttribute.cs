using Microsoft.AspNetCore.Authorization;
using NFine.Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace NFine.Web
{
    public class HandlerLoginAttribute: AuthorizeAttribute
    {
       
        public HandlerLoginAttribute(bool ignore = true)
        {
            if (OperatorProvider.Provider.GetCurrent() == null)
            {
                WebHelper.WriteCookie("nfine_login_error", "overdue");
                HttpContext.Current.Response.Redirect("/Login/Index");
            }
        }
    }
}
