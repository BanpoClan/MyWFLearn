using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebMvc.Areas.Controls.Controllers
{
    public class SelectMemberController : MyController
    {
        //
        // GET: /Controls/SelectMember/

        public ActionResult Index()
        {
            return View();
        }

        public string GetNames()
        {
            string values = Request.QueryString["values"];
            return new RoadFlow.Platform.Organize().GetNames(values);
        }

        public string GetNote()
        {
            string id = Request.QueryString["id"];
            Guid gid;
            if (id.IsNullOrEmpty())
            {
                return "";
            }
            RoadFlow.Platform.Organize borg = new RoadFlow.Platform.Organize();
            RoadFlow.Platform.Users buser = new RoadFlow.Platform.Users();
            if (id.StartsWith(RoadFlow.Platform.Users.PREFIX))
            {
                Guid uid = buser.RemovePrefix1(id).ToGuid();
                return string.Concat(borg.GetAllParentNames(buser.GetMainStation(uid)), " / ", buser.GetName(uid));
            }
            else if (id.StartsWith(RoadFlow.Platform.WorkGroup.PREFIX))
            {
                return new RoadFlow.Platform.WorkGroup().GetUsersNames(RoadFlow.Platform.WorkGroup.RemovePrefix(id).ToGuid(), '、');
            }
            else if (id.IsGuid(out gid))
            {
                return borg.GetAllParentNames(gid);
            }
            return "";
        }

    }
}
