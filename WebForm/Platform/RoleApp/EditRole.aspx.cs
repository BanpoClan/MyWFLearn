using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebForm.Platform.RoleApp
{
    public partial class EditRole : Common.BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            MyCreek.Platform.Role brole = new MyCreek.Platform.Role();
            MyCreek.Data.Model.Role role = null;
            string roleID = Request.QueryString["roleid"];
            Guid roleGID;
            string name = string.Empty;
            string useMember = string.Empty;
            string note = string.Empty;

            if (roleID.IsGuid(out roleGID))
            {
                role = brole.Get(roleGID);
            }

            if (IsPostBack)
            {
                if (!Request.Form["Copy"].IsNullOrEmpty())
                {
                    string tpl = Request.Form["ToTpl"];
                    if (tpl.IsGuid())
                    {
                        new MyCreek.Platform.RoleApp().CopyRoleApp(roleGID, tpl.ToGuid());
                        MyCreek.Platform.Log.Add("复制了模板应用", "源：" + roleID + "复制给：" + tpl, MyCreek.Platform.Log.Types.角色应用);
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "ok", "alert('复制成功!');", true);
                    }
                }

                if (!Request.Form["Save"].IsNullOrEmpty() && role != null)
                {
                    MyCreek.Platform.UsersRole busersRole = new MyCreek.Platform.UsersRole();
                    using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope())
                    {
                        name = Request.Form["Name"];
                        useMember = Request.Form["UseMember"];
                        note = Request.Form["Note"];

                        role.Name = name.Trim();
                        role.Note = note.IsNullOrEmpty() ? null : note.Trim();
                        role.UseMember = useMember.IsNullOrEmpty() ? null : useMember;
                        brole.Update(role);
                        busersRole.DeleteByRoleID(role.ID);
                        if (!useMember.IsNullOrEmpty())
                        {
                            busersRole.DeleteByRoleID(role.ID);
                            var users = new MyCreek.Platform.Organize().GetAllUsers(useMember);
                            foreach (var user in users)
                            {
                                MyCreek.Data.Model.UsersRole ur = new MyCreek.Data.Model.UsersRole();
                                ur.IsDefault = true;
                                ur.MemberID = user.ID;
                                ur.RoleID = role.ID;
                                busersRole.Add(ur);
                            }
                        }
                        scope.Complete();
                    }
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "ok", "alert('保存成功!');new RoadUI.Window().reloadOpener();new RoadUI.Window().close();", true);
                }

                if (!Request.Form["Delete"].IsNullOrEmpty())
                {
                    using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope())
                    {
                        brole.Delete(roleGID);
                        new MyCreek.Platform.RoleApp().DeleteByRoleID(roleGID);
                        new MyCreek.Platform.UsersRole().DeleteByRoleID(roleGID);
                        scope.Complete();
                    }
                    MyCreek.Platform.Log.Add("删除的角色其及相关数据", roleID, MyCreek.Platform.Log.Types.角色应用);
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "ok", "new RoadUI.Window().reloadOpener();new RoadUI.Window().close();", true);
                }
            }
            if (role != null)
            {
                this.Name.Value = role.Name;
                this.UseMember.Value = role.UseMember;
                this.Note.Value = role.Note;
            }
            this.RoleOptions.Text = brole.GetRoleOptions("", roleID);
        }
    }
}