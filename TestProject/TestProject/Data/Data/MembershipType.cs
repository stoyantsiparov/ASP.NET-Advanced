using System.ComponentModel.DataAnnotations;

namespace TestProject.Data.Data
{
	public class MembershipType
	{
		[Key]
		public required int Id { get; set; }

		[MaxLength(30)]
		public required string Name { get; set; }

		[Range(89, 1199)]
		public required decimal Price { get; set; }

		public virtual ICollection<Member> Members { get; set; } = new List<Member>();
	}
}
