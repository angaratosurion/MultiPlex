﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackCogs.Application;
using MultiPlex.Core.Data;
using MultiPlex.Core.Managers;

namespace MultiPlex.Core
{
   public  class CommonTools:BlackCogs.CommonTools
    {
        public static Context db = new Context();
        public static FileSystemManager filesysmngr = new FileSystemManager();
        public static WikiUserManager usrmng = new WikiUserManager();
        public static WikiManager wkmngr = new WikiManager();
        public static CategoryManager catmngr = new CategoryManager();
        public static TitleManager titlemngr = new TitleManager();
        public static WikiModeratorsInvitesManager wkinvmngr = new WikiModeratorsInvitesManager();
        
    }
}
