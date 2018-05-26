using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebForm.Platform.WorkFlowDelegation
{
    public partial class Default : Common.BasePage
    {
        protected MyCreek.Platform.Organize borganize = new MyCreek.Platform.Organize();
        protected MyCreek.Platform.Users busers = new MyCreek.Platform.Users();
        protected MyCreek.Platform.WorkFlow bworkFlow = new MyCreek.Platform.WorkFlow();
        protected IEnumerable<MyCreek.Data.Model.WorkFlowDelegation> workFlowDelegationList;
        protected string Query1 = string.Empty;
        protected bool isoneself = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            isoneself = "1" == Request.QueryString["isoneself"];
            if (isoneself)
            {
                this.S_UserID.Disabled = true;
                this.S_UserID.Value = MyCreek.Platform.Users.PREFIX + MyCreek.Platform.Users.CurrentUserID.ToString();
            }
            MyCreek.Platform.WorkFlowDelegation bworkFlowDelegation = new MyCreek.Platform.WorkFlowDelegation();
            MyCreek.Platform.Organize borganize = new MyCreek.Platform.Organize();
            MyCreek.Platform.Users busers = new MyCreek.Platform.Users();
            MyCreek.Platform.WorkFlow bworkFlow = new MyCreek.Platform.WorkFlow();
            
            string startTime = string.Empty;
            string endTime = string.Empty;
            string suserid = string.Empty;
            string Query1 = string.Format("&appid={0}&tabid={1}&isoneself={2}", Request.QueryString["appid"], 
                Request.QueryString["tabid"], Request.QueryString["isoneself"]);
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
            Query1 += "&S_StartTime=" + startTime + "&S_EndTime=" + endTime + "&S_UserID=" + suserid;
            string pager;
            bool isOneSelf = "1" == Request.QueryString["isoneself"];
            
            if (isOneSelf)
            {
                workFlowDelegationList = bworkFlowDelegation.GetPagerData(out pager, Query1, MyCreek.Platform.Users.CurrentUserID.ToString(), startTime, endTime);
            }
            else
            {
                workFlowDelegationList = bworkFlowDelegation.GetPagerData(out pager, Query1, MyCreek.Platform.Users.RemovePrefix(suserid), startTime, endTime);
            }
            this.Pager.Text = pager;
        }
    }
}