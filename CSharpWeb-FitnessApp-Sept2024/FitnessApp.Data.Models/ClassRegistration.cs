using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FitnessApp.Data.Models;

public class ClassRegistration
{
	[ForeignKey(nameof(Member))]
	public required int MemberId { get; set; }
	public required Member Member { get; set; }

	[ForeignKey(nameof(Class))]
	public required int ClassId { get; set; }
	public required Class Class { get; set; }
}