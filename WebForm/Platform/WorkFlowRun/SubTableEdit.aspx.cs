﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebForm.Platform.WorkFlowRun
{
    public partial class SubTableEdit : Common.BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            MyCreek.Data.Model.WorkFlowCustomEventParams parmas = new MyCreek.Data.Model.WorkFlowCustomEventParams();
            string instanceid1 = new MyCreek.Platform.WorkFlow().SaveFromData(Request.QueryString["instanceid"], parmas);
            Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "ok", "alert('保存成功!');new RoadUI.Window().reloadOpener();new RoadUI.Window().close();", true);
        }
    }
}