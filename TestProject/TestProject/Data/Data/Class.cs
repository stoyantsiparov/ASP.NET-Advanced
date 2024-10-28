using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestProject.Data.Data
{
	public class Class
	{
		[Key]
		public required int Id { get; set; }

		[MaxLength(30)]
		public required string Name { get; set; }

		[DataType(DataType.Date)]
		public required DateTime Schedule { get; set; }

		[Range(30, 180)]
		public required int Duration { get; set; }

		[ForeignKey(nameof(Instructor))]
		public required int InstructorId { get; set; }
		public required Instructor Instructor { get; set; }

		public virtual ICollection<ClassRegistration> ClassesRegistrations { get; set; } = new List<ClassRegistration>();
	}
}
