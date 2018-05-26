using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Oracle.ManagedDataAccess.Client;

namespace MyCreek.Data.ORACLE
{
    public class UsersRole : MyCreek.Data.Interface.IUsersRole
    {
        private DBHelper dbHelper = new DBHelper();
        /// <summary>
        /// 构造函数
        /// </summary>
        public UsersRole()
        {
        }
        /// <summary>
        /// 添加记录
        /// </summary>
        /// <param name="model">MyCreek.Data.Model.UsersRole实体类</param>
        /// <returns>操作所影响的行数</returns>
        public int Add(MyCreek.Data.Model.UsersRole model)
        {
            string sql = @"INSERT INTO UsersRole
				(MemberID,RoleID,IsDefault) 
				VALUES(:MemberID,:RoleID,:IsDefault)";
            OracleParameter[] parameters = new OracleParameter[]{
				new OracleParameter(":MemberID", OracleDbType.Varchar2, 40){ Value = model.MemberID },
				new OracleParameter(":RoleID", OracleDbType.Varchar2, 40){ Value = model.RoleID },
				new OracleParameter(":IsDefault", OracleDbType.Int32){ Value = model.IsDefault }
			};
            return dbHelper.Execute(sql, parameters);
        }
        /// <summary>
        /// 更新记录
        /// </summary>
        /// <param name="model">MyCreek.Data.Model.UsersRole实体类</param>
        public int Update(MyCreek.Data.Model.UsersRole model)
        {
            string sql = @"UPDATE UsersRole SET 
				IsDefault=:IsDefault
				WHERE MemberID=:MemberID and RoleID=:RoleID";
            OracleParameter[] parameters = new OracleParameter[]{
				new OracleParameter(":IsDefault", OracleDbType.Int32){ Value = model.IsDefault },
				new OracleParameter(":MemberID", OracleDbType.Varchar2, 40){ Value = model.MemberID },
				new OracleParameter(":RoleID", OracleDbType.Varchar2, 40){ Value = model.RoleID }
			};
            return dbHelper.Execute(sql, parameters);
        }
        /// <summary>
        /// 删除记录
        /// </summary>
        public int Delete(Guid memberid, Guid roleid)
        {
            string sql = "DELETE FROM UsersRole WHERE MemberID=:MemberID AND RoleID=:RoleID";
            OracleParameter[] parameters = new OracleParameter[]{
				new OracleParameter(":MemberID", OracleDbType.Varchar2){ Value = memberid },
				new OracleParameter(":RoleID", OracleDbType.Varchar2){ Value = roleid }
			};
            return dbHelper.Execute(sql, parameters);
        }
        /// <summary>
        /// 将DataRedar转换为List
        /// </summary>
        private List<MyCreek.Data.Model.UsersRole> DataReaderToList(OracleDataReader dataReader)
        {
            List<MyCreek.Data.Model.UsersRole> List = new List<MyCreek.Data.Model.UsersRole>();
            MyCreek.Data.Model.UsersRole model = null;
            while (dataReader.Read())
            {
                model = new MyCreek.Data.Model.UsersRole();
                model.MemberID = dataReader.GetString(0).ToGuid();
                model.RoleID = dataReader.GetString(1).ToGuid();
                model.IsDefault = 1 == dataReader.GetInt32(2);
                List.Add(model);
            }
            return List;
        }
        /// <summary>
        /// 查询所有记录
        /// </summary>
        public List<MyCreek.Data.Model.UsersRole> GetAll()
        {
            string sql = "SELECT * FROM UsersRole";
            OracleDataReader dataReader = dbHelper.GetDataReader(sql);
            List<MyCreek.Data.Model.UsersRole> List = DataReaderToList(dataReader);
            dataReader.Close();
            return List;
        }
        /// <summary>
        /// 查询记录数
        /// </summary>
        public long GetCount()
        {
            string sql = "SELECT COUNT(*) FROM UsersRole";
            long count;
            return long.TryParse(dbHelper.GetFieldValue(sql), out count) ? count : 0;
        }
        /// <summary>
        /// 根据主键查询一条记录
        /// </summary>
        public MyCreek.Data.Model.UsersRole Get(Guid memberid, Guid roleid)
        {
            string sql = "SELECT * FROM UsersRole WHERE MemberID=:MemberID AND RoleID=:RoleID";
            OracleParameter[] parameters = new OracleParameter[]{
				new OracleParameter(":MemberID", OracleDbType.Varchar2){ Value = memberid },
				new OracleParameter(":RoleID", OracleDbType.Varchar2){ Value = roleid }
			};
            OracleDataReader dataReader = dbHelper.GetDataReader(sql, parameters);
            List<MyCreek.Data.Model.UsersRole> List = DataReaderToList(dataReader);
            dataReader.Close();
            return List.Count > 0 ? List[0] : null;
        }

        /// <summary>
        /// 删除一个机构所有记录
        /// </summary>
        public int DeleteByUserID(Guid userID)
        {
            string sql = "DELETE FROM UsersRole WHERE MemberID=:MemberID";
            OracleParameter[] parameters = new OracleParameter[]{
				new OracleParameter(":MemberID", OracleDbType.Varchar2){ Value = userID }
			};
            return dbHelper.Execute(sql, parameters);
        }
        /// <summary>
        /// 删除一个角色所有记录
        /// </summary>
        public int DeleteByRoleID(Guid roleid)
        {
            string sql = "DELETE FROM UsersRole WHERE RoleID=:RoleID";
            OracleParameter[] parameters = new OracleParameter[]{
				new OracleParameter(":RoleID", OracleDbType.Varchar2){ Value = roleid }
			};
            return dbHelper.Execute(sql, parameters);
        }
        /// <summary>
        /// 根据一组机构ID查询记录
        /// </summary>
        public List<MyCreek.Data.Model.UsersRole> GetByUserIDArray(Guid[] userIDArray)
        {
            string sql = "SELECT * FROM UsersRole WHERE MemberID IN(" + MyCreek.Utility.Tools.GetSqlInString(userIDArray) + ")";
            OracleDataReader dataReader = dbHelper.GetDataReader(sql);
            List<MyCreek.Data.Model.UsersRole> List = DataReaderToList(dataReader);
            dataReader.Close();
            return List;
        }

        /// <summary>
        /// 根据人员ID查询记录
        /// </summary>
        public List<MyCreek.Data.Model.UsersRole> GetByUserID(Guid userID)
        {
            string sql = "SELECT * FROM UsersRole WHERE MemberID=:MemberID";
            OracleParameter[] parameters = new OracleParameter[]{
				new OracleParameter(":MemberID", OracleDbType.Varchar2){ Value = userID }
			};
            OracleDataReader dataReader = dbHelper.GetDataReader(sql, parameters);
            List<MyCreek.Data.Model.UsersRole> List = DataReaderToList(dataReader);
            dataReader.Close();
            return List;
        }
    }
}