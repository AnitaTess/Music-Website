using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Anita
{
    public class chinook : DbContext
    {
        public chinook()
        {
        }

        public chinook(DbContextOptions<chinook> options)
            : base(options)
        {
        }
        public DbSet<Album> albums { get; set; }
     public DbSet<Artist> artists { get; set; }
     public DbSet<Track> tracks { get; set; }
    

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            /* string path = System.IO.Path.Combine(System.Environment.CurrentDirectory, "Simpsons.db"); */
            string currentDirectory = System.Environment.CurrentDirectory;
            string parentDirectory = System.IO.Directory.GetParent(currentDirectory).FullName;
            string path = System.IO.Path.Combine(parentDirectory, "chinook.db");
            optionsBuilder.UseSqlite($"Filename={path}");
        }

        

    
    }
    }

