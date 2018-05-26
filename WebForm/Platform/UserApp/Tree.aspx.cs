using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebForm.Platform.UserApp
{
    public partial class Tree : Common.BasePage
    {
        protected string RoleOptions = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            string userid = Request.QueryString["id"];
            MyCreek.Platform.Role brole = new MyCreek.Platform.Role();
            var roles = new MyCreek.Platform.UsersRole().GetByUserID(userid.ToGuid());
            List<MyCreek.Data.Model.Role> roleList = new List<MyCreek.Data.Model.Role>();
            foreach (var role in roles)
            {
                var role1 = brole.Get(role.RoleID);
                if (role1 == null)
                {
                    continue;
                }
                roleList.Add(role1);
            }

            RoleOptions = new MyCreek.Platform.Role().GetRoleOptions(Request.QueryString["roleid"], "", roleList);

        }
    }
}