using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Oracle.ManagedDataAccess.Client;

namespace MyCreek.Data.ORACLE
{
    public class WorkFlowButtons : MyCreek.Data.Interface.IWorkFlowButtons
    {
        private DBHelper dbHelper = new DBHelper();
        /// <summary>
        /// 构造函数
        /// </summary>
        public WorkFlowButtons()
        {
        }
        /// <summary>
        /// 添加记录
        /// </summary>
        /// <param name="model">MyCreek.Data.Model.WorkFlowButtons实体类</param>
        /// <returns>操作所影响的行数</returns>
        public int Add(MyCreek.Data.Model.WorkFlowButtons model)
        {
            string sql = @"INSERT INTO WorkFlowButtons
				(ID,Title,Ico,Script,Note,Sort) 
				VALUES(:ID,:Title,:Ico,:Script,:Note,:Sort)";
            OracleParameter[] parameters = new OracleParameter[]{
				new OracleParameter(":ID", OracleDbType.Varchar2, 40){ Value = model.ID },
				new OracleParameter(":Title", OracleDbType.NVarchar2, 1000){ Value = model.Title },
				model.Ico == null ? new OracleParameter(":Ico", OracleDbType.Varchar2, 500) { Value = DBNull.Value } : new OracleParameter(":Ico", OracleDbType.Varchar2, 500) { Value = model.Ico },
				model.Script == null ? new OracleParameter(":Script", OracleDbType.Clob) { Value = DBNull.Value } : new OracleParameter(":Script", OracleDbType.Clob) { Value = model.Script },
				model.Note == null ? new OracleParameter(":Note", OracleDbType.Clob) { Value = DBNull.Value } : new OracleParameter(":Note", OracleDbType.Clob) { Value = model.Note },
				new OracleParameter(":Sort", OracleDbType.Int32){ Value = model.Sort }
			};
            return dbHelper.Execute(sql, parameters);
        }
        /// <summary>
        /// 更新记录
        /// </summary>
        /// <param name="model">MyCreek.Data.Model.WorkFlowButtons实体类</param>
        public int Update(MyCreek.Data.Model.WorkFlowButtons model)
        {
            string sql = @"UPDATE WorkFlowButtons SET 
				Title=:Title,Ico=:Ico,Script=:Script,Note=:Note,Sort=:Sort
				WHERE ID=:ID";
            OracleParameter[] parameters = new OracleParameter[]{
				new OracleParameter(":Title", OracleDbType.NVarchar2, 1000){ Value = model.Title },
				model.Ico == null ? new OracleParameter(":Ico", OracleDbType.Varchar2, 500) { Value = DBNull.Value } : new OracleParameter(":Ico", OracleDbType.Varchar2, 500) { Value = model.Ico },
				model.Script == null ? new OracleParameter(":Script", OracleDbType.Clob) { Value = DBNull.Value } : new OracleParameter(":Script", OracleDbType.Clob) { Value = model.Script },
				model.Note == null ? new OracleParameter(":Note", OracleDbType.Clob) { Value = DBNull.Value } : new OracleParameter(":Note", OracleDbType.Clob) { Value = model.Note },
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
            string sql = "DELETE FROM WorkFlowButtons WHERE ID=:ID";
            OracleParameter[] parameters = new OracleParameter[]{
				new OracleParameter(":ID", OracleDbType.Varchar2){ Value = id }
			};
            return dbHelper.Execute(sql, parameters);
        }
        /// <summary>
        /// 将DataRedar转换为List
        /// </summary>
        private List<MyCreek.Data.Model.WorkFlowButtons> DataReaderToList(OracleDataReader dataReader)
        {
            List<MyCreek.Data.Model.WorkFlowButtons> List = new List<MyCreek.Data.Model.WorkFlowButtons>();
            MyCreek.Data.Model.WorkFlowButtons model = null;
            while (dataReader.Read())
            {
                model = new MyCreek.Data.Model.WorkFlowButtons();
                model.ID = dataReader.GetString(0).ToGuid();
                model.Title = dataReader.GetString(1);
                if (!dataReader.IsDBNull(2))
                    model.Ico = dataReader.GetString(2);
                if (!dataReader.IsDBNull(3))
                    model.Script = dataReader.GetString(3);
                if (!dataReader.IsDBNull(4))
                    model.Note = dataReader.GetString(4);
                model.Sort = dataReader.GetInt32(5);
                List.Add(model);
            }
            return List;
        }
        /// <summary>
        /// 查询所有记录
        /// </summary>
        public List<MyCreek.Data.Model.WorkFlowButtons> GetAll()
        {
            string sql = "SELECT * FROM WorkFlowButtons";
            OracleDataReader dataReader = dbHelper.GetDataReader(sql);
            List<MyCreek.Data.Model.WorkFlowButtons> List = DataReaderToList(dataReader);
            dataReader.Close();
            return List;
        }
        /// <summary>
        /// 查询记录数
        /// </summary>
        public long GetCount()
        {
            string sql = "SELECT COUNT(*) FROM WorkFlowButtons";
            long count;
            return long.TryParse(dbHelper.GetFieldValue(sql), out count) ? count : 0;
        }
        /// <summary>
        /// 根据主键查询一条记录
        /// </summary>
        public MyCreek.Data.Model.WorkFlowButtons Get(Guid id)
        {
            string sql = "SELECT * FROM WorkFlowButtons WHERE ID=:ID";
            OracleParameter[] parameters = new OracleParameter[]{
				new OracleParameter(":ID", OracleDbType.Varchar2){ Value = id }
			};
            OracleDataReader dataReader = dbHelper.GetDataReader(sql, parameters);
            List<MyCreek.Data.Model.WorkFlowButtons> List = DataReaderToList(dataReader);
            dataReader.Close();
            return List.Count > 0 ? List[0] : null;
        }

        /// <summary>
        /// 查询最大排序
        /// </summary>
        public int GetMaxSort()
        {
            string sql = "SELECT nvl(MAX(Sort),0)+1 FROM WorkFlowButtons";
            string max = dbHelper.GetFieldValue(sql);
            return max.IsInt() ? max.ToInt() : 1;
        }
    }
}