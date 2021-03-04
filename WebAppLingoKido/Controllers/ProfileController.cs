using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebAppLingoKido.Controllers
{
    public class ProfileController : ApiController
    {

        //[AcceptVerbs()]
        public MiniUser GetUserByTell(string Tell)
        {
            MiniUser miniUser = new MiniUser();
            COM.User Usr = BLL.Profile.GetUserByTellNumber(Tell);
            if (Usr != null)
            {
                miniUser = new MiniUser()
                {
                    JoinDate = Usr.JoinDate,
                    Name = Usr.Name,
                    TellNumber = Usr.TellNumber,
                    UID = Usr.UID
                };
            }

            return null;
        }

        // GET: api/Profile
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Profile/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Profile
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Profile/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Profile/5
        public void Delete(int id)
        {
        }
    }

    public class MiniUser
    {
        public int UID { get; set; }
        public string Name { get; set; }
        public string TellNumber { get; set; }
        public DateTime JoinDate { get; set; }
        public int Role { get; set; }
    }
}
