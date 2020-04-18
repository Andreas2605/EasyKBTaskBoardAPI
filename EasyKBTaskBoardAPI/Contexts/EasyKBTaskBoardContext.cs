using EasyKBTaskBoard.API.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyKBTaskBoard.API.Contexts
{
    public class EasyKBTaskBoardContext : DbContext
    {
        public EasyKBTaskBoardContext(DbContextOptions<EasyKBTaskBoardContext> options) 
            : base(options)
        {
        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Board> Boards { get; set; }
        public DbSet<Entities.Task> Tasks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasSequence<int>("Ids", schema: "dbo")
                .StartsAt(1)
                .IncrementsBy(1);

            modelBuilder.Entity<Account>()
                .Property(o => o.Id)
                .HasDefaultValueSql("NEXT VALUE FOR dbo.Ids");

            modelBuilder.Entity<Account>().HasData(
                new Account()
                {
                    Id = 1,
                    FirstName = "Andreas",
                    LastName = "Kolenda",
                    Email = "akolenda73@gmail.com",
                    Password = "test123"
                },
                new Account()
                {
                    Id = 2,
                    FirstName = "Carlo",
                    LastName = "Tamburin",
                    Email = "carlo.tamburin@gmail.com",
                    Password = "test321"
                }
                );

            base.OnModelCreating(modelBuilder);
        }
    }
}
