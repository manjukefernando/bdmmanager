using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MBDM.Core.DataAccess.Provider {
    internal class MsSql : IProvider {

        public IDbConnection GetConnection() {
            return new SqlConnection();
        }

        public IDbCommand GetCommand() {
            return new SqlCommand();
        }

        public IDbDataAdapter GetAdapter() {
            return new SqlDataAdapter();
        }

        public IDbDataParameter GetParameter() {
            return new SqlParameter();
        }

        public void PrepareCommand(IDbConnection connection, IDbCommand command, CommandType commandType, string commandText, List<IDbDataParameter> parameters) {
           
                command.Connection = connection;
                command.CommandType = commandType;
                command.CommandText = commandText;

                foreach (var param in parameters) {
                    if (param.Direction == ParameterDirection.InputOutput && param.Value == null) {
                        param.Value = DBNull.Value;
                    }
                    command.Parameters.Add(param);
                }
           
        }

        private static void DisplayResults(IDataReader reader) {
            while (reader.Read()) {
                Console.WriteLine($"{reader.GetValue(2)}");
            }
        }



        public IDataReader GetAsyncReader(IDbCommand command, IDbConnection connection) {
                var result = ((SqlCommand)command).BeginExecuteReader();

                using (var sqlReader = ((SqlCommand)command).EndExecuteReader(result)) {
                    DisplayResults(sqlReader);
                    return sqlReader;
                }
           
        }
    }
}
