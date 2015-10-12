﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MultiPlex.Core.Data;
using MultiPlex.Core.Managers;

namespace MultiPlex.Core
{
   public class CommonTools
    {
        public static Context db = new Context();
        public static FileManager filemngr;
        public static UserManager usrmng = new UserManager();
        public static WikiManager wkmngr = new WikiManager();
        public static CategoryManager catmngr = new CategoryManager();
        public static  Boolean isEmpty(string str)
       {
           try
           {
               Boolean ap = true;
               if (str != null && str != String.Empty)
               {
                   ap = false;
               }

               return ap;
           }
           catch (Exception)
           {
               
               throw;
               return true;
           }
       }
       public static void ErrorReporting (Exception ex)
       {
           throw (ex);
       }
    }
}
