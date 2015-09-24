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
     public IDbSet<Plugin> Plugins { get; set; }
        public IDbSet<Page> Pages { get; set; }
        public IDbSet<News> News { get; set; }
        public IDbSet<Category> Catgories { get; set; }
        public IDbSet<Tag> Tags { get; set; }
        public IDbSet<Files> Files { get; set; }
        public IDbSet<FileType> FileTypes { get; set; }
        public IDbSet<Comment> Comments { get; set; }
        public IDbSet<CommentThread> CommentThreads { get; set; }

    }
}
