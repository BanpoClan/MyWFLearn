using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace WebForm.Platform.WorkFlowFormDesigner
{
    /// <summary>
    /// Save 的摘要说明
    /// </summary>
    public class Save : IHttpHandler, IReadOnlySessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string html = context.Request["html"];
            string name = context.Request["name"];
            string att = context.Request["att"];
            string id = context.Request["id"];
            string type = context.Request["type"];
            string subtable = context.Request["subtable"];
            string formEvents = context.Request["formEvents"];
            if (name.IsNullOrEmpty())
            {
                context.Response.Write("表单名称不能为空!");
                return;
            }

            Guid formID;
            if (!id.IsGuid(out formID))
            {
                context.Response.Write("表单ID无效!");
                return;
            }

            MyCreek.Platform.WorkFlowForm WFF = new MyCreek.Platform.WorkFlowForm();
            MyCreek.Data.Model.WorkFlowForm wff = WFF.Get(formID);
            bool isAdd = false;
            string oldXML = string.Empty;
            if (wff == null)
            {
                wff = new MyCreek.Data.Model.WorkFlowForm();
                wff.ID = formID;
                wff.CreateUserID = MyCreek.Platform.Users.CurrentUserID;
                wff.CreateUserName = MyCreek.Platform.Users.CurrentUserName;
                wff.CreateTime = MyCreek.Utility.DateTimeNew.Now;
                wff.Status = 0;
                isAdd = true;
            }
            else
            {
                oldXML = wff.Serialize();
            }

            wff.Type = type.ToGuid();
            wff.Attribute = att;
            wff.Html = html;
            wff.LastModifyTime = MyCreek.Utility.DateTimeNew.Now;
            wff.Name = name;
            wff.SubTableJson = subtable;
            wff.EventsJson = formEvents;

            if (isAdd)
            {
                WFF.Add(wff);
                MyCreek.Platform.Log.Add("添加了流程表单", wff.Serialize(), MyCreek.Platform.Log.Types.流程相关);
            }
            else
            {
                WFF.Update(wff);
                MyCreek.Platform.Log.Add("修改了流程表单", "", MyCreek.Platform.Log.Types.流程相关, oldXML, wff.Serialize());
            }
            context.Response.Write("保存成功!");
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}