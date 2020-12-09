using Microsoft.EntityFrameworkCore;
using NoSQLTest.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace NoSQLTest
{
    class AppDbContext : DbContext
    {
        public DbSet<AttributeType> AttributeTypes { get; set; }
        public DbSet<EntityType> EntityTypes { get; set; }
        public DbSet<EntityAttribute> EntityAttributes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=NoSQLTest;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EntityType>()
                .HasMany(et => et.Attributes)
                .WithOne(a => a.EntityType)
                .HasForeignKey(a => a.EntityTypeId);

            modelBuilder.Entity<EntityAttribute>()
                .HasOne(a => a.AttributeType)
                .WithMany()
                .HasForeignKey(a => a.AttributeTypeId);

            CreateAttributeTypes(modelBuilder);
        }

        private void CreateAttributeTypes(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AttributeType>()
                .HasData(new AttributeType
                {
                    Id = Constants.AttributeTypeBoolean,
                    Label = typeof(bool).ToString()
                },
                new AttributeType
                {
                    Id = Constants.AttributeTypeString,
                    Label = typeof(string).ToString()
                },
                new AttributeType
                {
                    Id = Constants.AttributeTypeDateTime,
                    Label = typeof(DateTime).ToString()
                },
                new AttributeType
                {
                    Id = Constants.AttributeTypeInt,
                    Label = typeof(int).ToString()
                },
                new AttributeType
                {
                    Id = Constants.AttributeTypeDouble,
                    Label = typeof(double).ToString()
                });
        }
    }
}
