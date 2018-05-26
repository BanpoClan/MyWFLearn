using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Oracle.ManagedDataAccess.Client;

namespace MyCreek.Data.ORACLE
{
    public class Dictionary : MyCreek.Data.Interface.IDictionary
    {
        private DBHelper dbHelper = new DBHelper();
        /// <summary>
        /// 构造函数
        /// </summary>
        public Dictionary()
        {
        }
        /// <summary>
        /// 添加记录
        /// </summary>
        /// <param name="model">MyCreek.Data.Model.Dictionary实体类</param>
        /// <returns>操作所影响的行数</returns>
        public int Add(MyCreek.Data.Model.Dictionary model)
        {
            string sql = @"INSERT INTO Dictionary
				(ID,ParentID,Title,Code,Value,Note,Other,Sort) 
				VALUES(:ID,:ParentID,:Title,:Code,:Value,:Note,:Other,:Sort)";
            OracleParameter[] parameters = new OracleParameter[]{
				new OracleParameter(":ID", OracleDbType.Varchar2, 40){ Value = model.ID },
				new OracleParameter(":ParentID", OracleDbType.Varchar2, 40){ Value = model.ParentID },
				new OracleParameter(":Title", OracleDbType.NVarchar2){ Value = model.Title },
				model.Code == null ? new OracleParameter(":Code", OracleDbType.Varchar2, 500) { Value = DBNull.Value } : new OracleParameter(":Code", OracleDbType.Varchar2, 500) { Value = model.Code },
				model.Value == null ? new OracleParameter(":Value", OracleDbType.Clob) { Value = DBNull.Value } : new OracleParameter(":Value", OracleDbType.Clob) { Value = model.Value },
				model.Note == null ? new OracleParameter(":Note", OracleDbType.Clob) { Value = DBNull.Value } : new OracleParameter(":Note", OracleDbType.Clob) { Value = model.Note },
				model.Other == null ? new OracleParameter(":Other", OracleDbType.Clob) { Value = DBNull.Value } : new OracleParameter(":Other", OracleDbType.Clob) { Value = model.Other },
				new OracleParameter(":Sort", OracleDbType.Int32){ Value = model.Sort }
			};
            return dbHelper.Execute(sql, parameters);
        }
        /// <summary>
        /// 更新记录
        /// </summary>
        /// <param name="model">MyCreek.Data.Model.Dictionary实体类</param>
        public int Update(MyCreek.Data.Model.Dictionary model)
        {
            string sql = @"UPDATE Dictionary SET 
				ParentID=:ParentID,Title=:Title,Code=:Code,Value=:Value,Note=:Note,Other=:Other,Sort=:Sort
				WHERE ID=:ID";
            OracleParameter[] parameters = new OracleParameter[]{
				new OracleParameter(":ParentID", OracleDbType.Varchar2, 40){ Value = model.ParentID },
				new OracleParameter(":Title", OracleDbType.NVarchar2){ Value = model.Title },
				model.Code == null ? new OracleParameter(":Code", OracleDbType.Varchar2, 500) { Value = DBNull.Value } : new OracleParameter(":Code", OracleDbType.Varchar2, 500) { Value = model.Code },
				model.Value == null ? new OracleParameter(":Value", OracleDbType.Clob) { Value = DBNull.Value } : new OracleParameter(":Value", OracleDbType.Clob) { Value = model.Value },
				model.Note == null ? new OracleParameter(":Note", OracleDbType.Clob) { Value = DBNull.Value } : new OracleParameter(":Note", OracleDbType.Clob) { Value = model.Note },
				model.Other == null ? new OracleParameter(":Other", OracleDbType.Clob) { Value = DBNull.Value } : new OracleParameter(":Other", OracleDbType.Clob) { Value = model.Other },
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
            string sql = "DELETE FROM Dictionary WHERE ID=:ID";
            OracleParameter[] parameters = new OracleParameter[]{
				new OracleParameter(":ID", OracleDbType.Varchar2){ Value = id }
			};
            return dbHelper.Execute(sql, parameters);
        }
        /// <summary>
        /// 将DataRedar转换为List
        /// </summary>
        private List<MyCreek.Data.Model.Dictionary> DataReaderToList(OracleDataReader dataReader)
        {
            List<MyCreek.Data.Model.Dictionary> List = new List<MyCreek.Data.Model.Dictionary>();
            MyCreek.Data.Model.Dictionary model = null;
            while (dataReader.Read())
            {
                model = new MyCreek.Data.Model.Dictionary();
                model.ID = dataReader.GetString(0).ToGuid();
                model.ParentID = dataReader.GetString(1).ToGuid();
                model.Title = dataReader.GetString(2);
                if (!dataReader.IsDBNull(3))
                    model.Code = dataReader.GetString(3);
                if (!dataReader.IsDBNull(4))
                    model.Value = dataReader.GetString(4);
                if (!dataReader.IsDBNull(5))
                    model.Note = dataReader.GetString(5);
                if (!dataReader.IsDBNull(6))
                    model.Other = dataReader.GetString(6);
                model.Sort = dataReader.GetInt32(7);
                List.Add(model);
            }
            return List;
        }
        /// <summary>
        /// 查询所有记录
        /// </summary>
        public List<MyCreek.Data.Model.Dictionary> GetAll()
        {
            string sql = "SELECT * FROM Dictionary";
            OracleDataReader dataReader = dbHelper.GetDataReader(sql);
            List<MyCreek.Data.Model.Dictionary> List = DataReaderToList(dataReader);
            dataReader.Close();
            return List;
        }
        /// <summary>
        /// 查询记录数
        /// </summary>
        public long GetCount()
        {
            string sql = "SELECT COUNT(*) FROM Dictionary";
            long count;
            return long.TryParse(dbHelper.GetFieldValue(sql), out count) ? count : 0;
        }
        /// <summary>
        /// 根据主键查询一条记录
        /// </summary>
        public MyCreek.Data.Model.Dictionary Get(Guid id)
        {
            string sql = "SELECT * FROM Dictionary WHERE ID=:ID";
            OracleParameter[] parameters = new OracleParameter[]{
				new OracleParameter(":ID", OracleDbType.Varchar2){ Value = id }
			};
            OracleDataReader dataReader = dbHelper.GetDataReader(sql, parameters);
            List<MyCreek.Data.Model.Dictionary> List = DataReaderToList(dataReader);
            dataReader.Close();
            return List.Count > 0 ? List[0] : null;
        }


        /// <summary>
        /// 查询根记录
        /// </summary>
        public MyCreek.Data.Model.Dictionary GetRoot()
        {
            string sql = "SELECT * FROM Dictionary WHERE ParentID=:ParentID";
            OracleParameter[] parameters = new OracleParameter[]{
				new OracleParameter(":ParentID", OracleDbType.Varchar2){ Value = Guid.Empty }
			};
            OracleDataReader dataReader = dbHelper.GetDataReader(sql, parameters);
            List<MyCreek.Data.Model.Dictionary> List = DataReaderToList(dataReader);
            dataReader.Close();
            return List.Count > 0 ? List[0] : null;
        }

        /// <summary>
        /// 查询下级记录
        /// </summary>
        public List<MyCreek.Data.Model.Dictionary> GetChilds(Guid id)
        {
            string sql = "SELECT * FROM Dictionary WHERE ParentID=:ParentID ORDER BY Sort";
            OracleParameter[] parameters = new OracleParameter[]{
				new OracleParameter(":ParentID", OracleDbType.Varchar2){ Value = id }
			};
            OracleDataReader dataReader = dbHelper.GetDataReader(sql, parameters);
            List<MyCreek.Data.Model.Dictionary> List = DataReaderToList(dataReader);
            dataReader.Close();
            return List;
        }
        /// <summary>
        /// 查询下级记录
        /// </summary>
        public List<MyCreek.Data.Model.Dictionary> GetChilds(string code)
        {
            string sql = "SELECT * FROM Dictionary WHERE ParentID=(SELECT ID FROM Dictionary WHERE Code=:Code) ORDER BY Sort";
            OracleParameter[] parameters = new OracleParameter[]{
				new OracleParameter(":Code", OracleDbType.Varchar2){ Value = code }
			};
            OracleDataReader dataReader = dbHelper.GetDataReader(sql, parameters);
            List<MyCreek.Data.Model.Dictionary> List = DataReaderToList(dataReader);
            dataReader.Close();
            return List;
        }

        /// <summary>
        /// 查询上级记录
        /// </summary>
        public MyCreek.Data.Model.Dictionary GetParent(Guid id)
        {
            string sql = "SELECT * FROM Dictionary WHERE ID=(SELECT ParentID FROM Dictionary WHERE ID=:ID)";
            OracleParameter[] parameters = new OracleParameter[]{
				new OracleParameter(":ID", OracleDbType.Varchar2){ Value = id }
			};
            OracleDataReader dataReader = dbHelper.GetDataReader(sql, parameters);
            List<MyCreek.Data.Model.Dictionary> List = DataReaderToList(dataReader);
            dataReader.Close();
            return List.Count > 0 ? List[0] : null;
        }

        /// <summary>
        /// 是否包含下级记录
        /// </summary>
        public bool HasChilds(Guid id)
        {
            string sql = "SELECT ID FROM Dictionary WHERE ParentID=:ParentID";
            OracleParameter[] parameters = new OracleParameter[]{
				new OracleParameter(":ParentID", OracleDbType.Varchar2){ Value = id }
			};
            OracleDataReader dataReader = dbHelper.GetDataReader(sql, parameters);
            bool has = dataReader.HasRows;
            dataReader.Close();
            return has;
        }

        /// <summary>
        /// 得到最大排序
        /// </summary>
        public int GetMaxSort(Guid id)
        {
            string sql = "SELECT MAX(Sort)+1 FROM Dictionary WHERE ParentID=:ParentID";
            OracleParameter[] parameters = new OracleParameter[]{
				new OracleParameter(":ParentID", OracleDbType.Varchar2){ Value = id }
			};
            string max = dbHelper.GetFieldValue(sql, parameters);
            int max1;
            return max.IsInt(out max1) ? max1 : 1;
        }

        /// <summary>
        /// 更新排序
        /// </summary>
        public int UpdateSort(Guid id, int sort)
        {
            string sql = "UPDATE Dictionary SET Sort=:Sort WHERE ID=:ID";
            OracleParameter[] parameters = new OracleParameter[]{
				new OracleParameter(":Sort", OracleDbType.Int32){ Value = sort },
                new OracleParameter(":ID", OracleDbType.Varchar2){ Value = id }
			};
            return dbHelper.Execute(sql, parameters);
        }

        /// <summary>
        /// 根据代码查询一条记录
        /// </summary>
        public MyCreek.Data.Model.Dictionary GetByCode(string code)
        {
            string sql = "SELECT * FROM Dictionary WHERE Code=:Code";
            OracleParameter[] parameters = new OracleParameter[]{
				new OracleParameter(":Code", OracleDbType.Varchar2){ Value = code }
			};
            OracleDataReader dataReader = dbHelper.GetDataReader(sql, parameters);
            List<MyCreek.Data.Model.Dictionary> List = DataReaderToList(dataReader);
            dataReader.Close();
            return List.Count > 0 ? List[0] : null;
        }
    }
}