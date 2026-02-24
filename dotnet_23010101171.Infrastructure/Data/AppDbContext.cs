using dotnet_23010101171.Core.Data;
using dotnet_23010101171.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace dotnet_23010101171.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Roles> Roles { get; set; }
    public DbSet<TicketComments> TicketComments { get; set; }
    public DbSet<Tickets> Tickets { get; set; }
    public DbSet<TicketStatusLogs> TicketStatusLogs { get; set; }
    public DbSet<Users> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Users>()
            .Property(b => b.CreatedAt)
            .HasDefaultValueSql("GETUTCDATE()");

        modelBuilder.Entity<TicketComments>()
            .Property(b => b.CreatedAt)
            .HasDefaultValueSql("GETUTCDATE()");

        modelBuilder.Entity<Tickets>()
            .Property(b => b.CreatedAt)
            .HasDefaultValueSql("GETUTCDATE()");

        modelBuilder.Entity<TicketStatusLogs>()
            .Property(b => b.CreatedAt)
            .HasDefaultValueSql("GETUTCDATE()");

        modelBuilder.Entity<Tickets>()
            .Property(b => b.Status)
            .HasDefaultValue(StatusEnum.Open);

        modelBuilder.Entity<Tickets>()
            .Property(b => b.Priority)
            .HasDefaultValue(PriorityEnum.Medium);

        modelBuilder.Entity<Roles>()
            .HasIndex(p => p.RoleName)
            .IsUnique();

        modelBuilder.Entity<Users>()
            .HasIndex(p => p.Email)
            .IsUnique();

        modelBuilder.Entity<Tickets>()
            .HasOne(t => t.CreatedByUser)
            .WithMany()
            .HasForeignKey(t => t.CreatedByUserId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Tickets>()
            .HasOne(t => t.AssignedToUser)
            .WithMany()
            .HasForeignKey(t => t.AssignedToUserId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<TicketComments>()
            .HasOne(t => t.Ticket)
            .WithMany()
            .HasForeignKey(t => t.TicketID)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<TicketStatusLogs>()
            .HasOne(t => t.Ticket)
            .WithMany()
            .HasForeignKey(t => t.TicketID)
            .OnDelete(DeleteBehavior.Cascade);
    }
}