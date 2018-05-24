using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebForm.Platform.Members
{
    /// <summary>
    /// Tree1 的摘要说明
    /// </summary>
    public class Tree1 : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string rootid = context.Request.QueryString["rootid"];
            string showtype = context.Request.QueryString["showtype"];
            RoadFlow.Platform.Organize BOrganize = new RoadFlow.Platform.Organize();
            System.Text.StringBuilder json = new System.Text.StringBuilder("[", 1000);

            if ("1" == showtype)//显示工作组
            {
                RoadFlow.Platform.WorkGroup BWorkGroup = new RoadFlow.Platform.WorkGroup();
                var workGroups = BWorkGroup.GetAll();

                json.Append("{");
                json.AppendFormat("\"id\":\"{0}\",", Guid.Empty);
                json.AppendFormat("\"parentID\":\"{0}\",", Guid.Empty);
                json.AppendFormat("\"title\":\"{0}\",", "工作组");
                json.AppendFormat("\"ico\":\"{0}\",", Common.Tools.BaseUrl + "/images/ico/group.gif");
                json.AppendFormat("\"link\":\"{0}\",", "");
                json.AppendFormat("\"type\":\"{0}\",", 5);
                json.AppendFormat("\"hasChilds\":\"{0}\",", workGroups.Count);
                json.Append("\"childs\":[");

                int countwg = workGroups.Count;
                int iwg = 0;
                foreach (var wg in workGroups)
                {
                    json.Append("{");
                    json.AppendFormat("\"id\":\"{0}\",", wg.ID);
                    json.AppendFormat("\"parentID\":\"{0}\",", Guid.Empty);
                    json.AppendFormat("\"title\":\"{0}\",", wg.Name);
                    json.AppendFormat("\"ico\":\"{0}\",", "");
                    json.AppendFormat("\"link\":\"{0}\",", "");
                    json.AppendFormat("\"type\":\"{0}\",", 5);
                    json.AppendFormat("\"hasChilds\":\"{0}\",", 0);
                    json.Append("\"childs\":[");
                    json.Append("]");
                    json.Append("}");
                    if (iwg++ < countwg - 1)
                    {
                        json.Append(",");
                    }
                }

                json.Append("]");
                json.Append("}");
                json.Append("]");
                context.Response.Write(json.ToString());
                context.Response.End();
            }


            Guid rootID;
            RoadFlow.Data.Model.Organize root;
            if (rootid.IsGuid(out rootID))
            {
                root = BOrganize.Get(rootID);
            }
            else
            {
                root = BOrganize.GetRoot();
            }

            List<RoadFlow.Data.Model.Users> users = new List<RoadFlow.Data.Model.Users>();

            RoadFlow.Platform.Users busers = new RoadFlow.Platform.Users();
            users = busers.GetAllByOrganizeID(root.ID);

            json.Append("{");
            json.AppendFormat("\"id\":\"{0}\",", root.ID);
            json.AppendFormat("\"parentID\":\"{0}\",", root.ParentID);
            json.AppendFormat("\"title\":\"{0}\",", root.Name);
            json.AppendFormat("\"ico\":\"{0}\",", Common.Tools.BaseUrl + "/images/ico/icon_site.gif");
            json.AppendFormat("\"link\":\"{0}\",", "");
            json.AppendFormat("\"type\":\"{0}\",", root.Type);
            json.AppendFormat("\"hasChilds\":\"{0}\",", root.ChildsLength == 0 && users.Count == 0 ? "0" : "1");
            json.Append("\"childs\":[");

            var orgs = BOrganize.GetChilds(root.ID);
            int count = orgs.Count;

            int i = 0;
            foreach (var org in orgs)
            {
                json.Append("{");
                json.AppendFormat("\"id\":\"{0}\",", org.ID);
                json.AppendFormat("\"parentID\":\"{0}\",", org.ParentID);
                json.AppendFormat("\"title\":\"{0}\",", org.Name);
                json.AppendFormat("\"ico\":\"{0}\",", "");
                json.AppendFormat("\"link\":\"{0}\",", "");
                json.AppendFormat("\"type\":\"{0}\",", org.Type);
                json.AppendFormat("\"hasChilds\":\"{0}\",", org.ChildsLength);
                json.Append("\"childs\":[");
                json.Append("]");
                json.Append("}");
                if (i++ < count - 1 || users.Count > 0)
                {
                    json.Append(",");
                }
            }

            if (users.Count > 0)
            {
                var userRelations = new RoadFlow.Platform.UsersRelation().GetAllByOrganizeID(root.ID);
                int count1 = users.Count;
                int j = 0;
                foreach (var user in users)
                {
                    var ur = userRelations.Find(p => p.UserID == user.ID);
                    json.Append("{");
                    json.AppendFormat("\"id\":\"{0}\",", user.ID);
                    json.AppendFormat("\"parentID\":\"{0}\",", root.ID);
                    json.AppendFormat("\"title\":\"{0}{1}\",", user.Name, ur != null && ur.IsMain == 0 ? "<span style='color:#999;'>[兼职]</span>" : "");
                    json.AppendFormat("\"ico\":\"{0}\",", "");
                    json.AppendFormat("\"link\":\"{0}\",", "");
                    json.AppendFormat("\"type\":\"{0}\",", "4");
                    json.AppendFormat("\"hasChilds\":\"{0}\",", "0");
                    json.Append("\"childs\":[");
                    json.Append("]");
                    json.Append("}");
                    if (j++ < count1 - 1)
                    {
                        json.Append(",");
                    }
                }
            }


            json.Append("]");
            json.Append("}");
            json.Append("]");

            context.Response.Write(json.ToString());
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