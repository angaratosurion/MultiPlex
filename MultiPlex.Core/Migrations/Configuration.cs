namespace MultiPlex.Core.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Data.Models;

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

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            var userStore = new UserStore<ApplicationUser>(context);
            var mngr = new UserManager<ApplicationUser>(userStore);
            context.Roles.AddOrUpdate(r => r.Name, new IdentityRole { Name = "Administrators" });
            Data.Models.ApplicationUser adm = new ApplicationUser();
            adm.Email = "admin@localhost.com";

            mngr.Create(adm, "Adm!n0");
            context.SaveChanges();
            IdentityRole adrol = context.Roles.First(x => x.Name == "Administrators");
            adm = mngr.FindByEmail("admin@localhost.com");
            mngr.AddToRole(adm.Id, adrol.Name);
        }
    }
}
