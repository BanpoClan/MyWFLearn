using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Oracle.ManagedDataAccess.Client;

namespace MyCreek.Data.ORACLE
{
    public class Users : MyCreek.Data.Interface.IUsers
    {
        private DBHelper dbHelper = new DBHelper();
        /// <summary>
        /// 构造函数
        /// </summary>
        public Users()
        {
        }
        /// <summary>
        /// 添加记录
        /// </summary>
        /// <param name="model">MyCreek.Data.Model.Users实体类</param>
        /// <returns>操作所影响的行数</returns>
        public int Add(MyCreek.Data.Model.Users model)
        {
            string sql = @"INSERT INTO Users
				(ID,Name,Account,Password,Status,Sort,Note) 
				VALUES(:ID,:Name,:Account,:Password,:Status,:Sort,:Note)";
            OracleParameter[] parameters = new OracleParameter[]{
				new OracleParameter(":ID", OracleDbType.Varchar2, 40){ Value = model.ID },
				new OracleParameter(":Name", OracleDbType.NVarchar2, 100){ Value = model.Name },
				new OracleParameter(":Account", OracleDbType.Varchar2, 255){ Value = model.Account },
				new OracleParameter(":Password", OracleDbType.Varchar2, 500){ Value = model.Password },
				new OracleParameter(":Status", OracleDbType.Int32){ Value = model.Status },
				new OracleParameter(":Sort", OracleDbType.Int32){ Value = model.Sort },
				model.Note == null ? new OracleParameter(":Note", OracleDbType.NVarchar2) { Value = DBNull.Value } : new OracleParameter(":Note", OracleDbType.NVarchar2) { Value = model.Note }
			};
            return dbHelper.Execute(sql, parameters);
        }
        /// <summary>
        /// 更新记录
        /// </summary>
        /// <param name="model">MyCreek.Data.Model.Users实体类</param>
        public int Update(MyCreek.Data.Model.Users model)
        {
            string sql = @"UPDATE Users SET 
				Name=:Name,Account=:Account,Password=:Password,Status=:Status,Sort=:Sort,Note=:Note
				WHERE ID=:ID";
            OracleParameter[] parameters = new OracleParameter[]{
				new OracleParameter(":Name", OracleDbType.NVarchar2, 100){ Value = model.Name },
				new OracleParameter(":Account", OracleDbType.Varchar2, 255){ Value = model.Account },
				new OracleParameter(":Password", OracleDbType.Varchar2, 500){ Value = model.Password },
				new OracleParameter(":Status", OracleDbType.Int32){ Value = model.Status },
				new OracleParameter(":Sort", OracleDbType.Int32){ Value = model.Sort },
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
            string sql = "DELETE FROM Users WHERE ID=:ID";
            OracleParameter[] parameters = new OracleParameter[]{
				new OracleParameter(":ID", OracleDbType.Varchar2){ Value = id }
			};
            return dbHelper.Execute(sql, parameters);
        }
        /// <summary>
        /// 将DataRedar转换为List
        /// </summary>
        private List<MyCreek.Data.Model.Users> DataReaderToList(OracleDataReader dataReader)
        {
            List<MyCreek.Data.Model.Users> List = new List<MyCreek.Data.Model.Users>();
            MyCreek.Data.Model.Users model = null;
            while (dataReader.Read())
            {
                model = new MyCreek.Data.Model.Users();
                model.ID = dataReader.GetString(0).ToGuid();
                model.Name = dataReader.GetString(1);
                model.Account = dataReader.GetString(2);
                model.Password = dataReader.GetString(3);
                model.Status = dataReader.GetInt32(4);
                model.Sort = dataReader.GetInt32(5);
                if (!dataReader.IsDBNull(6))
                    model.Note = dataReader.GetString(6);
                List.Add(model);
            }
            return List;
        }
        /// <summary>
        /// 查询所有记录
        /// </summary>
        public List<MyCreek.Data.Model.Users> GetAll()
        {
            string sql = "SELECT * FROM Users";
            OracleDataReader dataReader = dbHelper.GetDataReader(sql);
            List<MyCreek.Data.Model.Users> List = DataReaderToList(dataReader);
            dataReader.Close();
            return List;
        }
        /// <summary>
        /// 查询记录数
        /// </summary>
        public long GetCount()
        {
            string sql = "SELECT COUNT(*) FROM Users";
            long count;
            return long.TryParse(dbHelper.GetFieldValue(sql), out count) ? count : 0;
        }
        /// <summary>
        /// 根据主键查询一条记录
        /// </summary>
        public MyCreek.Data.Model.Users Get(Guid id)
        {
            string sql = "SELECT * FROM Users WHERE ID=:ID";
            OracleParameter[] parameters = new OracleParameter[]{
				new OracleParameter(":ID", OracleDbType.Varchar2){ Value = id }
			};
            OracleDataReader dataReader = dbHelper.GetDataReader(sql, parameters);
            List<MyCreek.Data.Model.Users> List = DataReaderToList(dataReader);
            dataReader.Close();
            return List.Count > 0 ? List[0] : null;
        }

        /// <summary>
        /// 根据帐号查询一条记录
        /// </summary>
        public MyCreek.Data.Model.Users GetByAccount(string account)
        {
            string sql = "SELECT * FROM Users WHERE Account=:Account";
            OracleParameter[] parameters = new OracleParameter[]{
				new OracleParameter(":Account", OracleDbType.Varchar2, 255){ Value = account }
			};
            OracleDataReader dataReader = dbHelper.GetDataReader(sql, parameters);
            List<MyCreek.Data.Model.Users> List = DataReaderToList(dataReader);
            dataReader.Close();
            return List.Count > 0 ? List[0] : null;
        }

        /// <summary>
        /// 查询一个岗位下所有人员
        /// </summary>
        /// <param name="organizeID"></param>
        /// <returns></returns>
        public List<Model.Users> GetAllByOrganizeID(Guid organizeID)
        {
            string sql = "SELECT * FROM Users WHERE ID in(SELECT UserID FROM UsersRelation WHERE OrganizeID=:OrganizeID) ORDER BY Sort";
            OracleParameter[] parameters = new OracleParameter[]{
				new OracleParameter(":OrganizeID", OracleDbType.Varchar2){ Value = organizeID }
			};
            OracleDataReader dataReader = dbHelper.GetDataReader(sql, parameters);
            List<MyCreek.Data.Model.Users> List = DataReaderToList(dataReader);
            dataReader.Close();
            return List;
        }
        /// <summary>
        /// 查询一组岗位下所有人员
        /// </summary>
        /// <param name="organizeID"></param>
        /// <returns></returns>
        public List<Model.Users> GetAllByOrganizeIDArray(Guid[] organizeIDArray)
        {
            if (organizeIDArray == null || organizeIDArray.Length == 0)
            {
                return new List<Model.Users>();
            }
            string sql = "SELECT * FROM Users WHERE ID in(SELECT UserID FROM UsersRelation WHERE OrganizeID in(" + MyCreek.Utility.Tools.GetSqlInString(organizeIDArray) + ")) ORDER BY Sort";
            OracleDataReader dataReader = dbHelper.GetDataReader(sql);
            List<MyCreek.Data.Model.Users> List = DataReaderToList(dataReader);
            dataReader.Close();
            return List;
        }
        /// <summary>
        /// 检查帐号是否重复
        /// </summary>
        /// <param name="account">帐号</param>
        /// <param name="userID">人员ID(此人员除外)</param>
        /// <returns></returns>
        public bool HasAccount(string account, string userID = "")
        {
            string sql = "SELECT ID FROM Users WHERE Account=:Account";
            List<OracleParameter> plist = new List<OracleParameter>();
            plist.Add(new OracleParameter(":Account", OracleDbType.Varchar2) { Value = account });
            if (userID.IsGuid())
            {
                sql += " and ID<>:ID";
                plist.Add(new OracleParameter(":ID", OracleDbType.Varchar2) { Value = userID.ToGuid() });
            }
            OracleDataReader dataReader = dbHelper.GetDataReader(sql, plist.ToArray());
            bool flag = dataReader.HasRows;
            dataReader.Close();
            return flag;
        }
        /// <summary>
        /// 修改用户密码
        /// </summary>
        /// <param name="password"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        public bool UpdatePassword(string password, Guid userID)
        {
            string sql = "UPDATE Users SET Password=:Password WHERE ID=:ID";
            OracleParameter[] parameters = new OracleParameter[]{
				new OracleParameter(":Password", OracleDbType.Varchar2){ Value = password },
                new OracleParameter(":ID", OracleDbType.Varchar2){ Value = userID }
			};
            return dbHelper.Execute(sql, parameters) == 1;
        }

        /// <summary>
        /// 更新排序
        /// </summary>
        public int UpdateSort(Guid userID, int sort)
        {
            string sql = "UPDATE Users SET Sort=:Sort WHERE ID=:ID";
            OracleParameter[] parameters = new OracleParameter[]{
				new OracleParameter(":Sort", OracleDbType.Int32){ Value = sort },
                new OracleParameter(":ID", OracleDbType.Varchar2){ Value = userID }
			};
            return dbHelper.Execute(sql, parameters);
        }
    }
}