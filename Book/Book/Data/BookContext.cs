using Book.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Book.Data
{
    public class BookContext : DbContext
    {
        public BookContext(DbContextOptions<BookContext> options) : base(options)
        {
        }

        public DbSet<UserModel> User { get; set; }
        public DbSet<UserBooksModel> Books { get; set; }
        public DbSet<SellBookModel> SellBook { get; set; }
        public DbSet<OrderModel> Order { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<UserModel>().ToTable("UserInformationTable")
                                            .Ignore(b => b.Books)
                                            .HasMany(c => c.Books)
                                            .WithOne();

            modelBuilder.Entity<UserBooksModel>().ToTable("Books")
                                             .HasOne(b => b.User)
                                             .WithMany(b => b.Books)
                                             .HasForeignKey(b => b.UserId);

            modelBuilder.Entity<SellBookModel>().ToTable("SellBook")
                                                .HasKey(b => b.Id);

            modelBuilder.Entity<OrderModel>().ToTable("Order")
                                                .HasKey(b => b.Id);
        }
    }
}
