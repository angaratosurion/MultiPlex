using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MultiPlex.Core.Data.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace MultiPlex.Core.Data
{
    public class Context : IdentityDbContext<ApplicationUser>
    {
        public Context()
            : base("DefaultConnection")
        { 
           
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Properties()
            //        .Where(p => p.Name == "id")
            //        .Configure(p => p.IsKey()); 
           
            
            
            base.OnModelCreating(modelBuilder);
        }
        public static Context Create()
        {
            return new Context();
        }

        public DbSet<Content > Content { get; set; }
        public DbSet<Title> Title { get; set; }
        public DbSet<WikiModel> Wikis { get; set; }

    }
}
