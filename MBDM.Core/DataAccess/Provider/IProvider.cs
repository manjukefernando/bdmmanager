using System.Collections.Generic;
using System.Data;

namespace MBDM.Core.DataAccess.Provider {
    public interface IProvider {
        IDbConnection GetConnection();
        IDbCommand GetCommand();
        IDbDataAdapter GetAdapter();
        IDbDataParameter GetParameter();
        void PrepareCommand(IDbConnection connection, IDbCommand command, CommandType commandType, string commandText, List<IDbDataParameter> parameters);
        IDataReader GetAsyncReader(IDbCommand command, IDbConnection connection);
    }
}
