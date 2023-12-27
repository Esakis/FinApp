using Microsoft.EntityFrameworkCore;
using Store.Models;

namespace Store.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<TransactionModel> Transactions { get; set; } = default!;
        public DbSet<CategoryModel> Categories { get; set; } = default!;
        public DbSet<UserModel> Users { get; set; } = default!;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<TransactionModel>()
                .HasOne<CategoryModel>() 
                .WithMany()
                .HasForeignKey(t => t.CategoryId)
                .IsRequired(); 

            modelBuilder.Entity<TransactionModel>()
                .HasOne<UserModel>() 
                .WithMany()
                .HasForeignKey(t => t.UserId)
                .IsRequired(); 


            modelBuilder.Entity<CategoryModel>().HasData(
               new CategoryModel { Id = 1, Name = "Jedzenie", CategoryIncome = false },
               new CategoryModel { Id = 2, Name = "Rozrywka", CategoryIncome = false },
               new CategoryModel { Id = 3, Name = "Rachunki", CategoryIncome = false },
               new CategoryModel { Id = 4, Name = "Transport", CategoryIncome = false },
               new CategoryModel { Id = 5, Name = "Zdrowie" , CategoryIncome = false },
               new CategoryModel { Id = 6, Name = "Edukacja" , CategoryIncome = false },
               new CategoryModel { Id = 7, Name = "Zakupy" , CategoryIncome = false },
               new CategoryModel { Id = 8, Name = "Oszczędności" , CategoryIncome = true },
               new CategoryModel { Id = 9, Name = "Podróże", CategoryIncome = false },
               new CategoryModel { Id = 10, Name = "Inne", CategoryIncome = false },
               new CategoryModel { Id = 11, Name = "Przychód", CategoryIncome = true }
            );

            modelBuilder.Entity<UserModel>().HasData(
               new UserModel
               {
                   Id = 1,
                   Username = "defaultuser",
                   Email = "default@example.com",
                   PasswordHash = "hashed_password",
                   FirstName = "Default",
                   LastName = "User",
                   Balance = 1000000
               }
           );
        }
    }
}
