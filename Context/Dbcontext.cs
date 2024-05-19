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
            //optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESS;Database=MushroomPocket;Trusted_Connection=True;");
            optionsBuilder.UseSqlite(@"Data Source=MushroomPocket.db;");
        }
    }
}
