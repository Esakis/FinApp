using Microsoft.EntityFrameworkCore;
using Store.Models;

namespace Store.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<TransactionModel> Transactions { get; set; }
        public DbSet<CategoryModel> Categories { get; set; }
        public DbSet<UserModel> Users { get; set; } 

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

           
            modelBuilder.Entity<TransactionModel>()
                .HasOne(t => t.Category)
                .WithMany(c => c.Transactions)
                .HasForeignKey(t => t.CategoryId);

           
            modelBuilder.Entity<TransactionModel>()
                .HasOne(t => t.User)
                .WithMany(u => u.Transactions)
                .HasForeignKey(t => t.UserId);

      
            modelBuilder.Entity<CategoryModel>().HasData(
               new CategoryModel { Id = 1, Name = "Jedzenie" },
               new CategoryModel { Id = 2, Name = "Rozrywka" },
               new CategoryModel { Id = 3, Name = "Rachunki" },
               new CategoryModel { Id = 4, Name = "Transport" },
               new CategoryModel { Id = 5, Name = "Zdrowie" },
               new CategoryModel { Id = 6, Name = "Edukacja" },
               new CategoryModel { Id = 7, Name = "Zakupy" },
               new CategoryModel { Id = 8, Name = "Oszczędności" },
               new CategoryModel { Id = 9, Name = "Podróże" },
               new CategoryModel { Id = 10, Name = "Inne" }
            );
        }
    }
}
