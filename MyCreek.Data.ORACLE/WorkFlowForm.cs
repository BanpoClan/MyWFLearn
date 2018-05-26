using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Oracle.ManagedDataAccess.Client;

namespace MyCreek.Data.ORACLE
{
    public class WorkFlowForm : MyCreek.Data.Interface.IWorkFlowForm
    {
        private DBHelper dbHelper = new DBHelper();
        /// <summary>
        /// 构造函数
        /// </summary>
        public WorkFlowForm()
        {
        }
        /// <summary>
        /// 添加记录
        /// </summary>
        /// <param name="model">MyCreek.Data.Model.WorkFlowForm实体类</param>
        /// <returns>操作所影响的行数</returns>
        public int Add(MyCreek.Data.Model.WorkFlowForm model)
        {
            string sql = @"INSERT INTO WorkFlowForm
				(ID,Name,Type,CreateUserID,CreateUserName,CreateTime,LastModifyTime,Html,SubTableJson,EventsJson,Attribute,Status) 
				VALUES(:ID,:Name,:Type,:CreateUserID,:CreateUserName,:CreateTime,:LastModifyTime,:Html,:SubTableJson,:EventsJson,:Attribute,:Status)";
            OracleParameter[] parameters = new OracleParameter[]{
				new OracleParameter(":ID", OracleDbType.Varchar2, 40){ Value = model.ID },
				new OracleParameter(":Name", OracleDbType.NVarchar2, 1000){ Value = model.Name },
				new OracleParameter(":Type", OracleDbType.Varchar2, 40){ Value = model.Type },
				new OracleParameter(":CreateUserID", OracleDbType.Varchar2, 40){ Value = model.CreateUserID },
				new OracleParameter(":CreateUserName", OracleDbType.NVarchar2, 100){ Value = model.CreateUserName },
				new OracleParameter(":CreateTime", OracleDbType.Date, 8){ Value = model.CreateTime },
				new OracleParameter(":LastModifyTime", OracleDbType.Date, 8){ Value = model.LastModifyTime },
				model.Html == null ? new OracleParameter(":Html", OracleDbType.Clob) { Value = DBNull.Value } : new OracleParameter(":Html", OracleDbType.Clob) { Value = model.Html },
				model.SubTableJson == null ? new OracleParameter(":SubTableJson", OracleDbType.Clob) { Value = DBNull.Value } : new OracleParameter(":SubTableJson", OracleDbType.Clob) { Value = model.SubTableJson },
				model.EventsJson == null ? new OracleParameter(":EventsJson", OracleDbType.Clob) { Value = DBNull.Value } : new OracleParameter(":EventsJson", OracleDbType.Clob) { Value = model.EventsJson },
				model.Attribute == null ? new OracleParameter(":Attribute", OracleDbType.Clob) { Value = DBNull.Value } : new OracleParameter(":Attribute", OracleDbType.Clob) { Value = model.Attribute },
				new OracleParameter(":Status", OracleDbType.Int32){ Value = model.Status }
			};
            return dbHelper.Execute(sql, parameters);
        }
        /// <summary>
        /// 更新记录
        /// </summary>
        /// <param name="model">MyCreek.Data.Model.WorkFlowForm实体类</param>
        public int Update(MyCreek.Data.Model.WorkFlowForm model)
        {
            string sql = @"UPDATE WorkFlowForm SET 
				Name=:Name,Type=:Type,CreateUserID=:CreateUserID,CreateUserName=:CreateUserName,CreateTime=:CreateTime,LastModifyTime=:LastModifyTime,Html=:Html,SubTableJson=:SubTableJson,EventsJson=:EventsJson,Attribute=:Attribute,Status=:Status
				WHERE ID=:ID";
            OracleParameter[] parameters = new OracleParameter[]{
				new OracleParameter(":Name", OracleDbType.NVarchar2, 1000){ Value = model.Name },
				new OracleParameter(":Type", OracleDbType.Varchar2, 40){ Value = model.Type },
				new OracleParameter(":CreateUserID", OracleDbType.Varchar2, 40){ Value = model.CreateUserID },
				new OracleParameter(":CreateUserName", OracleDbType.NVarchar2, 100){ Value = model.CreateUserName },
				new OracleParameter(":CreateTime", OracleDbType.Date, 8){ Value = model.CreateTime },
				new OracleParameter(":LastModifyTime", OracleDbType.Date, 8){ Value = model.LastModifyTime },
				model.Html == null ? new OracleParameter(":Html", OracleDbType.Clob) { Value = DBNull.Value } : new OracleParameter(":Html", OracleDbType.Clob) { Value = model.Html },
				model.SubTableJson == null ? new OracleParameter(":SubTableJson", OracleDbType.Clob) { Value = DBNull.Value } : new OracleParameter(":SubTableJson", OracleDbType.Clob) { Value = model.SubTableJson },
				model.EventsJson == null ? new OracleParameter(":EventsJson", OracleDbType.Clob) { Value = DBNull.Value } : new OracleParameter(":EventsJson", OracleDbType.Clob) { Value = model.EventsJson },
				model.Attribute == null ? new OracleParameter(":Attribute", OracleDbType.Clob) { Value = DBNull.Value } : new OracleParameter(":Attribute", OracleDbType.Clob) { Value = model.Attribute },
				new OracleParameter(":Status", OracleDbType.Int32){ Value = model.Status },
				new OracleParameter(":ID", OracleDbType.Varchar2, 40){ Value = model.ID }
			};
            return dbHelper.Execute(sql, parameters);
        }
        /// <summary>
        /// 删除记录
        /// </summary>
        public int Delete(Guid id)
        {
            string sql = "DELETE FROM WorkFlowForm WHERE ID=:ID";
            OracleParameter[] parameters = new OracleParameter[]{
				new OracleParameter(":ID", OracleDbType.Varchar2){ Value = id }
			};
            return dbHelper.Execute(sql, parameters);
        }
        /// <summary>
        /// 将DataRedar转换为List
        /// </summary>
        private List<MyCreek.Data.Model.WorkFlowForm> DataReaderToList(OracleDataReader dataReader)
        {
            List<MyCreek.Data.Model.WorkFlowForm> List = new List<MyCreek.Data.Model.WorkFlowForm>();
            MyCreek.Data.Model.WorkFlowForm model = null;
            while (dataReader.Read())
            {
                model = new MyCreek.Data.Model.WorkFlowForm();
                model.ID = dataReader.GetString(0).ToGuid();
                model.Name = dataReader.GetString(1);
                model.Type = dataReader.GetString(2).ToGuid();
                model.CreateUserID = dataReader.GetString(3).ToGuid();
                model.CreateUserName = dataReader.GetString(4);
                model.CreateTime = dataReader.GetDateTime(5);
                model.LastModifyTime = dataReader.GetDateTime(6);
                if (!dataReader.IsDBNull(7))
                    model.Html = dataReader.GetString(7);
                if (!dataReader.IsDBNull(8))
                    model.SubTableJson = dataReader.GetString(8);
                if (!dataReader.IsDBNull(9))
                    model.EventsJson = dataReader.GetString(9);
                if (!dataReader.IsDBNull(10))
                    model.Attribute = dataReader.GetString(10);
                model.Status = dataReader.GetInt32(11);
                List.Add(model);
            }
            return List;
        }
        /// <summary>
        /// 查询所有记录
        /// </summary>
        public List<MyCreek.Data.Model.WorkFlowForm> GetAll()
        {
            string sql = "SELECT * FROM WorkFlowForm";
            OracleDataReader dataReader = dbHelper.GetDataReader(sql);
            List<MyCreek.Data.Model.WorkFlowForm> List = DataReaderToList(dataReader);
            dataReader.Close();
            return List;
        }
        /// <summary>
        /// 查询记录数
        /// </summary>
        public long GetCount()
        {
            string sql = "SELECT COUNT(*) FROM WorkFlowForm";
            long count;
            return long.TryParse(dbHelper.GetFieldValue(sql), out count) ? count : 0;
        }
        /// <summary>
        /// 根据主键查询一条记录
        /// </summary>
        public MyCreek.Data.Model.WorkFlowForm Get(Guid id)
        {
            string sql = "SELECT * FROM WorkFlowForm WHERE ID=:ID";
            OracleParameter[] parameters = new OracleParameter[]{
				new OracleParameter(":ID", OracleDbType.Varchar2){ Value = id }
			};
            OracleDataReader dataReader = dbHelper.GetDataReader(sql, parameters);
            List<MyCreek.Data.Model.WorkFlowForm> List = DataReaderToList(dataReader);
            dataReader.Close();
            return List.Count > 0 ? List[0] : null;
        }



        /// <summary>
        /// 查询一个分类所有记录
        /// </summary>
        public List<MyCreek.Data.Model.WorkFlowForm> GetAllByType(string types)
        {
            string sql = "SELECT * FROM WorkFlowForm where Type IN(" + MyCreek.Utility.Tools.GetSqlInString(types) + ")";
            OracleDataReader dataReader = dbHelper.GetDataReader(sql);
            List<MyCreek.Data.Model.WorkFlowForm> List = DataReaderToList(dataReader);
            dataReader.Close();
            return List;
        }
    }
}