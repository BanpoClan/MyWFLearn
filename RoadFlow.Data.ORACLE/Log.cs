using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Oracle.ManagedDataAccess.Client;

namespace RoadFlow.Data.ORACLE
{
    public class Log : RoadFlow.Data.Interface.ILog
    {
        private DBHelper dbHelper = new DBHelper();
        /// <summary>
        /// 构造函数
        /// </summary>
        public Log()
        {
        }
        /// <summary>
        /// 添加记录
        /// </summary>
        /// <param name="model">RoadFlow.Data.Model.Log实体类</param>
        /// <returns>操作所影响的行数</returns>
        public int Add(RoadFlow.Data.Model.Log model)
        {
            string sql = @"INSERT INTO Log
				(ID,Title,Type,WriteTime,UserID,UserName,IPAddress,URL,Contents,Others,OldXml,NewXml) 
				VALUES(:ID,:Title,:Type,:WriteTime,:UserID,:UserName,:IPAddress,:URL,:Contents,:Others,:OldXml,:NewXml)";
            OracleParameter[] parameters = new OracleParameter[]{
				new OracleParameter(":ID", OracleDbType.Varchar2, 40){ Value = model.ID },
				new OracleParameter(":Title", OracleDbType.NVarchar2){ Value = model.Title },
				new OracleParameter(":Type", OracleDbType.NVarchar2, 100){ Value = model.Type },
				new OracleParameter(":WriteTime", OracleDbType.Date, 8){ Value = model.WriteTime },
				model.UserID == null ? new OracleParameter(":UserID", OracleDbType.Varchar2, 40) { Value = DBNull.Value } : new OracleParameter(":UserID", OracleDbType.Varchar2, 40) { Value = model.UserID },
				model.UserName == null ? new OracleParameter(":UserName", OracleDbType.NVarchar2, 100) { Value = DBNull.Value } : new OracleParameter(":UserName", OracleDbType.NVarchar2, 100) { Value = model.UserName },
				model.IPAddress == null ? new OracleParameter(":IPAddress", OracleDbType.Varchar2, 50) { Value = DBNull.Value } : new OracleParameter(":IPAddress", OracleDbType.Varchar2, 50) { Value = model.IPAddress },
				model.URL == null ? new OracleParameter(":URL", OracleDbType.Clob) { Value = DBNull.Value } : new OracleParameter(":URL", OracleDbType.Clob) { Value = model.URL },
				model.Contents == null ? new OracleParameter(":Contents", OracleDbType.Clob) { Value = DBNull.Value } : new OracleParameter(":Contents", OracleDbType.Clob) { Value = model.Contents },
				model.Others == null ? new OracleParameter(":Others", OracleDbType.Clob) { Value = DBNull.Value } : new OracleParameter(":Others", OracleDbType.Clob) { Value = model.Others },
				model.OldXml == null ? new OracleParameter(":OldXml", OracleDbType.Clob) { Value = DBNull.Value } : new OracleParameter(":OldXml", OracleDbType.Clob) { Value = model.OldXml },
				model.NewXml == null ? new OracleParameter(":NewXml", OracleDbType.Clob) { Value = DBNull.Value } : new OracleParameter(":NewXml", OracleDbType.Clob) { Value = model.NewXml }
			};
            return dbHelper.Execute(sql, parameters);
        }
        /// <summary>
        /// 更新记录
        /// </summary>
        /// <param name="model">RoadFlow.Data.Model.Log实体类</param>
        public int Update(RoadFlow.Data.Model.Log model)
        {
            string sql = @"UPDATE Log SET 
				Title=:Title,Type=:Type,WriteTime=:WriteTime,UserID=:UserID,UserName=:UserName,IPAddress=:IPAddress,URL=:URL,Contents=:Contents,Others=:Others,OldXml=:OldXml,NewXml=:NewXml
				WHERE ID=:ID";
            OracleParameter[] parameters = new OracleParameter[]{
				new OracleParameter(":Title", OracleDbType.NVarchar2){ Value = model.Title },
				new OracleParameter(":Type", OracleDbType.NVarchar2, 100){ Value = model.Type },
				new OracleParameter(":WriteTime", OracleDbType.Date, 8){ Value = model.WriteTime },
				model.UserID == null ? new OracleParameter(":UserID", OracleDbType.Varchar2, 40) { Value = DBNull.Value } : new OracleParameter(":UserID", OracleDbType.Varchar2, 40) { Value = model.UserID },
				model.UserName == null ? new OracleParameter(":UserName", OracleDbType.NVarchar2, 100) { Value = DBNull.Value } : new OracleParameter(":UserName", OracleDbType.NVarchar2, 100) { Value = model.UserName },
				model.IPAddress == null ? new OracleParameter(":IPAddress", OracleDbType.Varchar2, 50) { Value = DBNull.Value } : new OracleParameter(":IPAddress", OracleDbType.Varchar2, 50) { Value = model.IPAddress },
				model.URL == null ? new OracleParameter(":URL", OracleDbType.Clob) { Value = DBNull.Value } : new OracleParameter(":URL", OracleDbType.Clob) { Value = model.URL },
				model.Contents == null ? new OracleParameter(":Contents", OracleDbType.Clob) { Value = DBNull.Value } : new OracleParameter(":Contents", OracleDbType.Clob) { Value = model.Contents },
				model.Others == null ? new OracleParameter(":Others", OracleDbType.Clob) { Value = DBNull.Value } : new OracleParameter(":Others", OracleDbType.Clob) { Value = model.Others },
				model.OldXml == null ? new OracleParameter(":OldXml", OracleDbType.Clob) { Value = DBNull.Value } : new OracleParameter(":OldXml", OracleDbType.Clob) { Value = model.OldXml },
				model.NewXml == null ? new OracleParameter(":NewXml", OracleDbType.Clob) { Value = DBNull.Value } : new OracleParameter(":NewXml", OracleDbType.Clob) { Value = model.NewXml },
				new OracleParameter(":ID", OracleDbType.Varchar2, 40){ Value = model.ID }
			};
            return dbHelper.Execute(sql, parameters);
        }
        /// <summary>
        /// 删除记录
        /// </summary>
        public int Delete(Guid id)
        {
            string sql = "DELETE FROM Log WHERE ID=:ID";
            OracleParameter[] parameters = new OracleParameter[]{
				new OracleParameter(":ID", OracleDbType.Varchar2){ Value = id }
			};
            return dbHelper.Execute(sql, parameters);
        }
        /// <summary>
        /// 将DataRedar转换为List
        /// </summary>
        private List<RoadFlow.Data.Model.Log> DataReaderToList(OracleDataReader dataReader)
        {
            List<RoadFlow.Data.Model.Log> List = new List<RoadFlow.Data.Model.Log>();
            RoadFlow.Data.Model.Log model = null;
            while (dataReader.Read())
            {
                model = new RoadFlow.Data.Model.Log();
                model.ID = dataReader.GetString(0).ToGuid();
                model.Title = dataReader.GetString(1);
                model.Type = dataReader.GetString(2);
                model.WriteTime = dataReader.GetDateTime(3);
                if (!dataReader.IsDBNull(4))
                    model.UserID = dataReader.GetString(4).ToGuid();
                if (!dataReader.IsDBNull(5))
                    model.UserName = dataReader.GetString(5);
                if (!dataReader.IsDBNull(6))
                    model.IPAddress = dataReader.GetString(6);
                if (!dataReader.IsDBNull(7))
                    model.URL = dataReader.GetString(7);
                if (!dataReader.IsDBNull(8))
                    model.Contents = dataReader.GetString(8);
                if (!dataReader.IsDBNull(9))
                    model.Others = dataReader.GetString(9);
                if (!dataReader.IsDBNull(10))
                    model.OldXml = dataReader.GetString(10);
                if (!dataReader.IsDBNull(11))
                    model.NewXml = dataReader.GetString(11);
                List.Add(model);
            }
            return List;
        }
        /// <summary>
        /// 查询所有记录
        /// </summary>
        public List<RoadFlow.Data.Model.Log> GetAll()
        {
            string sql = "SELECT * FROM Log";
            OracleDataReader dataReader = dbHelper.GetDataReader(sql);
            List<RoadFlow.Data.Model.Log> List = DataReaderToList(dataReader);
            dataReader.Close();
            return List;
        }
        /// <summary>
        /// 查询记录数
        /// </summary>
        public long GetCount()
        {
            string sql = "SELECT COUNT(*) FROM Log";
            long count;
            return long.TryParse(dbHelper.GetFieldValue(sql), out count) ? count : 0;
        }
        /// <summary>
        /// 根据主键查询一条记录
        /// </summary>
        public RoadFlow.Data.Model.Log Get(Guid id)
        {
            string sql = "SELECT * FROM Log WHERE ID=:ID";
            OracleParameter[] parameters = new OracleParameter[]{
				new OracleParameter(":ID", OracleDbType.Varchar2){ Value = id }
			};
            OracleDataReader dataReader = dbHelper.GetDataReader(sql, parameters);
            List<RoadFlow.Data.Model.Log> List = DataReaderToList(dataReader);
            dataReader.Close();
            return List.Count > 0 ? List[0] : null;
        }

        /// <summary>
        /// 得到一页日志数据
        /// </summary>
        /// <param name="pager"></param>
        /// <param name="query"></param>
        /// <param name="order"></param>
        /// <param name="size"></param>
        /// <param name="number"></param>
        /// <param name="title"></param>
        /// <param name="type"></param>
        /// <param name="date1"></param>
        /// <param name="date2"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        public DataTable GetPagerData(out string pager, string query = "", int size = 15, int number = 1, string title = "", string type = "", string date1 = "", string date2 = "", string userID = "")
        {
            StringBuilder where = new StringBuilder();
            List<OracleParameter> parList = new List<OracleParameter>();
            if (!title.IsNullOrEmpty())
            {
                where.Append("AND CHARINDEX(:Title,Title)>0 ");
                parList.Add(new OracleParameter(":Title", OracleDbType.NVarchar2) { Value = title });
            }
            if (!type.IsNullOrEmpty())
            {
                where.Append("AND Type=:Type ");
                parList.Add(new OracleParameter(":Type", OracleDbType.NVarchar2) { Value = type });
            }
            if (date1.IsDateTime())
            {
                where.Append("AND WriteTime>=:Date1 ");
                parList.Add(new OracleParameter(":Date1", OracleDbType.Date) { Value = date1.ToDateTime().ToString("yyyy-MM-dd 00:00:00") });
            }
            if (date2.IsDateTime())
            {
                where.Append("AND WriteTime<=:Date2 ");
                parList.Add(new OracleParameter(":Date2", OracleDbType.Date) { Value = date2.ToDateTime().AddDays(1).ToString("yyyy-MM-dd 00:00:00") });
            }
            if (userID.IsGuid())
            {
                where.Append("AND UserID=:UserID ");
                parList.Add(new OracleParameter(":UserID", OracleDbType.Varchar2) { Value = userID.ToGuid() });
            }
            long count;
            string sql = dbHelper.GetPaerSql("Log", "ID,Title,Type,WriteTime,UserName,IPAddress", where.ToString(), "WriteTime DESC", size, number, out count, parList.ToArray());
            pager = RoadFlow.Utility.Tools.GetPagerHtml(count, size, number, query);
            return dbHelper.GetDataTable(sql.ToString(), parList.ToArray());
        }
    }
}