namespace MultiPlex.Core.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Data.Models;
    using BlackCogs.Data.Models;
    public   class Configuration : DbMigrationsConfiguration<MultiPlex.Core.Data.Context>
    {
        public Configuration()
        {
            //AutomaticMigrationsEnabled = false;
            AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(MultiPlex.Core.Data.Context context)
        {
            //  This method will be called after migrating to the latest version.
            var userStore = new UserStore<ApplicationUser>(context);
            var mngr = new UserManager<ApplicationUser>(userStore);
            IdentityRole role = new IdentityRole("Administrators");
            context.Roles.AddOrUpdate(r => r.Name,role );
         
            //var passwordHash = new PasswordHasher();
            //string password = passwordHash.HashPassword("Adm!n0");
            //context.Users.AddOrUpdate(u => u.UserName,
            //    new ApplicationUser
            //    {
            //        UserName = "admin@localhost.com",
            //        PasswordHash = password,
            //        Email= "admin@localhost.com"
            //        //   PhoneNumber = "08869879"

            //    });




            context.SaveChanges();
            ApplicationUser adm = new ApplicationUser();
            adm.Email = "admin@localhost.com";
            mngr.Create(adm, "Adm!n0");
            IdentityRole adrol = context.Roles.First(x => x.Name == "Administrators");
            adm = mngr.FindByEmail("admin@localhost.com");
            if (adm != null)
            {
                mngr.AddToRole(adm.Id, adrol.Name);
            }
        }
    }
}
