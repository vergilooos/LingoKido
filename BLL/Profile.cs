using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class Profile
    {
        public static COM.User GetUserByTellNumber(string TellNumber)
        {
            try
            {
                using (var ent = DB.Entity)
                {
                    return ent
                        .Users
                        .Where( usr => usr.TellNumber == TellNumber)
                        .SingleOrDefault();
                }
            }
            catch (Exception e)
            {
                new System.Threading.Thread(delegate (){Log.DoLog(COM.Action.GetUserByTell, TellNumber, -100, e.Message);});
                return null;
            }
        }

        //add
        //update
        //getall
        //AUDG
    }
}
