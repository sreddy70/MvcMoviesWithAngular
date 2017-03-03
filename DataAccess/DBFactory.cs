using MySql.Data.MySqlClient;
using System.Data.Common;
using System.Data.SqlClient;

namespace DataAccess
{
    internal class DBFactory
    {
        private static DbProviderFactory objFactory = null;

        public static DbProviderFactory GetProvider(DBEnum.ProviderType provider)
        {
            switch (provider)
            {
                case DBEnum.ProviderType.SqlServer:
                    objFactory = SqlClientFactory.Instance;
                    break;
                case DBEnum.ProviderType.MySql:
                    objFactory = MySqlClientFactory.Instance;
                    break;                
            }
            return objFactory;
        }

        public static DbDataAdapter GetDataAdapter(DBEnum.ProviderType providerType)
        {
            switch (providerType)
            {
                case DBEnum.ProviderType.SqlServer:
                    return new SqlDataAdapter();
                case DBEnum.ProviderType.MySql:
                    return new MySqlDataAdapter();
                default:
                    return null;
            }
        }
    }
}
