using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Oracle.ManagedDataAccess.Client;

namespace MyCreek.Data.ORACLE
{
    public class UsersRelation : MyCreek.Data.Interface.IUsersRelation
    {
        private DBHelper dbHelper = new DBHelper();
        /// <summary>
        /// 构造函数
        /// </summary>
        public UsersRelation()
        {
        }
        /// <summary>
        /// 添加记录
        /// </summary>
        /// <param name="model">MyCreek.Data.Model.UsersRelation实体类</param>
        /// <returns>操作所影响的行数</returns>
        public int Add(MyCreek.Data.Model.UsersRelation model)
        {
            string sql = @"INSERT INTO UsersRelation
				(UserID,OrganizeID,IsMain,Sort) 
				VALUES(:UserID,:OrganizeID,:IsMain,:Sort)";
            OracleParameter[] parameters = new OracleParameter[]{
				new OracleParameter(":UserID", OracleDbType.Varchar2, 40){ Value = model.UserID },
				new OracleParameter(":OrganizeID", OracleDbType.Varchar2, 40){ Value = model.OrganizeID },
				new OracleParameter(":IsMain", OracleDbType.Int32){ Value = model.IsMain },
				new OracleParameter(":Sort", OracleDbType.Int32){ Value = model.Sort }
			};
            return dbHelper.Execute(sql, parameters);
        }
        /// <summary>
        /// 更新记录
        /// </summary>
        /// <param name="model">MyCreek.Data.Model.UsersRelation实体类</param>
        public int Update(MyCreek.Data.Model.UsersRelation model)
        {
            string sql = @"UPDATE UsersRelation SET 
				IsMain=:IsMain,Sort=:Sort
				WHERE UserID=:UserID and OrganizeID=:OrganizeID";
            OracleParameter[] parameters = new OracleParameter[]{
				new OracleParameter(":IsMain", OracleDbType.Int32){ Value = model.IsMain },
				new OracleParameter(":Sort", OracleDbType.Int32){ Value = model.Sort },
				new OracleParameter(":UserID", OracleDbType.Varchar2, 40){ Value = model.UserID },
				new OracleParameter(":OrganizeID", OracleDbType.Varchar2, 40){ Value = model.OrganizeID }
			};
            return dbHelper.Execute(sql, parameters);
        }
        /// <summary>
        /// 删除记录
        /// </summary>
        public int Delete(Guid userid, Guid organizeid)
        {
            string sql = "DELETE FROM UsersRelation WHERE UserID=:UserID AND OrganizeID=:OrganizeID";
            OracleParameter[] parameters = new OracleParameter[]{
				new OracleParameter(":UserID", OracleDbType.Varchar2){ Value = userid },
				new OracleParameter(":OrganizeID", OracleDbType.Varchar2){ Value = organizeid }
			};
            return dbHelper.Execute(sql, parameters);
        }
        /// <summary>
        /// 将DataRedar转换为List
        /// </summary>
        private List<MyCreek.Data.Model.UsersRelation> DataReaderToList(OracleDataReader dataReader)
        {
            List<MyCreek.Data.Model.UsersRelation> List = new List<MyCreek.Data.Model.UsersRelation>();
            MyCreek.Data.Model.UsersRelation model = null;
            while (dataReader.Read())
            {
                model = new MyCreek.Data.Model.UsersRelation();
                model.UserID = dataReader.GetString(0).ToGuid();
                model.OrganizeID = dataReader.GetString(1).ToGuid();
                model.IsMain = dataReader.GetInt32(2);
                model.Sort = dataReader.GetInt32(3);
                List.Add(model);
            }
            return List;
        }
        /// <summary>
        /// 查询所有记录
        /// </summary>
        public List<MyCreek.Data.Model.UsersRelation> GetAll()
        {
            string sql = "SELECT * FROM UsersRelation";
            OracleDataReader dataReader = dbHelper.GetDataReader(sql);
            List<MyCreek.Data.Model.UsersRelation> List = DataReaderToList(dataReader);
            dataReader.Close();
            return List;
        }
        /// <summary>
        /// 查询记录数
        /// </summary>
        public long GetCount()
        {
            string sql = "SELECT COUNT(*) FROM UsersRelation";
            long count;
            return long.TryParse(dbHelper.GetFieldValue(sql), out count) ? count : 0;
        }
        /// <summary>
        /// 根据主键查询一条记录
        /// </summary>
        public MyCreek.Data.Model.UsersRelation Get(Guid userid, Guid organizeid)
        {
            string sql = "SELECT * FROM UsersRelation WHERE UserID=:UserID AND OrganizeID=:OrganizeID";
            OracleParameter[] parameters = new OracleParameter[]{
				new OracleParameter(":UserID", OracleDbType.Varchar2){ Value = userid },
				new OracleParameter(":OrganizeID", OracleDbType.Varchar2){ Value = organizeid }
			};
            OracleDataReader dataReader = dbHelper.GetDataReader(sql, parameters);
            List<MyCreek.Data.Model.UsersRelation> List = DataReaderToList(dataReader);
            dataReader.Close();
            return List.Count > 0 ? List[0] : null;
        }

        /// <summary>
        /// 查询一个岗位下所有记录
        /// </summary>
        public List<MyCreek.Data.Model.UsersRelation> GetAllByOrganizeID(Guid organizeID)
        {
            string sql = "SELECT * FROM UsersRelation WHERE OrganizeID=:OrganizeID";
            OracleParameter[] parameters = new OracleParameter[]{
				new OracleParameter(":OrganizeID", OracleDbType.Varchar2){ Value = organizeID }
			};
            OracleDataReader dataReader = dbHelper.GetDataReader(sql, parameters);
            List<MyCreek.Data.Model.UsersRelation> List = DataReaderToList(dataReader);
            dataReader.Close();
            return List;
        }

        /// <summary>
        /// 查询一个用户所有记录
        /// </summary>
        public List<MyCreek.Data.Model.UsersRelation> GetAllByUserID(Guid userID)
        {
            string sql = "SELECT * FROM UsersRelation WHERE UserID=:UserID";
            OracleParameter[] parameters = new OracleParameter[]{
				new OracleParameter(":UserID", OracleDbType.Varchar2){ Value = userID }
			};
            OracleDataReader dataReader = dbHelper.GetDataReader(sql, parameters);
            List<MyCreek.Data.Model.UsersRelation> List = DataReaderToList(dataReader);
            dataReader.Close();
            return List;
        }

        /// <summary>
        /// 查询一个用户主要岗位
        /// </summary>
        public MyCreek.Data.Model.UsersRelation GetMainByUserID(Guid userID)
        {
            string sql = "SELECT * FROM UsersRelation WHERE UserID=:UserID AND IsMain=1";
            OracleParameter[] parameters = new OracleParameter[]{
				new OracleParameter(":UserID",OracleDbType.Varchar2){ Value = userID }
			};
            OracleDataReader dataReader = dbHelper.GetDataReader(sql, parameters);
            List<MyCreek.Data.Model.UsersRelation> List = DataReaderToList(dataReader);
            dataReader.Close();
            return List.Count > 0 ? List[0] : null;
        }


        /// <summary>
        /// 删除一个用户记录
        /// </summary>
        public int DeleteByUserID(Guid userID)
        {
            string sql = "DELETE FROM UsersRelation WHERE UserID=:UserID";
            OracleParameter[] parameters = new OracleParameter[]{
				new OracleParameter(":UserID", OracleDbType.Varchar2){ Value = userID }
			};
            return dbHelper.Execute(sql, parameters);
        }

        /// <summary>
        /// 删除一个用户的兼职记录
        /// </summary>
        public int DeleteNotIsMainByUserID(Guid userID)
        {
            string sql = "DELETE FROM UsersRelation WHERE IsMain=0 AND UserID=:UserID";
            OracleParameter[] parameters = new OracleParameter[]{
				new OracleParameter(":UserID", OracleDbType.Varchar2){ Value = userID }
			};
            return dbHelper.Execute(sql, parameters);
        }

        /// <summary>
        /// 删除一个机构下所有记录
        /// </summary>
        public int DeleteByOrganizeID(Guid organizeID)
        {
            string sql = "DELETE FROM UsersRelation WHERE OrganizeID=:OrganizeID";
            OracleParameter[] parameters = new OracleParameter[]{
				new OracleParameter(":OrganizeID", OracleDbType.Varchar2){ Value = organizeID }
			};
            return dbHelper.Execute(sql, parameters);
        }

        /// <summary>
        /// 得到最大排序值
        /// </summary>
        /// <returns></returns>
        public int GetMaxSort(Guid organizeID)
        {
            string sql = "SELECT nvl(MAX(Sort),0)+1 FROM UsersRelation WHERE OrganizeID=:OrganizeID";
            OracleParameter[] parameters = new OracleParameter[]{
				new OracleParameter(":OrganizeID", OracleDbType.Varchar2){ Value = organizeID }
			};
            DBHelper dbHelper = new DBHelper();
            string sort = dbHelper.GetFieldValue(sql, parameters);
            return sort.ToInt();
        }
    }
}