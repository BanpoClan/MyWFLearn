using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Oracle.ManagedDataAccess.Client;

namespace RoadFlow.Data.ORACLE
{
    public class UsersInfo : RoadFlow.Data.Interface.IUsersInfo
    {
        private DBHelper dbHelper = new DBHelper();
        /// <summary>
        /// 构造函数
        /// </summary>
        public UsersInfo()
        {
        }
        /// <summary>
        /// 添加记录
        /// </summary>
        /// <param name="model">RoadFlow.Data.Model.UsersInfo实体类</param>
        /// <returns>操作所影响的行数</returns>
        public int Add(RoadFlow.Data.Model.UsersInfo model)
        {
            string sql = @"INSERT INTO UsersInfo
				(UserID,Officer,Tel,Fax,Address,Email,QQ,MSN,Note) 
				VALUES(:UserID,:Officer,:Tel,:Fax,:Address,:Email,:QQ,:MSN,:Note)";
            OracleParameter[] parameters = new OracleParameter[]{
				new OracleParameter(":UserID", OracleDbType.Varchar2, 40){ Value = model.UserID },
				model.Officer == null ? new OracleParameter(":Officer", OracleDbType.NVarchar2, 1000) { Value = DBNull.Value } : new OracleParameter(":Officer", OracleDbType.NVarchar2, 1000) { Value = model.Officer },
				model.Tel == null ? new OracleParameter(":Tel", OracleDbType.Varchar2, 500) { Value = DBNull.Value } : new OracleParameter(":Tel", OracleDbType.Varchar2, 500) { Value = model.Tel },
				model.Fax == null ? new OracleParameter(":Fax", OracleDbType.Varchar2, 500) { Value = DBNull.Value } : new OracleParameter(":Fax", OracleDbType.Varchar2, 500) { Value = model.Fax },
				model.Address == null ? new OracleParameter(":Address", OracleDbType.Varchar2, 500) { Value = DBNull.Value } : new OracleParameter(":Address", OracleDbType.Varchar2, 500) { Value = model.Address },
				model.Email == null ? new OracleParameter(":Email", OracleDbType.Varchar2, 50) { Value = DBNull.Value } : new OracleParameter(":Email", OracleDbType.Varchar2, 50) { Value = model.Email },
				model.QQ == null ? new OracleParameter(":QQ", OracleDbType.Varchar2, 50) { Value = DBNull.Value } : new OracleParameter(":QQ", OracleDbType.Varchar2, 50) { Value = model.QQ },
				model.MSN == null ? new OracleParameter(":MSN", OracleDbType.Varchar2, 50) { Value = DBNull.Value } : new OracleParameter(":MSN", OracleDbType.Varchar2, 50) { Value = model.MSN },
				model.Note == null ? new OracleParameter(":Note", OracleDbType.NVarchar2) { Value = DBNull.Value } : new OracleParameter(":Note", OracleDbType.NVarchar2) { Value = model.Note }
			};
            return dbHelper.Execute(sql, parameters);
        }
        /// <summary>
        /// 更新记录
        /// </summary>
        /// <param name="model">RoadFlow.Data.Model.UsersInfo实体类</param>
        public int Update(RoadFlow.Data.Model.UsersInfo model)
        {
            string sql = @"UPDATE UsersInfo SET 
				Officer=:Officer,Tel=:Tel,Fax=:Fax,Address=:Address,Email=:Email,QQ=:QQ,MSN=:MSN,Note=:Note
				WHERE UserID=:UserID";
            OracleParameter[] parameters = new OracleParameter[]{
				model.Officer == null ? new OracleParameter(":Officer", OracleDbType.NVarchar2, 1000) { Value = DBNull.Value } : new OracleParameter(":Officer", OracleDbType.NVarchar2, 1000) { Value = model.Officer },
				model.Tel == null ? new OracleParameter(":Tel", OracleDbType.Varchar2, 500) { Value = DBNull.Value } : new OracleParameter(":Tel", OracleDbType.Varchar2, 500) { Value = model.Tel },
				model.Fax == null ? new OracleParameter(":Fax", OracleDbType.Varchar2, 500) { Value = DBNull.Value } : new OracleParameter(":Fax", OracleDbType.Varchar2, 500) { Value = model.Fax },
				model.Address == null ? new OracleParameter(":Address", OracleDbType.Varchar2, 500) { Value = DBNull.Value } : new OracleParameter(":Address", OracleDbType.Varchar2, 500) { Value = model.Address },
				model.Email == null ? new OracleParameter(":Email", OracleDbType.Varchar2, 50) { Value = DBNull.Value } : new OracleParameter(":Email", OracleDbType.Varchar2, 50) { Value = model.Email },
				model.QQ == null ? new OracleParameter(":QQ", OracleDbType.Varchar2, 50) { Value = DBNull.Value } : new OracleParameter(":QQ", OracleDbType.Varchar2, 50) { Value = model.QQ },
				model.MSN == null ? new OracleParameter(":MSN", OracleDbType.Varchar2, 50) { Value = DBNull.Value } : new OracleParameter(":MSN", OracleDbType.Varchar2, 50) { Value = model.MSN },
				model.Note == null ? new OracleParameter(":Note", OracleDbType.NVarchar2) { Value = DBNull.Value } : new OracleParameter(":Note", OracleDbType.NVarchar2) { Value = model.Note },
				new OracleParameter(":UserID", OracleDbType.Varchar2, 40){ Value = model.UserID }
			};
            return dbHelper.Execute(sql, parameters);
        }
        /// <summary>
        /// 删除记录
        /// </summary>
        public int Delete(Guid userid)
        {
            string sql = "DELETE FROM UsersInfo WHERE UserID=:UserID";
            OracleParameter[] parameters = new OracleParameter[]{
				new OracleParameter(":UserID", OracleDbType.Varchar2){ Value = userid }
			};
            return dbHelper.Execute(sql, parameters);
        }
        /// <summary>
        /// 将DataRedar转换为List
        /// </summary>
        private List<RoadFlow.Data.Model.UsersInfo> DataReaderToList(OracleDataReader dataReader)
        {
            List<RoadFlow.Data.Model.UsersInfo> List = new List<RoadFlow.Data.Model.UsersInfo>();
            RoadFlow.Data.Model.UsersInfo model = null;
            while (dataReader.Read())
            {
                model = new RoadFlow.Data.Model.UsersInfo();
                model.UserID = dataReader.GetString(0).ToGuid();
                if (!dataReader.IsDBNull(1))
                    model.Officer = dataReader.GetString(1);
                if (!dataReader.IsDBNull(2))
                    model.Tel = dataReader.GetString(2);
                if (!dataReader.IsDBNull(3))
                    model.Fax = dataReader.GetString(3);
                if (!dataReader.IsDBNull(4))
                    model.Address = dataReader.GetString(4);
                if (!dataReader.IsDBNull(5))
                    model.Email = dataReader.GetString(5);
                if (!dataReader.IsDBNull(6))
                    model.QQ = dataReader.GetString(6);
                if (!dataReader.IsDBNull(7))
                    model.MSN = dataReader.GetString(7);
                if (!dataReader.IsDBNull(8))
                    model.Note = dataReader.GetString(8);
                List.Add(model);
            }
            return List;
        }
        /// <summary>
        /// 查询所有记录
        /// </summary>
        public List<RoadFlow.Data.Model.UsersInfo> GetAll()
        {
            string sql = "SELECT * FROM UsersInfo";
            OracleDataReader dataReader = dbHelper.GetDataReader(sql);
            List<RoadFlow.Data.Model.UsersInfo> List = DataReaderToList(dataReader);
            dataReader.Close();
            return List;
        }
        /// <summary>
        /// 查询记录数
        /// </summary>
        public long GetCount()
        {
            string sql = "SELECT COUNT(*) FROM UsersInfo";
            long count;
            return long.TryParse(dbHelper.GetFieldValue(sql), out count) ? count : 0;
        }
        /// <summary>
        /// 根据主键查询一条记录
        /// </summary>
        public RoadFlow.Data.Model.UsersInfo Get(Guid userid)
        {
            string sql = "SELECT * FROM UsersInfo WHERE UserID=:UserID";
            OracleParameter[] parameters = new OracleParameter[]{
				new OracleParameter(":UserID", OracleDbType.Varchar2){ Value = userid }
			};
            OracleDataReader dataReader = dbHelper.GetDataReader(sql, parameters);
            List<RoadFlow.Data.Model.UsersInfo> List = DataReaderToList(dataReader);
            dataReader.Close();
            return List.Count > 0 ? List[0] : null;
        }
    }
}