using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebForm.Platform.WorkFlowComments
{
    public partial class Default : Common.BasePage
    {
        protected bool isOneSelf = false;
        protected string query1 = string.Empty;
        protected IEnumerable<MyCreek.Data.Model.WorkFlowComment> workFlowCommentList;
        protected MyCreek.Platform.Organize borganize = new MyCreek.Platform.Organize();
        protected void Page_Load(object sender, EventArgs e)
        {
            MyCreek.Platform.WorkFlowComment bworkFlowComment = new MyCreek.Platform.WorkFlowComment();
            MyCreek.Platform.Organize borganize = new MyCreek.Platform.Organize();
            query1 = string.Format("&appid={0}&tabid={1}&isoneself={2}", Request.QueryString["appid"], Request.QueryString["tabid"], Request.QueryString["isoneself"]);
            if (IsPostBack)
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

            isOneSelf = "1" == Request.QueryString["isoneself"];
            if (isOneSelf)
            {
                workFlowCommentList = workFlowCommentList.Where(p => p.MemberID == MyCreek.Platform.Users.PREFIX + MyCreek.Platform.Users.CurrentUserID.ToString());
            }
        }
    }
}