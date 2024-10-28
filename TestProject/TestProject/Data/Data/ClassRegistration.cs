using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestProject.Data.Data
{
	public class ClassRegistration
	{
		[Key]
		public required int Id { get; set; }

		[ForeignKey(nameof(Member))]
		public required int MemberId { get; set; }
		public required Member Member { get; set; }

		[ForeignKey(nameof(Class))]
		public required int ClassId { get; set; }
		public required Class Class { get; set; }
	}
}
