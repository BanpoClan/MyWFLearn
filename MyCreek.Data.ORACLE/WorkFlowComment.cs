using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Oracle.ManagedDataAccess.Client;

namespace MyCreek.Data.ORACLE
{
    public class WorkFlowComment : MyCreek.Data.Interface.IWorkFlowComment
    {
        private DBHelper dbHelper = new DBHelper();
        /// <summary>
        /// 构造函数
        /// </summary>
        public WorkFlowComment()
        {
        }
        /// <summary>
        /// 添加记录
        /// </summary>
        /// <param name="model">MyCreek.Data.Model.WorkFlowComment实体类</param>
        /// <returns>操作所影响的行数</returns>
        public int Add(MyCreek.Data.Model.WorkFlowComment model)
        {
            string sql = @"INSERT INTO WorkFlowComment
				(ID,MemberID,Comment,Type,Sort) 
				VALUES(:ID,:MemberID,:Comment,:Type,:Sort)";
            OracleParameter[] parameters = new OracleParameter[]{
				new OracleParameter(":ID", OracleDbType.Varchar2, 40){ Value = model.ID },
				new OracleParameter(":MemberID", OracleDbType.Clob){ Value = model.MemberID },
				new OracleParameter(":Comment", OracleDbType.NVarchar2, 1000){ Value = model.Comment },
				new OracleParameter(":Type", OracleDbType.Int32){ Value = model.Type },
				new OracleParameter(":Sort", OracleDbType.Int32){ Value = model.Sort }
			};
            return dbHelper.Execute(sql, parameters);
        }
        /// <summary>
        /// 更新记录
        /// </summary>
        /// <param name="model">MyCreek.Data.Model.WorkFlowComment实体类</param>
        public int Update(MyCreek.Data.Model.WorkFlowComment model)
        {
            string sql = @"UPDATE WorkFlowComment SET 
				MemberID=:MemberID,Comment=:Comment,Type=:Type,Sort=:Sort
				WHERE ID=:ID";
            OracleParameter[] parameters = new OracleParameter[]{
				new OracleParameter(":MemberID", OracleDbType.Clob){ Value = model.MemberID },
				new OracleParameter(":Comment", OracleDbType.NVarchar2, 1000){ Value = model.Comment },
				new OracleParameter(":Type", OracleDbType.Int32){ Value = model.Type },
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
            string sql = "DELETE FROM WorkFlowComment WHERE ID=:ID";
            OracleParameter[] parameters = new OracleParameter[]{
				new OracleParameter(":ID", OracleDbType.Varchar2){ Value = id }
			};
            return dbHelper.Execute(sql, parameters);
        }
        /// <summary>
        /// 将DataRedar转换为List
        /// </summary>
        private List<MyCreek.Data.Model.WorkFlowComment> DataReaderToList(OracleDataReader dataReader)
        {
            List<MyCreek.Data.Model.WorkFlowComment> List = new List<MyCreek.Data.Model.WorkFlowComment>();
            MyCreek.Data.Model.WorkFlowComment model = null;
            while (dataReader.Read())
            {
                model = new MyCreek.Data.Model.WorkFlowComment();
                model.ID = dataReader.GetString(0).ToGuid();
                model.MemberID = dataReader.GetString(1);
                model.Comment = dataReader.GetString(2);
                model.Type = dataReader.GetInt32(3);
                model.Sort = dataReader.GetInt32(4);
                List.Add(model);
            }
            return List;
        }
        /// <summary>
        /// 查询所有记录
        /// </summary>
        public List<MyCreek.Data.Model.WorkFlowComment> GetAll()
        {
            string sql = "SELECT * FROM WorkFlowComment";
            OracleDataReader dataReader = dbHelper.GetDataReader(sql);
            List<MyCreek.Data.Model.WorkFlowComment> List = DataReaderToList(dataReader);
            dataReader.Close();
            return List;
        }
        /// <summary>
        /// 查询记录数
        /// </summary>
        public long GetCount()
        {
            string sql = "SELECT COUNT(*) FROM WorkFlowComment";
            long count;
            return long.TryParse(dbHelper.GetFieldValue(sql), out count) ? count : 0;
        }
        /// <summary>
        /// 根据主键查询一条记录
        /// </summary>
        public MyCreek.Data.Model.WorkFlowComment Get(Guid id)
        {
            string sql = "SELECT * FROM WorkFlowComment WHERE ID=:ID";
            OracleParameter[] parameters = new OracleParameter[]{
				new OracleParameter(":ID", OracleDbType.Varchar2){ Value = id }
			};
            OracleDataReader dataReader = dbHelper.GetDataReader(sql, parameters);
            List<MyCreek.Data.Model.WorkFlowComment> List = DataReaderToList(dataReader);
            dataReader.Close();
            return List.Count > 0 ? List[0] : null;
        }


        /// <summary>
        /// 查询管理员的所有记录
        /// </summary>
        public List<MyCreek.Data.Model.WorkFlowComment> GetManagerAll()
        {
            string sql = "SELECT * FROM WorkFlowComment WHERE Type=0";
            OracleDataReader dataReader = dbHelper.GetDataReader(sql);
            List<MyCreek.Data.Model.WorkFlowComment> List = DataReaderToList(dataReader);
            dataReader.Close();
            return List;
        }
        /// <summary>
        /// 得到管理员类别的最大排序值
        /// </summary>
        /// <returns></returns>
        public int GetManagerMaxSort()
        {
            string sql = "SELECT nvl(MAX(Sort)+1,1) FROM WorkFlowComment WHERE Type=0";
            OracleDataReader dataReader = dbHelper.GetDataReader(sql);
            if (dataReader.HasRows)
            {
                dataReader.Read();
                int sort = dataReader.GetInt32(0);
                dataReader.Close();
                return sort;
            }
            return 1;
        }

        /// <summary>
        /// 得到一个人员的最大排序值
        /// </summary>
        /// <returns></returns>
        public int GetUserMaxSort(Guid userID)
        {
            string sql = "SELECT nvl(MAX(Sort)+1,1) FROM WorkFlowComment WHERE MemberID=:MemberID";
            OracleParameter[] parameters = new OracleParameter[]{
				new OracleParameter(":MemberID", OracleDbType.Varchar2){ Value = userID }
			};
            OracleDataReader dataReader = dbHelper.GetDataReader(sql, parameters);
            if (dataReader.HasRows)
            {
                dataReader.Read();
                int sort = dataReader.GetInt32(0);
                dataReader.Close();
                return sort;
            }
            return 1;
        }
    }
}