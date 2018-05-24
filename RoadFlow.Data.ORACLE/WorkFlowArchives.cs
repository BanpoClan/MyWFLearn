using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Oracle.ManagedDataAccess.Client;

namespace RoadFlow.Data.ORACLE
{
    public class WorkFlowArchives : RoadFlow.Data.Interface.IWorkFlowArchives
    {
        private DBHelper dbHelper = new DBHelper();
        /// <summary>
        /// 构造函数
        /// </summary>
        public WorkFlowArchives()
        {
        }
        /// <summary>
        /// 添加记录
        /// </summary>
        /// <param name="model">RoadFlow.Data.Model.WorkFlowArchives实体类</param>
        /// <returns>操作所影响的行数</returns>
        public int Add(RoadFlow.Data.Model.WorkFlowArchives model)
        {
            string sql = @"INSERT INTO WorkFlowArchives
				(ID,FlowID,StepID,FlowName,StepName,TaskID,GroupID,InstanceID,Title,Contents,Comments,WriteTime) 
				VALUES(:ID,:FlowID,:StepID,:FlowName,:StepName,:TaskID,:GroupID,:InstanceID,:Title,:Contents,:Comments,:WriteTime)";
            OracleParameter[] parameters = new OracleParameter[]{
				new OracleParameter(":ID", OracleDbType.Varchar2, 40){ Value = model.ID },
				new OracleParameter(":FlowID", OracleDbType.Varchar2, 40){ Value = model.FlowID },
				new OracleParameter(":StepID", OracleDbType.Varchar2, 40){ Value = model.StepID },
				new OracleParameter(":FlowName", OracleDbType.NVarchar2, 1000){ Value = model.FlowName },
				new OracleParameter(":StepName", OracleDbType.NVarchar2, 1000){ Value = model.StepName },
				new OracleParameter(":TaskID", OracleDbType.Varchar2, 40){ Value = model.TaskID },
				new OracleParameter(":GroupID", OracleDbType.Varchar2, 40){ Value = model.GroupID },
				new OracleParameter(":InstanceID", OracleDbType.Varchar2, 500){ Value = model.InstanceID },
				new OracleParameter(":Title", OracleDbType.NVarchar2, 8000){ Value = model.Title },
				new OracleParameter(":Contents", OracleDbType.Clob){ Value = model.Contents },
				new OracleParameter(":Comments", OracleDbType.Clob){ Value = model.Comments },
				new OracleParameter(":WriteTime", OracleDbType.Date, 8){ Value = model.WriteTime }
			};
            return dbHelper.Execute(sql, parameters);
        }
        /// <summary>
        /// 更新记录
        /// </summary>
        /// <param name="model">RoadFlow.Data.Model.WorkFlowArchives实体类</param>
        public int Update(RoadFlow.Data.Model.WorkFlowArchives model)
        {
            string sql = @"UPDATE WorkFlowArchives SET 
				FlowID=:FlowID,StepID=:StepID,FlowName=:FlowName,StepName=:StepName,TaskID=:TaskID,GroupID=:GroupID,InstanceID=:InstanceID,Title=:Title,Contents=:Contents,Comments=:Comments,WriteTime=:WriteTime
				WHERE ID=:ID";
            OracleParameter[] parameters = new OracleParameter[]{
				new OracleParameter(":FlowID", OracleDbType.Varchar2, 40){ Value = model.FlowID },
				new OracleParameter(":StepID", OracleDbType.Varchar2, 40){ Value = model.StepID },
				new OracleParameter(":FlowName", OracleDbType.NVarchar2, 1000){ Value = model.FlowName },
				new OracleParameter(":StepName", OracleDbType.NVarchar2, 1000){ Value = model.StepName },
				new OracleParameter(":TaskID", OracleDbType.Varchar2, 40){ Value = model.TaskID },
				new OracleParameter(":GroupID", OracleDbType.Varchar2, 40){ Value = model.GroupID },
				new OracleParameter(":InstanceID", OracleDbType.Varchar2, 500){ Value = model.InstanceID },
				new OracleParameter(":Title", OracleDbType.NVarchar2, 8000){ Value = model.Title },
				new OracleParameter(":Contents", OracleDbType.Clob){ Value = model.Contents },
				new OracleParameter(":Comments", OracleDbType.Clob){ Value = model.Comments },
				new OracleParameter(":WriteTime", OracleDbType.Date, 8){ Value = model.WriteTime },
				new OracleParameter(":ID", OracleDbType.Varchar2, 40){ Value = model.ID }
			};
            return dbHelper.Execute(sql, parameters);
        }
        /// <summary>
        /// 删除记录
        /// </summary>
        public int Delete(Guid id)
        {
            string sql = "DELETE FROM WorkFlowArchives WHERE ID=:ID";
            OracleParameter[] parameters = new OracleParameter[]{
				new OracleParameter(":ID", OracleDbType.Varchar2){ Value = id }
			};
            return dbHelper.Execute(sql, parameters);
        }
        /// <summary>
        /// 将DataRedar转换为List
        /// </summary>
        private List<RoadFlow.Data.Model.WorkFlowArchives> DataReaderToList(OracleDataReader dataReader)
        {
            List<RoadFlow.Data.Model.WorkFlowArchives> List = new List<RoadFlow.Data.Model.WorkFlowArchives>();
            RoadFlow.Data.Model.WorkFlowArchives model = null;
            while (dataReader.Read())
            {
                model = new RoadFlow.Data.Model.WorkFlowArchives();
                model.ID = dataReader.GetString(0).ToGuid();
                model.FlowID = dataReader.GetString(1).ToGuid();
                model.StepID = dataReader.GetString(2).ToGuid();
                model.FlowName = dataReader.GetString(3);
                model.StepName = dataReader.GetString(4);
                model.TaskID = dataReader.GetString(5).ToGuid();
                model.GroupID = dataReader.GetString(6).ToGuid();
                model.InstanceID = dataReader.GetString(7);
                model.Title = dataReader.GetString(8);
                model.Contents = dataReader.GetString(9);
                model.Comments = dataReader.GetString(10);
                model.WriteTime = dataReader.GetDateTime(11);
                List.Add(model);
            }
            return List;
        }
        /// <summary>
        /// 查询所有记录
        /// </summary>
        public List<RoadFlow.Data.Model.WorkFlowArchives> GetAll()
        {
            string sql = "SELECT * FROM WorkFlowArchives";
            OracleDataReader dataReader = dbHelper.GetDataReader(sql);
            List<RoadFlow.Data.Model.WorkFlowArchives> List = DataReaderToList(dataReader);
            dataReader.Close();
            return List;
        }
        /// <summary>
        /// 查询记录数
        /// </summary>
        public long GetCount()
        {
            string sql = "SELECT COUNT(*) FROM WorkFlowArchives";
            long count;
            return long.TryParse(dbHelper.GetFieldValue(sql), out count) ? count : 0;
        }
        /// <summary>
        /// 根据主键查询一条记录
        /// </summary>
        public RoadFlow.Data.Model.WorkFlowArchives Get(Guid id)
        {
            string sql = "SELECT * FROM WorkFlowArchives WHERE ID=:ID";
            OracleParameter[] parameters = new OracleParameter[]{
				new OracleParameter(":ID", OracleDbType.Varchar2){ Value = id }
			};
            OracleDataReader dataReader = dbHelper.GetDataReader(sql, parameters);
            List<RoadFlow.Data.Model.WorkFlowArchives> List = DataReaderToList(dataReader);
            dataReader.Close();
            return List.Count > 0 ? List[0] : null;
        }

        /// <summary>
        /// 得到一页数据
        /// </summary>
        /// <param name="pager"></param>
        /// <param name="query"></param>
        /// <param name="title"></param>
        /// <param name="flowIDString"></param>
        /// <returns></returns>
        public System.Data.DataTable GetPagerData(out string pager, string query = "", string title = "", string flowIDString = "")
        {
            StringBuilder WHERE = new StringBuilder();
            List<OracleParameter> parList = new List<OracleParameter>();
            if (!title.IsNullOrEmpty())
            {
                WHERE.Append("AND CHARINDEX(:Title,Title)>0 ");
                parList.Add(new OracleParameter(":Title", OracleDbType.NVarchar2) { Value = title });
            }
            if (!flowIDString.IsNullOrEmpty())
            {
                WHERE.AppendFormat("AND FlowID IN({0}) ", RoadFlow.Utility.Tools.GetSqlInString(flowIDString));
            }
            long count;
            int size = RoadFlow.Utility.Tools.GetPageSize();
            int number = RoadFlow.Utility.Tools.GetPageNumber();
            string sql = dbHelper.GetPaerSql("WorkFlowArchives", "*", WHERE.ToString(), "WriteTime DESC", size, number, out count, parList.ToArray());

            pager = RoadFlow.Utility.Tools.GetPagerHtml(count, size, number, query);
            return dbHelper.GetDataTable(sql, parList.ToArray());
        }
    }
}