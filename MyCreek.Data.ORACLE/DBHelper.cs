using System;
using System.Text;
using System.Data;
using Oracle.ManagedDataAccess.Client;
using System.Collections.Generic;

namespace MyCreek.Data.ORACLE
{
    /// <summary>
    /// Oracle助手类
    /// </summary>
    public class DBHelper
    {
        private string connectionString;

        public DBHelper()
        {
            this.connectionString = MyCreek.Utility.Config.PlatformConnectionStringORACLE;
        }
        public DBHelper(string connString)
        {
            this.connectionString = connString;
        }

        /// <summary>
        /// 连接字符串
        /// </summary>
        public string ConnectionString
        {
            get { return this.connectionString; }
        }

        /// <summary>
        /// 释放连接
        /// </summary>
        public void Dispose()
        {

        }

        public void BulkCopy(System.Data.DataTable dt)
        {
            using (OracleConnection conn = new OracleConnection(ConnectionString))
            {
                string sql = "select * from " + dt.TableName + " where 1=0";
                using (OracleDataAdapter dap = new OracleDataAdapter(sql, conn))
                {
                    DataTable dt1 = new DataTable();
                    dap.Fill(dt1);
                    foreach (DataRow dr in dt.Rows)
                    {
                        dt1.ImportRow(dr);
                    }
                    OracleCommandBuilder ocb = new OracleCommandBuilder(dap);
                    dap.Update(dt1);
                }


                //using (OracleBulkCopy bulkCopy = new OracleBulkCopy(ConnectionString, OracleBulkCopyOptions.UseInternalTransaction))
                //{
                //    bulkCopy.DestinationTableName = dt.TableName;
                //    bulkCopy.BatchSize = dt.Rows.Count;
                //    try
                //    {
                //        conn.Open();
                //        if (dt != null && dt.Rows.Count > 0)
                //        {
                //            bulkCopy.WriteToServer(dt);
                //        }
                //    }
                //    catch (Exception ex)
                //    {
                //        throw ex;
                //    }
                //}
            }
        }

        /// <summary>
        /// 得到一个OracleDataReader
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <returns></returns>
        public OracleDataReader GetDataReader(string sql)
        {
            OracleConnection conn = new OracleConnection(ConnectionString);
            conn.Open();
            using (OracleCommand cmd = new OracleCommand(sql, conn))
            {
                cmd.Prepare();
                return cmd.ExecuteReader(CommandBehavior.CloseConnection);
            }
            
        }

        /// <summary>
        /// 得到一个OracleDataReader
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <returns></returns>
        public OracleDataReader GetDataReader(string sql, OracleParameter[] parameter)
        {
            OracleConnection conn = new OracleConnection(ConnectionString);
            conn.Open();
            using (OracleCommand cmd = new OracleCommand(sql, conn))
            {
                if (parameter != null && parameter.Length > 0)
                    cmd.Parameters.AddRange(parameter);
                OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                cmd.Parameters.Clear();
                cmd.Prepare();
                return dr;
            }
        }

        /// <summary>
        /// 得到一个DataTable
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <returns></returns>
        public DataTable GetDataTable(string sql)
        {
            using (OracleConnection conn = new OracleConnection(ConnectionString))
            {
                conn.Open();
                using (OracleCommand cmd = new OracleCommand(sql, conn))
                {
                    OracleDataReader dr = cmd.ExecuteReader();
                    DataTable dt = new DataTable();
                    dt.Load(dr);
                    dr.Close();
                    dr.Dispose();
                    cmd.Prepare();
                    return dt;
                }
            }
        }

        /// <summary>
        /// 得到一个DataTable
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <returns></returns>
        public DataTable GetDataTable(string sql, OracleParameter[] parameter)
        {
            using (OracleConnection conn = new OracleConnection(ConnectionString))
            {
                conn.Open();
                using (OracleCommand cmd = new OracleCommand(sql, conn))
                {
                    if (parameter != null && parameter.Length > 0)
                        cmd.Parameters.AddRange(parameter);
                    OracleDataReader dr = cmd.ExecuteReader();
                    DataTable dt = new DataTable();
                    dt.Load(dr);
                    dr.Close();
                    dr.Dispose();
                    cmd.Parameters.Clear();
                    cmd.Prepare();
                    return dt;
                }
            }
        }

        /// <summary>
        /// 得到数据集
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public DataSet GetDataSet(string sql)
        {
            using (OracleConnection conn = new OracleConnection(ConnectionString))
            {
                conn.Open();
                using (OracleDataAdapter dap = new OracleDataAdapter(sql, conn))
                {
                    DataSet ds = new DataSet();
                    dap.Fill(ds);
                    return ds;
                }
            }
        }

        /// <summary>
        /// 执行SQL
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public int Execute(string sql)
        {
            using (OracleConnection conn = new OracleConnection(ConnectionString))
            {
                conn.Open();
                using (OracleCommand cmd = new OracleCommand(sql, conn))
                {
                    cmd.Prepare();
                    return cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// 执行SQL(事务)
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public int Execute(List<string> sqlList)
        {
            using (OracleConnection conn = new OracleConnection(ConnectionString))
            {
                conn.Open();
                using (OracleCommand cmd = new OracleCommand())
                {
                    int i = 0;
                    cmd.Connection = conn;
                    foreach (string sql in sqlList)
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = sql;
                        cmd.Prepare();
                        i += cmd.ExecuteNonQuery();
                    }
                    return i;
                }
            }
        }

        /// <summary>
        /// 执行带参数的SQL
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public int Execute(string sql, OracleParameter[] parameter)
        {
            using (OracleConnection conn = new OracleConnection(ConnectionString))
            {
                conn.Open();
                using (OracleCommand cmd = new OracleCommand(sql, conn))
                {
                    if (parameter != null && parameter.Length > 0)
                        cmd.Parameters.AddRange(parameter);
                    int i = cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();
                    cmd.Prepare();
                    return i;
                }
            }
        }

        /// <summary>
        /// 执行SQL(事务)
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public int Execute(List<string> sqlList, List<OracleParameter[]> parameterList)
        {
            if (sqlList.Count > parameterList.Count)
            {
                throw new Exception("参数错误");
            }
            using (OracleConnection conn = new OracleConnection(ConnectionString))
            {
                conn.Open();
                using (OracleCommand cmd = new OracleCommand())
                {
                    int i = 0;
                    cmd.Connection = conn;
                    for (int j = 0; j < sqlList.Count; j++)
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = sqlList[j];
                        if (parameterList[j] != null && parameterList[j].Length > 0)
                        {
                            cmd.Parameters.AddRange(parameterList[j]);
                        }
                        i += cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();
                        cmd.Prepare();
                    }
                    return i;
                }
            }
        }

        /// <summary>
        /// 得到一个字段的值
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public string ExecuteScalar(string sql)
        {
            using (OracleConnection conn = new OracleConnection(ConnectionString))
            {
                conn.Open();
                using (OracleCommand cmd = new OracleCommand(sql, conn))
                {
                    object obj = cmd.ExecuteScalar();
                    cmd.Prepare();
                    return obj != null ? obj.ToString() : string.Empty;
                }
            }
        }

        /// <summary>
        /// 得到一个字段的值
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public string ExecuteScalar(string sql, OracleParameter[] parameter)
        {
            using (OracleConnection conn = new OracleConnection(ConnectionString))
            {
                conn.Open();
                using (OracleCommand cmd = new OracleCommand(sql, conn))
                {
                    if (parameter != null && parameter.Length > 0)
                        cmd.Parameters.AddRange(parameter);
                    object obj = cmd.ExecuteScalar();
                    cmd.Parameters.Clear();
                    cmd.Prepare();
                    return obj != null ? obj.ToString() : string.Empty;
                }
            }
        }
        /// <summary>
        /// 得到一个字段的值
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public string GetFieldValue(string sql)
        {
            return ExecuteScalar(sql);
        }
        /// <summary>
        /// 得到一个字段的值
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public string GetFieldValue(string sql, OracleParameter[] parameter)
        {
            return ExecuteScalar(sql, parameter);
        }

        /// <summary>
        /// 获取一个sql的字段名称
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <returns></returns>
        public string GetFields(string sql, OracleParameter[] param)
        {
            using (OracleConnection conn = new OracleConnection(ConnectionString))
            {
                conn.Open();
                System.Text.StringBuilder names = new System.Text.StringBuilder(500);
                using (OracleCommand cmd = new OracleCommand(sql, conn))
                {
                    if (param != null && param.Length > 0)
                        cmd.Parameters.AddRange(param);
                    OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.SchemaOnly);
                    for (int i = 0; i < dr.FieldCount; i++)
                    {
                        names.Append("[" + dr.GetName(i) + "]" + (i < dr.FieldCount - 1 ? "," : string.Empty));
                    }
                    cmd.Parameters.Clear();
                    dr.Close();
                    dr.Dispose();
                    cmd.Prepare();
                    return names.ToString();
                }
            }
        }

        /// <summary>
        /// 获取一个sql的字段名称
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="tableName">表名 </param>
        /// <returns></returns>
        public string GetFields(string sql, OracleParameter[] param, out string tableName)
        {
            using (OracleConnection conn = new OracleConnection(ConnectionString))
            {
                conn.Open();
                System.Text.StringBuilder names = new System.Text.StringBuilder(500);
                using (OracleCommand cmd = new OracleCommand(sql, conn))
                {
                    if (param != null && param.Length > 0)
                        cmd.Parameters.AddRange(param);
                    OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.SchemaOnly);
                    tableName = dr.GetSchemaTable().TableName;
                    for (int i = 0; i < dr.FieldCount; i++)
                    {
                        names.Append("[" + dr.GetName(i) + "]" + (i < dr.FieldCount - 1 ? "," : string.Empty));
                    }
                    cmd.Parameters.Clear();
                    dr.Close();
                    dr.Dispose();
                    cmd.Prepare();
                    return names.ToString();
                }
            }
        }

        /// <summary>
        /// 得到分页sql
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public string GetPaerSql(string sql, int size, int number, out long count, OracleParameter[] param = null)
        {
            string count1 = GetFieldValue(string.Format("select count(*) from ({0}) PagerCountTemp", sql), param);
            long i;
            count = count1.IsLong(out i) ? i : 0;

            StringBuilder sql1 = new StringBuilder();
            sql1.Append("select * from (");
            sql1.Append(sql);
            sql1.AppendFormat(") PagerTempTable");
            if (count > size)
            {
                sql1.AppendFormat(" WHERE PagerAutoRowNumber BETWEEN {0} AND {1}", number * size - size + 1, number * size);
            }

            return sql1.ToString();
        }

        /// <summary>
        /// 得到分页sql
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public string GetPaerSql(string table, string fileds, string where, string order, int size, int number, out long count, OracleParameter[] param = null)
        {
            string where1 = string.Empty;
            if (where.IsNullOrEmpty())
            {
                where1 = "";
            }
            else
            {
                where1 = where.Trim();
                if (where1.StartsWith("and", StringComparison.CurrentCultureIgnoreCase))
                {
                    where1 = where1.Substring(3);
                }
            }
            string where2 = where1.IsNullOrEmpty() ? "" : "WHERE " + where1;
            string sql = string.Format("SELECT PagerTempTable.*,ROWNUM AS PagerAutoRowNumber FROM (SELECT {0} FROM {1} {2} ORDER BY {3}) PagerTempTable", fileds, table, where2, order);


            string count1 = GetFieldValue(string.Format("SELECT COUNT(*) FROM {0} {1}", table, where2), param);
            long i;
            count = count1.IsLong(out i) ? i : 0;

            StringBuilder sql1 = new StringBuilder();
            sql1.Append("SELECT * FROM (");
            sql1.Append(sql);
            sql1.AppendFormat(") PagerTempTable");
            if (count > size)
            {
                sql1.AppendFormat(" WHERE PagerAutoRowNumber BETWEEN {0} AND {1}", number * size - size + 1, number * size);
            }

            return sql1.ToString();
        }

        
    }
}
