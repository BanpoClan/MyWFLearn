using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Oracle.ManagedDataAccess.Client;

namespace RoadFlow.Data.ORACLE
{
    public class AppLibrary : RoadFlow.Data.Interface.IAppLibrary
    {
        private DBHelper dbHelper = new DBHelper();
        /// <summary>
        /// 构造函数
        /// </summary>
        public AppLibrary()
        {
        }
        /// <summary>
        /// 添加记录
        /// </summary>
        /// <param name="model">RoadFlow.Data.Model.AppLibrary实体类</param>
        /// <returns>操作所影响的行数</returns>
        public int Add(RoadFlow.Data.Model.AppLibrary model)
        {
            string sql = @"INSERT INTO AppLibrary
				(ID,Title,Address,Type,OpenMode,Width,Height,Params,Manager,Note,Code) 
				VALUES(:ID,:Title,:Address,:Type,:OpenMode,:Width,:Height,:Params,:Manager,:Note,:Code,:UseMember)";
            OracleParameter[] parameters = new OracleParameter[]{
				new OracleParameter(":ID", OracleDbType.Varchar2, 40){ Value = model.ID },
				new OracleParameter(":Title", OracleDbType.NVarchar2, 510){ Value = model.Title },
				new OracleParameter(":Address", OracleDbType.Varchar2, 200){ Value = model.Address },
				new OracleParameter(":Type", OracleDbType.Varchar2, 40){ Value = model.Type },
				new OracleParameter(":OpenMode", OracleDbType.Int32){ Value = model.OpenMode },
				model.Width == null ? new OracleParameter(":Width", OracleDbType.Int32) { Value = DBNull.Value } : new OracleParameter(":Width", OracleDbType.Int32) { Value = model.Width },
				model.Height == null ? new OracleParameter(":Height", OracleDbType.Int32) { Value = DBNull.Value } : new OracleParameter(":Height", OracleDbType.Int32) { Value = model.Height },
				model.Params == null ? new OracleParameter(":Params", OracleDbType.Clob) { Value = DBNull.Value } : new OracleParameter(":Params", OracleDbType.Clob) { Value = model.Params },
				model.Manager == null ? new OracleParameter(":Manager", OracleDbType.Clob) { Value = DBNull.Value } : new OracleParameter(":Manager", OracleDbType.Clob) { Value = model.Manager },
				model.Note == null ? new OracleParameter(":Note", OracleDbType.Clob) { Value = DBNull.Value } : new OracleParameter(":Note", OracleDbType.Clob) { Value = model.Note },
				model.Code == null ? new OracleParameter(":Code", OracleDbType.Varchar2, 50) { Value = DBNull.Value } : new OracleParameter(":Code", OracleDbType.Varchar2, 50) { Value = model.Code },
                model.UseMember == null ? new OracleParameter(":UseMember", OracleDbType.Varchar2) { Value = DBNull.Value } : new OracleParameter(":UseMember", OracleDbType.Varchar2) { Value = model.UseMember }
			};
            return dbHelper.Execute(sql, parameters);
        }
        /// <summary>
        /// 更新记录
        /// </summary>
        /// <param name="model">RoadFlow.Data.Model.AppLibrary实体类</param>
        public int Update(RoadFlow.Data.Model.AppLibrary model)
        {
            string sql = @"UPDATE AppLibrary SET 
				Title=:Title,Address=:Address,Type=:Type,OpenMode=:OpenMode,Width=:Width,Height=:Height,Params=:Params,Manager=:Manager,Note=:Note,Code=:Code,UseMember=:UseMember
				WHERE ID=:ID";
            OracleParameter[] parameters = new OracleParameter[]{
				new OracleParameter(":Title", OracleDbType.NVarchar2, 510){ Value = model.Title },
				new OracleParameter(":Address", OracleDbType.Varchar2, 200){ Value = model.Address },
				new OracleParameter(":Type", OracleDbType.Varchar2, 40){ Value = model.Type },
				new OracleParameter(":OpenMode", OracleDbType.Int32){ Value = model.OpenMode },
				model.Width == null ? new OracleParameter(":Width", OracleDbType.Int32) { Value = DBNull.Value } : new OracleParameter(":Width", OracleDbType.Int32) { Value = model.Width },
				model.Height == null ? new OracleParameter(":Height", OracleDbType.Int32) { Value = DBNull.Value } : new OracleParameter(":Height", OracleDbType.Int32) { Value = model.Height },
				model.Params == null ? new OracleParameter(":Params", OracleDbType.Clob) { Value = DBNull.Value } : new OracleParameter(":Params", OracleDbType.Clob) { Value = model.Params },
				model.Manager == null ? new OracleParameter(":Manager", OracleDbType.Clob) { Value = DBNull.Value } : new OracleParameter(":Manager", OracleDbType.Clob) { Value = model.Manager },
				model.Note == null ? new OracleParameter(":Note", OracleDbType.Clob) { Value = DBNull.Value } : new OracleParameter(":Note", OracleDbType.Clob) { Value = model.Note },
				model.Code == null ? new OracleParameter(":Code", OracleDbType.Varchar2, 50) { Value = DBNull.Value } : new OracleParameter(":Code", OracleDbType.Varchar2, 50) { Value = model.Code },
                model.UseMember == null ? new OracleParameter(":UseMember", OracleDbType.Varchar2) { Value = DBNull.Value } : new OracleParameter(":UseMember", OracleDbType.Varchar2) { Value = model.UseMember },
				new OracleParameter(":ID", OracleDbType.Varchar2, 40){ Value = model.ID }
			};
            return dbHelper.Execute(sql, parameters);
        }
        /// <summary>
        /// 删除记录
        /// </summary>
        public int Delete(Guid id)
        {
            string sql = "DELETE FROM AppLibrary WHERE ID=:ID";
            OracleParameter[] parameters = new OracleParameter[]{
				new OracleParameter(":ID", OracleDbType.Varchar2){ Value = id }
			};
            return dbHelper.Execute(sql, parameters);
        }
        /// <summary>
        /// 将DataRedar转换为List
        /// </summary>
        private List<RoadFlow.Data.Model.AppLibrary> DataReaderToList(OracleDataReader dataReader)
        {
            List<RoadFlow.Data.Model.AppLibrary> List = new List<RoadFlow.Data.Model.AppLibrary>();
            RoadFlow.Data.Model.AppLibrary model = null;
            while (dataReader.Read())
            {
                model = new RoadFlow.Data.Model.AppLibrary();
                model.ID = dataReader.GetString(0).ToGuid();
                model.Title = dataReader.GetString(1);
                model.Address = dataReader.GetString(2);
                model.Type = dataReader.GetString(3).ToGuid();
                model.OpenMode = dataReader.GetInt32(4);
                if (!dataReader.IsDBNull(5))
                    model.Width = dataReader.GetInt32(5);
                if (!dataReader.IsDBNull(6))
                    model.Height = dataReader.GetInt32(6);
                if (!dataReader.IsDBNull(7))
                    model.Params = dataReader.GetString(7);
                if (!dataReader.IsDBNull(8))
                    model.Manager = dataReader.GetString(8);
                if (!dataReader.IsDBNull(9))
                    model.Note = dataReader.GetString(9);
                if (!dataReader.IsDBNull(10))
                    model.Code = dataReader.GetString(10);
                List.Add(model);
            }
            return List;
        }
        /// <summary>
        /// 查询所有记录
        /// </summary>
        public List<RoadFlow.Data.Model.AppLibrary> GetAll()
        {
            string sql = "SELECT * FROM AppLibrary";
            OracleDataReader dataReader = dbHelper.GetDataReader(sql);
            List<RoadFlow.Data.Model.AppLibrary> List = DataReaderToList(dataReader);
            dataReader.Close();
            return List;
        }
        /// <summary>
        /// 查询记录数
        /// </summary>
        public long GetCount()
        {
            string sql = "SELECT COUNT(*) FROM AppLibrary";
            long count;
            return long.TryParse(dbHelper.GetFieldValue(sql), out count) ? count : 0;
        }
        /// <summary>
        /// 根据主键查询一条记录
        /// </summary>
        public RoadFlow.Data.Model.AppLibrary Get(Guid id)
        {
            string sql = "SELECT * FROM AppLibrary WHERE ID=:ID";
            OracleParameter[] parameters = new OracleParameter[]{
				new OracleParameter(":ID", OracleDbType.Varchar2){ Value = id }
			};
            OracleDataReader dataReader = dbHelper.GetDataReader(sql, parameters);
            List<RoadFlow.Data.Model.AppLibrary> List = DataReaderToList(dataReader);
            dataReader.Close();
            return List.Count > 0 ? List[0] : null;
        }

        /// <summary>
        /// 得到一页数据
        /// </summary>
        /// <param name="pager"></param>
        /// <param name="query"></param>
        /// <param name="order"></param>
        /// <param name="size"></param>
        /// <param name="numbe"></param>
        /// <param name="title"></param>
        /// <param name="type"></param>
        /// <param name="address"></param>
        /// <returns></returns>
        public List<RoadFlow.Data.Model.AppLibrary> GetPagerData(out string pager, string query = "", string order = "Type,Title", int size = 15, int number = 1, string title = "", string type = "", string address = "")
        {
            StringBuilder WHERE = new StringBuilder();
            List<OracleParameter> parList = new List<OracleParameter>();
            if (!title.IsNullOrEmpty())
            {
                WHERE.Append("AND INSTR(Title,:Title,1,1)>0 ");
                parList.Add(new OracleParameter(":Title", OracleDbType.NVarchar2) { Value = title });
            }
            if (!type.IsNullOrEmpty())
            {
                WHERE.AppendFormat("AND Type IN({0}) ", RoadFlow.Utility.Tools.GetSqlInString(type));
            }
            if (!address.IsNullOrEmpty())
            {
                WHERE.Append("AND INSTR(Address,:Address,1,1)>0 ");
                parList.Add(new OracleParameter(":Address", OracleDbType.Varchar2) { Value = address });
            }
            long count;
            string sql = dbHelper.GetPaerSql("AppLibrary", "*", WHERE.ToString(), order, size, number, out count, parList.ToArray());
            pager = RoadFlow.Utility.Tools.GetPagerHtml(count, size, number, query);
            OracleDataReader dataReader = dbHelper.GetDataReader(sql, parList.ToArray());
            List<RoadFlow.Data.Model.AppLibrary> List = DataReaderToList(dataReader);
            dataReader.Close();
            return List;
        }

        /// <summary>
        /// 查询一个类别下所有记录
        /// </summary>
        public List<RoadFlow.Data.Model.AppLibrary> GetAllByType(string types)
        {
            string sql = "SELECT * FROM AppLibrary WHERE Type IN(" + RoadFlow.Utility.Tools.GetSqlInString(types) + ")";
            OracleDataReader dataReader = dbHelper.GetDataReader(sql);
            List<RoadFlow.Data.Model.AppLibrary> List = DataReaderToList(dataReader);
            dataReader.Close();
            return List;
        }

        /// <summary>
        /// 删除记录
        /// </summary>
        public int Delete(string[] idArray)
        {
            string sql = "DELETE FROM AppLibrary WHERE ID IN(" + RoadFlow.Utility.Tools.GetSqlInString(idArray) + ")";
            return dbHelper.Execute(sql);
        }

        /// <summary>
        /// 根据代码查询一条记录
        /// </summary>
        public RoadFlow.Data.Model.AppLibrary GetByCode(string code)
        {
            string sql = "SELECT * FROM AppLibrary WHERE Code=:Code";
            OracleParameter[] parameters = new OracleParameter[]{
				new OracleParameter(":Code", OracleDbType.Varchar2, 50){ Value = code }
			};
            OracleDataReader dataReader = dbHelper.GetDataReader(sql, parameters);
            List<RoadFlow.Data.Model.AppLibrary> List = DataReaderToList(dataReader);
            dataReader.Close();
            return List.Count > 0 ? List[0] : null;
        }
    }
}