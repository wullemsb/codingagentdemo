#nullable enable

using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Data;

public class EventsDbContext : DbContext
{
    public EventsDbContext(DbContextOptions<EventsDbContext> options) : base(options)
    {
    }

    public DbSet<Event> Events { get; set; } = null!;
    public DbSet<EventRegistration> EventRegistrations { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure Event entity
        modelBuilder.Entity<Event>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Location).IsRequired().HasMaxLength(500);
            entity.Property(e => e.Date).IsRequired();
            entity.Property(e => e.StartTime).IsRequired();
            entity.Property(e => e.CreatedAt).IsRequired();
            entity.Property(e => e.UpdatedAt).IsRequired();
        });

        // Configure EventRegistration entity
        modelBuilder.Entity<EventRegistration>(entity =>
        {
            entity.HasKey(er => er.Id);
            entity.Property(er => er.Name).IsRequired().HasMaxLength(100);
            entity.Property(er => er.Email).IsRequired().HasMaxLength(200);
            entity.Property(er => er.Pronouns).HasMaxLength(100);
            entity.Property(er => er.OptInForCommunication).IsRequired();
            entity.Property(er => er.CreatedAt).IsRequired();

            // Configure relationship
            entity.HasOne(er => er.Event)
                  .WithMany()
                  .HasForeignKey(er => er.EventId)
                  .OnDelete(DeleteBehavior.Cascade);
        });
    }
}