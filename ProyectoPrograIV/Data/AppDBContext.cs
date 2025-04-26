using System.Reflection.Emit;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProyectoPrograIV.Models;

namespace ProyectoPrograIV.Data
{
    public class AppDBContext : IdentityDbContext<Users>
    {
        public AppDBContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Move> Moves { get; set; }
        public DbSet<Monster> Monsters { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Friendship> Friendships { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Friendship>()
                .HasOne(f => f.User)
                .WithMany(u => u.Friends)
                .HasForeignKey(f => f.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Friendship>()
                .HasOne(f => f.Friend)
                .WithMany(u => u.FriendOf)
                .HasForeignKey(f => f.FriendId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Monster>()
                .HasMany(m => m.Moves)
                .WithMany(m => m.Monsters)
                .UsingEntity(j => j.ToTable("MonsterMoves"));

        }
    }
}
