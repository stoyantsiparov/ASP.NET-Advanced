using System.ComponentModel.DataAnnotations.Schema;

namespace FitnessApp.Data.Models;

public class EventRegistration
{
	[ForeignKey(nameof(Member))]
	public required int MemberId { get; set; }
	public required Member Member { get; set; }

	[ForeignKey(nameof(FitnessEvent))]
	public required int EventId { get; set; }
	public required FitnessEvent FitnessEvent { get; set; }
}