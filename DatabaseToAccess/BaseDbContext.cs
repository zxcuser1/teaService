using Microsoft.EntityFrameworkCore;
using Business.Data.Models;
namespace DatabaseToAccess
{

    public class BaseDbContext : DbContext
    {
        public DbSet<Ingredient> Ingredients {get;set;}
        public DbSet<Role> Roles {get;set;}
        public DbSet<Tea> Teas {get; set;}
        public DbSet<TeaIngredient> TeaIngredients {get;set;}
        public DbSet<User> Users {get;set;}
        public DbSet<UserIngredient> UserIngredients {get;set;}
        public BaseDbContext (DbContextOptions<BaseDbContext> opt) : base(opt) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<TeaIngredient>()
            .HasOne(ti => ti.Tea)
            .WithMany()
            .HasForeignKey(ti => ti.TeaId)
            .OnDelete(DeleteBehavior.Cascade);
            
            modelBuilder.Entity<TeaIngredient>()
            .HasOne(ti => ti.Ingredient)
            .WithMany()
            .HasForeignKey(ti => ti.IngredientId)
            .OnDelete(DeleteBehavior.Restrict);
            
            modelBuilder.Entity<Ingredient>()
            .HasQueryFilter(i => !i.IsDeleted && i.IsModerated);

            modelBuilder.Entity<Tea>()
            .HasQueryFilter(t => !t.IsDeleted && t.IsModerated);
            
        }

    }
}