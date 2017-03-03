using System;
using System.Data;
using System.Data.Common;

namespace DataAccess
{
    public class DatabaseHelper : IDisposable
    {
        private string strConnectionString;
        private DbConnection objConnection;
        private DbCommand objCommand;
        private DbProviderFactory objFactory = null;
        //private ParameterCache parameterCache = ParameterCache.GetParameterCache();

        public DatabaseHelper()
        {
        }

        public DatabaseHelper(string connectionstring, DBEnum.ProviderType provider)
        {
            this.strConnectionString = connectionstring;
            objFactory = DBFactory.GetProvider(provider);
            objConnection = objFactory.CreateConnection();
            objCommand = objFactory.CreateCommand();
            objConnection.ConnectionString = this.strConnectionString;
            objCommand.Connection = objConnection;
        }

        #region AddParameter

        internal int AddParameter(string name, object value)
        {
            DbParameter dbParameter = objFactory.CreateParameter();
            dbParameter.ParameterName = name;
            dbParameter.Value = value;
            return objCommand.Parameters.Add(dbParameter);
        }

        internal int AddParameter(DbParameter parameter)
        {
            return objCommand.Parameters.Add(parameter);
        }

        internal int AddParameter(string name, DBEnum.StoredProcedureParameterDirection parameterDirection)
        {
            DbParameter parameter = objFactory.CreateParameter();
            parameter.ParameterName = name;
            parameter.Value = String.Empty;
            parameter.DbType = DbType.String;
            parameter.Size = 50;
            switch (parameterDirection)
            {
                case DBEnum.StoredProcedureParameterDirection.Input:
                    parameter.Direction = System.Data.ParameterDirection.Input;
                    break;
                case DBEnum.StoredProcedureParameterDirection.Output:
                    parameter.Direction = System.Data.ParameterDirection.Output;
                    break;
                case DBEnum.StoredProcedureParameterDirection.InputOutput:
                    parameter.Direction = System.Data.ParameterDirection.InputOutput;
                    break;
                case DBEnum.StoredProcedureParameterDirection.ReturnValue:
                    parameter.Direction = System.Data.ParameterDirection.ReturnValue;
                    break;
            }
            return objCommand.Parameters.Add(parameter);
        }

        internal int AddParameter(string name, object value, DBEnum.StoredProcedureParameterDirection parameterDirection)
        {
            DbParameter parameter = objFactory.CreateParameter();
            parameter.ParameterName = name;
            parameter.Value = value;
            parameter.DbType = DbType.String;
            parameter.Size = 50;
            switch (parameterDirection)
            {
                case DBEnum.StoredProcedureParameterDirection.Input:
                    parameter.Direction = System.Data.ParameterDirection.Input;
                    break;
                case DBEnum.StoredProcedureParameterDirection.Output:
                    parameter.Direction = System.Data.ParameterDirection.Output;
                    break;
                case DBEnum.StoredProcedureParameterDirection.InputOutput:
                    parameter.Direction = System.Data.ParameterDirection.InputOutput;
                    break;
                case DBEnum.StoredProcedureParameterDirection.ReturnValue:
                    parameter.Direction = System.Data.ParameterDirection.ReturnValue;
                    break;
            }
            return objCommand.Parameters.Add(parameter);
        }

        internal int AddParameter(string name, DBEnum.StoredProcedureParameterDirection parameterDirection, int size, DbType dbType)
        {
            DbParameter parameter = objFactory.CreateParameter();
            parameter.ParameterName = name;
            parameter.DbType = dbType;
            parameter.Size = size;
            switch (parameterDirection)
            {
                case DBEnum.StoredProcedureParameterDirection.Input:
                    parameter.Direction = System.Data.ParameterDirection.Input;
                    break;
                case DBEnum.StoredProcedureParameterDirection.Output:
                    parameter.Direction = System.Data.ParameterDirection.Output;
                    break;
                case DBEnum.StoredProcedureParameterDirection.InputOutput:
                    parameter.Direction = System.Data.ParameterDirection.InputOutput;
                    break;
                case DBEnum.StoredProcedureParameterDirection.ReturnValue:
                    parameter.Direction = System.Data.ParameterDirection.ReturnValue;
                    break;
            }
            return objCommand.Parameters.Add(parameter);
        }

        internal int AddParameter(string name, object value, DBEnum.StoredProcedureParameterDirection parameterDirection, int size, DbType dbType)
        {
            DbParameter parameter = objFactory.CreateParameter();
            parameter.ParameterName = name;
            parameter.Value = value;
            parameter.DbType = dbType;
            parameter.Size = size;
            switch (parameterDirection)
            {
                case DBEnum.StoredProcedureParameterDirection.Input:
                    parameter.Direction = System.Data.ParameterDirection.Input;
                    break;
                case DBEnum.StoredProcedureParameterDirection.Output:
                    parameter.Direction = System.Data.ParameterDirection.Output;
                    break;
                case DBEnum.StoredProcedureParameterDirection.InputOutput:
                    parameter.Direction = System.Data.ParameterDirection.InputOutput;
                    break;
                case DBEnum.StoredProcedureParameterDirection.ReturnValue:
                    parameter.Direction = System.Data.ParameterDirection.ReturnValue;
                    break;
            }
            return objCommand.Parameters.Add(parameter);
        }

        #endregion

        internal DbCommand Command
        {
            get
            {
                return objCommand;
            }
        }

        internal DbConnection Connection
        {
            get
            {
                return objConnection;
            }
        }

        #region Transaction

        internal void BeginTransaction()
        {
            if (objConnection.State == System.Data.ConnectionState.Closed)
            {
                objConnection.Open();
            }
            objCommand.Transaction = objConnection.BeginTransaction();
        }

        internal void CommitTransaction()
        {
            objCommand.Transaction.Commit();
            objConnection.Close();
        }

        internal void RollbackTransaction()
        {
            objCommand.Transaction.Rollback();
            objConnection.Close();
        }

        #endregion

        #region ExecuteNonQuery
        internal int ExecuteNonQuery(string query)
        {
            return ExecuteNonQuery(query, CommandType.Text, DBEnum.DatabaseConnectionState.CloseOnExit);
        }

        internal int ExecuteNonQuery(string query, CommandType commandtype)
        {
            return ExecuteNonQuery(query, commandtype, DBEnum.DatabaseConnectionState.CloseOnExit);
        }

        internal int ExecuteNonQuery(string query, DBEnum.DatabaseConnectionState connectionstate)
        {
            return ExecuteNonQuery(query, CommandType.Text, connectionstate);
        }

        internal int ExecuteNonQuery(string query, CommandType commandtype, DBEnum.DatabaseConnectionState connectionstate)
        {
            objCommand.CommandText = query;
            objCommand.CommandType = commandtype;

            int i = -1;
            try
            {
                if (objConnection.State == System.Data.ConnectionState.Closed)
                {
                    objConnection.Open();
                }

                i = objCommand.ExecuteNonQuery();
            }
            catch
            {
                throw;
            }

            finally
            {
                objCommand.Parameters.Clear();
                if (connectionstate == DBEnum.DatabaseConnectionState.CloseOnExit)
                {
                    objConnection.Close();
                }
            }
            return i;
        }

        #endregion

        #region Executescalar

        internal object ExecuteScalar(string query)
        {
            return ExecuteScalar(query, CommandType.Text, DBEnum.DatabaseConnectionState.CloseOnExit);
        }

        internal object ExecuteScalar(string query, CommandType commandtype)
        {
            return ExecuteScalar(query, commandtype, DBEnum.DatabaseConnectionState.CloseOnExit);
        }

        internal object ExecuteScalar(string query, DBEnum.DatabaseConnectionState connectionstate)
        {
            return ExecuteScalar(query, CommandType.Text, connectionstate);
        }

        internal object ExecuteScalar(string query, CommandType commandtype, DBEnum.DatabaseConnectionState connectionstate)
        {
            objCommand.CommandText = query;
            objCommand.CommandType = commandtype;
            object o = null;

            try
            {
                if (objConnection.State == System.Data.ConnectionState.Closed)
                {
                    objConnection.Open();
                }
                o = objCommand.ExecuteScalar();
            }
            catch
            {
                throw;
            }
            finally
            {
                objCommand.Parameters.Clear();
                if (connectionstate == DBEnum.DatabaseConnectionState.CloseOnExit)
                {
                    objConnection.Close();
                }
            }
            return o;
        }

        #endregion

        #region ExecuteReader

        internal DbDataReader ExecuteReader(string query)
        {
            return ExecuteReader(query, CommandType.Text, DBEnum.DatabaseConnectionState.CloseOnExit);
        }

        internal DbDataReader ExecuteReader(string query, CommandType commandtype)
        {
            return ExecuteReader(query, commandtype, DBEnum.DatabaseConnectionState.CloseOnExit);
        }

        internal DbDataReader ExecuteReader(string query, DBEnum.DatabaseConnectionState connectionstate)
        {
            return ExecuteReader(query, CommandType.Text, connectionstate);
        }

        internal DbDataReader ExecuteReader(string query, CommandType commandtype, DBEnum.DatabaseConnectionState connectionstate)
        {
            objCommand.CommandText = query;
            objCommand.CommandType = commandtype;
            DbDataReader reader = null;

            try
            {
                if (objConnection.State == System.Data.ConnectionState.Closed)
                {
                    objConnection.Open();
                }

                if (connectionstate == DBEnum.DatabaseConnectionState.CloseOnExit)
                {
                    reader = objCommand.ExecuteReader(CommandBehavior.CloseConnection);
                }

                else
                {
                    reader = objCommand.ExecuteReader();
                }
            }
            catch
            {
                throw;
            }

            finally
            {
                objCommand.Parameters.Clear();
            }

            return reader;
        }

        #endregion

        #region ExecuteDataset

        internal DataSet ExecuteDataSet(string query)
        {
            return ExecuteDataSet(query, CommandType.Text, DBEnum.DatabaseConnectionState.CloseOnExit);
        }

        internal DataSet ExecuteDataSet(string query, CommandType commandtype)
        {
            return ExecuteDataSet(query, commandtype, DBEnum.DatabaseConnectionState.CloseOnExit);
        }

        internal DataSet ExecuteDataSet(string query,DBEnum.DatabaseConnectionState connectionstate)
        {
            return ExecuteDataSet(query,  connectionstate);
        }

        internal DataSet ExecuteDataSet(string query, CommandType commandtype, DBEnum.DatabaseConnectionState connectionstate)
        {
            DbDataAdapter adapter = objFactory.CreateDataAdapter();
            objCommand.CommandText = query;
            objCommand.CommandType = commandtype;
            adapter.SelectCommand = objCommand;
            DataSet ds = new DataSet();

            try
            {
                adapter.Fill(ds);
            }

            catch
            {
                throw;
            }

            finally
            {
                objCommand.Parameters.Clear();
                if (connectionstate == DBEnum.DatabaseConnectionState.CloseOnExit)
                {
                    if (objConnection.State == System.Data.ConnectionState.Open)
                    {
                        objConnection.Close();
                    }
                }
            }
            return ds;
        }

        #endregion

        public void Dispose()
        {
            if (objConnection.State == ConnectionState.Open)
            {
                objConnection.Close();
                objConnection.Dispose();
            }
            objCommand.Dispose();
        }

        internal IDataReader ExecuteReader(string storedProcedureName, params object[] parameters)
        {
            objCommand.CommandText = storedProcedureName;
            objCommand.CommandType = CommandType.StoredProcedure;
            DbDataReader reader = null;

            try
            {
                RetrieveParameters(objCommand);
                SetParameterValues(objCommand, parameters);
                if (objConnection.State == System.Data.ConnectionState.Closed)
                {
                    objConnection.Open();
                }
                reader = objCommand.ExecuteReader();
            }
            catch
            {
                throw;
            }

            finally
            {
                objCommand.Parameters.Clear();
            }

            return reader;

        }

        internal void SetParameterValues(DbCommand objCommand, object[] parameters)
        {
            int index = 0;
            for (int i = 0; i < parameters.Length; i++)
            {
                DbParameter parameter = objCommand.Parameters[i + index];
                SetParameterValue(objCommand, parameter.ParameterName, parameters[i]);
            }
        }

        internal virtual void SetParameterValue(DbCommand dbCommand, string parameterName, object value)
        {
            dbCommand.Parameters[parameterName].Value = (value == null) ? DBNull.Value : value;
        }

        internal void RetrieveParameters(DbCommand dbCommand)
        {

            //if (parameterCache.ContainsParameters(Connection.ConnectionString, dbCommand.CommandText))
            //{
            //    DbParameter[] parameters = parameterCache.GetParameters(Connection.ConnectionString, dbCommand.CommandText);
            //    dbCommand.Parameters.AddRange(parameters);
            //}

            //else
            //{
            //    string connectionString = Connection.ConnectionString;
            //    dbCommand.Connection = Connection;
            //    Connection.Open();
            //    SqlCommandBuilder.DeriveParameters(dbCommand as SqlCommand);
            //    parameterCache.AddParameters(connectionString, dbCommand.CommandText, dbCommand.Parameters);

            //}

        }

        internal object GetParameter(string name)
        {
            return objCommand.Parameters[name].Value;
        }
    }
}
