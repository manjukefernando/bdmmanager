using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MBDM.API.Models {
    public class Member {
        public int MemberId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string PostalCode { get; set; }
        public string Email { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool ActiveStatus { get; set; }

    }
}