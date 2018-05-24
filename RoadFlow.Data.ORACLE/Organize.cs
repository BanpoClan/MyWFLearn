using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Oracle.ManagedDataAccess.Client;

namespace RoadFlow.Data.ORACLE
{
    public class Organize : RoadFlow.Data.Interface.IOrganize
    {
        private DBHelper dbHelper = new DBHelper();
        /// <summary>
        /// 构造函数
        /// </summary>
        public Organize()
        {
        }
        /// <summary>
        /// 添加记录
        /// </summary>
        /// <param name="model">RoadFlow.Data.Model.Organize实体类</param>
        /// <returns>操作所影响的行数</returns>
        public int Add(RoadFlow.Data.Model.Organize model)
        {
            string sql = @"INSERT INTO Organize
				(ID,Name,Number1,Type,Status,ParentID,Sort,Depth,ChildsLength,ChargeLeader,Leader,Note) 
				VALUES(:ID,:Name,:Number1,:Type,:Status,:ParentID,:Sort,:Depth,:ChildsLength,:ChargeLeader,:Leader,:Note)";
            OracleParameter[] parameters = new OracleParameter[]{
				new OracleParameter(":ID", OracleDbType.Varchar2, 40){ Value = model.ID },
				new OracleParameter(":Name", OracleDbType.Varchar2, 2000){ Value = model.Name },
				new OracleParameter(":Number1", OracleDbType.Varchar2, 900){ Value = model.Number },
				new OracleParameter(":Type", OracleDbType.Int32){ Value = model.Type },
				new OracleParameter(":Status", OracleDbType.Int32){ Value = model.Status },
				new OracleParameter(":ParentID", OracleDbType.Varchar2, 40){ Value = model.ParentID },
				new OracleParameter(":Sort", OracleDbType.Int32){ Value = model.Sort },
				new OracleParameter(":Depth", OracleDbType.Int32){ Value = model.Depth },
				new OracleParameter(":ChildsLength", OracleDbType.Int32){ Value = model.ChildsLength },
				model.ChargeLeader == null ? new OracleParameter(":ChargeLeader", OracleDbType.Varchar2, 200) { Value = DBNull.Value } : new OracleParameter(":ChargeLeader", OracleDbType.Varchar2, 200) { Value = model.ChargeLeader },
				model.Leader == null ? new OracleParameter(":Leader", OracleDbType.Varchar2, 200) { Value = DBNull.Value } : new OracleParameter(":Leader", OracleDbType.Varchar2, 200) { Value = model.Leader },
				model.Note == null ? new OracleParameter(":Note", OracleDbType.NVarchar2) { Value = DBNull.Value } : new OracleParameter(":Note", OracleDbType.NVarchar2) { Value = model.Note }
			};
            return dbHelper.Execute(sql, parameters);
        }
        /// <summary>
        /// 更新记录
        /// </summary>
        /// <param name="model">RoadFlow.Data.Model.Organize实体类</param>
        public int Update(RoadFlow.Data.Model.Organize model)
        {
            string sql = @"UPDATE Organize SET 
				Name=:Name,Number1=:Number1,Type=:Type,Status=:Status,ParentID=:ParentID,Sort=:Sort,Depth=:Depth,ChildsLength=:ChildsLength,ChargeLeader=:ChargeLeader,Leader=:Leader,Note=:Note
				WHERE ID=:ID";
            OracleParameter[] parameters = new OracleParameter[]{
				new OracleParameter(":Name", OracleDbType.Varchar2, 2000){ Value = model.Name },
				new OracleParameter(":Number1", OracleDbType.Varchar2, 900){ Value = model.Number },
				new OracleParameter(":Type", OracleDbType.Int32){ Value = model.Type },
				new OracleParameter(":Status", OracleDbType.Int32){ Value = model.Status },
				new OracleParameter(":ParentID", OracleDbType.Varchar2, 40){ Value = model.ParentID },
				new OracleParameter(":Sort", OracleDbType.Int32){ Value = model.Sort },
				new OracleParameter(":Depth", OracleDbType.Int32){ Value = model.Depth },
				new OracleParameter(":ChildsLength", OracleDbType.Int32){ Value = model.ChildsLength },
				model.ChargeLeader == null ? new OracleParameter(":ChargeLeader", OracleDbType.Varchar2, 200) { Value = DBNull.Value } : new OracleParameter(":ChargeLeader", OracleDbType.Varchar2, 200) { Value = model.ChargeLeader },
				model.Leader == null ? new OracleParameter(":Leader", OracleDbType.Varchar2, 200) { Value = DBNull.Value } : new OracleParameter(":Leader", OracleDbType.Varchar2, 200) { Value = model.Leader },
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
            string sql = "DELETE FROM Organize WHERE ID=:ID";
            OracleParameter[] parameters = new OracleParameter[]{
				new OracleParameter(":ID", OracleDbType.Varchar2){ Value = id }
			};
            return dbHelper.Execute(sql, parameters);
        }
        /// <summary>
        /// 将DataRedar转换为List
        /// </summary>
        private List<RoadFlow.Data.Model.Organize> DataReaderToList(OracleDataReader dataReader)
        {
            List<RoadFlow.Data.Model.Organize> List = new List<RoadFlow.Data.Model.Organize>();
            RoadFlow.Data.Model.Organize model = null;
            while (dataReader.Read())
            {
                model = new RoadFlow.Data.Model.Organize();
                model.ID = dataReader.GetString(0).ToGuid();
                model.Name = dataReader.GetString(1);
                model.Number = dataReader.GetString(2);
                model.Type = dataReader.GetInt32(3);
                model.Status = dataReader.GetInt32(4);
                model.ParentID = dataReader.GetString(5).ToGuid();
                model.Sort = dataReader.GetInt32(6);
                model.Depth = dataReader.GetInt32(7);
                model.ChildsLength = dataReader.GetInt32(8);
                if (!dataReader.IsDBNull(9))
                    model.ChargeLeader = dataReader.GetString(9);
                if (!dataReader.IsDBNull(10))
                    model.Leader = dataReader.GetString(10);
                if (!dataReader.IsDBNull(11))
                    model.Note = dataReader.GetString(11);
                List.Add(model);
            }
            return List;
        }
        /// <summary>
        /// 查询所有记录
        /// </summary>
        public List<RoadFlow.Data.Model.Organize> GetAll()
        {
            string sql = "SELECT * FROM Organize";
            OracleDataReader dataReader = dbHelper.GetDataReader(sql);
            List<RoadFlow.Data.Model.Organize> List = DataReaderToList(dataReader);
            dataReader.Close();
            return List;
        }
        /// <summary>
        /// 查询记录数
        /// </summary>
        public long GetCount()
        {
            string sql = "SELECT COUNT(*) FROM Organize";
            long count;
            return long.TryParse(dbHelper.GetFieldValue(sql), out count) ? count : 0;
        }
        /// <summary>
        /// 根据主键查询一条记录
        /// </summary>
        public RoadFlow.Data.Model.Organize Get(Guid id)
        {
            string sql = "SELECT * FROM Organize WHERE ID=:ID";
            OracleParameter[] parameters = new OracleParameter[]{
				new OracleParameter(":ID", OracleDbType.Varchar2){ Value = id }
			};
            OracleDataReader dataReader = dbHelper.GetDataReader(sql, parameters);
            List<RoadFlow.Data.Model.Organize> List = DataReaderToList(dataReader);
            dataReader.Close();
            return List.Count > 0 ? List[0] : null;
        }


        /// <summary>
        /// 查询根记录
        /// </summary>
        public RoadFlow.Data.Model.Organize GetRoot()
        {
            string sql = "SELECT * FROM Organize WHERE ParentID=:ParentID";
            OracleParameter[] parameters = new OracleParameter[]{
				new OracleParameter(":ParentID", OracleDbType.Varchar2){ Value = Guid.Empty }
			};
            OracleDataReader dataReader = dbHelper.GetDataReader(sql, parameters);
            List<RoadFlow.Data.Model.Organize> List = DataReaderToList(dataReader);
            dataReader.Close();
            return List.Count > 0 ? List[0] : null;
        }

        /// <summary>
        /// 查询下级记录
        /// </summary>
        public List<RoadFlow.Data.Model.Organize> GetChilds(Guid ID)
        {
            string sql = "SELECT * FROM Organize WHERE ParentID=:ParentID ORDER BY Sort";
            OracleParameter[] parameters = new OracleParameter[]{
				new OracleParameter(":ParentID", OracleDbType.Varchar2){ Value = ID }
			};
            OracleDataReader dataReader = dbHelper.GetDataReader(sql, parameters);
            List<RoadFlow.Data.Model.Organize> List = DataReaderToList(dataReader);
            dataReader.Close();
            return List;
        }

        /// <summary>
        /// 得到最大排序值
        /// </summary>
        /// <returns></returns>
        public int GetMaxSort(Guid id)
        {
            string sql = "SELECT nvl(MAX(Sort),0)+1 FROM Organize WHERE ParentID=:ParentID";
            OracleParameter[] parameters = new OracleParameter[]{
				new OracleParameter(":ParentID", OracleDbType.Varchar2){ Value = id }
			};
            string sort = dbHelper.GetFieldValue(sql, parameters);
            return sort.ToInt();
        }

        /// <summary>
        /// 更新下级数
        /// </summary>
        /// <returns></returns>
        public int UpdateChildsLength(Guid id, int length)
        {
            string sql = "UPDATE Organize SET ChildsLength=:ChildsLength WHERE ID=:ID";
            OracleParameter[] parameters = new OracleParameter[]{
                new OracleParameter(":ChildsLength", OracleDbType.Int32){ Value = length },
				new OracleParameter(":ID", OracleDbType.Varchar2){ Value = id }
			};
            return dbHelper.Execute(sql, parameters);
        }

        /// <summary>
        /// 更新排序
        /// </summary>
        /// <returns></returns>
        public int UpdateSort(Guid id, int sort)
        {
            string sql = "UPDATE Organize SET Sort=:Sort WHERE ID=:ID";
            OracleParameter[] parameters = new OracleParameter[]{
                new OracleParameter(":Sort", OracleDbType.Int32){ Value = sort },
				new OracleParameter(":ID", OracleDbType.Varchar2){ Value = id }
			};
            return dbHelper.Execute(sql, parameters);
        }

        /// <summary>
        /// 查询一个组织的所有上级
        /// </summary>
        public List<RoadFlow.Data.Model.Organize> GetAllParent(string number)
        {
            string sql = "SELECT * FROM Organize WHERE ID IN(" + RoadFlow.Utility.Tools.GetSqlInString(number) + ") ORDER BY Depth";
            OracleDataReader dataReader = dbHelper.GetDataReader(sql);
            List<RoadFlow.Data.Model.Organize> List = DataReaderToList(dataReader);
            dataReader.Close();
            return List;
        }

        /// <summary>
        /// 查询一个组织的所有下级
        /// </summary>
        /// <param name="number">编号</param>
        /// <returns></returns>
        public List<RoadFlow.Data.Model.Organize> GetAllChild(string number)
        {
            string sql = "SELECT * FROM Organize WHERE NUMBER1 LIKE '" + number.ReplaceSql() + "%' ORDER BY Sort";
            OracleDataReader dataReader = dbHelper.GetDataReader(sql);
            List<RoadFlow.Data.Model.Organize> List = DataReaderToList(dataReader);
            dataReader.Close();
            return List;
        }

    }
}