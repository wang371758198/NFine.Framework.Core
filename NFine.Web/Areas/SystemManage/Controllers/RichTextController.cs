using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using NFine.Application.SystemManage;
using NFine.Code;
using NFine.Domain.Entity.SystemManage;

namespace NFine.Web.Areas.SystemManage.Controllers
{
    public class RichTextController : BaseController
    {
        private  NewInfoApp app = new NewInfoApp();

        public IActionResult List()
        {
            return View();
        }

       
        public IActionResult SubmitForm(NewsInfoEntity newsInfoEntity,string keyValue)
        {
            app.SubmitForm(newsInfoEntity, keyValue);
            return Success("操作成功。");
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
                return Success("数据实体不存在。");

            app.DeleteForm(keyValue);
            return Success("删除成功。");
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
    }
}
