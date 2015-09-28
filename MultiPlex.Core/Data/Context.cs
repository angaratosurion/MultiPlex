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
            modelBuilder.Entity<Content>()
                .HasRequired(s => s.WrittenBy)
                .WithMany().WillCascadeOnDelete(false);
            modelBuilder.Entity<Title>()
                .HasRequired(t => t.WrittenBy)
                .WithMany().WillCascadeOnDelete(false);




            modelBuilder.Entity<Title>()
                .HasRequired(t=>t.Wiki)
                .WithMany().WillCascadeOnDelete(false);
          


        }
        public static Context Create()
        {
            return new Context();
        }

        public IDbSet<Content > Content { get; set; }
        public IDbSet<Title> Title { get; set; }
        public IDbSet<WikiModel> Wikis { get; set; }
        public IDbSet<Category> Categories { get; set; }

    }
}
