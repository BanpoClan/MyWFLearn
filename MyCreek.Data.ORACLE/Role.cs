using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Oracle.ManagedDataAccess.Client;

namespace MyCreek.Data.ORACLE
{
    public class Role : MyCreek.Data.Interface.IRole
    {
        private DBHelper dbHelper = new DBHelper();
        /// <summary>
        /// 构造函数
        /// </summary>
        public Role()
        {
        }
        /// <summary>
        /// 添加记录
        /// </summary>
        /// <param name="model">MyCreek.Data.Model.Role实体类</param>
        /// <returns>操作所影响的行数</returns>
        public int Add(MyCreek.Data.Model.Role model)
        {
            string sql = @"INSERT INTO Role
				(ID,Name,UseMember,Note) 
				VALUES(:ID,:Name,:UseMember,:Note)";
            OracleParameter[] parameters = new OracleParameter[]{
				new OracleParameter(":ID", OracleDbType.Varchar2, 40){ Value = model.ID },
				new OracleParameter(":Name", OracleDbType.NVarchar2, 400){ Value = model.Name },
				model.UseMember == null ? new OracleParameter(":UseMember", OracleDbType.Clob) { Value = DBNull.Value } : new OracleParameter(":UseMember", OracleDbType.Clob) { Value = model.UseMember },
				model.Note == null ? new OracleParameter(":Note", OracleDbType.NVarchar2) { Value = DBNull.Value } : new OracleParameter(":Note", OracleDbType.NVarchar2) { Value = model.Note }
			};
            return dbHelper.Execute(sql, parameters);
        }
        /// <summary>
        /// 更新记录
        /// </summary>
        /// <param name="model">MyCreek.Data.Model.Role实体类</param>
        public int Update(MyCreek.Data.Model.Role model)
        {
            string sql = @"UPDATE Role SET 
				Name=:Name,UseMember=:UseMember,Note=:Note
				WHERE ID=:ID";
            OracleParameter[] parameters = new OracleParameter[]{
				new OracleParameter(":Name", OracleDbType.NVarchar2, 400){ Value = model.Name },
				model.UseMember == null ? new OracleParameter(":UseMember", OracleDbType.Clob) { Value = DBNull.Value } : new OracleParameter(":UseMember", OracleDbType.Clob) { Value = model.UseMember },
				model.Note == null ? new OracleParameter(":Note", OracleDbType.NVarchar2) { Value = DBNull.Value } : new OracleParameter(":Note", OracleDbType.NVarchar2) { Value = model.Note },
				new OracleParameter(":ID", OracleDbType.Varchar2, 40){ Value = model.ID }
			};
            return dbHelper.Execute(sql, parameters);
        }
        /// <summary>
        /// 删除记录
        /// </summary>
        public int Delete(Guid id)
        {
            string sql = "DELETE FROM Role WHERE ID=:ID";
            OracleParameter[] parameters = new OracleParameter[]{
				new OracleParameter(":ID", OracleDbType.Varchar2){ Value = id }
			};
            return dbHelper.Execute(sql, parameters);
        }
        /// <summary>
        /// 将DataRedar转换为List
        /// </summary>
        private List<MyCreek.Data.Model.Role> DataReaderToList(OracleDataReader dataReader)
        {
            List<MyCreek.Data.Model.Role> List = new List<MyCreek.Data.Model.Role>();
            MyCreek.Data.Model.Role model = null;
            while (dataReader.Read())
            {
                model = new MyCreek.Data.Model.Role();
                model.ID = dataReader.GetString(0).ToGuid();
                model.Name = dataReader.GetString(1);
                if (!dataReader.IsDBNull(2))
                    model.UseMember = dataReader.GetString(2);
                if (!dataReader.IsDBNull(3))
                    model.Note = dataReader.GetString(3);
                List.Add(model);
            }
            return List;
        }
        /// <summary>
        /// 查询所有记录
        /// </summary>
        public List<MyCreek.Data.Model.Role> GetAll()
        {
            string sql = "SELECT * FROM Role";
            OracleDataReader dataReader = dbHelper.GetDataReader(sql);
            List<MyCreek.Data.Model.Role> List = DataReaderToList(dataReader);
            dataReader.Close();
            return List;
        }
        /// <summary>
        /// 查询记录数
        /// </summary>
        public long GetCount()
        {
            string sql = "SELECT COUNT(*) FROM Role";
            long count;
            return long.TryParse(dbHelper.GetFieldValue(sql), out count) ? count : 0;
        }
        /// <summary>
        /// 根据主键查询一条记录
        /// </summary>
        public MyCreek.Data.Model.Role Get(Guid id)
        {
            string sql = "SELECT * FROM Role WHERE ID=:ID";
            OracleParameter[] parameters = new OracleParameter[]{
				new OracleParameter(":ID", OracleDbType.Varchar2){ Value = id }
			};
            OracleDataReader dataReader = dbHelper.GetDataReader(sql, parameters);
            List<MyCreek.Data.Model.Role> List = DataReaderToList(dataReader);
            dataReader.Close();
            return List.Count > 0 ? List[0] : null;
        }
    }
}