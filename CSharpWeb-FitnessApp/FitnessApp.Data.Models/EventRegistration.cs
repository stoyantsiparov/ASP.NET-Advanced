using System.ComponentModel.DataAnnotations.Schema;

namespace FitnessApp.Data.Models;

public class EventRegistration
{
	[ForeignKey(nameof(Member))]
	public int MemberId { get; set; }
	public Member Member { get; set; } = null!;

	[ForeignKey(nameof(FitnessEvent))]
	public int EventId { get; set; }
	public FitnessEvent FitnessEvent { get; set; } = null!;
}