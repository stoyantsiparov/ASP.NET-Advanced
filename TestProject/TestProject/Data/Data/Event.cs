using System.ComponentModel.DataAnnotations;

namespace TestProject.Data.Data
{
	public class Event
	{
		[Key]
		public int Id { get; set; }

		[MaxLength(50)]
		public required string Name { get; set; }

		[DataType(DataType.Date)]
		public required DateTime EndDate { get; set; }

		[MaxLength(100)]
		public required string Location { get; set; }

		[MaxLength(5000)]
		public required string Description { get; set; }
		
		public virtual ICollection<EventRegistration> EventsRegistrations { get; set; } = new List<EventRegistration>();
	}
}
