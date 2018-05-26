using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Oracle.ManagedDataAccess.Client;

namespace MyCreek.Data.ORACLE
{
    public class WorkFlowDelegation : MyCreek.Data.Interface.IWorkFlowDelegation
    {
        private DBHelper dbHelper = new DBHelper();
        /// <summary>
        /// 构造函数
        /// </summary>
        public WorkFlowDelegation()
        {
        }
        /// <summary>
        /// 添加记录
        /// </summary>
        /// <param name="model">MyCreek.Data.Model.WorkFlowDelegation实体类</param>
        /// <returns>操作所影响的行数</returns>
        public int Add(MyCreek.Data.Model.WorkFlowDelegation model)
        {
            string sql = @"INSERT INTO WorkFlowDelegation
				(ID,UserID,StartTime,EndTime,FlowID,ToUserID,WriteTime,Note) 
				VALUES(:ID,:UserID,:StartTime,:EndTime,:FlowID,:ToUserID,:WriteTime,:Note)";
            OracleParameter[] parameters = new OracleParameter[]{
				new OracleParameter(":ID", OracleDbType.Varchar2, 40){ Value = model.ID },
				new OracleParameter(":UserID", OracleDbType.Varchar2, 40){ Value = model.UserID },
				new OracleParameter(":StartTime", OracleDbType.Date, 8){ Value = model.StartTime },
				new OracleParameter(":EndTime", OracleDbType.Date, 8){ Value = model.EndTime },
				model.FlowID == null ? new OracleParameter(":FlowID", OracleDbType.Varchar2, 40) { Value = DBNull.Value } : new OracleParameter(":FlowID", OracleDbType.Varchar2, 40) { Value = model.FlowID },
				new OracleParameter(":ToUserID", OracleDbType.Varchar2, 40){ Value = model.ToUserID },
				new OracleParameter(":WriteTime", OracleDbType.Date, 8){ Value = model.WriteTime },
				model.Note == null ? new OracleParameter(":Note", OracleDbType.NVarchar2, 8000) { Value = DBNull.Value } : new OracleParameter(":Note", OracleDbType.NVarchar2, 8000) { Value = model.Note }
			};
            return dbHelper.Execute(sql, parameters);
        }
        /// <summary>
        /// 更新记录
        /// </summary>
        /// <param name="model">MyCreek.Data.Model.WorkFlowDelegation实体类</param>
        public int Update(MyCreek.Data.Model.WorkFlowDelegation model)
        {
            string sql = @"UPDATE WorkFlowDelegation SET 
				UserID=:UserID,StartTime=:StartTime,EndTime=:EndTime,FlowID=:FlowID,ToUserID=:ToUserID,WriteTime=:WriteTime,Note=:Note
				WHERE ID=:ID";
            OracleParameter[] parameters = new OracleParameter[]{
				new OracleParameter(":UserID", OracleDbType.Varchar2, 40){ Value = model.UserID },
				new OracleParameter(":StartTime", OracleDbType.Date, 8){ Value = model.StartTime },
				new OracleParameter(":EndTime", OracleDbType.Date, 8){ Value = model.EndTime },
				model.FlowID == null ? new OracleParameter(":FlowID", OracleDbType.Varchar2, 40) { Value = DBNull.Value } : new OracleParameter(":FlowID", OracleDbType.Varchar2, 40) { Value = model.FlowID },
				new OracleParameter(":ToUserID", OracleDbType.Varchar2, 40){ Value = model.ToUserID },
				new OracleParameter(":WriteTime", OracleDbType.Date, 8){ Value = model.WriteTime },
				model.Note == null ? new OracleParameter(":Note", OracleDbType.NVarchar2, 8000) { Value = DBNull.Value } : new OracleParameter(":Note", OracleDbType.NVarchar2, 8000) { Value = model.Note },
				new OracleParameter(":ID", OracleDbType.Varchar2, 40){ Value = model.ID }
			};
            return dbHelper.Execute(sql, parameters);
        }
        /// <summary>
        /// 删除记录
        /// </summary>
        public int Delete(Guid id)
        {
            string sql = "DELETE FROM WorkFlowDelegation WHERE ID=:ID";
            OracleParameter[] parameters = new OracleParameter[]{
				new OracleParameter(":ID", OracleDbType.Varchar2){ Value = id }
			};
            return dbHelper.Execute(sql, parameters);
        }
        /// <summary>
        /// 将DataRedar转换为List
        /// </summary>
        private List<MyCreek.Data.Model.WorkFlowDelegation> DataReaderToList(OracleDataReader dataReader)
        {
            List<MyCreek.Data.Model.WorkFlowDelegation> List = new List<MyCreek.Data.Model.WorkFlowDelegation>();
            MyCreek.Data.Model.WorkFlowDelegation model = null;
            while (dataReader.Read())
            {
                model = new MyCreek.Data.Model.WorkFlowDelegation();
                model.ID = dataReader.GetString(0).ToGuid();
                model.UserID = dataReader.GetString(1).ToGuid();
                model.StartTime = dataReader.GetDateTime(2);
                model.EndTime = dataReader.GetDateTime(3);
                if (!dataReader.IsDBNull(4))
                    model.FlowID = dataReader.GetString(4).ToGuid();
                model.ToUserID = dataReader.GetString(5).ToGuid();
                model.WriteTime = dataReader.GetDateTime(6);
                if (!dataReader.IsDBNull(7))
                    model.Note = dataReader.GetString(7);
                List.Add(model);
            }
            return List;
        }
        /// <summary>
        /// 查询所有记录
        /// </summary>
        public List<MyCreek.Data.Model.WorkFlowDelegation> GetAll()
        {
            string sql = "SELECT * FROM WorkFlowDelegation";
            OracleDataReader dataReader = dbHelper.GetDataReader(sql);
            List<MyCreek.Data.Model.WorkFlowDelegation> List = DataReaderToList(dataReader);
            dataReader.Close();
            return List;
        }
        /// <summary>
        /// 查询记录数
        /// </summary>
        public long GetCount()
        {
            string sql = "SELECT COUNT(*) FROM WorkFlowDelegation";
            long count;
            return long.TryParse(dbHelper.GetFieldValue(sql), out count) ? count : 0;
        }
        /// <summary>
        /// 根据主键查询一条记录
        /// </summary>
        public MyCreek.Data.Model.WorkFlowDelegation Get(Guid id)
        {
            string sql = "SELECT * FROM WorkFlowDelegation WHERE ID=:ID";
            OracleParameter[] parameters = new OracleParameter[]{
				new OracleParameter(":ID", OracleDbType.Varchar2){ Value = id }
			};
            OracleDataReader dataReader = dbHelper.GetDataReader(sql, parameters);
            List<MyCreek.Data.Model.WorkFlowDelegation> List = DataReaderToList(dataReader);
            dataReader.Close();
            return List.Count > 0 ? List[0] : null;
        }

        /// <summary>
        /// 查询一个用户所有记录
        /// </summary>
        public List<MyCreek.Data.Model.WorkFlowDelegation> GetByUserID(Guid userID)
        {
            string sql = "SELECT * FROM WorkFlowDelegation WHERE UserID=:UserID";
            OracleParameter[] parameters = new OracleParameter[]{
				new OracleParameter(":UserID", OracleDbType.Varchar2){ Value = userID }
			};
            OracleDataReader dataReader = dbHelper.GetDataReader(sql, parameters);
            List<MyCreek.Data.Model.WorkFlowDelegation> List = DataReaderToList(dataReader);
            dataReader.Close();
            return List;
        }

        /// <summary>
        /// 得到一页数据
        /// </summary>
        /// <param name="pager"></param>
        /// <param name="query"></param>
        /// <param name="userID"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public List<MyCreek.Data.Model.WorkFlowDelegation> GetPagerData(out string pager, string query = "", string userID="", string startTime = "", string endTime = "")
        {
            StringBuilder WHERE = new StringBuilder();
            List<OracleParameter> parList = new List<OracleParameter>();

            if (userID.IsGuid())
            {
                WHERE.Append("AND UserID=:UserID ");
                parList.Add(new OracleParameter(":UserID", OracleDbType.Varchar2) { Value = userID.ToGuid() });
            }
            if (startTime.IsDateTime())
            {
                WHERE.Append("AND StartTime>=:StartTime ");
                parList.Add(new OracleParameter(":StartTime", OracleDbType.Date) { Value = startTime.ToDateTime().ToString("yyyy-MM-dd").ToDateTime() });
            }
            if (endTime.IsDateTime())
            {
                WHERE.Append("AND EndTime<=:EndTime ");
                parList.Add(new OracleParameter(":EndTime", OracleDbType.Date) { Value = endTime.ToDateTime().AddDays(1).ToString("yyyy-MM-dd").ToDateTime() });
            }
            long count;
            int pageSize=MyCreek.Utility.Tools.GetPageSize();
            int pageNumber=MyCreek.Utility.Tools.GetPageNumber();
            string sql = dbHelper.GetPaerSql("WorkFlowDelegation", "*", WHERE.ToString(), "WriteTime Desc", pageSize, pageNumber, out count, parList.ToArray());

            pager = MyCreek.Utility.Tools.GetPagerHtml(count, pageSize, pageNumber, query);
            OracleDataReader dataReader = dbHelper.GetDataReader(sql, parList.ToArray());
            List<MyCreek.Data.Model.WorkFlowDelegation> List = DataReaderToList(dataReader);
            dataReader.Close();
            return List;
        }

        /// <summary>
        /// 得到未过期的委托
        /// </summary>
        public List<MyCreek.Data.Model.WorkFlowDelegation> GetNoExpiredList()
        {
            string sql = "SELECT * FROM WorkFlowDelegation WHERE EndTime>=:EndTime";
            OracleParameter[] parameters = new OracleParameter[]{
				new OracleParameter(":EndTime", OracleDbType.Date){ Value = MyCreek.Utility.DateTimeNew.Now }
			};
            OracleDataReader dataReader = dbHelper.GetDataReader(sql, parameters);
            List<MyCreek.Data.Model.WorkFlowDelegation> List = DataReaderToList(dataReader);
            dataReader.Close();
            return List;
        }
    }
}