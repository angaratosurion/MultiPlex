using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MultiPlex.Core.Data.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using BlackCogs.Data.Models;
namespace MultiPlex.Core.Data
{
    public class Context : BlackCogs.Data.Context//  IdentityDbContext<ApplicationUser>
    {
        //public Context()
        //    : base("DefaultConnection")
        //{ 
           
        //}
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Properties()
            //        .Where(p => p.Name == "id")
            //        .Configure(p => p.IsKey()); 
           
            
            
            base.OnModelCreating(modelBuilder);
            modelBuilder.Properties<DateTime>()
                  .Configure(c => c.HasColumnType("datetime2"));


            //modelBuilder.Entity<WikiContent>()
            //    .HasRequired(s => s.WrittenBy)
            //    .WithMany().WillCascadeOnDelete(false);
            //modelBuilder.Entity<WikiTitle>()
            //    .HasRequired(t => t.WrittenBy)
            //    .WithMany().WillCascadeOnDelete(false);


            //modelBuilder.Properties()
            //    .Where(p => p.Name == "Id")
            //    .Configure(p => p.HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity));
            //modelBuilder.Properties()
            //    .Where(p => p.Name == "RowVersion")
            //    .Configure(p => p.IsRowVersion());

            modelBuilder.Entity<WikiTitle>()
                .HasRequired(t=>t.Wiki)
                .WithMany().WillCascadeOnDelete(false);
            modelBuilder.Entity<WikiTitle>()
                .HasKey(p => p.TitleId);
            

            modelBuilder.Entity<WikiFile>()
                .HasRequired(f => f.Wiki)
                .WithMany()
                .WillCascadeOnDelete(false);
            modelBuilder.Entity<WikiModInvitations>()
               .HasRequired(f => f.Wiki)
               .WithMany()
               .WillCascadeOnDelete(false);
            //modelBuilder.Entity<Wiki>()
            //.HasRequired(f => f.Administrator)
            //.WithMany()
            //.WillCascadeOnDelete(false);




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
        public IDbSet<WikiModInvitations> ModInvites { get; set; }
      
    }
}
