using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace FitnessApp.Data.Models;

public class MembershipRegistration
{
	[ForeignKey(nameof(Member))]
	public string MemberId { get; set; } = null!;
	public IdentityUser Member { get; set; } = null!;

	[ForeignKey(nameof(MembershipType))]
	public int MembershipTypeId { get; set; }
	public MembershipType MembershipType { get; set; } = null!;
}