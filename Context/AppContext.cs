using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MushroomPocket.Models;

namespace MushroomPocket.Context
{
    public class AppContext : DbContext
    {
        readonly string _connectionString = "server=localhost; uid=root; pwd=12345; database=test";
        //readonly string _connectionString = "Server=localhost; User ID=pokemon; Password=pokemon; Database=pokemon";
        // Server=localhost; User ID=pokemon; Password=pokemon; Database=pokemon
        public DbSet<Character> Characters { get; set; }
        // public DbSet<MushroomMaster> MushroomMasters { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(_connectionString, ServerVersion.AutoDetect(_connectionString));
        }
    }
}
