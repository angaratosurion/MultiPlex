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
            modelBuilder.Entity<WikiContent>()
                .HasRequired(s => s.WrittenBy)
                .WithMany().WillCascadeOnDelete(false);
            modelBuilder.Entity<WikiTitle>()
                .HasRequired(t => t.WrittenBy)
                .WithMany().WillCascadeOnDelete(false);




            modelBuilder.Entity<WikiTitle>()
                .HasRequired(t=>t.Wiki)
                .WithMany().WillCascadeOnDelete(false);

            modelBuilder.Entity<WikiFile>()
                .HasRequired(f => f.Wiki)
                .WithMany()
                .WillCascadeOnDelete(false);

          


        }
        public static Context Create()
        {
            return new Context();
        }

        public IDbSet<WikiContent > Content { get; set; }
        public IDbSet<WikiTitle> Title { get; set; }
        public IDbSet<Models.Wiki> Wikis { get; set; }
        public IDbSet<WikiCategory> Categories { get; set; }
        public IDbSet<WikiFile> Files { get; set; }
    }
}
