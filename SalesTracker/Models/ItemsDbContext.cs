using System;
using Microsoft.EntityFrameworkCore;

namespace SalesTracker.Models
{
    public class ItemsDbContext : DbContext
    {

        public ItemsDbContext()
        {

        }
        public virtual DbSet<Item> Item { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder
                .UseMySql(@"Server=localhost;Port=8889;database=itemsdb;uid=root;pwd=root;");

        public ItemsDbContext(DbContextOptions<ItemsDbContext> options)
        : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }

}