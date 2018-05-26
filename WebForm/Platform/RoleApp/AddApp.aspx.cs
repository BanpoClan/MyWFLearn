﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebForm.Platform.RoleApp
{
    public partial class AddApp : Common.BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            MyCreek.Platform.AppLibrary bappLibrary = new MyCreek.Platform.AppLibrary();
            MyCreek.Platform.RoleApp broleApp = new MyCreek.Platform.RoleApp();
            MyCreek.Data.Model.RoleApp roleApp = null;

            string id = Request.QueryString["id"];
            if (IsPostBack)
            {
                roleApp = broleApp.Get(id.ToGuid());
                if (!Request.Form["Save"].IsNullOrEmpty())
                {
                    string name = Request.Form["Name"];
                    string type = Request.Form["Type"];
                    string appid = Request.Form["AppID"];
                    string params1 = Request.Form["Params"];
                    string ico = Request.Form["Ico"];

                    MyCreek.Data.Model.RoleApp roleApp1 = new MyCreek.Data.Model.RoleApp();

                    roleApp1.ID = Guid.NewGuid();
                    roleApp1.ParentID = id.ToGuid();
                    roleApp1.RoleID = roleApp.RoleID;
                    roleApp1.Title = name.Trim();
                    roleApp1.ParentID = roleApp.ID;
                    roleApp1.Sort = broleApp.GetMaxSort(roleApp.ID);
                    roleApp1.Type = 0;
                    if (appid.IsGuid())
                    {
                        roleApp1.AppID = appid.ToGuid();
                    }
                    else
                    {
                        roleApp1.AppID = null;
                    }
                    roleApp1.Params = params1.IsNullOrEmpty() ? null : params1.Trim();
                    if (!ico.IsNullOrEmpty())
                    {
                        roleApp1.Ico = ico;
                    }

                    broleApp.Add(roleApp1);
                    broleApp.ClearAllDataTableCache();
                    MyCreek.Platform.Log.Add("添加了应用模板", roleApp1.Serialize(), MyCreek.Platform.Log.Types.角色应用);
                    string refreshID = id;
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "ok", "alert('添加成功');parent.frames[0].reLoad('" + refreshID + "');", true);
                }
            }
            this.AppTypesOptions.Text = bappLibrary.GetTypeOptions();
        }
    }
}