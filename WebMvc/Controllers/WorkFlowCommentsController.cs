using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebMvc.Controllers
{
    public class WorkFlowCommentsController : MyController
    {
        //
        // GET: /WorkFlowComments/

        public ActionResult Index()
        {
            return Index(null);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(FormCollection collection)
        {
            MyCreek.Platform.WorkFlowComment bworkFlowComment = new MyCreek.Platform.WorkFlowComment();
            MyCreek.Platform.Organize borganize = new MyCreek.Platform.Organize();
            IEnumerable<MyCreek.Data.Model.WorkFlowComment> workFlowCommentList;

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
                        var comment = bworkFlowComment.Get(bid);
                        if (comment != null)
                        {
                            bworkFlowComment.Delete(bid);
                            MyCreek.Platform.Log.Add("删除了流程意见", comment.Serialize(), MyCreek.Platform.Log.Types.流程相关);
                        }
                    }
                    bworkFlowComment.RefreshCache();
                }

            }

            workFlowCommentList = bworkFlowComment.GetAll();

            bool isOneSelf = "1" == Request.QueryString["isoneself"];
            if (isOneSelf)
            {
                workFlowCommentList = workFlowCommentList.Where(p => p.MemberID == MyCreek.Platform.Users.PREFIX + MyCreek.Platform.Users.CurrentUserID.ToString());
            }
            return View(workFlowCommentList);
        }


        public ActionResult Edit()
        {
            return Edit(null);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(FormCollection collection)
        {
            MyCreek.Platform.WorkFlowComment bworkFlowComment = new MyCreek.Platform.WorkFlowComment();
            MyCreek.Data.Model.WorkFlowComment workFlowComment = null;
            string id = Request.QueryString["id"];

            string member = string.Empty;
            string comment = string.Empty;
            string sort = string.Empty;

            bool isOneSelf = "1" == Request.QueryString["isoneself"];

            Guid commentID;
            if (id.IsGuid(out commentID))
            {
                workFlowComment = bworkFlowComment.Get(commentID);
                member = workFlowComment.MemberID;
                comment = workFlowComment.Comment;
                sort = workFlowComment.Sort.ToString();
            }
            string oldXML = workFlowComment.Serialize();
            if (collection != null)
            {
                member = isOneSelf ? MyCreek.Platform.Users.PREFIX + MyCreek.Platform.Users.CurrentUserID.ToString() : Request.Form["Member"];
                comment = Request.Form["Comment"];
                sort = Request.Form["Sort"];

                bool isAdd = !id.IsGuid();
                if (workFlowComment == null)
                {
                    workFlowComment = new MyCreek.Data.Model.WorkFlowComment();
                    workFlowComment.ID = Guid.NewGuid();
                    workFlowComment.Type = isOneSelf ? 1 : 0;
                }

                workFlowComment.MemberID = member.IsNullOrEmpty() ? "" : member.Trim();
                workFlowComment.Comment = comment.IsNullOrEmpty() ? "" : comment.Trim();
                workFlowComment.Sort = sort.IsInt() ? sort.ToInt() : bworkFlowComment.GetManagerMaxSort();


                if (isAdd)
                {
                    bworkFlowComment.Add(workFlowComment);
                    MyCreek.Platform.Log.Add("添加了流程意见", workFlowComment.Serialize(), MyCreek.Platform.Log.Types.流程相关);
                }
                else
                {
                    bworkFlowComment.Update(workFlowComment);
                    MyCreek.Platform.Log.Add("修改了流程意见", "", MyCreek.Platform.Log.Types.流程相关, oldXML, workFlowComment.Serialize());
                }
                bworkFlowComment.RefreshCache();
                ViewBag.Script = "new RoadUI.Window().reloadOpener();alert('保存成功!');";
            }
            return View(workFlowComment == null ? new MyCreek.Data.Model.WorkFlowComment() : workFlowComment);
        }

    }
}
