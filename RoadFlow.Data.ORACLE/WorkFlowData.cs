using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Oracle.ManagedDataAccess.Client;

namespace RoadFlow.Data.ORACLE
{
    public class WorkFlowData : RoadFlow.Data.Interface.IWorkFlowData
    {
        private DBHelper dbHelper = new DBHelper();
        /// <summary>
        /// 构造函数
        /// </summary>
        public WorkFlowData()
        {
        }
        /// <summary>
        /// 添加记录
        /// </summary>
        /// <param name="model">RoadFlow.Data.Model.WorkFlowData实体类</param>
        /// <returns>操作所影响的行数</returns>
        public int Add(RoadFlow.Data.Model.WorkFlowData model)
        {
            string sql = @"INSERT INTO WorkFlowData
				(ID,InstanceID,LinkID,TableName,FieldName,Value) 
				VALUES(:ID,:InstanceID,:LinkID,:TableName,:FieldName,:Value)";
            OracleParameter[] parameters = new OracleParameter[]{
				new OracleParameter(":ID", OracleDbType.Varchar2, 40){ Value = model.ID },
				new OracleParameter(":InstanceID", OracleDbType.Varchar2, 40){ Value = model.InstanceID },
				new OracleParameter(":LinkID", OracleDbType.Varchar2, 40){ Value = model.LinkID },
				new OracleParameter(":TableName", OracleDbType.Varchar2, 500){ Value = model.TableName },
				new OracleParameter(":FieldName", OracleDbType.Varchar2, 500){ Value = model.FieldName },
				new OracleParameter(":Value", OracleDbType.Varchar2, 4000){ Value = model.Value }
			};
            return dbHelper.Execute(sql, parameters);
        }
        /// <summary>
        /// 更新记录
        /// </summary>
        /// <param name="model">RoadFlow.Data.Model.WorkFlowData实体类</param>
        public int Update(RoadFlow.Data.Model.WorkFlowData model)
        {
            string sql = @"UPDATE WorkFlowData SET 
				InstanceID=:InstanceID,LinkID=:LinkID,TableName=:TableName,FieldName=:FieldName,Value=:Value
				WHERE ID=:ID";
            OracleParameter[] parameters = new OracleParameter[]{
				new OracleParameter(":InstanceID", OracleDbType.Varchar2, 40){ Value = model.InstanceID },
				new OracleParameter(":LinkID", OracleDbType.Varchar2, 40){ Value = model.LinkID },
				new OracleParameter(":TableName", OracleDbType.Varchar2, 500){ Value = model.TableName },
				new OracleParameter(":FieldName", OracleDbType.Varchar2, 500){ Value = model.FieldName },
				new OracleParameter(":Value", OracleDbType.Varchar2, 4000){ Value = model.Value },
				new OracleParameter(":ID", OracleDbType.Varchar2, 40){ Value = model.ID }
			};
            return dbHelper.Execute(sql, parameters);
        }
        /// <summary>
        /// 删除记录
        /// </summary>
        public int Delete(Guid id)
        {
            string sql = "DELETE FROM WorkFlowData WHERE ID=:ID";
            OracleParameter[] parameters = new OracleParameter[]{
				new OracleParameter(":ID", OracleDbType.Varchar2){ Value = id }
			};
            return dbHelper.Execute(sql, parameters);
        }
        /// <summary>
        /// 将DataRedar转换为List
        /// </summary>
        private List<RoadFlow.Data.Model.WorkFlowData> DataReaderToList(OracleDataReader dataReader)
        {
            List<RoadFlow.Data.Model.WorkFlowData> List = new List<RoadFlow.Data.Model.WorkFlowData>();
            RoadFlow.Data.Model.WorkFlowData model = null;
            while (dataReader.Read())
            {
                model = new RoadFlow.Data.Model.WorkFlowData();
                model.ID = dataReader.GetString(0).ToGuid();
                model.InstanceID = dataReader.GetString(1).ToGuid();
                model.LinkID = dataReader.GetString(2).ToGuid();
                model.TableName = dataReader.GetString(3);
                model.FieldName = dataReader.GetString(4);
                model.Value = dataReader.GetString(5);
                List.Add(model);
            }
            return List;
        }
        /// <summary>
        /// 查询所有记录
        /// </summary>
        public List<RoadFlow.Data.Model.WorkFlowData> GetAll()
        {
            string sql = "SELECT * FROM WorkFlowData";
            OracleDataReader dataReader = dbHelper.GetDataReader(sql);
            List<RoadFlow.Data.Model.WorkFlowData> List = DataReaderToList(dataReader);
            dataReader.Close();
            return List;
        }
        /// <summary>
        /// 查询记录数
        /// </summary>
        public long GetCount()
        {
            string sql = "SELECT COUNT(*) FROM WorkFlowData";
            long count;
            return long.TryParse(dbHelper.GetFieldValue(sql), out count) ? count : 0;
        }
        /// <summary>
        /// 根据主键查询一条记录
        /// </summary>
        public RoadFlow.Data.Model.WorkFlowData Get(Guid id)
        {
            string sql = "SELECT * FROM WorkFlowData WHERE ID=:ID";
            OracleParameter[] parameters = new OracleParameter[]{
				new OracleParameter(":ID", OracleDbType.Varchar2){ Value = id }
			};
            OracleDataReader dataReader = dbHelper.GetDataReader(sql, parameters);
            List<RoadFlow.Data.Model.WorkFlowData> List = DataReaderToList(dataReader);
            dataReader.Close();
            return List.Count > 0 ? List[0] : null;
        }


        /// <summary>
        /// 查询一个实例ID所有记录
        /// </summary>
        public List<RoadFlow.Data.Model.WorkFlowData> GetAll(Guid instanceID)
        {
            string sql = "SELECT * FROM WorkFlowData WHERE InstanceID=:InstanceID";
            OracleParameter[] parameters = new OracleParameter[]{
				new OracleParameter(":InstanceID", OracleDbType.Varchar2){ Value = instanceID }
			};
            OracleDataReader dataReader = dbHelper.GetDataReader(sql, parameters);
            List<RoadFlow.Data.Model.WorkFlowData> List = DataReaderToList(dataReader);
            dataReader.Close();
            return List;
        }
    }
}