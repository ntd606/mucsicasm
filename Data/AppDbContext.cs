using Microsoft.EntityFrameworkCore;
using mucsic.Models;

namespace mucsic.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    public DbSet<Song> Songs { get; set; }
    public DbSet<Singer> Singers { get; set; }
    public DbSet<Composer> Composers { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        // Quan hệ Song - Singer
        modelBuilder.Entity<Song>()
            .HasOne(s => s.Singer)
            .WithMany(a => a.Songs)
            .HasForeignKey(s => s.SingerId)
            .OnDelete(DeleteBehavior.Restrict);
        // Quan hệ Song - Composer
        modelBuilder.Entity<Song>()
            .HasOne(s => s.Composer)
            .WithMany(c => c.Songs)
            .HasForeignKey(s => s.ComposerId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
