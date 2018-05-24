using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;

namespace WebMvc.Areas.Controls.Controllers
{
    public class SelectDictController : MyController
    {
        //
        // GET: /Controls/SelectDict/

        public ActionResult Index()
        {
            RoadFlow.Platform.Dictionary Dict = new RoadFlow.Platform.Dictionary();

            string values = Request.QueryString["values"] ?? "";
            string rootid = Request.QueryString["rootid"];
            string datasource = Request.QueryString["datasource"];
            string sql = Request.QueryString["sql"];

            DataTable SqlDataTable = new DataTable();
            if ("1" == datasource)
            {
                string dbconn = Request.QueryString["dbconn"];
                RoadFlow.Platform.DBConnection conn = new RoadFlow.Platform.DBConnection();
                var conn1 = conn.Get(dbconn.ToGuid());
                SqlDataTable = conn.GetDataTable(conn1, sql.UrlDecode().ReplaceSelectSql());
            }

            System.Text.StringBuilder defautlSB = new System.Text.StringBuilder();
            foreach (string value in values.Split(','))
            {
                switch (datasource)
                {
                    case "0":
                    default:
                        Guid id;
                        if (!value.IsGuid(out id))
                        {
                            continue;
                        }
                        defautlSB.AppendFormat("<div onclick=\"currentDel=this;showinfo('{0}');\" class=\"selectorDiv\" ondblclick=\"currentDel=this;del();\" value=\"{0}\">", value);
                        defautlSB.Append(Dict.GetTitle(id));
                        defautlSB.Append("</div>");
                        break;
                    case "1"://SQL
                        string title1 = string.Empty;
                        foreach (DataRow dr in SqlDataTable.Rows)
                        {
                            if (value == dr[0].ToString())
                            {
                                title1 = SqlDataTable.Columns.Count > 1 ? dr[1].ToString() : value;
                                break;
                            }
                        }
                        defautlSB.AppendFormat("<div onclick=\"currentDel=this;showinfo('{0}');\" class=\"selectorDiv\" ondblclick=\"currentDel=this;del();\" value=\"{0}\">", value);
                        defautlSB.Append(title1);
                        defautlSB.Append("</div>");
                        break;
                    case "2"://url
                        string url2 = Request.QueryString["url2"];
                        if (!url2.IsNullOrEmpty())
                        {
                            url2 = url2.IndexOf('?') >= 0 ? url2 + "&values=" + value : url2 + "?values=" + value;
                            System.Text.StringBuilder sb = new System.Text.StringBuilder();
                            
                            try
                            {
                                System.IO.TextWriter tw = new System.IO.StringWriter(sb);
                                Server.Execute(url2, tw);
                            }
                            catch{ }
                            defautlSB.AppendFormat("<div onclick=\"currentDel=this;showinfo('{0}');\" class=\"selectorDiv\" ondblclick=\"currentDel=this;del();\" value=\"{0}\">", value);
                            defautlSB.Append(sb.ToString());
                            defautlSB.Append("</div>");
                        }
                        break;
                    case "3"://table
                        string dbconn = Request.QueryString["dbconn"];
                        string dbtable = Request.QueryString["dbtable"];
                        string valuefield = Request.QueryString["valuefield"];
                        string titlefield = Request.QueryString["titlefield"];
                        string parentfield = Request.QueryString["parentfield"];
                        string where = Request.QueryString["where1"];
                        RoadFlow.Platform.DBConnection bdbconn = new RoadFlow.Platform.DBConnection();
                        var conn = bdbconn.Get(dbconn.ToGuid());
                        string sql2 = "select " + titlefield + " from " + dbtable + " where " + valuefield + "='" + value + "'";
                        DataTable dt = bdbconn.GetDataTable(conn, sql2.ReplaceSelectSql());
                        string title3 = string.Empty;
                        if (dt.Rows.Count > 0)
                        {
                            title3 = dt.Rows[0][0].ToString();
                        }
                        defautlSB.AppendFormat("<div onclick=\"currentDel=this;showinfo('{0}');\" class=\"selectorDiv\" ondblclick=\"currentDel=this;del();\" value=\"{0}\">", value);
                        defautlSB.Append(title3);
                        defautlSB.Append("</div>");
                        ViewBag.where = where.UrlEncode();
                        break;
                }
            }
            ViewBag.defaultValuesString = defautlSB.ToString();
            return View();
        }

        public string GetNames()
        {
            string values = Request.QueryString["values"] ?? "";
            RoadFlow.Platform.Dictionary Dict = new RoadFlow.Platform.Dictionary();
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            foreach (string value in values.Split(','))
            {
                var dict = Dict.Get(value.ToGuid(), true);
                if (dict != null)
                {
                    sb.Append(dict.Title);
                    sb.Append(',');
                }
            }
            return sb.ToString().TrimEnd(',');
        }

        public string GetNote()
        {
            string id = Request.QueryString["id"];
            Guid gid;
            string note = "";
            if (id.IsGuid(out gid))
            {
                var dict = new RoadFlow.Platform.Dictionary().Get(gid, true);
                if (dict != null)
                {
                    note = dict.Note;
                }
            }
            return note;
        }

        public string GetNames_SQL()
        {
            string dbconn = Request.QueryString["dbconn"];
            string sql = Request.QueryString["sql"];
            RoadFlow.Platform.DBConnection conn = new RoadFlow.Platform.DBConnection();
            var conn1 = conn.Get(dbconn.ToGuid());
            DataTable dt = conn.GetDataTable(conn1, sql.UrlDecode().ReplaceSelectSql());
            string values = Request.QueryString["values"] ?? "";
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            foreach (string value in values.Split(','))
            {
                string value1 = string.Empty;
                string title1 = string.Empty;
                foreach (DataRow dr in dt.Rows)
                {
                    value1 = dr[0].ToString();
                    if (value == value1)
                    {
                        title1 = dt.Columns.Count > 1 ? dr[1].ToString() : value1;
                        break;
                    }
                }
                sb.Append(title1);
                sb.Append(',');
            }
            return sb.ToString().TrimEnd(',');
        }

        public string GetJson_SQL()
        {
            if (!Common.Tools.CheckLogin(false))
            {
                return "{}";
            }
            string dbconn = Request.QueryString["dbconn"];
            string sql = Request.QueryString["sql"];
            RoadFlow.Platform.DBConnection conn = new RoadFlow.Platform.DBConnection();
            var conn1 = conn.Get(dbconn.ToGuid());
            System.Data.DataTable dt = conn.GetDataTable(conn1, sql.UrlDecode().ReplaceSelectSql());
            System.Text.StringBuilder json = new System.Text.StringBuilder(1000);
            foreach (System.Data.DataRow dr in dt.Rows)
            {
                string value = dr[0].ToString();
                string title = dt.Columns.Count > 1 ? dr[1].ToString() : value;
                json.Append("{");
                json.AppendFormat("\"id\":\"{0}\",", value);
                json.AppendFormat("\"parentID\":\"{0}\",", Guid.Empty.ToString());
                json.AppendFormat("\"title\":\"{0}\",", title);
                json.AppendFormat("\"type\":\"{0}\",", "2"); //类型：0根 1父 2子
                json.AppendFormat("\"ico\":\"{0}\",", "");
                json.AppendFormat("\"hasChilds\":\"{0}\",", "0");
                json.Append("\"childs\":[]},");
            }
            return "[" + json.ToString().TrimEnd(',') + "]";
        }


        public string GetJson_Table()
        {
            string dbconn = Request.QueryString["dbconn"];
            string dbtable = Request.QueryString["dbtable"];
            string valuefield = Request.QueryString["valuefield"];
            string titlefield = Request.QueryString["titlefield"];
            string parentfield = Request.QueryString["parentfield"];
            string where = (Request.QueryString["where"] ?? "").UrlDecode();

            RoadFlow.Platform.DBConnection bdbconn = new RoadFlow.Platform.DBConnection();
            var conn = bdbconn.Get(dbconn.ToGuid());
            string sql = "select " + valuefield + "," + titlefield + " from " + dbtable + (where.IsNullOrEmpty() ? "" : " where " + where);
            DataTable dt = bdbconn.GetDataTable(conn, sql.ReplaceSelectSql());
            System.Text.StringBuilder json = new System.Text.StringBuilder(1000);
            foreach (System.Data.DataRow dr in dt.Rows)
            {
                string value = dr[0].ToString();
                string title = dt.Columns.Count > 1 ? dr[1].ToString() : value;
                string sql1 = "select * from " + dbtable + " where " + parentfield + "='" + value + "'";
                bool hasChilds = bdbconn.GetDataTable(conn, sql1.ReplaceSelectSql()).Rows.Count > 0;
                json.Append("{");
                json.AppendFormat("\"id\":\"{0}\",", value);
                json.AppendFormat("\"parentID\":\"{0}\",", Guid.Empty.ToString());
                json.AppendFormat("\"title\":\"{0}\",", title);
                json.AppendFormat("\"type\":\"{0}\",", hasChilds ? "1" : "2"); //类型：0根 1父 2子
                json.AppendFormat("\"ico\":\"{0}\",", "");
                json.AppendFormat("\"hasChilds\":\"{0}\",", hasChilds ? "1" : "0");
                json.Append("\"childs\":[]},");
            }
            return "[" + json.ToString().TrimEnd(',') + "]";
        }

        public string GetJson_TableRefresh()
        {
            string dbconn = Request.QueryString["dbconn"];
            string dbtable = Request.QueryString["dbtable"];
            string valuefield = Request.QueryString["valuefield"];
            string titlefield = Request.QueryString["titlefield"];
            string parentfield = Request.QueryString["parentfield"];
            string where = Request.QueryString["where"];
            string id = Request.QueryString["refreshid"];

            RoadFlow.Platform.DBConnection bdbconn = new RoadFlow.Platform.DBConnection();
            var conn = bdbconn.Get(dbconn.ToGuid());
            string sql = "select " + valuefield + "," + titlefield + " from " + dbtable + " where " + parentfield + "='" + id + "'";
            DataTable dt = bdbconn.GetDataTable(conn, sql.ReplaceSelectSql());
            System.Text.StringBuilder json = new System.Text.StringBuilder(1000);
            foreach (System.Data.DataRow dr in dt.Rows)
            {
                string value = dr[0].ToString();
                string title = dt.Columns.Count > 1 ? dr[1].ToString() : value;
                string sql1 = "select * from " + dbtable + " where " + parentfield + "='" + value + "'";
                bool hasChilds = bdbconn.GetDataTable(conn, sql1.ReplaceSelectSql()).Rows.Count > 0;
                json.Append("{");
                json.AppendFormat("\"id\":\"{0}\",", value);
                json.AppendFormat("\"parentID\":\"{0}\",", Guid.Empty.ToString());
                json.AppendFormat("\"title\":\"{0}\",", title);
                json.AppendFormat("\"type\":\"{0}\",", hasChilds ? "1" : "2"); //类型：0根 1父 2子
                json.AppendFormat("\"ico\":\"{0}\",", "");
                json.AppendFormat("\"hasChilds\":\"{0}\",", hasChilds ? "1" : "0");
                json.Append("\"childs\":[]},");
            }
            return "[" + json.ToString().TrimEnd(',') + "]";
        }

        public string GetNames_Table()
        {
            string dbconn = Request.QueryString["dbconn"];
            string dbtable = Request.QueryString["dbtable"];
            string valuefield = Request.QueryString["valuefield"];
            string titlefield = Request.QueryString["titlefield"];
            string parentfield = Request.QueryString["parentfield"];
            string where = Request.QueryString["where"];
            string values = Request.QueryString["values"] ?? "";

            RoadFlow.Platform.DBConnection bdbconn = new RoadFlow.Platform.DBConnection();
            var conn = bdbconn.Get(dbconn.ToGuid());
            System.Text.StringBuilder names = new System.Text.StringBuilder();
            foreach (string value in values.Split(','))
            {
                if (value.IsNullOrEmpty())
                {
                    continue;
                }
                string sql = "select " + titlefield + " from " + dbtable + " where " + valuefield + "='" + value + "'";
                DataTable dt = bdbconn.GetDataTable(conn, sql.ReplaceSelectSql());
                if (dt.Rows.Count > 0)
                {
                    names.Append(dt.Rows[0][0].ToString());
                    names.Append(",");
                }
            }
            return names.ToString().TrimEnd(',');
        }
    }
}
