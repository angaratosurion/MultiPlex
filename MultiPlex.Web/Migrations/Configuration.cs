namespace MultiPlex.Web.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Core.Data;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Core.Data.Models;
    using Microsoft.AspNet.Identity;
    using BlackCogs.Data.Models;
    internal sealed class Configuration : MultiPlex.Core.Migrations.Configuration// DbMigrationsConfiguration<MultiPlex.Web.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
        }
        public void pubSeed(Context context)
        {
            this.Seed(context);
        }            

        protected override void Seed(Context context)
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
            ApplicationUser adm = new ApplicationUser();
            
            

            adm.Email = "admin@localhost.com";

            mngr.Create(adm, "Adm!n0");
           
            context.SaveChanges();
            IdentityRole adrol = context.Roles.First(x => x.Name == "Administrators");
            adm = mngr.FindByEmail("admin@localhost.com");
            mngr.AddToRole(adm.Id, adrol.Name);
            context.SaveChanges();

        }
    }
}
