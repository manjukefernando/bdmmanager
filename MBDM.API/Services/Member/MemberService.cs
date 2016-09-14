using System.Data;
using MBDM.Core;

namespace MBDM.API.Services.Member {
    public class MemberService {

        public IDataReader GetAllMembers() {
            const string dbQuery = "SELECT GETDATE() AS CurrentDT";
            var dbManager = new DbManager();
            return dbManager.GetReader(CommandType.Text, dbQuery);

        }
    }
}