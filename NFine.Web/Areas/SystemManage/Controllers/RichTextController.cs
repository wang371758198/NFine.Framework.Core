using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using NFine.Application.SystemManage;
using NFine.Code;
using NFine.Domain.Entity.SystemManage;

namespace NFine.Web.Areas.SystemManage.Controllers
{
    public class RichTextController : Controller
    {
        private NewInfoApp app;
        public RichTextController(NewInfoApp newInfoApp)
        {
            this.app = newInfoApp;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult List()
        {
            return View();
        }

       
        public IActionResult SubmitForm(NewsInfoEntity newsInfoEntity,string keyValue)
        {
            app.SubmitForm(newsInfoEntity, keyValue);
            return Content(new AjaxResult { state = ResultType.success.ToString(), message = "操作成功。" }.ToJson());
        }

        public IActionResult GetForm(string keyValue)
        {
            if (string.IsNullOrWhiteSpace(keyValue))
                return Content("null");
            return Content(app.GetForm(keyValue).ToJson());
        }


        public IActionResult DeleteForm(string keyValue)
        {
            if (string.IsNullOrWhiteSpace(keyValue))
                return Content(new AjaxResult { state = ResultType.success.ToString(), message = "数据实体不存在。" }.ToJson());

            app.DeleteForm(keyValue);
            return Content(new AjaxResult { state = ResultType.success.ToString(), message = "操作成功。" }.ToJson());
        }

        public ActionResult GetGridJson(Pagination pagination, string keyword)
        {
            var data = new
            {
                rows = app.GetList(pagination,keyword),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
           
            return Content(data.ToJson());
        }

        public void UEditor()
        {
            string actionQuery = HttpContext.Request.Query["action"];
            string callback = HttpContext.Request.Query["callback"];
            Handler action = null;
            switch (actionQuery)
            {
                case "config":
                    action = new ConfigHandler();
                    break;
                case "uploadimage":
                    action = new UploadHandler(new UploadConfig()
                    {
                        AllowExtensions = Config.GetStringList("imageAllowFiles"),
                        PathFormat = Config.GetString("imagePathFormat"),
                        SizeLimit = Config.GetInt("imageMaxSize"),
                        UploadFieldName = Config.GetString("imageFieldName")
                    });
                    break;
                case "uploadscrawl":
                    action = new UploadHandler(new UploadConfig()
                    {
                        AllowExtensions = new string[] { ".png" },
                        PathFormat = Config.GetString("scrawlPathFormat"),
                        SizeLimit = Config.GetInt("scrawlMaxSize"),
                        UploadFieldName = Config.GetString("scrawlFieldName"),
                        Base64 = true,
                        Base64Filename = "scrawl.png"
                    });
                    break;
                case "uploadvideo":
                    action = new UploadHandler( new UploadConfig()
                    {
                        AllowExtensions = Config.GetStringList("videoAllowFiles"),
                        PathFormat = Config.GetString("videoPathFormat"),
                        SizeLimit = Config.GetInt("videoMaxSize"),
                        UploadFieldName = Config.GetString("videoFieldName")
                    });
                    break;
                case "uploadfile":
                    action = new UploadHandler( new UploadConfig()
                    {
                        AllowExtensions = Config.GetStringList("fileAllowFiles"),
                        PathFormat = Config.GetString("filePathFormat"),
                        SizeLimit = Config.GetInt("fileMaxSize"),
                        UploadFieldName = Config.GetString("fileFieldName")
                    });
                    break;
                case "listimage":
                    action = new ListFileManager( Config.GetString("imageManagerListPath"), Config.GetStringList("imageManagerAllowFiles"));
                    break;
                case "listfile":
                    action = new ListFileManager( Config.GetString("fileManagerListPath"), Config.GetStringList("fileManagerAllowFiles"));
                    break;
                case "catchimage":
                    action = new CrawlerHandler();
                    break;
                default:
                    action = new NotSupportedHandler();
                    break;
            }
            action.Process();
           
        }


        public IActionResult Preview(string keyValue)
        {
            if (string.IsNullOrWhiteSpace(keyValue))
                return Content("null");
            return View(app.GetForm(keyValue));
        }


        public IActionResult GetNewsInfoList(int type, int topNumber = 8)
        {
            var list = app.GetList(type, topNumber);
            list?.ForEach(_ =>
            {
                if (_.F_Title.Length > 15)
                    _.F_Title = _.F_Title.Substring(0, 15) + "...";
            });
            return Content(list?.ToJson());
        }

    }
}
