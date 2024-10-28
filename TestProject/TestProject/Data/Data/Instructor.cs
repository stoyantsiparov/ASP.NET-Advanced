using System.ComponentModel.DataAnnotations;

namespace TestProject.Data.Data
{
	public class Instructor
	{
		[Key]
		public required int Id { get; set; }

		[MaxLength(50)]
		public required string FirstName { get; set; }

		[MaxLength(50)]
		public required string LastName { get; set; }

		[MaxLength(100)]
		public required string Email { get; set; }

		[MaxLength(15)]
		public required string Phone { get; set; }

		public virtual ICollection<Class> Classes { get; set; } = new List<Class>();
	}
}
