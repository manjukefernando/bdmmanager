using System;
using System.Collections.Generic;
using System.Data;
using MBDM.Core.DataAccess;
using MBDM.Core.DataAccess.Provider;

namespace MBDM.Core
{
    public class DbManager : IDisposable {
        private static IProvider _provider;
        private static IDbConnection _connection = null;
        private static IDbCommand _command = null;
        private static IDbDataAdapter _adapter = null;
        private static IDataReader _dataReader = null;
        private static DataSet _dataSet = null;
        private static List<IDbDataParameter> _parameters = null;


        public DbManager(string connectionStringId) {
            _parameters = new List<IDbDataParameter>();
            _provider = ProviderFactory.CreateProvider(ProviderFactory.GetProvider());
            _connection = ConnectionManager.GetConnection(connectionStringId);
            _command = null;
            _adapter = null;
            _dataReader = null;
        }


        public DbManager()
            : this(Utility.GetDefaultConnectionStringId()) {
        }

        public DbManager(string connectionString, string provider) {
            _parameters = new List<IDbDataParameter>();
            _provider = ProviderFactory.CreateProvider(provider);
            _connection = ConnectionManager.GetConnection(connectionString, provider);
            _command = null;
            _adapter = null;
            _dataReader = null;
        }


        /// <summary>
        /// Adds a parameter to the parameter colleciton
        /// </summary>
        /// <param name="parameterName"></param>
        /// <param name="parameterValue"></param>
        /// <param name="parameterType"></param>
        /// <param name="parameterSize"></param>
        /// <param name="direction"></param>
        //public void AddParameter(string parameterName, object parameterValue, DbType? parameterType = null, int? parameterSize = null, ParameterDirection direction = ParameterDirection.Input) {
        public void AddParameter(string parameterName, object parameterValue, DbType? parameterType, int? parameterSize, ParameterDirection? direction) {
                var parameter = _provider.GetParameter();
                parameter.ParameterName = parameterName;
                parameter.Value = parameterValue;
                if (parameterType != null) {
                    parameter.DbType = (DbType)parameterType;
                }
                if (parameterSize != null) {
                    parameter.Size = (int)parameterSize;
                }
                if (direction != null) {
                    parameter.Direction = (ParameterDirection)direction;
                }
                else {
                    parameter.Direction = ParameterDirection.Input;
                }

                _parameters.Add(parameter);

        }

        /// <summary>
        /// Gets a Datareader for a given command text
        /// </summary>
        /// <param name="commandType"></param>
        /// <param name="commandText"></param>
        /// <returns>IDataReader</returns>
        public IDataReader GetReader(CommandType commandType, string commandText) {
            _dataReader = null;
            try {
                using (_command = _provider.GetCommand()) {
                    _provider.PrepareCommand(_connection, _command, commandType, commandText, _parameters);
                    _dataReader = _command.ExecuteReader();
                    _command.Parameters.Clear();
                    _parameters = new List<IDbDataParameter>();
                }
            }
            catch (Exception ex) {
                throw ex;
            }

            return _dataReader;
        }

        //public virtual IDataReader GetReaderAsynch(CommandType commandType, string commandText) {
        //    _DataReader = null;
        //    try {
        //        using (_Command = _Provider.GetCommand()) {
        //            _Provider.PrepareCommand(_Connection, _Command, commandType, commandText, _Parameters);
        //            IAsyncResult result = _Command
        //        }
        //    }
        //    catch (Exception ex) {
        //        throw ex;
        //    }

        //    return _DataReader;
        //}

        public IDbCommand GetAsyncCommand(CommandType commandType, string commandText) {
            try {
                using (_command = _provider.GetCommand()) {
                    _provider.PrepareCommand(_connection, _command, commandType, commandText, _parameters);
                    _parameters = new List<IDbDataParameter>();
                    return _command;
                }
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public IDataReader GetAsyncReader(CommandType commandType, string commandText) {
            _dataReader = null;
            try {
                using (_command = _provider.GetCommand()) {
                    _provider.PrepareCommand(_connection, _command, commandType, commandText, _parameters);
                    //_DataReader = _Command.ExecuteReader();
                    _provider.GetAsyncReader(_command, _connection);
                    _command.Parameters.Clear();
                    _parameters = new List<IDbDataParameter>();
                }
            }
            catch (Exception ex) {
                throw ex;
            }

            return _dataReader;
        }

        /// <summary>
        /// Gets a DataSet for a given command text
        /// </summary>
        /// <param name="commandType"></param>
        /// <param name="commandText"></param>
        /// <returns>DataSet</returns>
        public DataSet GetDataSet(CommandType commandType, string commandText) {
            _dataSet = new DataSet();
            try {
                using (_command = _provider.GetCommand()) {
                    _provider.PrepareCommand(_connection, _command, commandType, commandText, _parameters);
                    _adapter = _provider.GetAdapter();
                    _adapter.SelectCommand = _command;
                    _adapter.Fill(_dataSet);
                    _command.Parameters.Clear();
                    _parameters = new List<IDbDataParameter>();
                }
            }
            catch (Exception ex) {
                throw ex;
            }
            return _dataSet;
        }

        /// <summary>
        /// Executes the query and retruns the first column of the first row in the resultset returned by the query.
        /// </summary>
        /// <param name="commandType"></param>
        /// <param name="commandText"></param>
        /// <returns>object</returns>
        public object ExecuteScalar(CommandType commandType, string commandText) {
            object RetVal;
            try {
                using (_command = _provider.GetCommand()) {
                    _provider.PrepareCommand(_connection, _command, commandType, commandText, _parameters);
                    RetVal = _command.ExecuteScalar();
                    _command.Parameters.Clear();
                    _parameters = new List<IDbDataParameter>();
                }
            }
            catch (Exception ex) {
                throw ex;
            }
            return RetVal;
        }

        /// <summary>
        /// Executes a SQL statement against the connection object and returns the number of rows affected
        /// </summary>
        /// <param name="commandType"></param>
        /// <param name="commandText"></param>
        /// <returns>int</returns>
        public int ExecuteNonQuery(CommandType commandType, string commandText) {
            int RetVal = 0;
            try {
                using (_command = _provider.GetCommand()) {
                    _provider.PrepareCommand(_connection, _command, commandType, commandText, _parameters);
                    RetVal = _command.ExecuteNonQuery();
                    _command.Parameters.Clear();
                    _parameters = new List<IDbDataParameter>();
                }
            }
            catch (Exception ex) {
                throw ex;
            }
            return RetVal;
        }


        public void Dispose() {
            if (_connection != null && _connection.State != ConnectionState.Closed) {
                _connection.Close();
            }
        }

    }
}
