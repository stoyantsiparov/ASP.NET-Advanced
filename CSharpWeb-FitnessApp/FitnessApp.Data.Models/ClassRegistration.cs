using System.ComponentModel.DataAnnotations.Schema;

namespace FitnessApp.Data.Models;

public class ClassRegistration
{
	[ForeignKey(nameof(Member))]
	public int MemberId { get; set; }
	public Member Member { get; set; } = null!;

	[ForeignKey(nameof(Class))]
	public int ClassId { get; set; }
	public Class Class { get; set; } = null!;
}