using System;
using System.Configuration;
using System.Data;
using MBDM.Core.DataAccess.Provider;

namespace MBDM.Core.DataAccess {
    public class ConnectionManager {
        //private static string _DefaultConnectionString = "DefaultConnection";
        //private static string _DefaultConnectionStringEntry = "DefaultConnectionString";

        private static IDbConnection _iDbConnection;
        private static string _connectionString = string.Empty;


        /// <summary>
        /// Closes an open connection
        /// </summary>
        private static void CloseConnection() {
            try {
                if (_iDbConnection != null && _iDbConnection.State == ConnectionState.Open) {
                    _iDbConnection.Close();
                    _iDbConnection.Dispose();
                }
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        /// <summary>
        /// Opens a database connection for a given connection string id. The type of connection will be decided 
        /// using the provider element in the connection string
        /// </summary>
        /// <param name="connectionStringId"></param>
        /// <returns>IDbConnection</returns>
        public static IDbConnection GetConnection(string connectionStringId) {
            try {

                string conStringId = connectionStringId;

                if (connectionStringId.ToString() == "") {
                    conStringId = Utility.GetApplicationSetting(Utility.GetDefaultConnectionStringEntry());
                }

                IProvider Provider = ProviderFactory.CreateProvider(ProviderFactory.GetProvider(conStringId));
                _iDbConnection = Provider.GetConnection();
                _iDbConnection.ConnectionString = GetConnectionString(conStringId);
                if (_iDbConnection.State != ConnectionState.Open) {
                    _iDbConnection.Open();
                }
            }
            catch (Exception ex) {
                throw ex;
            }
            return _iDbConnection;
        }

        /// <summary>
        /// Opens database connection using given connection string and provider
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        public static IDbConnection GetConnection(string connectionString, string provider) {
            try {
                IProvider Provider = ProviderFactory.CreateProvider(provider);
                _iDbConnection = Provider.GetConnection();
                _iDbConnection.ConnectionString = connectionString;
                if (_iDbConnection.State != ConnectionState.Open) {
                    _iDbConnection.Open();
                }
            }
            catch (Exception ex) {
                throw ex;
            }
            return _iDbConnection;
        }


        /// <summary>
        /// Gets the connection string for a given key from the application's configuration file
        /// </summary>
        /// <param name="connectionStringId"></param>
        /// <returns>String containing the connection string</returns>
        public static string GetConnectionString(string connectionStringId) {
                //return ConfigurationManager.ConnectionStrings[connectionStringId].ConnectionString.Decrypt();
                return ConfigurationManager.ConnectionStrings[connectionStringId].ConnectionString;
        }

        /// <summary>
        /// Gets the default connection string 'DefaultConnection' from the application's configuration file
        /// </summary>
        /// <returns>String containing the connection string</returns>
        public static string GetConnectionString() {
            try {
                string defConString = Utility.GetApplicationSetting(Utility.GetDefaultConnectionStringEntry());
                if (defConString == null) {
                    defConString = Utility.GetDefaultConnectionStringId();
                }

                //return ConfigurationManager.ConnectionStrings[defConString].ConnectionString.Decrypt();
                return ConfigurationManager.ConnectionStrings[defConString].ConnectionString;
            }
            catch (Exception ex) {
                throw ex;
            }
        }


    }
}
