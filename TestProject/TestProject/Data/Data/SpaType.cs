using System.ComponentModel.DataAnnotations;

namespace TestProject.Data.Data
{
	public class SpaType
	{
		[Key]
		public int Id { get; set; }

		[MaxLength(50)]
		public required string Name { get; set; }

		[Range(30, 180)]
		public required int Duration { get; set; }

		[Range(99, 399)]
		public required decimal Price { get; set; }

		public virtual ICollection<SpaRegistration> SpaRegistrations { get; set; } = new List<SpaRegistration>();
	}
}
