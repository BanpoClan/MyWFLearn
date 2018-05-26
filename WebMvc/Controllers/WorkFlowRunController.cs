using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebMvc.Controllers
{
    public class WorkFlowRunController : Controller
    {
        //
        // GET: /WorkFlowRun/

        public ActionResult Index()
        {
            return View();
        }


        public ActionResult ShowComment()
        {
            return View();
        }

        public ActionResult Print()
        {
            return View();
        }

        public ActionResult Execute()
        {
            return View();
        }

        public ActionResult FlowBack()
        {
            return View();
        }

        public ActionResult FlowRedirect()
        {
            return View();
        }

        public ActionResult FlowSend()
        {
            return View();
        }

        public ActionResult Process()
        {
            return View();
        }

        public ActionResult Sign()
        {
            return View();
        }

        public ActionResult SaveData()
        {
            return View();
        }


        /// <summary>
        /// 查看流程图
        /// </summary>
        /// <returns></returns>
        public ActionResult ShowDesign()
        {
            return View();
        }

        public ActionResult SubTableEdit()
        {
            return View();
        }

        public string SubTableDelete()
        {
            string secondtableconnid = Request.QueryString["secondtableconnid"];
            string secondtable = Request.QueryString["secondtable"];
            string secondtableprimarykey = Request.QueryString["secondtableprimarykey"];
            string secondtablepkvalue = Request.QueryString["secondtablepkvalue"];
            MyCreek.Platform.DBConnection bdbconn = new MyCreek.Platform.DBConnection();

            int i = bdbconn.DeleteData(secondtableconnid.ToGuid(), secondtable, secondtableprimarykey, secondtablepkvalue);
            if (i > 0)
            {
                return "删除成功!";
            }
            else
            {
                return "删除失败!";
            }
        }
    }
}
