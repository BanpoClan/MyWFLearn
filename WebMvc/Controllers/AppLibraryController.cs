using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebMvc.Controllers
{
    public class AppLibraryController : MyController
    {
        //
        // GET: /AppLibrary/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Tree()
        {
            return View();
        }

        public ActionResult List()
        {
            string title1 = Request.QueryString["title1"];
            string address = Request.QueryString["address"];
            return query(title1, address);
        }

        [HttpPost]
        public RedirectToRouteResult Delete()
        {
            MyCreek.Platform.AppLibrary bappLibrary = new MyCreek.Platform.AppLibrary();
            string deleteID = Request.Form["checkbox_app"];
            System.Text.StringBuilder delxml = new System.Text.StringBuilder();
            foreach (string id in deleteID.Split(','))
            {
                Guid gid;
                if (id.IsGuid(out gid))
                {
                    var app = bappLibrary.Get(gid);
                    if (app != null)
                    {
                        delxml.Append(app.Serialize());
                        bappLibrary.Delete(gid);
                    }
                }
            }
            MyCreek.Platform.Log.Add("删除了一批应用程序库", delxml.ToString(), MyCreek.Platform.Log.Types.角色应用);
            return RedirectToAction("List", Common.Tools.GetRouteValueDictionary());
        }

        [HttpPost]
        public ActionResult List(FormCollection collection)
        {
            string title1 = collection["title1"];
            string address = collection["address"];
            return query(title1, address);
        }

        private ActionResult query(string title1, string address)
        {
            string pager;
            string appid = Request.QueryString["appid"];
            string tabid = Request.QueryString["tabid"];
            string typeid = Request.QueryString["typeid"];
            MyCreek.Platform.Dictionary bdict = new MyCreek.Platform.Dictionary();
            MyCreek.Platform.AppLibrary bapp = new MyCreek.Platform.AppLibrary();
            string typeidstring = typeid.IsGuid() ? bapp.GetAllChildsIDString(typeid.ToGuid()) : "";
            string query = string.Format("&appid={0}&tabid={1}&title1={2}&typeid={3}&address={4}",
                        Request.QueryString["appid"],
                        Request.QueryString["tabid"],
                        title1.UrlEncode(), typeid, address.UrlEncode()
                        );
            string query1 = string.Format("{0}&pagesize={1}&pagenumber={2}", query, Request.QueryString["pagesize"], Request.QueryString["pagenumber"]);
            List<MyCreek.Data.Model.AppLibrary> appList = bapp.GetPagerData(out pager, query, title1, typeidstring, address);
            ViewBag.Pager = pager;
            ViewBag.AppID = appid;
            ViewBag.TabID = tabid;
            ViewBag.TypeID = typeid;
            ViewBag.Title1 = title1;
            ViewBag.Address = address;
            ViewBag.Query1 = query1;
            return View(appList);
        }

        public ActionResult Edit()
        {
            return Edit(null);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(FormCollection collection)
        {
            string editID = Request.QueryString["id"];
            string type = Request.QueryString["typeid"];

            MyCreek.Platform.AppLibrary bappLibrary = new MyCreek.Platform.AppLibrary();
            MyCreek.Data.Model.AppLibrary appLibrary = null;
            if (editID.IsGuid())
            {
                appLibrary = bappLibrary.Get(editID.ToGuid());
            }
            bool isAdd = !editID.IsGuid();
            string oldXML = string.Empty;
            if (appLibrary == null)
            {
                appLibrary = new MyCreek.Data.Model.AppLibrary();
                appLibrary.ID = Guid.NewGuid();
                ViewBag.TypeOptions = new MyCreek.Platform.AppLibrary().GetTypeOptions(type);
                ViewBag.OpenOptions = new MyCreek.Platform.Dictionary().GetOptionsByCode("appopenmodel", value: "");
            }
            else
            {
                oldXML = appLibrary.Serialize();
                ViewBag.TypeOptions = new MyCreek.Platform.AppLibrary().GetTypeOptions(appLibrary.Type.ToString());
                ViewBag.OpenOptions = new MyCreek.Platform.Dictionary().GetOptionsByCode("appopenmodel", value: appLibrary.OpenMode.ToString());
            }

            if (collection != null)
            {
                string title = collection["title"];
                string address = collection["address"];
                string openModel = collection["openModel"];
                string width = collection["width"];
                string height = collection["height"];
                string params1 = collection["Params"];
                string note = collection["note"];
                string useMember = collection["UseMember"];
                type = collection["type"];

                appLibrary.Address = address.Trim();
                appLibrary.Height = height.ToIntOrNull();
                appLibrary.Note = note;
                appLibrary.OpenMode = openModel.ToInt();
                appLibrary.Params = params1;
                appLibrary.Title = title;
                appLibrary.Type = type.ToGuid();
                appLibrary.Width = width.ToIntOrNull();

                if (!useMember.IsNullOrEmpty())
                {
                    appLibrary.UseMember = useMember;
                }
                else
                {
                    appLibrary.UseMember = null;
                }

                if (isAdd)
                {
                    bappLibrary.Add(appLibrary);
                    MyCreek.Platform.Log.Add("添加了应用程序库", appLibrary.Serialize(), MyCreek.Platform.Log.Types.角色应用);
                    ViewBag.Script = "alert('添加成功!');new RoadUI.Window().reloadOpener();new RoadUI.Window().close();";
                }
                else
                {
                    bappLibrary.Update(appLibrary);
                    MyCreek.Platform.Log.Add("修改了应用程序库", "", MyCreek.Platform.Log.Types.角色应用, oldXML, appLibrary.Serialize());
                    ViewBag.Script = "alert('修改成功!');new RoadUI.Window().reloadOpener();new RoadUI.Window().close();";
                }
                bappLibrary.UpdateUseMemberCache(appLibrary.ID);
                bappLibrary.ClearCache();
                new MyCreek.Platform.RoleApp().ClearAllDataTableCache();
            }
            return View(appLibrary);
        }
    }
}
