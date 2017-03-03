using System;
using System.Data;
using System.Data.Common;

namespace DataAccess
{
    public abstract class DBManagerBase
    {
        protected DatabaseHelper databaseHelper = null;
        protected DbDataReader dbDataReader = null;
        protected DataSet dataSet = null;
        protected DBEnum.ProviderType providerType;
        protected String connectionString = String.Empty;

        public bool IsOpen { get; set; }

        public string ConnectionString { get; set; }

        public DBEnum.ProviderType DBProvider { get; set; }

        public DataSet DBSet
        {
            get
            {
                return dataSet;
            }

        }

        public DbDataReader DBReader
        {
            get
            {
                return dbDataReader;
            }

        }

        public DbConnection Connection
        {
            get
            {
                return databaseHelper.Connection;
            }
        }

        public DbCommand Command
        {
            get
            {
                return databaseHelper.Command;
            }
        }

        protected void Open(string connectionString)
        {
            databaseHelper = new DatabaseHelper(connectionString, DBProvider);
        }

        protected void Close()
        {
            if (dbDataReader != null)
                if (!dbDataReader.IsClosed)
                    dbDataReader.Close();
            databaseHelper.Dispose();
        }

        public void BeginTransaction()
        {
            databaseHelper.BeginTransaction();
        }

        public void CommitTransaction()
        {
            databaseHelper.CommitTransaction();
        }

        public void RollbackTransaction()
        {
            databaseHelper.RollbackTransaction();
        }
    }
}
