using NFine.Application.SystemManage;
using NFine.Code;
using NFine.Domain.Entity.SystemManage;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace NFine.Web.Areas.SystemManage.Controllers
{
    public class RoleAuthorizeController : BaseController
    {
        private RoleAuthorizeApp roleAuthorizeApp;
        private ModuleApp moduleApp;
        private ModuleButtonApp moduleButtonApp;
        public RoleAuthorizeController(RoleAuthorizeApp roleAuthorizeApp, ModuleApp moduleApp, ModuleButtonApp moduleButtonApp)
        {
            this.roleAuthorizeApp = roleAuthorizeApp;
            this.moduleApp = moduleApp;
            this.moduleButtonApp = moduleButtonApp;
        }
        public ActionResult GetPermissionTree(string roleId)
        {
            var moduledata = moduleApp.GetList();
            var buttondata = moduleButtonApp.GetList();
            var authorizedata = new List<RoleAuthorizeEntity>();
            if (!string.IsNullOrEmpty(roleId))
            {
                authorizedata = roleAuthorizeApp.GetList(roleId);
            }
            var treeList = new List<TreeViewModel>();
            foreach (ModuleEntity item in moduledata)
            {
                TreeViewModel tree = new TreeViewModel();
                bool hasChildren = moduledata.Count(t => t.F_ParentId == item.F_Id) == 0 ? false : true;
                tree.id = item.F_Id;
                tree.text = item.F_FullName;
                tree.value = item.F_EnCode;
                tree.parentId = item.F_ParentId;
                tree.isexpand = true;
                tree.complete = true;
                tree.showcheck = true;
                tree.checkstate = authorizedata.Count(t => t.F_ItemId == item.F_Id);
                tree.hasChildren = true;
                tree.img = item.F_Icon == "" ? "" : item.F_Icon;
                treeList.Add(tree);
            }
            foreach (ModuleButtonEntity item in buttondata)
            {
                TreeViewModel tree = new TreeViewModel();
                bool hasChildren = buttondata.Count(t => t.F_ParentId == item.F_Id) == 0 ? false : true;
                tree.id = item.F_Id;
                tree.text = item.F_FullName;
                tree.value = item.F_EnCode;
                tree.parentId = item.F_ParentId == "0" ? item.F_ModuleId : item.F_ParentId;
                tree.isexpand = true;
                tree.complete = true;
                tree.showcheck = true;
                tree.checkstate = authorizedata.Count(t => t.F_ItemId == item.F_Id);
                tree.hasChildren = hasChildren;
                tree.img = item.F_Icon == "" ? "" : item.F_Icon;
                treeList.Add(tree);
            }
            return Content(treeList.TreeViewJson());
        }
    }
}
