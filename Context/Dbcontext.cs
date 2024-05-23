using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MushroomPocket.Models;

namespace MushroomPocket.Context
{
    public class Dbcontext : DbContext
    {
        //public DbSet<MushroomMaster> Mushroom { get; set; }
        public DbSet<Character> Character { get; set; }
        public DbSet<Items> Items { get; set; } 
        public DbSet<Inventory> Inventory { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(@"Data Source=MushroomPocket.db;");
        }

        // This is the relationship between Inventory and Items
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Inventory>()
                .HasOne(i => i.Items) // Inventory has one Items
                .WithMany(i => i.Inventory) // Items has many Inventory
                .HasForeignKey(i => i.ItemId);
        }
    }
}
