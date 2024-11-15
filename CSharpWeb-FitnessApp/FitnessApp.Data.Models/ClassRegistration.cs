using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace FitnessApp.Data.Models;

public class ClassRegistration
{
	[ForeignKey(nameof(Member))]
	public string MemberId { get; set; } = null!;
    public IdentityUser Member { get; set; } = null!;

	[ForeignKey(nameof(Class))]
	public int ClassId { get; set; }
	public Class Class { get; set; } = null!;
}