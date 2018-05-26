using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebForm.Platform.WorkFlowTasks
{
    public partial class Detail : Common.BasePage
    {
        protected MyCreek.Platform.WorkFlowTask bworkFlowTask = new MyCreek.Platform.WorkFlowTask();
        protected MyCreek.Platform.WorkFlow bworkFlow = new MyCreek.Platform.WorkFlow();
        protected string flowid = string.Empty;
        protected string groupid = string.Empty;
        protected string displayModel = string.Empty;
        protected string query = string.Empty;
        protected IOrderedEnumerable<MyCreek.Data.Model.WorkFlowTask> tasks;
        protected MyCreek.Data.Model.WorkFlowInstalled wfInstall = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            flowid = Request.QueryString["flowid1"] ?? Request.QueryString["flowid"];
            groupid = Request.QueryString["groupid"];
            displayModel = Request.QueryString["displaymodel"];

            wfInstall = bworkFlow.GetWorkFlowRunModel(flowid);
            tasks = bworkFlowTask.GetTaskList(flowid.ToGuid(), groupid.ToGuid()).OrderBy(p => p.Sort);
            query = string.Format("&flowid1={0}&groupid={1}&appid={2}&tabid={3}&title={4}&flowid={5}&sender={6}&date1={7}&date2={8}&iframeid={9}&openerid={10}",
                flowid, groupid,
                Request.QueryString["appid"],
                Request.QueryString["tabid"],
                Request.QueryString["title"].UrlEncode(),
                Request.QueryString["flowid"],
                Request.QueryString["sender"],
                Request.QueryString["date1"],
                Request.QueryString["date2"],
                Request.QueryString["iframeid"],
                Request.QueryString["openerid"]
                );
        }
    }
}