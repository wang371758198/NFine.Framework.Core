using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Newtonsoft.Json;


/// <summary>
/// Handler 的摘要说明
/// </summary>
public abstract class Handler
{

    public abstract void Process();

    protected void WriteJson(object response)
    {
        

        string jsonpCallback = HttpContext.Current.Request.Query["callback"],
            json = JsonConvert.SerializeObject(response);
        if (String.IsNullOrWhiteSpace(jsonpCallback))
        {
            HttpContext.Current.Response.ContentType = "text/plain;charset=UTF-8";
            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(json);
            HttpContext.Current.Response.Body.Write(buffer, 0, buffer.Length);
        }
        else 
        {
            HttpContext.Current.Response.ContentType = "application/javascript;charset=UTF-8";
            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(String.Format("{0}({1});", jsonpCallback, json));
            HttpContext.Current.Response.Body.Write(buffer, 0, buffer.Length);
        }
    }

   
}
