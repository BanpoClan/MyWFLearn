using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Oracle.ManagedDataAccess.Client;

namespace MyCreek.Data.ORACLE
{
    public class UsersApp : MyCreek.Data.Interface.IUsersApp
    {
        private DBHelper dbHelper = new DBHelper();
        /// <summary>
        /// 构造函数
        /// </summary>
        public UsersApp()
        {
        }
        /// <summary>
        /// 添加记录
        /// </summary>
        /// <param name="model">MyCreek.Data.Model.UsersApp实体类</param>
        /// <returns>操作所影响的行数</returns>
        public int Add(MyCreek.Data.Model.UsersApp model)
        {
            string sql = @"INSERT INTO UsersApp
				(ID,UserID,ParentID,RoleID,AppID,Title,Params,Ico,Sort) 
				VALUES(:ID,:UserID,:ParentID,:RoleID,:AppID,:Title,:Params,:Ico,:Sort)";
            OracleParameter[] parameters = new OracleParameter[]{
				new OracleParameter(":ID", OracleDbType.Varchar2, 40){ Value = model.ID },
				new OracleParameter(":UserID", OracleDbType.Varchar2, 40){ Value = model.UserID },
				new OracleParameter(":ParentID", OracleDbType.Varchar2, 40){ Value = model.ParentID },
				new OracleParameter(":RoleID", OracleDbType.Varchar2, 40){ Value = model.RoleID },
				model.AppID == null ? new OracleParameter(":AppID", OracleDbType.Varchar2, 40) { Value = DBNull.Value } : new OracleParameter(":AppID", OracleDbType.Varchar2, 40) { Value = model.AppID },
				model.Title == null ? new OracleParameter(":Title", OracleDbType.NVarchar2, 400) { Value = DBNull.Value } : new OracleParameter(":Title", OracleDbType.NVarchar2, 400) { Value = model.Title },
				model.Params == null ? new OracleParameter(":Params", OracleDbType.Varchar2, 500) { Value = DBNull.Value } : new OracleParameter(":Params", OracleDbType.Varchar2, 500) { Value = model.Params },
				model.Ico == null ? new OracleParameter(":Ico", OracleDbType.Varchar2, 500) { Value = DBNull.Value } : new OracleParameter(":Ico", OracleDbType.Varchar2, 500) { Value = model.Ico },
				new OracleParameter(":Sort", OracleDbType.Int32){ Value = model.Sort }
			};
            return dbHelper.Execute(sql, parameters);
        }
        /// <summary>
        /// 更新记录
        /// </summary>
        /// <param name="model">MyCreek.Data.Model.UsersApp实体类</param>
        public int Update(MyCreek.Data.Model.UsersApp model)
        {
            string sql = @"UPDATE UsersApp SET 
				UserID=:UserID,ParentID=:ParentID,RoleID=:RoleID,AppID=:AppID,Title=:Title,Params=:Params,Ico=:Ico,Sort=:Sort
				WHERE ID=:ID";
            OracleParameter[] parameters = new OracleParameter[]{
				new OracleParameter(":UserID", OracleDbType.Varchar2, 40){ Value = model.UserID },
				new OracleParameter(":ParentID", OracleDbType.Varchar2, 40){ Value = model.ParentID },
				new OracleParameter(":RoleID", OracleDbType.Varchar2, 40){ Value = model.RoleID },
				model.AppID == null ? new OracleParameter(":AppID", OracleDbType.Varchar2, 40) { Value = DBNull.Value } : new OracleParameter(":AppID", OracleDbType.Varchar2, 40) { Value = model.AppID },
				model.Title == null ? new OracleParameter(":Title", OracleDbType.NVarchar2, 400) { Value = DBNull.Value } : new OracleParameter(":Title", OracleDbType.NVarchar2, 400) { Value = model.Title },
				model.Params == null ? new OracleParameter(":Params", OracleDbType.Varchar2, 500) { Value = DBNull.Value } : new OracleParameter(":Params", OracleDbType.Varchar2, 500) { Value = model.Params },
				model.Ico == null ? new OracleParameter(":Ico", OracleDbType.Varchar2, 500) { Value = DBNull.Value } : new OracleParameter(":Ico", OracleDbType.Varchar2, 500) { Value = model.Ico },
				new OracleParameter(":Sort", OracleDbType.Int32){ Value = model.Sort },
				new OracleParameter(":ID", OracleDbType.Varchar2, 40){ Value = model.ID }
			};
            return dbHelper.Execute(sql, parameters);
        }
        /// <summary>
        /// 删除记录
        /// </summary>
        public int Delete(Guid id)
        {
            string sql = "DELETE FROM UsersApp WHERE ID=:ID";
            OracleParameter[] parameters = new OracleParameter[]{
				new OracleParameter(":ID", OracleDbType.Varchar2){ Value = id }
			};
            return dbHelper.Execute(sql, parameters);
        }
        /// <summary>
        /// 将DataRedar转换为List
        /// </summary>
        private List<MyCreek.Data.Model.UsersApp> DataReaderToList(OracleDataReader dataReader)
        {
            List<MyCreek.Data.Model.UsersApp> List = new List<MyCreek.Data.Model.UsersApp>();
            MyCreek.Data.Model.UsersApp model = null;
            while (dataReader.Read())
            {
                model = new MyCreek.Data.Model.UsersApp();
                model.ID = dataReader.GetString(0).ToGuid();
                model.UserID = dataReader.GetString(1).ToGuid();
                model.ParentID = dataReader.GetString(2).ToGuid();
                model.RoleID = dataReader.GetString(3).ToGuid();
                if (!dataReader.IsDBNull(4))
                    model.AppID = dataReader.GetString(4).ToGuid();
                if (!dataReader.IsDBNull(5))
                    model.Title = dataReader.GetString(5);
                if (!dataReader.IsDBNull(6))
                    model.Params = dataReader.GetString(6);
                if (!dataReader.IsDBNull(7))
                    model.Ico = dataReader.GetString(7);
                model.Sort = dataReader.GetInt32(8);
                List.Add(model);
            }
            return List;
        }
        /// <summary>
        /// 查询所有记录
        /// </summary>
        public List<MyCreek.Data.Model.UsersApp> GetAll()
        {
            string sql = "SELECT * FROM UsersApp";
            OracleDataReader dataReader = dbHelper.GetDataReader(sql);
            List<MyCreek.Data.Model.UsersApp> List = DataReaderToList(dataReader);
            dataReader.Close();
            return List;
        }
        /// <summary>
        /// 查询记录数
        /// </summary>
        public long GetCount()
        {
            string sql = "SELECT COUNT(*) FROM UsersApp";
            long count;
            return long.TryParse(dbHelper.GetFieldValue(sql), out count) ? count : 0;
        }
        /// <summary>
        /// 根据主键查询一条记录
        /// </summary>
        public MyCreek.Data.Model.UsersApp Get(Guid id)
        {
            string sql = "SELECT * FROM UsersApp WHERE ID=:ID";
            OracleParameter[] parameters = new OracleParameter[]{
				new OracleParameter(":ID", OracleDbType.Varchar2){ Value = id }
			};
            OracleDataReader dataReader = dbHelper.GetDataReader(sql, parameters);
            List<MyCreek.Data.Model.UsersApp> List = DataReaderToList(dataReader);
            dataReader.Close();
            return List.Count > 0 ? List[0] : null;
        }


        /// <summary>
        /// 查询所有记录
        /// </summary>
        public System.Data.DataTable GetAllDataTable()
        {
            string sql = "SELECT a.*,b.Address,b.OpenMode,b.Width,b.Height,b.Params AS Params1,b.Manager FROM UsersApp a LEFT JOIN AppLibrary b ON a.AppID=b.ID";
            return dbHelper.GetDataTable(sql);
        }

        /// <summary>
        /// 更新排序
        /// </summary>
        public int UpdateSort(Guid id, int sort)
        {
            string sql = "UPDATE UsersApp SET Sort=:Sort WHERE ID=:ID";
            OracleParameter[] parameters = new OracleParameter[]{
                new OracleParameter(":Sort", OracleDbType.Int32){ Value = sort },
				new OracleParameter(":ID", OracleDbType.Varchar2){ Value = id }
			};
            return dbHelper.Execute(sql, parameters);
        }

        /// <summary>
        /// 查询下级记录
        /// </summary>
        public List<MyCreek.Data.Model.UsersApp> GetChild(Guid id)
        {
            string sql = "SELECT * FROM UsersApp WHERE ParentID=:ParentID ORDER BY Sort";
            OracleParameter[] parameters = new OracleParameter[]{
				new OracleParameter(":ParentID", OracleDbType.Varchar2){ Value = id }
			};
            OracleDataReader dataReader = dbHelper.GetDataReader(sql, parameters);
            List<MyCreek.Data.Model.UsersApp> List = DataReaderToList(dataReader);
            dataReader.Close();
            return List;
        }

        /// <summary>
        /// 删除一个用户记录
        /// </summary>
        public int DeleteByUserID(Guid userID)
        {
            string sql = "DELETE FROM UsersApp WHERE UserID=:UserID";
            OracleParameter[] parameters = new OracleParameter[]{
				new OracleParameter(":UserID", OracleDbType.Varchar2){ Value = userID }
			};
            OracleDataReader dataReader = dbHelper.GetDataReader(sql, parameters);
            return dbHelper.Execute(sql, parameters);
        }
    }
}