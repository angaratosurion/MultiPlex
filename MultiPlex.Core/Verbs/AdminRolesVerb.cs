﻿using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackCogs.Interfaces;
using MultiPlex.Core.Application;

namespace MultiPlex.Core.Verbs
{
    [Export(typeof(IActionVerb)), ExportMetadata("Category", "AdminNavigation")]
    public class AdminRolesVerb : IActionVerb
    {
        public string Action
        {
            get
            {
                return "GetRoles";
            }
        }

        public string Controller
        {
            get
            {
                return "WikiUser";
            }
        }

        public string Description
        {
            get
            {
                return "Here you Administrage the Existing User Roles on the Wiki";
            }
        }

        public bool isAdminPalnel
        {
            get
            {
                return true;
            }
        }

        public string Name
        {
            get
            {
                return "Roles Administration";
            }
        }
        public string Moduledescription
        {
            get
            {
                MultiPlexInfo inf = new MultiPlexInfo();
                return inf.Description;
            }
        }

        public string ModuleName
        {
            get
            {
                MultiPlexInfo inf = new MultiPlexInfo();
                return inf.Name;
            }
        }
    }
}
