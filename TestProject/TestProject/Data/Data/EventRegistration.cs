using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestProject.Data.Data
{
	public class EventRegistration
	{
		[Key]
		public int Id { get; set; }

		[ForeignKey(nameof(Member))]
		public required int MemberId { get; set; }
		public required Member Member { get; set; }

		[ForeignKey(nameof(Event))]
		public required int EventId { get; set; }
		public required Event Event { get; set; }
	}
}
