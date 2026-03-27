using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TicketManagerApi.Entities;

namespace TicketManagerApi.Data;

public class TicketManagerContext (
  DbContextOptions<TicketManagerContext> options)
  : IdentityDbContext<User, IdentityRole<int>, int>(options)
{
  public DbSet<Application> Applications => Set<Application>();
  public DbSet<Ticket> Tickets => Set<Ticket>();
  public DbSet<Attachment> Attachments => Set<Attachment>();


  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    base.OnModelCreating(modelBuilder);

    EnumAsString(modelBuilder);
    ConfigRelationships(modelBuilder);
  }

  private static void EnumAsString(ModelBuilder modelBuilder)
  {
    // Configure enums as strings
    modelBuilder.Entity<Ticket>()
      .Property(t => t.Status)
      .HasConversion<string>();

    modelBuilder.Entity<Ticket>()
      .Property(t => t.Severity)
      .HasConversion<string>();
  }

  private static void ConfigRelationships(ModelBuilder modelBuilder)
  {    
    // Many to Many
    modelBuilder.Entity<User>()
      .HasMany(e => e.TicketsAssigned)
      .WithMany(e => e.AssignedTo)
      .UsingEntity("TicketsAssignedToUsers");
    
    modelBuilder.Entity<User>()
      .HasMany(e => e.TicketsUpvoted)
      .WithMany(e => e.UpvotedBy)
      .UsingEntity("TicketsUpvotedByUsers");
      
    modelBuilder.Entity<User>()
      .HasMany(e => e.TicketsFollowed)
      .WithMany(e => e.FollowedBy)
      .UsingEntity("TicketsFollowedByUsers");

    modelBuilder.Entity<User>()
      .HasMany(e => e.MemberOf)
      .WithMany(e => e.Members)
      .UsingEntity("UsersMemberOfApplications");

    // One to Many
    modelBuilder.Entity<Application>()
      .HasOne(e => e.Owner)
      .WithMany(e => e.OwnedApplications)
      .HasForeignKey(e => e.OwnerId);
    
    modelBuilder.Entity<Ticket>()
      .HasOne(e => e.Application)
      .WithMany(e => e.Tickets)
      .HasForeignKey(e => e.ApplicationId);
    
    modelBuilder.Entity<Ticket>()
      .HasOne(e => e.Author)
      .WithMany(e => e.TicketsCreated)
      .HasForeignKey(e => e.AuthorId);
  }
}