using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebMvc.Controllers
{
    public class WorkFlowDelegationController : MyController
    {
        //
        // GET: /WorkFlowDelegation/

        public ActionResult Index()
        {
            return Index(null);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(FormCollection collection)
        {
            MyCreek.Platform.WorkFlowDelegation bworkFlowDelegation = new MyCreek.Platform.WorkFlowDelegation();
            MyCreek.Platform.Organize borganize = new MyCreek.Platform.Organize();
            MyCreek.Platform.Users busers = new MyCreek.Platform.Users();
            MyCreek.Platform.WorkFlow bworkFlow = new MyCreek.Platform.WorkFlow();
            IEnumerable<MyCreek.Data.Model.WorkFlowDelegation> workFlowDelegationList;

            string startTime = string.Empty;
            string endTime = string.Empty;
            string suserid = string.Empty;
            string query1 = string.Format("&appid={0}&tabid={1}&isoneself={2}", Request.QueryString["appid"], Request.QueryString["tabid"], Request.QueryString["isoneself"]);
            if (collection != null)
            {
                if (!Request.Form["DeleteBut"].IsNullOrEmpty())
                {
                    string ids = Request.Form["checkbox_app"];
                    foreach (string id in ids.Split(','))
                    {
                        Guid bid;
                        if (!id.IsGuid(out bid))
                        {
                            continue;
                        }
                        var comment = bworkFlowDelegation.Get(bid);
                        if (comment != null)
                        {
                            bworkFlowDelegation.Delete(bid);
                            MyCreek.Platform.Log.Add("删除了流程意见", comment.Serialize(), MyCreek.Platform.Log.Types.流程相关);
                        }
                    }
                    bworkFlowDelegation.RefreshCache();
                }
                startTime = Request.Form["S_StartTime"];
                endTime = Request.Form["S_EndTime"];
                suserid = Request.Form["S_UserID"];
            }
            else
            {
                startTime = Request.QueryString["S_StartTime"];
                endTime = Request.QueryString["S_EndTime"];
                suserid = Request.QueryString["S_UserID"];
            }
            query1 += "&S_StartTime=" + startTime + "&S_EndTime=" + endTime + "&S_UserID=" + suserid;
            string pager;
            bool isOneSelf = "1" == Request.QueryString["isoneself"];
            if (isOneSelf)
            {
                workFlowDelegationList = bworkFlowDelegation.GetPagerData(out pager, query1, MyCreek.Platform.Users.CurrentUserID.ToString(), startTime, endTime);
            }
            else
            {
                workFlowDelegationList = bworkFlowDelegation.GetPagerData(out pager, query1, MyCreek.Platform.Users.RemovePrefix(suserid), startTime, endTime);
            }
            ViewBag.Query1 = query1;
            ViewBag.startTime = startTime;
            ViewBag.endTime = endTime;
            ViewBag.suserid = suserid;
            return View(workFlowDelegationList);
        }

        public ActionResult Edit()
        {
            return Edit(null);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(FormCollection collection)
        {
            MyCreek.Platform.WorkFlowDelegation bworkFlowDelegation = new MyCreek.Platform.WorkFlowDelegation();
            MyCreek.Data.Model.WorkFlowDelegation workFlowDelegation = null;
            string id = Request.QueryString["id"];

            string UserID = string.Empty;
            string ToUserID = string.Empty;
            string StartTime = string.Empty;
            string EndTime = string.Empty;
            string FlowID = string.Empty;
            string Note = string.Empty;

            bool isOneSelf = "1" == Request.QueryString["isoneself"];

            Guid delegationID;
            if (id.IsGuid(out delegationID))
            {
                workFlowDelegation = bworkFlowDelegation.Get(delegationID);
                if (workFlowDelegation != null)
                {
                    FlowID = workFlowDelegation.FlowID.ToString();
                }
            }
            string oldXML = workFlowDelegation.Serialize();

            if (collection != null)
            {
                UserID = Request.Form["UserID"];
                ToUserID = Request.Form["ToUserID"];
                StartTime = Request.Form["StartTime"];
                EndTime = Request.Form["EndTime"];
                FlowID = Request.Form["FlowID"];
                Note = Request.Form["Note"];

                bool isAdd = !id.IsGuid();
                if (workFlowDelegation == null)
                {
                    workFlowDelegation = new MyCreek.Data.Model.WorkFlowDelegation();
                    workFlowDelegation.ID = Guid.NewGuid();
                }
                workFlowDelegation.UserID = isOneSelf ? MyCreek.Platform.Users.CurrentUserID : MyCreek.Platform.Users.RemovePrefix(UserID).ToGuid();
                workFlowDelegation.EndTime = EndTime.ToDateTime();
                if (FlowID.IsGuid())
                {
                    workFlowDelegation.FlowID = FlowID.ToGuid();
                }
                workFlowDelegation.Note = Note.IsNullOrEmpty() ? null : Note;
                workFlowDelegation.StartTime = StartTime.ToDateTime();
                workFlowDelegation.ToUserID = MyCreek.Platform.Users.RemovePrefix(ToUserID).ToGuid();
                workFlowDelegation.WriteTime = MyCreek.Utility.DateTimeNew.Now;



                if (isAdd)
                {
                    bworkFlowDelegation.Add(workFlowDelegation);
                    MyCreek.Platform.Log.Add("添加了工作委托", workFlowDelegation.Serialize(), MyCreek.Platform.Log.Types.流程相关);
                }
                else
                {
                    bworkFlowDelegation.Update(workFlowDelegation);
                    MyCreek.Platform.Log.Add("修改了工作委托", "", MyCreek.Platform.Log.Types.流程相关, oldXML, workFlowDelegation.Serialize());
                }
                bworkFlowDelegation.RefreshCache();
                ViewBag.Script = "alert('保存成功!');new RoadUI.Window().reloadOpener();new RoadUI.Window().close();";
            }
            ViewBag.FlowOptions = new MyCreek.Platform.WorkFlow().GetOptions(FlowID);
            return View(workFlowDelegation == null ? new MyCreek.Data.Model.WorkFlowDelegation() { UserID = MyCreek.Platform.Users.CurrentUserID } : workFlowDelegation);
        }

    }
}
