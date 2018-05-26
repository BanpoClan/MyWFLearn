using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebMvc.Controllers
{
    public class UserAppController : MyController
    {
        //
        // GET: /UserApp/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Tree()
        {
            string userid = Request.QueryString["id"];
            MyCreek.Platform.Role brole = new MyCreek.Platform.Role();
            var roles = new MyCreek.Platform.UsersRole().GetByUserID(userid.ToGuid());
            List<MyCreek.Data.Model.Role> roleList = new List<MyCreek.Data.Model.Role>();
            foreach (var role in roles)
            {
                var role1 = brole.Get(role.RoleID);
                if (role1 == null)
                {
                    continue;
                }
                roleList.Add(role1);
            }

            ViewBag.RoleOptions = new MyCreek.Platform.Role().GetRoleOptions(Request.QueryString["roleid"], "", roleList);

            return View();
        }

        public string Tree1()
        {
            string roleID = Request.QueryString["roleid"];
            Guid roleGuid;
            if (!roleID.IsGuid(out roleGuid))
            {
                return "[]";
            }

            MyCreek.Platform.RoleApp BRoleApp = new MyCreek.Platform.RoleApp();
            var appDt = BRoleApp.GetAllDataTableByRoleID(roleGuid);
            if (appDt.Rows.Count == 0)
            {
                return "[]";
            }

            var root = appDt.Select("ParentID='" + Guid.Empty.ToString() + "'");
            if (root.Length == 0)
            {
                return "[]";
            }

            var apps = appDt.Select("ParentID='" + root[0]["ID"].ToString() + "'");
            System.Text.StringBuilder json = new System.Text.StringBuilder("[", 1000);
            System.Data.DataRow rootDr = root[0];
            json.Append("{");
            json.AppendFormat("\"id\":\"{0}\",", rootDr["ID"]);
            json.AppendFormat("\"title\":\"{0}\",", rootDr["Title"]);
            json.AppendFormat("\"ico\":\"{0}\",", rootDr["Ico"]);
            json.AppendFormat("\"link\":\"{0}\",", rootDr["Address"]);
            json.AppendFormat("\"type\":\"{0}\",", "0");
            json.AppendFormat("\"model\":\"{0}\",", rootDr["OpenMode"]);
            json.AppendFormat("\"width\":\"{0}\",", rootDr["Width"]);
            json.AppendFormat("\"height\":\"{0}\",", rootDr["Height"]);
            json.AppendFormat("\"hasChilds\":\"{0}\",", apps.Length > 0 ? "1" : "0");
            json.AppendFormat("\"childs\":[");

            for (int i = 0; i < apps.Length; i++)
            {
                System.Data.DataRow dr = apps[i];
                var childs = appDt.Select("ParentID='" + dr["ID"].ToString() + "'");
                json.Append("{");
                json.AppendFormat("\"id\":\"{0}\",", dr["ID"]);
                json.AppendFormat("\"title\":\"{0}\",", dr["Title"]);
                json.AppendFormat("\"ico\":\"{0}\",", dr["Ico"]);
                json.AppendFormat("\"link\":\"{0}\",", dr["Address"]);
                json.AppendFormat("\"type\":\"{0}\",", "0");
                json.AppendFormat("\"model\":\"{0}\",", dr["OpenMode"]);
                json.AppendFormat("\"width\":\"{0}\",", dr["Width"]);
                json.AppendFormat("\"height\":\"{0}\",", dr["Height"]);
                json.AppendFormat("\"hasChilds\":\"{0}\",", childs.Length > 0 ? "1" : "0");
                json.AppendFormat("\"childs\":[");
                json.Append("]");
                json.Append("}");
                if (i < apps.Length - 1)
                {
                    json.Append(",");
                }
            }
            json.Append("]");
            json.Append("}");
            json.Append("]");

            return json.ToString();
        }

        public string TreeRefresh()
        {
            string id = Request["refreshid"];
            string userID = Request.QueryString["userid"];
            Guid rid;
            if (!id.IsGuid(out rid))
            {
                return "[]";
            }
            MyCreek.Platform.RoleApp BRoleApp = new MyCreek.Platform.RoleApp();
            MyCreek.Platform.UsersApp BUsersApp = new MyCreek.Platform.UsersApp();
            var childs = BRoleApp.GetChild(rid);

            //加载个人应用
            if (userID.IsGuid())
            {
                BUsersApp.AppendUserApps(userID.ToGuid(), rid, childs);
            }

            System.Text.StringBuilder json = new System.Text.StringBuilder("[", childs.Count * 50);
            int count = childs.Count;
            int i = 0;
            foreach (var child in childs.OrderBy(p => p.Sort))
            {
                json.Append("{");
                json.AppendFormat("\"id\":\"{0}\",", child.ID.ToString());
                json.AppendFormat("\"title\":\"{0}\",", child.Title);
                json.AppendFormat("\"ico\":\"{0}\",", child.Ico);
                json.AppendFormat("\"link\":\"{0}\",", "");
                json.AppendFormat("\"type\":\"{0}\",", child.Type);
                json.AppendFormat("\"model\":\"{0}\",", "");
                json.AppendFormat("\"width\":\"{0}\",", "");
                json.AppendFormat("\"height\":\"{0}\",", "");
                json.AppendFormat("\"hasChilds\":\"{0}\",", BRoleApp.HasChild(child.ID) || BUsersApp.HasChild(child.ID) ? "1" : "0");
                json.AppendFormat("\"childs\":[");
                json.Append("]");
                json.Append("}");
                if (i++ < count - 1)
                {
                    json.Append(",");
                }
            }
            json.Append("]");
            return json.ToString();
        }

        public ActionResult Empty()
        {
            return null;
        }

        public ActionResult Body()
        {
            MyCreek.Platform.AppLibrary bappLibrary = new MyCreek.Platform.AppLibrary();
            MyCreek.Platform.RoleApp broleApp = new MyCreek.Platform.RoleApp();
            MyCreek.Data.Model.RoleApp roleApp = null;
            string id = Request.QueryString["id"];

            string name = string.Empty;
            string type = string.Empty;
            string appid = string.Empty;
            string params1 = string.Empty;
            string ico = string.Empty;

            Guid appID;
            if (id.IsGuid(out appID))
            {
                roleApp = broleApp.Get(appID);
                if (roleApp != null)
                {
                    name = roleApp.Title;
                    type = roleApp.AppID.HasValue ? bappLibrary.GetTypeByID(roleApp.AppID.Value) : "";
                    appid = roleApp.AppID.ToString();
                    params1 = roleApp.Params;
                    ico = roleApp.Ico;
                }
            }
            if (roleApp == null)
            {
                roleApp = new MyCreek.Data.Model.RoleApp();
            }
            ViewBag.AppID = appid;
            ViewBag.AppTypesOptions = bappLibrary.GetTypeOptions(type);
            return View(roleApp);
        }


        public ActionResult AddApp()
        {
            return AddApp(null);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddApp(FormCollection collection)
        {
            MyCreek.Platform.AppLibrary bappLibrary = new MyCreek.Platform.AppLibrary();
            MyCreek.Platform.UsersApp busersApp = new MyCreek.Platform.UsersApp();
            MyCreek.Platform.RoleApp broleApp = new MyCreek.Platform.RoleApp();
            MyCreek.Data.Model.UsersApp usersApp = null;

            string id = Request.QueryString["id"];
            string userID = Request.QueryString["userid"];
            string roleID = Request.QueryString["roleid"];

            if (collection != null && id.IsGuid() && userID.IsGuid())
            {
                usersApp = busersApp.Get(id.ToGuid());
                if (!Request.Form["Save"].IsNullOrEmpty())
                {
                    string name = Request.Form["Name"];
                    string type = Request.Form["Type"];
                    string appid = Request.Form["AppID"];
                    string params1 = Request.Form["Params"];
                    string ico = Request.Form["Ico"];

                    MyCreek.Data.Model.UsersApp usersApp1 = new MyCreek.Data.Model.UsersApp();

                    usersApp1.ID = Guid.NewGuid();
                    usersApp1.ParentID = id.ToGuid();
                    usersApp1.Title = name.Trim();
                    usersApp1.Sort = broleApp.GetMaxSort(id.ToGuid());
                    usersApp1.UserID = userID.ToGuid();
                    usersApp1.RoleID = roleID.IsGuid() ? roleID.ToGuid() : Guid.Empty;
                    if (appid.IsGuid())
                    {
                        usersApp1.AppID = appid.ToGuid();
                    }
                    else
                    {
                        usersApp1.AppID = null;
                    }
                    usersApp1.Params = params1.IsNullOrEmpty() ? null : params1.Trim();
                    if (!ico.IsNullOrEmpty())
                    {
                        usersApp1.Ico = ico;
                    }

                    busersApp.Add(usersApp1);
                    busersApp.ClearCache();
                    MyCreek.Platform.Log.Add("添加了个人应用", busersApp.Serialize(), MyCreek.Platform.Log.Types.角色应用);
                    string refreshID = id;
                    ViewBag.Script = "alert('添加成功!'); parent.frames[0].reLoad('" + refreshID + "')";
                }
            }

            ViewBag.AppTypesOptions = bappLibrary.GetTypeOptions();
            return View();
        }

        public ActionResult Body1()
        {
            return Body1(null);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Body1(FormCollection collection)
        {
            MyCreek.Platform.AppLibrary bappLibrary = new MyCreek.Platform.AppLibrary();
            MyCreek.Platform.RoleApp broleApp = new MyCreek.Platform.RoleApp();
            MyCreek.Platform.UsersApp buserApp = new MyCreek.Platform.UsersApp();
            MyCreek.Data.Model.UsersApp usersApp = null;

            string id = Request.QueryString["id"];

            string name = string.Empty;
            string type = string.Empty;
            string appid = string.Empty;
            string params1 = string.Empty;
            string ico = string.Empty;

            Guid appID;
            if (id.IsGuid(out appID))
            {
                usersApp = buserApp.Get(appID);
                if (usersApp != null)
                {
                    name = usersApp.Title;
                    type = usersApp.AppID.HasValue ? bappLibrary.GetTypeByID(usersApp.AppID.Value) : "";
                    appid = usersApp.AppID.ToString();
                    params1 = usersApp.Params;
                    ico = usersApp.Ico;
                }
            }


            if (collection != null && usersApp != null)
            {
                if (!Request.Form["Save"].IsNullOrEmpty())
                {
                    name = Request.Form["Name"];
                    type = Request.Form["Type"];
                    appid = Request.Form["AppID"];
                    params1 = Request.Form["Params"];
                    ico = Request.Form["Ico"];

                    string oldXML = usersApp.Serialize();
                    usersApp.Title = name.Trim();
                    if (appid.IsGuid())
                    {
                        usersApp.AppID = appid.ToGuid();
                    }
                    else
                    {
                        usersApp.AppID = null;
                    }
                    usersApp.Params = params1.IsNullOrEmpty() ? null : params1.Trim();
                    if (!ico.IsNullOrEmpty())
                    {
                        usersApp.Ico = ico;
                    }
                    else
                    {
                        usersApp.Ico = null;
                    }

                    buserApp.Update(usersApp);
                    buserApp.ClearCache();
                    MyCreek.Platform.Log.Add("修改了个人应用", "", MyCreek.Platform.Log.Types.角色应用, oldXML, usersApp.Serialize());
                    string refreshID = usersApp.ParentID.ToString();
                    ViewBag.Script = "alert('保存成功!'); parent.frames[0].reLoad('" + refreshID + "')";
                }

                if (!Request.Form["Delete"].IsNullOrEmpty())
                {
                    int i = buserApp.DeleteAndAllChilds(usersApp.ID);
                    buserApp.ClearCache();
                    MyCreek.Platform.Log.Add("删除了个人应用", usersApp.Serialize(), MyCreek.Platform.Log.Types.角色应用);
                    string refreshID = usersApp.ParentID.ToString();
                    var parent = buserApp.Get(usersApp.ParentID);
                    string page = parent == null ? "Body" : "Body1";
                    ViewBag.Script = "parent.frames[0].reLoad('" + refreshID + "');window.location='" + page + "?id=" + refreshID + "&appid=" + Request.QueryString["appid"] + "&tabid=" + Request.QueryString["tabid"] + "&userid=" + Request.QueryString["userid"] + "';";
                }
            }
            ViewBag.AppID = appid;
            ViewBag.AppTypesOptions = bappLibrary.GetTypeOptions(type);
            return View(usersApp == null ? new MyCreek.Data.Model.UsersApp() : usersApp);
        }

    }
}
