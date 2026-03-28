using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace TicketManagerApi.Entities;

[Index(nameof(Email), IsUnique = true)]
public class User : IdentityUser<int>
{
  public List<Ticket> TicketsCreated { get; set; } = [];
  public List<Ticket> TicketsFollowed { get; set; } = [];
  public List<Ticket> TicketsUpvoted { get; set; } = [];
  public List<Ticket> TicketsAssigned { get; set; } = [];

  public List<Application> MemberOf { get; set; } = [];
  public List<Application> OwnedApplications { get; set; } = [];
}
