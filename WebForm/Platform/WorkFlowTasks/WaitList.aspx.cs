using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebForm.Platform.WorkFlowTasks
{
    public partial class WaitList : Common.BasePage
    {
        protected string query = string.Empty;
        protected MyCreek.Platform.WorkFlowTask bworkFlowTask = new MyCreek.Platform.WorkFlowTask();
        protected MyCreek.Platform.WorkFlow bworkFlow = new MyCreek.Platform.WorkFlow();
        protected IEnumerable<MyCreek.Data.Model.WorkFlowTask> taskList;
        protected void Page_Load(object sender1, EventArgs e)
        {
            string title = "";
            string flowid = "";
            string sender = "";
            string date1 = "";
            string date2 = "";

            if (IsPostBack)
            {
                title = Request.Form["Title1"];
                flowid = Request.Form["FlowID"];
                sender = Request.Form["SenderID"];
                date1 = Request.Form["Date1"];
                date2 = Request.Form["Date2"];
            }
            else
            {
                title = Request.QueryString["title"];
                flowid = Request.QueryString["flowid"];
                sender = Request.QueryString["sender"];
                date1 = Request.QueryString["date1"];
                date2 = Request.QueryString["date2"];
            }
            
            query = string.Format("&appid={0}&tabid={1}&title={2}&flowid={3}&sender={4}&date1={5}&date2={6}",
                Request.QueryString["appid"], Request.QueryString["tabid"], title.UrlEncode(), flowid, sender, date1, date2);
            string pager;

            taskList = bworkFlowTask.GetTasks(MyCreek.Platform.Users.CurrentUserID,
               out pager, query, title, flowid, sender, date1, date2);

            //var flows = new MyCreek.Platform.AppLibrary().GetAll();
            //System.Text.StringBuilder sb = new System.Text.StringBuilder("<table>");
            //sb.Append("<thead class='mygrid1'>");
            //sb.Append("<tr>");
            //sb.Append("<th width='33%'>应用名称</th><th width='33%'>地址</th><th width='33%'>类型</th>");
            //sb.Append("</tr>");
            //sb.Append("</thead><tbody>");

            //foreach (var flow in flows)
            //{
            //    sb.Append("<tr>");
            //    sb.Append("<td value='" + flow.ID + "' " + (("," + flowid + ",").Contains(flow.ID.ToString()) ? "isselected='1'" : "") + ">" + flow.Title + "</td>");
            //    sb.Append("<td>" + flow.Note + "</td>");
            //    sb.Append("<td>" + flow.Height + "</td>");
            //    sb.Append("</tr>");
            //}
            //sb.Append("</tbody></table>");

            this.flowOptions.Text = bworkFlow.GetOptions(flowid);
            this.Pager.Text = pager;
        }
    }
}