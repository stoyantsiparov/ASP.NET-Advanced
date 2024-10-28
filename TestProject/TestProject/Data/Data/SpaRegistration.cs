using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestProject.Data.Data
{
	public class SpaRegistration
	{
		[Key]
		public int Id { get; set; }

		[DataType(DataType.Date)]
		public required DateTime AppointmentDate { get; set; }

		[ForeignKey(nameof(Member))]
		public required int MemberId { get; set; }
		public required Member Member { get; set; }

		[ForeignKey(nameof(SpaType))]
		public required int SpaTypeId { get; set; }
		public required SpaType SpaType { get; set; }
	}
}
