using System.ComponentModel.DataAnnotations;
using static FitnessApp.Common.EntityValidationConstants.FitnessEvent;

namespace FitnessApp.Data.Models;

public class FitnessEvent
{
	[Key]
	public int Id { get; set; }

	[MaxLength(TitleMaxLength)]
	public required string Title { get; set; }

	[MaxLength(DescriptionMaxLength)]
	public required string Description { get; set; }

	[MaxLength(LocationMaxLength)]
	public required string Location { get; set; }

	[DataType(DataType.Date)]
	[DisplayFormat(DataFormatString = DateTimeFormat, ApplyFormatInEditMode = true)]
	public required DateTime StartDate { get; set; }

	[DataType(DataType.Date)]
	[DisplayFormat(DataFormatString = DateTimeFormat, ApplyFormatInEditMode = true)]
	public required DateTime EndDate { get; set; }

	public virtual ICollection<EventRegistration> EventsRegistrations { get; set; } = new List<EventRegistration>();
}