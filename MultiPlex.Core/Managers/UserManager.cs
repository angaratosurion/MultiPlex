using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MultiPlex.Core.Data;
using MultiPlex.Core.Data.Models;

namespace MultiPlex.Core.Managers
{
    public class UserManager
    {
        Context db = new Context();
        public ApplicationUser GetUser (string id)
        {
            try
            {
                ApplicationUser ap = null;
                if ( id !=null)
                {
                    ap = this.db.Users.First(u => u.Id == id);
                }
                return ap;
            }
            catch (Exception ex)
            {

                CommonTools.ErrorReporting(ex);
                return null;
            }
        }
    }
}
