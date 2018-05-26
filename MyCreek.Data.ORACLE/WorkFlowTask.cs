using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Oracle.ManagedDataAccess.Client;
using System.Linq;

namespace MyCreek.Data.ORACLE
{
    public class WorkFlowTask : MyCreek.Data.Interface.IWorkFlowTask
    {
        private DBHelper dbHelper = new DBHelper();
        /// <summary>
        /// 构造函数
        /// </summary>
        public WorkFlowTask()
        {
        }
        /// <summary>
        /// 添加记录
        /// </summary>
        /// <param name="model">MyCreek.Data.Model.WorkFlowTask实体类</param>
        /// <returns>操作所影响的行数</returns>
        public int Add(MyCreek.Data.Model.WorkFlowTask model)
        {
            string sql = @"INSERT INTO WorkFlowTask
				(ID,PrevID,PrevStepID,FlowID,StepID,StepName,InstanceID,GroupID,Type,Title,SenderID,SenderName,SenderTime,ReceiveID,ReceiveName,ReceiveTime,OpenTime,CompletedTime,CompletedTime1,Comment1,IsSign,Status,Note,Sort,SubFlowGroupID) 
				VALUES(:ID,:PrevID,:PrevStepID,:FlowID,:StepID,:StepName,:InstanceID,:GroupID,:Type,:Title,:SenderID,:SenderName,:SenderTime,:ReceiveID,:ReceiveName,:ReceiveTime,:OpenTime,:CompletedTime,:CompletedTime1,:Comment1,:IsSign,:Status,:Note,:Sort,:SubFlowGroupID)";
            OracleParameter[] parameters = new OracleParameter[]{
				new OracleParameter(":ID", OracleDbType.Varchar2, 40){ Value = model.ID },
				new OracleParameter(":PrevID", OracleDbType.Varchar2, 40){ Value = model.PrevID },
				new OracleParameter(":PrevStepID", OracleDbType.Varchar2, 40){ Value = model.PrevStepID },
				new OracleParameter(":FlowID", OracleDbType.Varchar2, 40){ Value = model.FlowID },
				new OracleParameter(":StepID", OracleDbType.Varchar2, 40){ Value = model.StepID },
				new OracleParameter(":StepName", OracleDbType.NVarchar2, 1000){ Value = model.StepName },
				new OracleParameter(":InstanceID", OracleDbType.Varchar2, 50){ Value = model.InstanceID },
				new OracleParameter(":GroupID", OracleDbType.Varchar2, 40){ Value = model.GroupID },
				new OracleParameter(":Type", OracleDbType.Int32){ Value = model.Type },
				new OracleParameter(":Title", OracleDbType.NVarchar2, 4000){ Value = model.Title },
				new OracleParameter(":SenderID", OracleDbType.Varchar2, 40){ Value = model.SenderID },
				new OracleParameter(":SenderName", OracleDbType.NVarchar2, 100){ Value = model.SenderName },
				new OracleParameter(":SenderTime", OracleDbType.Date, 8){ Value = model.SenderTime },
				new OracleParameter(":ReceiveID", OracleDbType.Varchar2, 40){ Value = model.ReceiveID },
				new OracleParameter(":ReceiveName", OracleDbType.NVarchar2, 100){ Value = model.ReceiveName },
				new OracleParameter(":ReceiveTime", OracleDbType.Date, 8){ Value = model.ReceiveTime },
				model.OpenTime == null ? new OracleParameter(":OpenTime", OracleDbType.Date, 8) { Value = DBNull.Value } : new OracleParameter(":OpenTime", OracleDbType.Date, 8) { Value = model.OpenTime },
				model.CompletedTime == null ? new OracleParameter(":CompletedTime", OracleDbType.Date, 8) { Value = DBNull.Value } : new OracleParameter(":CompletedTime", OracleDbType.Date, 8) { Value = model.CompletedTime },
				model.CompletedTime1 == null ? new OracleParameter(":CompletedTime1", OracleDbType.Date, 8) { Value = DBNull.Value } : new OracleParameter(":CompletedTime1", OracleDbType.Date, 8) { Value = model.CompletedTime1 },
				model.Comment == null ? new OracleParameter(":Comment1", OracleDbType.Clob) { Value = DBNull.Value } : new OracleParameter(":Comment1", OracleDbType.Clob) { Value = model.Comment },
				model.IsSign == null ? new OracleParameter(":IsSign", OracleDbType.Int32) { Value = DBNull.Value } : new OracleParameter(":IsSign", OracleDbType.Int32) { Value = model.IsSign },
				new OracleParameter(":Status", OracleDbType.Int32){ Value = model.Status },
				model.Note == null ? new OracleParameter(":Note", OracleDbType.NVarchar2) { Value = DBNull.Value } : new OracleParameter(":Note", OracleDbType.NVarchar2) { Value = model.Note },
				new OracleParameter(":Sort", OracleDbType.Int32){ Value = model.Sort },
                model.SubFlowGroupID == null ? new OracleParameter(":SubFlowGroupID", OracleDbType.Varchar2) { Value = DBNull.Value } : new OracleParameter(":SubFlowGroupID", OracleDbType.Varchar2) { Value = model.SubFlowGroupID }
			};
            return dbHelper.Execute(sql, parameters);
        }
        /// <summary>
        /// 更新记录
        /// </summary>
        /// <param name="model">MyCreek.Data.Model.WorkFlowTask实体类</param>
        public int Update(MyCreek.Data.Model.WorkFlowTask model)
        {
            string sql = @"UPDATE WorkFlowTask SET 
				PrevID=:PrevID,PrevStepID=:PrevStepID,FlowID=:FlowID,StepID=:StepID,StepName=:StepName,InstanceID=:InstanceID,GroupID=:GroupID,Type=:Type,Title=:Title,SenderID=:SenderID,SenderName=:SenderName,SenderTime=:SenderTime,ReceiveID=:ReceiveID,ReceiveName=:ReceiveName,ReceiveTime=:ReceiveTime,OpenTime=:OpenTime,CompletedTime=:CompletedTime,CompletedTime1=:CompletedTime1,Comment1=:Comment1,IsSign=:IsSign,Status=:Status,Note=:Note,Sort=:Sort,SubFlowGroupID=:SubFlowGroupID
				WHERE ID=:ID";
            OracleParameter[] parameters = new OracleParameter[]{
				new OracleParameter(":PrevID", OracleDbType.Varchar2, 40){ Value = model.PrevID },
				new OracleParameter(":PrevStepID", OracleDbType.Varchar2, 40){ Value = model.PrevStepID },
				new OracleParameter(":FlowID", OracleDbType.Varchar2, 40){ Value = model.FlowID },
				new OracleParameter(":StepID", OracleDbType.Varchar2, 40){ Value = model.StepID },
				new OracleParameter(":StepName", OracleDbType.NVarchar2, 1000){ Value = model.StepName },
				new OracleParameter(":InstanceID", OracleDbType.Varchar2, 50){ Value = model.InstanceID },
				new OracleParameter(":GroupID", OracleDbType.Varchar2, 40){ Value = model.GroupID },
				new OracleParameter(":Type", OracleDbType.Int32){ Value = model.Type },
				new OracleParameter(":Title", OracleDbType.NVarchar2, 4000){ Value = model.Title },
				new OracleParameter(":SenderID", OracleDbType.Varchar2, 40){ Value = model.SenderID },
				new OracleParameter(":SenderName", OracleDbType.NVarchar2, 100){ Value = model.SenderName },
				new OracleParameter(":SenderTime", OracleDbType.Date, 8){ Value = model.SenderTime },
				new OracleParameter(":ReceiveID", OracleDbType.Varchar2, 40){ Value = model.ReceiveID },
				new OracleParameter(":ReceiveName", OracleDbType.NVarchar2, 100){ Value = model.ReceiveName },
				new OracleParameter(":ReceiveTime", OracleDbType.Date, 8){ Value = model.ReceiveTime },
				model.OpenTime == null ? new OracleParameter(":OpenTime", OracleDbType.Date, 8) { Value = DBNull.Value } : new OracleParameter(":OpenTime", OracleDbType.Date, 8) { Value = model.OpenTime },
				model.CompletedTime == null ? new OracleParameter(":CompletedTime", OracleDbType.Date, 8) { Value = DBNull.Value } : new OracleParameter(":CompletedTime", OracleDbType.Date, 8) { Value = model.CompletedTime },
				model.CompletedTime1 == null ? new OracleParameter(":CompletedTime1", OracleDbType.Date, 8) { Value = DBNull.Value } : new OracleParameter(":CompletedTime1", OracleDbType.Date, 8) { Value = model.CompletedTime1 },
				model.Comment == null ? new OracleParameter(":Comment1", OracleDbType.Clob) { Value = DBNull.Value } : new OracleParameter(":Comment1", OracleDbType.Clob) { Value = model.Comment },
				model.IsSign == null ? new OracleParameter(":IsSign", OracleDbType.Int32) { Value = DBNull.Value } : new OracleParameter(":IsSign", OracleDbType.Int32) { Value = model.IsSign },
				new OracleParameter(":Status", OracleDbType.Int32){ Value = model.Status },
				model.Note == null ? new OracleParameter(":Note", OracleDbType.NVarchar2) { Value = DBNull.Value } : new OracleParameter(":Note", OracleDbType.NVarchar2) { Value = model.Note },
				new OracleParameter(":Sort", OracleDbType.Int32){ Value = model.Sort },
				new OracleParameter(":ID", OracleDbType.Varchar2, 40){ Value = model.ID },
                model.SubFlowGroupID == null ? new OracleParameter(":SubFlowGroupID", OracleDbType.Varchar2) { Value = DBNull.Value } : new OracleParameter(":SubFlowGroupID", OracleDbType.Varchar2) { Value = model.SubFlowGroupID }
			};
            return dbHelper.Execute(sql, parameters);
        }
        /// <summary>
        /// 删除记录
        /// </summary>
        public int Delete(Guid id)
        {
            string sql = "DELETE FROM WorkFlowTask WHERE ID=:ID";
            OracleParameter[] parameters = new OracleParameter[]{
				new OracleParameter(":ID", OracleDbType.Varchar2){ Value = id }
			};
            return dbHelper.Execute(sql, parameters);
        }
        /// <summary>
        /// 将DataRedar转换为List
        /// </summary>
        private List<MyCreek.Data.Model.WorkFlowTask> DataReaderToList(OracleDataReader dataReader)
        {
            List<MyCreek.Data.Model.WorkFlowTask> List = new List<MyCreek.Data.Model.WorkFlowTask>();
            MyCreek.Data.Model.WorkFlowTask model = null;
            while (dataReader.Read())
            {
                model = new MyCreek.Data.Model.WorkFlowTask();
                model.ID = dataReader.GetString(0).ToGuid();
                model.PrevID = dataReader.GetString(1).ToGuid();
                model.PrevStepID = dataReader.GetString(2).ToGuid();
                model.FlowID = dataReader.GetString(3).ToGuid();
                model.StepID = dataReader.GetString(4).ToGuid();
                model.StepName = dataReader.GetString(5);
                model.InstanceID = dataReader.GetString(6);
                model.GroupID = dataReader.GetString(7).ToGuid();
                model.Type = dataReader.GetInt32(8);
                model.Title = dataReader.GetString(9);
                model.SenderID = dataReader.GetString(10).ToGuid();
                model.SenderName = dataReader.GetString(11);
                model.SenderTime = dataReader.GetDateTime(12);
                model.ReceiveID = dataReader.GetString(13).ToGuid();
                model.ReceiveName = dataReader.GetString(14);
                model.ReceiveTime = dataReader.GetDateTime(15);
                if (!dataReader.IsDBNull(16))
                    model.OpenTime = dataReader.GetDateTime(16);
                if (!dataReader.IsDBNull(17))
                    model.CompletedTime = dataReader.GetDateTime(17);
                if (!dataReader.IsDBNull(18))
                    model.CompletedTime1 = dataReader.GetDateTime(18);
                if (!dataReader.IsDBNull(19))
                    model.Comment = dataReader.GetString(19);
                if (!dataReader.IsDBNull(20))
                    model.IsSign = dataReader.GetInt32(20);
                model.Status = dataReader.GetInt32(21);
                if (!dataReader.IsDBNull(22))
                    model.Note = dataReader.GetString(22);
                model.Sort = dataReader.GetInt32(23);
                if (!dataReader.IsDBNull(24))
                    model.SubFlowGroupID = dataReader.GetString(24).ToGuid();
                List.Add(model);
            }
            return List;
        }
        /// <summary>
        /// 查询所有记录
        /// </summary>
        public List<MyCreek.Data.Model.WorkFlowTask> GetAll()
        {
            string sql = "SELECT * FROM WorkFlowTask";
            OracleDataReader dataReader = dbHelper.GetDataReader(sql);
            List<MyCreek.Data.Model.WorkFlowTask> List = DataReaderToList(dataReader);
            dataReader.Close();
            return List;
        }
        /// <summary>
        /// 查询记录数
        /// </summary>
        public long GetCount()
        {
            string sql = "SELECT COUNT(*) FROM WorkFlowTask";
            long count;
            return long.TryParse(dbHelper.GetFieldValue(sql), out count) ? count : 0;
        }
        /// <summary>
        /// 根据主键查询一条记录
        /// </summary>
        public MyCreek.Data.Model.WorkFlowTask Get(Guid id)
        {
            string sql = "SELECT * FROM WorkFlowTask WHERE ID=:ID";
            OracleParameter[] parameters = new OracleParameter[]{
				new OracleParameter(":ID", OracleDbType.Varchar2){ Value = id }
			};
            OracleDataReader dataReader = dbHelper.GetDataReader(sql, parameters);
            List<MyCreek.Data.Model.WorkFlowTask> List = DataReaderToList(dataReader);
            dataReader.Close();
            return List.Count > 0 ? List[0] : null;
        }

        /// <summary>
        /// 删除一个实例
        /// </summary>
        public int Delete(Guid flowID, Guid groupID)
        {
            string sql = "DELETE FROM WorkFlowTask WHERE FlowID=:FlowID AND GroupID=:GroupID";
            OracleParameter[] parameters = new OracleParameter[]{
				new OracleParameter(":FlowID", OracleDbType.Varchar2){ Value = flowID },
                new OracleParameter(":GroupID", OracleDbType.Varchar2){ Value = groupID }
			};
            return dbHelper.Execute(sql, parameters);
        }
       
        /// <summary>
        /// 更新打开时间
        /// </summary>
        /// <param name="id"></param>
        /// <param name="openTime"></param>
        /// <param name="isStatus">是否将状态更新为1</param>
        public void UpdateOpenTime(Guid id, DateTime openTime, bool isStatus = false)
        {
            string sql = "UPDATE WorkFlowTask SET OpenTime=:OpenTime " + (isStatus ? ", Status=1" : "") + " WHERE ID=:ID AND OpenTime IS NULL";
            
            OracleParameter[] parameters = new OracleParameter[]{
                openTime==DateTime.MinValue? new OracleParameter(":OpenTime", OracleDbType.Date){ Value = DBNull.Value} :
                    new OracleParameter(":OpenTime", OracleDbType.Date){ Value = openTime },
				new OracleParameter(":ID", OracleDbType.Varchar2){ Value = id }
			};
            dbHelper.Execute(sql, parameters);
        }


        /// <summary>
        /// 查询待办任务
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="pager"></param>
        /// <param name="query"></param>
        /// <param name="title"></param>
        /// <param name="flowid"></param>
        /// <param name="date1"></param>
        /// <param name="date2"></param>
        /// <param name="type">0待办 1已完成</param>
        /// <returns></returns>
        public List<MyCreek.Data.Model.WorkFlowTask> GetTasks(Guid userID, out string pager, string query="", string title="", string flowid="", string sender="", string date1="", string date2="", int type=0)
        {
            List<OracleParameter> parList = new List<OracleParameter>();
            StringBuilder sql = new StringBuilder("SELECT PagerTempTable.*,ROWNUM AS PagerAutoRowNumber FROM(SELECT * FROM WorkFlowTask WHERE ReceiveID=:ReceiveID ");
            sql.Append(type == 0 ? " AND Status IN(0,1)" : " AND Status IN(2,3)");
            parList.Add(new OracleParameter(":ReceiveID", OracleDbType.Varchar2) { Value = userID });
            if (!title.IsNullOrEmpty())
            {
                sql.Append(" AND INSTR(Title,:Title,1,1)>0");
                parList.Add(new OracleParameter(":Title", OracleDbType.NVarchar2, 2000) { Value = title });
            }
            if (flowid.IsGuid())
            {
                sql.Append(" AND FlowID=:FlowID");
                parList.Add(new OracleParameter(":FlowID", OracleDbType.Varchar2) { Value = flowid.ToGuid() });
            }
            else if (!flowid.IsNullOrEmpty() && flowid.IndexOf(',') >= 0)
            {
                sql.Append(" AND FlowID IN(" + MyCreek.Utility.Tools.GetSqlInString(flowid) + ")");
            }
            if (sender.IsGuid())
            {
                sql.Append(" AND SenderID=:SenderID");
                parList.Add(new OracleParameter(":SenderID", OracleDbType.Varchar2) { Value = sender.ToGuid() });
            }
            if (date1.IsDateTime())
            {
                sql.Append(" AND ReceiveTime>=:ReceiveTime");
                parList.Add(new OracleParameter(":ReceiveTime", OracleDbType.Date) { Value = date1.ToDateTime().Date });
            }
            if (date2.IsDateTime())
            {
                sql.Append(" AND ReceiveTime<=:ReceiveTime1");
                parList.Add(new OracleParameter(":ReceiveTime1", OracleDbType.Date) { Value = date2.ToDateTime().AddDays(1).Date });
            }
            sql.Append(" ORDER BY " + (type == 0 ? "ReceiveTime DESC" : "CompletedTime1 DESC") + ") PagerTempTable");

            
            long count;
            int size = MyCreek.Utility.Tools.GetPageSize();
            int number = MyCreek.Utility.Tools.GetPageNumber();
            string sql1 = dbHelper.GetPaerSql(sql.ToString(), size, number, out count, parList.ToArray());
            pager = MyCreek.Utility.Tools.GetPagerHtml(count, size, number, query);

            OracleDataReader dataReader = dbHelper.GetDataReader(sql1, parList.ToArray());
            List<MyCreek.Data.Model.WorkFlowTask> List = DataReaderToList(dataReader);
            dataReader.Close();
            return List;
        }

        /// <summary>
        /// 得到流程实例列表
        /// </summary>
        /// <param name="flowID"></param>
        /// <param name="senderID"></param>
        /// <param name="receiveID"></param>
        /// <param name="pager"></param>
        /// <param name="query"></param>
        /// <param name="title"></param>
        /// <param name="flowid"></param>
        /// <param name="date1"></param>
        /// <param name="date2"></param>
        /// <param name="status">是否完成 0:全部 1:未完成 2:已完成</param>
        /// <returns></returns>
        public List<MyCreek.Data.Model.WorkFlowTask> GetInstances(Guid[] flowID, Guid[] senderID, Guid[] receiveID, out string pager, string query = "", string title = "", string flowid = "", string date1 = "", string date2 = "", int status = 0)
        {
            List<OracleParameter> parList = new List<OracleParameter>();
            StringBuilder sql = new StringBuilder(@"SELECT PagerTempTable.*,ROWNUM AS PagerAutoRowNumber FROM (SELECT a.* FROM WorkFlowTask a
                WHERE a.ID=(SELECT ID FROM WorkFlowTask WHERE FlowID=a.FlowID AND GroupID=a.GroupID ");

            if (status != 0)
            {
                if (status == 1)
                {
                    sql.Append(" AND a.Status IN(0,1,5)");
                }
                else if (status == 2)
                {
                    sql.Append(" AND a.Status IN(2,3,4)");
                }
            }
            
            if (flowID != null && flowID.Length > 0)
            {
                sql.Append(string.Format(" AND a.FlowID IN({0})", MyCreek.Utility.Tools.GetSqlInString(flowID)));
            }
            if (senderID != null && senderID.Length > 0)
            {
                if (senderID.Length == 1)
                {
                    sql.Append(" AND a.SenderID=:SenderID");
                    parList.Add(new OracleParameter(":SenderID", OracleDbType.Varchar2) { Value = senderID[0] });
                }
                else
                {
                    sql.Append(string.Format(" AND a.SenderID IN({0})", MyCreek.Utility.Tools.GetSqlInString(senderID)));
                }
            }
            if (receiveID != null && receiveID.Length > 0)
            {
                if (senderID.Length == 1)
                {
                    sql.Append(" AND a.ReceiveID=:ReceiveID");
                    parList.Add(new OracleParameter(":ReceiveID", OracleDbType.Varchar2) { Value = receiveID[0] });
                }
                else
                {
                    sql.Append(string.Format(" AND a.ReceiveID IN({0})", MyCreek.Utility.Tools.GetSqlInString(receiveID)));
                }
            }
            if (!title.IsNullOrEmpty())
            {
                sql.Append(" AND INSTR(a.Title,:Title,1,1)>0");
                parList.Add(new OracleParameter(":Title", OracleDbType.NVarchar2, 2000) { Value = title });
            }
            if (flowid.IsGuid())
            {
                sql.Append(" AND a.FlowID=:FlowID");
                parList.Add(new OracleParameter(":FlowID", OracleDbType.Varchar2) { Value = flowid.ToGuid() });
            }
            else if (!flowid.IsNullOrEmpty() && flowid.IndexOf(',') >= 0)
            {
                sql.Append(" AND a.FlowID IN(" + MyCreek.Utility.Tools.GetSqlInString(flowid) + ")");
            }
            if (date1.IsDateTime())
            {
                sql.Append(" AND a.SenderTime>=:SenderTime");
                parList.Add(new OracleParameter(":SenderTime", OracleDbType.Date) { Value = date1.ToDateTime().Date });
            }
            if (date2.IsDateTime())
            {
                sql.Append(" AND a.SenderTime<=:SenderTime1");
                parList.Add(new OracleParameter(":SenderTime1", OracleDbType.Date) { Value = date1.ToDateTime().AddDays(1).Date });
            }
            sql.Append(" AND ROWNUM<=1) ORDER BY Sort DESC) PagerTempTable");

            long count;
            int size = MyCreek.Utility.Tools.GetPageSize();
            int number = MyCreek.Utility.Tools.GetPageNumber();
            string sql1 = dbHelper.GetPaerSql(sql.ToString(), size, number, out count, parList.ToArray());
            pager = MyCreek.Utility.Tools.GetPagerHtml(count, size, number, query);
          
            OracleDataReader dataReader = dbHelper.GetDataReader(sql1, parList.ToArray());
            List<MyCreek.Data.Model.WorkFlowTask> List = DataReaderToList(dataReader);
            dataReader.Close();
            return List;
        }

        /// <summary>
        /// 得到一个流程实例的发起者
        /// </summary>
        /// <param name="flowID"></param>
        /// <param name="groupID"></param>
        /// <returns></returns>
        public Guid GetFirstSnderID(Guid flowID, Guid groupID)
        {
            string sql = "SELECT SenderID FROM WorkFlowTask WHERE FlowID=:FlowID AND GroupID=:GroupID AND PrevID=:PrevID";
            OracleParameter[] parameters = new OracleParameter[]{
				new OracleParameter(":FlowID", OracleDbType.Varchar2){ Value = flowID },
                new OracleParameter(":GroupID", OracleDbType.Varchar2){ Value = groupID },
                new OracleParameter(":PrevID", OracleDbType.Varchar2){ Value = Guid.Empty }
			};
            string senderID = dbHelper.GetFieldValue(sql, parameters);
            return senderID.ToGuid();
        }

        /// <summary>
        /// 得到一个流程实例一个步骤的处理者
        /// </summary>
        /// <param name="flowID"></param>
        /// <param name="groupID"></param>
        /// <returns></returns>
        public List<Guid> GetStepSnderID(Guid flowID, Guid stepID, Guid groupID)
        {
            string sql = "SELECT ReceiveID FROM WorkFlowTask WHERE FlowID=:FlowID AND StepID=:StepID AND GroupID=:GroupID";
            OracleParameter[] parameters = new OracleParameter[]{
				new OracleParameter(":FlowID", OracleDbType.Varchar2){ Value = flowID },
                new OracleParameter(":StepID", OracleDbType.Varchar2){ Value = stepID },
                new OracleParameter(":GroupID", OracleDbType.Varchar2){ Value = groupID }
			};
            DataTable dt = dbHelper.GetDataTable(sql, parameters);
            List<Guid> senderList = new List<Guid>();
            foreach (DataRow dr in dt.Rows)
            {
                Guid senderID;
                if (Guid.TryParse(dr[0].ToString(), out senderID))
                {
                    senderList.Add(senderID);
                }
            }
            return senderList;
        }
        /// <summary>
        /// 得到一个流程实例前一步骤的处理者
        /// </summary>
        /// <param name="flowID"></param>
        /// <param name="groupID"></param>
        /// <returns></returns>
        public List<Guid> GetPrevSnderID(Guid flowID, Guid stepID, Guid groupID)
        {
            string sql = "SELECT ReceiveID FROM WorkFlowTask WHERE ID=(SELECT PrevID FROM WorkFlowTask WHERE FlowID=:FlowID AND StepID=:StepID AND GroupID=:GroupID)";
            OracleParameter[] parameters = new OracleParameter[]{
				new OracleParameter(":FlowID", OracleDbType.Varchar2){ Value = flowID },
                new OracleParameter(":StepID", OracleDbType.Varchar2){ Value = stepID },
                new OracleParameter(":GroupID", OracleDbType.Varchar2){ Value = groupID }
			};
            DataTable dt = dbHelper.GetDataTable(sql, parameters);
            List<Guid> senderList = new List<Guid>();
            foreach (DataRow dr in dt.Rows)
            {
                Guid senderID;
                if (Guid.TryParse(dr[0].ToString(), out senderID))
                {
                    senderList.Add(senderID);
                }
            }
            return senderList;
        }

        /// <summary>
        /// 完成一个任务
        /// </summary>
        /// <param name="taskID"></param>
        /// <param name="comment"></param>
        /// <param name="isSign"></param>
        /// <returns></returns>
        public int Completed(Guid taskID, string comment = "", bool isSign = false, int status = 2, string note="")
        {
            string sql = "UPDATE WorkFlowTask SET Comment1=:Comment1,CompletedTime1=:CompletedTime1,IsSign=:IsSign,Status=:Status,Note=:Note WHERE ID=:ID";
            OracleParameter[] parameters = new OracleParameter[]{
				comment.IsNullOrEmpty() ? 
                new OracleParameter(":Comment1", OracleDbType.Varchar2){ Value = DBNull.Value } : 
                new OracleParameter(":Comment1", OracleDbType.Varchar2){ Value = comment },
                new OracleParameter(":CompletedTime1", OracleDbType.Date){ Value = MyCreek.Utility.DateTimeNew.Now },
                new OracleParameter(":IsSign", OracleDbType.Int32){ Value = isSign?1:0 },
                new OracleParameter(":Status", OracleDbType.Int32){ Value = status },
                note.IsNullOrEmpty()?
                new OracleParameter(":Note", OracleDbType.NVarchar2){ Value = DBNull.Value }:
                new OracleParameter(":Note", OracleDbType.NVarchar2){ Value = note },
                new OracleParameter(":ID", OracleDbType.Varchar2){ Value = taskID }
			};        
            return dbHelper.Execute(sql, parameters);
        }

        /// <summary>
        /// 更新一个任务后后续任务状态
        /// </summary>
        /// <param name="taskID"></param>
        /// <param name="comment"></param>
        /// <param name="isSign"></param>
        /// <returns></returns>
        public int UpdateNextTaskStatus(Guid taskID, int status)
        {
            string sql = "UPDATE WorkFlowTask SET Status=:Status WHERE PrevID=:PrevID";
            OracleParameter[] parameters = new OracleParameter[]{
                new OracleParameter(":Status", OracleDbType.Int32){ Value = status },
                new OracleParameter(":PrevID", OracleDbType.Varchar2){ Value = taskID }
			};
            return dbHelper.Execute(sql, parameters);
        }


        /// <summary>
        /// 得到一个流程实例一个步骤的任务
        /// </summary>
        /// <param name="flowID"></param>
        /// <param name="groupID"></param>
        /// <returns></returns>
        public List<Model.WorkFlowTask> GetTaskList(Guid flowID, Guid stepID, Guid groupID)
        {
            string sql = "SELECT * FROM WorkFlowTask WHERE FlowID=:FlowID AND StepID=:StepID AND GroupID=:GroupID";
            OracleParameter[] parameters = new OracleParameter[]{
				new OracleParameter(":FlowID", OracleDbType.Varchar2){ Value = flowID },
                new OracleParameter(":StepID", OracleDbType.Varchar2){ Value = stepID },
                new OracleParameter(":GroupID", OracleDbType.Varchar2){ Value = groupID }
			};
            OracleDataReader dataReader = dbHelper.GetDataReader(sql, parameters);
            List<MyCreek.Data.Model.WorkFlowTask> List = DataReaderToList(dataReader);
            dataReader.Close();
            return List;
        }

        /// <summary>
        /// 得到一个流程实例一个步骤一个人员的任务
        /// </summary>
        /// <param name="flowID"></param>
        /// <param name="stepID"></param>
        /// <param name="groupID"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        public List<Model.WorkFlowTask> GetUserTaskList(Guid flowID, Guid stepID, Guid groupID, Guid userID)
        {
            string sql = "SELECT * FROM WorkFlowTask WHERE FlowID=:FlowID AND StepID=:StepID AND GroupID=:GroupID AND ReceiveID=:ReceiveID";
            OracleParameter[] parameters = new OracleParameter[]{
				new OracleParameter(":FlowID", OracleDbType.Varchar2){ Value = flowID },
                new OracleParameter(":StepID", OracleDbType.Varchar2){ Value = stepID },
                new OracleParameter(":GroupID", OracleDbType.Varchar2){ Value = groupID },
                new OracleParameter(":ReceiveID", OracleDbType.Varchar2){ Value = userID }
			};
            OracleDataReader dataReader = dbHelper.GetDataReader(sql, parameters);
            List<MyCreek.Data.Model.WorkFlowTask> List = DataReaderToList(dataReader);
            dataReader.Close();
            return List;
        }


        /// <summary>
        /// 得到一个实例的任务
        /// </summary>
        /// <param name="flowID"></param>
        /// <param name="groupID"></param>
        /// <returns></returns>
        public List<MyCreek.Data.Model.WorkFlowTask> GetTaskList(Guid flowID, Guid groupID)
        {
            string sql = string.Empty;
            OracleParameter[] parameters;
            if (flowID == null || flowID.IsEmptyGuid())
            {
                sql = "SELECT * FROM WorkFlowTask WHERE GroupID=:GroupID";
                parameters = new OracleParameter[]{
                    new OracleParameter(":GroupID", OracleDbType.Varchar2){ Value = groupID }
			    };
            }
            else
            {
                sql = "SELECT * FROM WorkFlowTask WHERE FlowID=:FlowID AND GroupID=:GroupID";
                parameters = new OracleParameter[]{
				    new OracleParameter(":FlowID", OracleDbType.Varchar2){ Value = flowID },
                    new OracleParameter(":GroupID", OracleDbType.Varchar2){ Value = groupID }
			    };
            }
            OracleDataReader dataReader = dbHelper.GetDataReader(sql, parameters);
            List<MyCreek.Data.Model.WorkFlowTask> List = DataReaderToList(dataReader);
            dataReader.Close();
            return List;
        }

        /// <summary>
        /// 得到和一个任务同级的任务
        /// </summary>
        /// <param name="taskID">任务ID</param>
        /// <param name="isStepID">是否区分步骤ID，多步骤会签区分的是上一步骤ID</param>
        /// <returns></returns>
        public List<MyCreek.Data.Model.WorkFlowTask> GetTaskList(Guid taskID, bool isStepID = true)
        {
            var task = Get(taskID);
            if (task == null)
            {
                return new List<Model.WorkFlowTask>() { };
            }
            string sql = string.Format("SELECT * FROM WorkFlowTask WHERE PrevID=@PrevID AND {0}", isStepID ? "StepID=:StepID" : "PrevStepID=:StepID");
            OracleParameter[] parameters1 = new OracleParameter[]{
                new OracleParameter("@PrevID", OracleDbType.Varchar2){ Value = task.PrevID },
                new OracleParameter("@StepID", OracleDbType.Varchar2){ Value = isStepID ? task.StepID : task.PrevStepID }
			};
            OracleDataReader dataReader = dbHelper.GetDataReader(sql, parameters1);
            List<MyCreek.Data.Model.WorkFlowTask> List = DataReaderToList(dataReader);
            dataReader.Close();
            return List;
        }

        /// <summary>
        /// 得到一个任务的前一任务
        /// </summary>
        /// <param name="flowID"></param>
        /// <param name="groupID"></param>
        /// <returns></returns>
        public List<Model.WorkFlowTask> GetPrevTaskList(Guid taskID)
        {
            string sql = "SELECT * FROM WorkFlowTask WHERE ID=(SELECT PrevID FROM WorkFlowTask WHERE ID=:ID)";
            OracleParameter[] parameters = new OracleParameter[]{
                new OracleParameter(":ID", OracleDbType.Varchar2){ Value = taskID }
			};
            OracleDataReader dataReader = dbHelper.GetDataReader(sql, parameters);
            List<MyCreek.Data.Model.WorkFlowTask> List = DataReaderToList(dataReader);
            dataReader.Close();
            return List;
        }

        /// <summary>
        /// 得到一个任务的后续任务
        /// </summary>
        /// <param name="flowID"></param>
        /// <param name="groupID"></param>
        /// <returns></returns>
        public List<Model.WorkFlowTask> GetNextTaskList(Guid taskID)
        {
            string sql = "SELECT * FROM WorkFlowTask WHERE PrevID=:PrevID";
            OracleParameter[] parameters = new OracleParameter[]{
                new OracleParameter(":PrevID", OracleDbType.Varchar2){ Value = taskID }
			};
            OracleDataReader dataReader = dbHelper.GetDataReader(sql, parameters);
            List<MyCreek.Data.Model.WorkFlowTask> List = DataReaderToList(dataReader);
            dataReader.Close();
            return List;
        }


        /// <summary>
        /// 查询一个流程是否有任务数据
        /// </summary>
        /// <param name="flowID"></param>
        /// <returns></returns>
        public bool HasTasks(Guid flowID)
        {
            string sql = "SELECT ID FROM WorkFlowTask WHERE FlowID=:FlowID AND ROWNUM<=1";
            OracleParameter[] parameters = new OracleParameter[]{
                new OracleParameter(":FlowID", OracleDbType.Varchar2){ Value = flowID }
			};
            OracleDataReader dataReader = dbHelper.GetDataReader(sql, parameters);
            bool has = dataReader.HasRows;
            dataReader.Close();
            return has;
        }

        /// <summary>
        /// 查询一个用户在一个步骤是否有未完成任务
        /// </summary>
        /// <param name="flowID"></param>
        /// <returns></returns>
        public bool HasNoCompletedTasks(Guid flowID, Guid stepID, Guid groupID, Guid userID)
        {
            string sql = "SELECT ID FROM WorkFlowTask WHERE FlowID=:FlowID AND StepID=:StepID AND GroupID=:GroupID AND ReceiveID=:ReceiveID AND Status IN(0,1) AND ROWNUM<=1";
            OracleParameter[] parameters = new OracleParameter[]{
                new OracleParameter(":FlowID", OracleDbType.Varchar2){ Value = flowID },
                new OracleParameter(":StepID", OracleDbType.Varchar2){ Value = stepID },
                new OracleParameter(":GroupID", OracleDbType.Varchar2){ Value = groupID },
                new OracleParameter(":ReceiveID", OracleDbType.Varchar2){ Value = userID }
			};
            OracleDataReader dataReader = dbHelper.GetDataReader(sql, parameters);
            bool has = dataReader.HasRows;
            dataReader.Close();
            return has;
        }

        /// <summary>
        /// 得到一个任务的状态
        /// </summary>
        /// <param name="taskID"></param>
        /// <returns></returns>
        public int GetTaskStatus(Guid taskID)
        {
            string sql = "SELECT Status FROM WorkFlowTask WHERE ID=:ID";
            OracleParameter[] parameters = new OracleParameter[]{
                new OracleParameter(":ID", OracleDbType.Varchar2){ Value = taskID }
			};
            string status = dbHelper.GetFieldValue(sql, parameters);
            int s;
            return status.IsInt(out s) ? s : -1;
        }

        /// <summary>
        /// 根据SubFlowID得到一个任务
        /// </summary>
        /// <param name="subflowGroupID"></param>
        /// <returns></returns>
        public List<Model.WorkFlowTask> GetBySubFlowGroupID(Guid subflowGroupID)
        {
            string sql = "SELECT * FROM WorkFlowTask WHERE SubFlowGroupID=:SubFlowGroupID";
            OracleParameter[] parameters = new OracleParameter[]{
                new OracleParameter(":SubFlowGroupID", OracleDbType.Varchar2){ Value = subflowGroupID.ToString() }
			};
            OracleDataReader dataReader = dbHelper.GetDataReader(sql, parameters);
            List<MyCreek.Data.Model.WorkFlowTask> List = DataReaderToList(dataReader);
            dataReader.Close();
            return List;
        }
    }
}