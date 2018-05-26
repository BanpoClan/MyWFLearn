using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebForm.Platform.WorkFlowComments
{
    public partial class Edit : Common.BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            MyCreek.Platform.WorkFlowComment bworkFlowComment = new MyCreek.Platform.WorkFlowComment();
            MyCreek.Data.Model.WorkFlowComment workFlowComment = null;
            string id = Request.QueryString["id"];

            string member = string.Empty;
            string comment = string.Empty;
            string sort = string.Empty;

            bool isOneSelf = "1" == Request.QueryString["isoneself"];
            if (isOneSelf)
            {
                this.usermemberid.Visible = false;
            }
            Guid commentID;
            if (id.IsGuid(out commentID))
            {
                workFlowComment = bworkFlowComment.Get(commentID);
                member = workFlowComment.MemberID;
                comment = workFlowComment.Comment;
                sort = workFlowComment.Sort.ToString();
            }

            string oldXML = workFlowComment.Serialize();
            if (IsPostBack)
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
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "ok", "new RoadUI.Window().reloadOpener();alert('保存成功!'); new RoadUI.Window().close();", true);
            }

            if (workFlowComment != null)
            {
                this.Comment.Value = workFlowComment.Comment;
                this.Member.Value = workFlowComment.MemberID;
                this.Sort.Value = workFlowComment.Sort.ToString();
            }
        }
    }
}