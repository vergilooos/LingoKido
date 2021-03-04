using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    internal static class DB
    {
        public static DAL.LingoKidoEntities Entity
        {
            get
            {
                var ent = new DAL.LingoKidoEntities();
                ent.Configuration.ValidateOnSaveEnabled = false;
                ent.Configuration.ProxyCreationEnabled = false;
                return ent;
            }
        }
    }
}
