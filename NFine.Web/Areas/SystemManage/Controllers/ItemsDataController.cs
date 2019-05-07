using NFine.Application.SystemManage;
using NFine.Code;
using NFine.Domain.Entity.SystemManage;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System;

namespace NFine.Web.Areas.SystemManage.Controllers
{
    public class ItemsDataController : BaseController
    {
        private ItemsDetailApp itemsDetailApp;
        public ItemsDataController(ItemsDetailApp itemsDetailApp)
        {
            this.itemsDetailApp = itemsDetailApp;
        }
        [HttpGet]
       // [HandlerAjaxOnly]
        public ActionResult GetGridJson(string itemId, string keyword)
        {
            var data = itemsDetailApp.GetList(itemId, keyword);
            return Content(data.ToJson());
        }

        public ActionResult GetListEnCode(string enCode)
        {
            if (string.IsNullOrWhiteSpace(enCode))
                throw new ArgumentNullException("enCode");

            var data = itemsDetailApp.GetListEnCode(enCode);
            return Content(data.ToJson());
        }

        [HttpGet]
        //[HandlerAjaxOnly]
        public ActionResult GetSelectJson(string enCode)
        {
            var data = itemsDetailApp.GetItemList(enCode);
            List<object> list = new List<object>();
            foreach (ItemsDetailEntity item in data)
            {
                list.Add(new { id = item.F_ItemCode, text = item.F_ItemName });
            }
            return Content(list.ToJson());
        }
        [HttpGet]
       // [HandlerAjaxOnly]
        public ActionResult GetFormJson(string keyValue)
        {
            var data = itemsDetailApp.GetForm(keyValue);
            return Content(data.ToJson());
        }
        [HttpPost]
        //[HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public ActionResult SubmitForm(ItemsDetailEntity itemsDetailEntity, string keyValue)
        {
            itemsDetailApp.SubmitForm(itemsDetailEntity, keyValue);
            return Success("操作成功。");
        }
        [HttpPost]
        //[HandlerAjaxOnly]
        //[HandlerAuthorize]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteForm(string keyValue)
        {
            itemsDetailApp.DeleteForm(keyValue);
            return Success("删除成功。");
        }
    }
}
