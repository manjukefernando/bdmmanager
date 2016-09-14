using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MBDM.API.Services.Member {
    public class MemberServiceHandler {
        public string GetAllMembers()
        {
            var data = new MemberService().GetAllMembers();
            var output = "";
            while (data.Read())
            {
                output =  Convert.ToString(data["CurrentDT"]);
            }

            return output;
        }
    }
}