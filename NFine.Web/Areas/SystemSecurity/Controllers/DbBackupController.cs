using NFine.Application.SystemSecurity;
using NFine.Code;
using NFine.Domain.Entity.SystemSecurity;
using Microsoft.AspNetCore.Mvc;

namespace NFine.Web.Areas.SystemSecurity.Controllers
{
    public class DbBackupController : BaseController
    {
        private DbBackupApp dbBackupApp = new DbBackupApp();

        [HttpGet]
        //[HandlerAjaxOnly]
        public ActionResult GetGridJson(string queryJson)
        {
            var data = dbBackupApp.GetList(queryJson);
            return Content(data.ToJson());
        }
        [HttpPost]
        //[HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public ActionResult SubmitForm(DbBackupEntity dbBackupEntity)
        {
           // dbBackupEntity.F_FilePath = Server.MapPath("~/Resource/DbBackup/" + dbBackupEntity.F_FileName + ".bak");
            dbBackupEntity.F_FileName = dbBackupEntity.F_FileName + ".bak";
            dbBackupApp.SubmitForm(dbBackupEntity);
            return Success("操作成功。");
        }
        [HttpPost]
        //[HandlerAjaxOnly]
        //[HandlerAuthorize]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteForm(string keyValue)
        {
            dbBackupApp.DeleteForm(keyValue);
            return Success("删除成功。");
        }
        [HttpPost]
        //[HandlerAuthorize]
        public void DownloadBackup(string keyValue)
        {
            //var data = dbBackupApp.GetForm(keyValue);
            //string filename =  System.Net.WebUtility.UrlDecode(data.F_FileName);
            //string filepath = Server.MapPath(data.F_FilePath);
            //if (FileDownHelper.FileExists(filepath))
            //{
            //    FileDownHelper.DownLoadold(filepath, filename);
            //}
        }
    }
}
