using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Oracle.ManagedDataAccess.Client;

namespace MyCreek.Data.ORACLE
{
    public class WorkFlow : MyCreek.Data.Interface.IWorkFlow
    {
        private DBHelper dbHelper = new DBHelper();
        /// <summary>
        /// 构造函数
        /// </summary>
        public WorkFlow()
        {
        }
        /// <summary>
        /// 添加记录
        /// </summary>
        /// <param name="model">MyCreek.Data.Model.WorkFlow实体类</param>
        /// <returns>操作所影响的行数</returns>
        public int Add(MyCreek.Data.Model.WorkFlow model)
        {
            string sql = @"INSERT INTO WorkFlow
				(ID,Name,Type,Manager,InstanceManager,CreateDate,CreateUserID,DesignJSON,InstallDate,InstallUserID,RunJSON,Status) 
				VALUES(:ID,:Name,:Type,:Manager,:InstanceManager,:CreateDate,:CreateUserID,:DesignJSON,:InstallDate,:InstallUserID,:RunJSON,:Status)";
            OracleParameter[] parameters = new OracleParameter[]{
				new OracleParameter(":ID", OracleDbType.Varchar2, 40){ Value = model.ID },
				new OracleParameter(":Name", OracleDbType.NVarchar2, 1000){ Value = model.Name },
				new OracleParameter(":Type", OracleDbType.Varchar2, 40){ Value = model.Type },
				new OracleParameter(":Manager", OracleDbType.Varchar2, 5000){ Value = model.Manager },
				new OracleParameter(":InstanceManager", OracleDbType.Varchar2, 5000){ Value = model.InstanceManager },
				new OracleParameter(":CreateDate", OracleDbType.Date, 8){ Value = model.CreateDate },
				new OracleParameter(":CreateUserID", OracleDbType.Varchar2, 40){ Value = model.CreateUserID },
				model.DesignJSON == null ? new OracleParameter(":DesignJSON", OracleDbType.Clob) { Value = DBNull.Value } : new OracleParameter(":DesignJSON", OracleDbType.Clob) { Value = model.DesignJSON },
				model.InstallDate == null ? new OracleParameter(":InstallDate", OracleDbType.Date, 8) { Value = DBNull.Value } : new OracleParameter(":InstallDate", OracleDbType.Date, 8) { Value = model.InstallDate },
				model.InstallUserID == null ? new OracleParameter(":InstallUserID", OracleDbType.Varchar2, 40) { Value = DBNull.Value } : new OracleParameter(":InstallUserID", OracleDbType.Varchar2, 40) { Value = model.InstallUserID },
				model.RunJSON == null ? new OracleParameter(":RunJSON", OracleDbType.Clob) { Value = DBNull.Value } : new OracleParameter(":RunJSON", OracleDbType.Clob) { Value = model.RunJSON },
				new OracleParameter(":Status", OracleDbType.Int32){ Value = model.Status }
			};
            return dbHelper.Execute(sql, parameters);
        }
        /// <summary>
        /// 更新记录
        /// </summary>
        /// <param name="model">MyCreek.Data.Model.WorkFlow实体类</param>
        public int Update(MyCreek.Data.Model.WorkFlow model)
        {
            string sql = @"UPDATE WorkFlow SET 
				Name=:Name,Type=:Type,Manager=:Manager,InstanceManager=:InstanceManager,CreateDate=:CreateDate,CreateUserID=:CreateUserID,DesignJSON=:DesignJSON,InstallDate=:InstallDate,InstallUserID=:InstallUserID,RunJSON=:RunJSON,Status=:Status
				WHERE ID=:ID";
            OracleParameter[] parameters = new OracleParameter[]{
				new OracleParameter(":Name", OracleDbType.NVarchar2, 1000){ Value = model.Name },
				new OracleParameter(":Type", OracleDbType.Varchar2, 40){ Value = model.Type },
				new OracleParameter(":Manager", OracleDbType.Varchar2, 5000){ Value = model.Manager },
				new OracleParameter(":InstanceManager", OracleDbType.Varchar2, 5000){ Value = model.InstanceManager },
				new OracleParameter(":CreateDate", OracleDbType.Date, 8){ Value = model.CreateDate },
				new OracleParameter(":CreateUserID", OracleDbType.Varchar2, 40){ Value = model.CreateUserID },
				model.DesignJSON == null ? new OracleParameter(":DesignJSON", OracleDbType.Clob) { Value = DBNull.Value } : new OracleParameter(":DesignJSON", OracleDbType.Clob) { Value = model.DesignJSON },
				model.InstallDate == null ? new OracleParameter(":InstallDate", OracleDbType.Date, 8) { Value = DBNull.Value } : new OracleParameter(":InstallDate", OracleDbType.Date, 8) { Value = model.InstallDate },
				model.InstallUserID == null ? new OracleParameter(":InstallUserID", OracleDbType.Varchar2, 40) { Value = DBNull.Value } : new OracleParameter(":InstallUserID", OracleDbType.Varchar2, 40) { Value = model.InstallUserID },
				model.RunJSON == null ? new OracleParameter(":RunJSON", OracleDbType.Clob) { Value = DBNull.Value } : new OracleParameter(":RunJSON", OracleDbType.Clob) { Value = model.RunJSON },
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
            string sql = "DELETE FROM WorkFlow WHERE ID=:ID";
            OracleParameter[] parameters = new OracleParameter[]{
				new OracleParameter(":ID", OracleDbType.Varchar2){ Value = id }
			};
            return dbHelper.Execute(sql, parameters);
        }
        /// <summary>
        /// 将DataRedar转换为List
        /// </summary>
        private List<MyCreek.Data.Model.WorkFlow> DataReaderToList(OracleDataReader dataReader)
        {
            List<MyCreek.Data.Model.WorkFlow> List = new List<MyCreek.Data.Model.WorkFlow>();
            MyCreek.Data.Model.WorkFlow model = null;
            while (dataReader.Read())
            {
                model = new MyCreek.Data.Model.WorkFlow();
                model.ID = dataReader.GetString(0).ToGuid();
                model.Name = dataReader.GetString(1);
                model.Type = dataReader.GetString(2).ToGuid();
                model.Manager = dataReader.GetString(3);
                model.InstanceManager = dataReader.GetString(4);
                model.CreateDate = dataReader.GetDateTime(5);
                model.CreateUserID = dataReader.GetString(6).ToGuid();
                if (!dataReader.IsDBNull(7))
                    model.DesignJSON = dataReader.GetString(7);
                if (!dataReader.IsDBNull(8))
                    model.InstallDate = dataReader.GetDateTime(8);
                if (!dataReader.IsDBNull(9))
                    model.InstallUserID = dataReader.GetString(9).ToGuid();
                if (!dataReader.IsDBNull(10))
                    model.RunJSON = dataReader.GetString(10);
                model.Status = dataReader.GetInt32(11);
                List.Add(model);
            }
            return List;
        }
        /// <summary>
        /// 查询所有记录
        /// </summary>
        public List<MyCreek.Data.Model.WorkFlow> GetAll()
        {
            string sql = "SELECT * FROM WorkFlow";
            OracleDataReader dataReader = dbHelper.GetDataReader(sql);
            List<MyCreek.Data.Model.WorkFlow> List = DataReaderToList(dataReader);
            dataReader.Close();
            return List;
        }
        /// <summary>
        /// 查询记录数
        /// </summary>
        public long GetCount()
        {
            string sql = "SELECT COUNT(*) FROM WorkFlow";
            long count;
            return long.TryParse(dbHelper.GetFieldValue(sql), out count) ? count : 0;
        }
        /// <summary>
        /// 根据主键查询一条记录
        /// </summary>
        public MyCreek.Data.Model.WorkFlow Get(Guid id)
        {
            string sql = "SELECT * FROM WorkFlow WHERE ID=:ID";
            OracleParameter[] parameters = new OracleParameter[]{
				new OracleParameter(":ID", OracleDbType.Varchar2){ Value = id }
			};
            OracleDataReader dataReader = dbHelper.GetDataReader(sql, parameters);
            List<MyCreek.Data.Model.WorkFlow> List = DataReaderToList(dataReader);
            dataReader.Close();
            return List.Count > 0 ? List[0] : null;
        }


        /// <summary>
        /// 查询所有类型
        /// </summary>
        public List<string> GetAllTypes()
        {
            string sql = "SELECT Type FROM WorkFlow GROUP BY Type";
            List<string> list = new List<string>();
            OracleDataReader dataReader = dbHelper.GetDataReader(sql);
            while (dataReader.Read())
            {
                list.Add(dataReader.GetString(0));
            }
            dataReader.Close();
            return list;
        }

        /// <summary>
        /// 查询所有ID和名称
        /// </summary>
        public Dictionary<Guid,string> GetAllIDAndName()
        {
            string sql = "SELECT ID,Name FROM WorkFlow WHERE Status<4 ORDER BY Name";
            Dictionary<Guid, string> dict = new Dictionary<Guid, string>();
            OracleDataReader dataReader = dbHelper.GetDataReader(sql);
            while (dataReader.Read())
            {
                dict.Add(dataReader.GetString(0).ToGuid(), dataReader.GetString(1));
            }
            dataReader.Close();
            return dict;
        }

        /// <summary>
        /// 查询所有记录
        /// </summary>
        public List<MyCreek.Data.Model.WorkFlow> GetByTypes(string typeString)
        {
            string sql = "SELECT * FROM WorkFlow where Type IN(" + MyCreek.Utility.Tools.GetSqlInString(typeString) + ")";
            OracleDataReader dataReader = dbHelper.GetDataReader(sql);
            List<MyCreek.Data.Model.WorkFlow> List = DataReaderToList(dataReader);
            dataReader.Close();
            return List;
        }
    }
}