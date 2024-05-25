using Microsoft.EntityFrameworkCore;
using Task.Models;

namespace Task.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }
        public DbSet<Node> Nodes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Node>()
                .HasOne(n => n.ParentNode)
                .WithMany(n => n.ChildNodes)
                .HasForeignKey(n => n.ParentNodeId);
        }
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    // ...
        //    modelBuilder.Entity<Node>.Property(p => p.Birth).IsOptional();
        //}
    }
  

}
