using MBDM.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MBDM.API.Services.Member;

namespace MBDM.API.Controllers
{
    [RoutePrefix("api/Members")]
    public class MemberController : ApiController
    {
        //For test purpose only
        private readonly Member[] _members = new Member[]
        {
            new Member {MemberId = 1, FirstName = "Manjuke", LastName = "Fernando", Address = "181, Stirling Road, #03-216",Email = "manjuke.fernando@airliquide.com", PostalCode ="140181"},
            new Member {MemberId = 2, FirstName = "ABC", LastName = "Fernando", Address = "Some Adress details",Email = "abc.fdo@airliquide.com", PostalCode ="999999"}
        };

        
        [Route("")]
        [HttpGet]
        public IEnumerable<Member> GetAllMembers()
        {
            return _members;
        }

        [Route("GetSystemDate")]
        [HttpGet]

        public IHttpActionResult GetDate()
        {
            var date = new MemberServiceHandler().GetAllMembers();
            if (date != null)
            {
                return Ok(date);
            }
            else
            {
                return NotFound();
            }
        }

        
        [Route("{memberId}")]
        [HttpGet]
        public IHttpActionResult GetMember(int memberId)
        {
            var member = _members.FirstOrDefault(m => m.MemberId == memberId);
            if (member == null)
            {
                return NotFound();
            }
            return Ok(member);
        }

        [Route("")]
        [HttpPost]
        public IHttpActionResult AddOrUpdateMember(Member member)
        {
            return Ok(/*Call member save service call */);
        }
    }
}
