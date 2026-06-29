using Microsoft.EntityFrameworkCore;
using Business.Data.Models;
using Business.Data.Enums;
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
        public DbSet<RefreshToken> RefreshTokens {get;set;}
        public DbSet<UserSession> UserSessions {get;set;}
        public BaseDbContext (DbContextOptions<BaseDbContext> opt) : base(opt) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<TeaIngredient>()
            .HasOne(ti => ti.Tea)
            .WithMany(t => t.TeaIngredients)
            .HasForeignKey(ti => ti.TeaId)
            .OnDelete(DeleteBehavior.Cascade);
            
            modelBuilder.Entity<TeaIngredient>()
            .HasOne(ti => ti.Ingredient)
            .WithMany()
            .HasForeignKey(ti => ti.IngredientId)
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<RefreshToken>()
            .HasOne(token => token.PreviousToken)
            .WithMany()
            .HasForeignKey(token => token.PreviousTokenId)
            .OnDelete(DeleteBehavior.Restrict);
            
            modelBuilder.Entity<RefreshToken>()
            .HasIndex(r => r.PreviousTokenId)
            .IsUnique();

            modelBuilder.Entity<RefreshToken>()
            .HasIndex(r => r.TokenHash)
            .IsUnique();

            modelBuilder.Entity<UserSession>()
            .HasIndex(u => u.CurrentRefreshTokenId)
            .IsUnique();

            modelBuilder.Entity<Ingredient>()
            .HasQueryFilter(i => !i.IsDeleted && i.ModerationStatus == ModerationStatus.Approved);

            modelBuilder.Entity<Tea>()
            .HasQueryFilter(t => !t.IsDeleted && t.ModerationStatus == ModerationStatus.Approved);
            
        }

    }
}