using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebMvc.Controllers
{
    public class HomeController : MyController
    {
        //
        // GET: /Home/
        public ActionResult Index()
        {
            MyCreek.Platform.UsersRole buserRole = new MyCreek.Platform.UsersRole();
            MyCreek.Platform.Role brole = new MyCreek.Platform.Role();
            var roles = buserRole.GetByUserID(MyCreek.Platform.Users.CurrentUserID);
            ViewBag.RoleLength = roles.Count;
            ViewBag.DefaultRoleID = string.Empty;
            ViewBag.RolesOptions = string.Empty;
            if (roles.Count > 0)
            {
                var mainRole = roles.Find(p => p.IsDefault);
                ViewBag.defaultRoleID = mainRole != null ? mainRole.RoleID.ToString() : roles.First().RoleID.ToString();
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

                ViewBag.RolesOptions = brole.GetRoleOptions("", "", roleList);
            }

            var user = MyCreek.Platform.Users.CurrentUser;
            ViewBag.UserName = user == null ? "" : user.Name;
            ViewBag.DateTime = MyCreek.Utility.DateTimeNew.Now.ToDateWeekString();

            return View();
        }

        public ActionResult Home()
        {
            return View();
        }

        public string Menu()
        { 
            string roleID = Request.QueryString["roleid"];
            string userID = Request.QueryString["userid"];
            Guid gid,uid;
            if(!roleID.IsGuid(out gid) || !userID.IsGuid(out uid))
            {
                return "[]";
            }
            else
            {
                return new MyCreek.Platform.RoleApp().GetRoleAppJsonString(gid, uid, Url.Content("~/").TrimEnd('/'));
            }
        }

        public string MenuRefresh()
        { 
            string roleID=Request.QueryString["roleid"];
            string userID = Request.QueryString["userid"];
            string refreshID = Request.QueryString["refreshid"];
            Guid gid,refreshid,uid;
            if(!roleID.IsGuid(out gid) || !refreshID.IsGuid(out refreshid) || !userID.IsGuid(out uid))
            {
                return "[]";
            }
            else
            {
                return new MyCreek.Platform.RoleApp().GetRoleAppRefreshJsonString(gid, uid, refreshid, Url.Content("~/").TrimEnd('/'));
            }
        }

    }
}
