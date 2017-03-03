namespace DataAccess
{
    public class DBEnum
    {
        #region Enums

        public enum ProviderType
        {
            SqlServer,
            MySql
        }

        public enum DatabaseConnectionState
        {
            KeepOpen, CloseOnExit
        }

        public enum StoredProcedureParameterDirection
        {
            Input, InputOutput, Output, ReturnValue
        }

        #endregion
    }
}
