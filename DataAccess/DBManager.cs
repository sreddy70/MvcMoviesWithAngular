using System;
using System.Configuration;
using System.Data;
using System.Data.Common;

namespace DataAccess
{
    public sealed class DBManager : DBManagerBase
    {
        public DBManager() { }
        public DBManager(DBEnum.ProviderType provider)
        {
            DBProvider = provider;
        }

        #region Connection

        public void OpenConnection()
        {
            //connectionString = ConfigurationSettings.AppSettings["ConnectionString"].ToString();
            ConnectionStringSettings connectionSettings = ConfigurationManager.ConnectionStrings["MySqlLocalDb"];
            connectionString = connectionSettings.ConnectionString;
            base.Open(connectionString);
            base.IsOpen = true;
        }

        public void OpenConnection(String connectionString)
        {
            base.Open(connectionString);
            base.IsOpen = true;
        }

        public void CloseConnection()
        {
			if (base.IsOpen)
                base.Close();

            base.IsOpen = false;
        }

        #endregion

        #region AddParameter

        public int AddParameter(string name, object value)
        {
            return databaseHelper.AddParameter(name, value);
        }

        public int AddParameter(string name, DBEnum.StoredProcedureParameterDirection parameterDirection)
        {
            return databaseHelper.AddParameter(name, parameterDirection);

        }

        public int AddParameter(string name, object value, DBEnum.StoredProcedureParameterDirection parameterDirection)
        {
            return databaseHelper.AddParameter(name, value, parameterDirection);
        }

        public int AddParameter(string name, DBEnum.StoredProcedureParameterDirection parameterDirection, int size, DbType dbType)
        {
            return databaseHelper.AddParameter(name, parameterDirection, size, dbType);
        }

        public int AddParameter(string name, object value, DBEnum.StoredProcedureParameterDirection parameterDirection, int size, DbType dbType)
        {
            return databaseHelper.AddParameter(name, value, parameterDirection, size, dbType);
        }

        #endregion

        #region GetParameter

        public object GetParameter(string name)
        {
            return databaseHelper.GetParameter(name);
        }

        #endregion

        #region ExecuteReader

        public DbDataReader ExecuteReader(string query)
        {
            this.dbDataReader = databaseHelper.ExecuteReader(query);
            return this.dbDataReader;
        }

        public DbDataReader ExecuteReader(string query, CommandType commandtype)
        {
            this.dbDataReader = databaseHelper.ExecuteReader(query, commandtype, DBEnum.DatabaseConnectionState.CloseOnExit);
            return this.dbDataReader;
        }

        public IDataReader ExecuteReader(string storedProcedureName, params object[] parameters)
        {
            this.dbDataReader = (DbDataReader)databaseHelper.ExecuteReader(storedProcedureName, parameters);
            return this.dbDataReader;
        }

        public DbDataReader ExecuteReader(string query, CommandType commandtype, DBEnum.DatabaseConnectionState connectionstate)
        {
            this.dbDataReader = databaseHelper.ExecuteReader(query, commandtype, connectionstate);
            return this.dbDataReader;
        }

        public DbDataReader ExecuteReader(string query, DBEnum.DatabaseConnectionState connectionstate)
        {
            this.dbDataReader = databaseHelper.ExecuteReader(query, connectionstate);
            return this.dbDataReader;
        }

        #endregion

        #region ExecuteScalar

        public object ExecuteScalar(string query)
        {
            return databaseHelper.ExecuteScalar(query);
        }

        public object ExecuteScalar(string query, CommandType commandtype)
        {
            return databaseHelper.ExecuteScalar(query, commandtype);
        }

        public object ExecuteScalar(string query, DBEnum.DatabaseConnectionState connectionstate)
        {
            return databaseHelper.ExecuteScalar(query, connectionstate);
        }

        public object ExecuteScalar(string query, CommandType commandtype, DBEnum.DatabaseConnectionState connectionstate)
        {
            return databaseHelper.ExecuteScalar(query, commandtype, connectionstate);
        }

        #endregion

        #region ExecuteDataset

        public DataSet ExecuteDataSet(string query)
        {
            this.dataSet = databaseHelper.ExecuteDataSet(query);
            return this.dataSet;
        }

        public DataSet ExecuteDataSet(string query, CommandType commandtype)
        {
            this.dataSet = databaseHelper.ExecuteDataSet(query, commandtype);
            return this.dataSet;
        }

        public DataSet ExecuteDataSet(string query, CommandType commandtype, DBEnum.DatabaseConnectionState databaseConnectionState)
        {
            this.dataSet = databaseHelper.ExecuteDataSet(query, commandtype, databaseConnectionState);
            return this.dataSet;
        }

        #endregion

        #region ExecuteNonQuery

        public int ExecuteNonQuery(string query, CommandType commandtype)
        {
            return databaseHelper.ExecuteNonQuery(query, commandtype);
        }

        public int ExecuteNonQuery(string query, CommandType commandtype, DBEnum.DatabaseConnectionState databaseConnectionState)
        {
            return databaseHelper.ExecuteNonQuery(query, commandtype, databaseConnectionState);
        }

        #endregion
    }
}
