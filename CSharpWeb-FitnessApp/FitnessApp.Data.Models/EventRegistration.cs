using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace FitnessApp.Data.Models;

public class EventRegistration
{
	[ForeignKey(nameof(Member))]
	public string MemberId { get; set; } = null!;
    public IdentityUser Member { get; set; } = null!;

	[ForeignKey(nameof(FitnessEvent))]
	public int EventId { get; set; }
	public FitnessEvent FitnessEvent { get; set; } = null!;
}