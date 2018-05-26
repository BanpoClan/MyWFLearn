﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebForm.Platform.Members
{
    public partial class WorkGroupAdd : Common.BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            MyCreek.Platform.WorkGroup bwg = new MyCreek.Platform.WorkGroup();
            string name = string.Empty;
            string members = string.Empty;
            string note = string.Empty;
            if (IsPostBack)
            {
                name = Request.Form["Name"];
                members = Request.Form["Members"];
                note = Request.Form["Note"];

                MyCreek.Data.Model.WorkGroup wg = new MyCreek.Data.Model.WorkGroup();
                wg.ID = Guid.NewGuid();
                wg.Name = name.Trim();
                wg.Members = members;
                if (!note.IsNullOrEmpty())
                {
                    wg.Note = note;
                }
                bwg.Add(wg);
                string query = Request.Url.Query;
                MyCreek.Platform.Log.Add("添加了工作组", wg.Serialize(), MyCreek.Platform.Log.Types.组织机构);
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "ok", "parent.frames[0].treecng('1');alert('添加成功!');window.location = 'WorkGroup.aspx' + '" + query + "';", true);

            }
        }
    }
}