using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using static FitnessApp.Common.EntityValidationConstants.FitnessEvent;

namespace FitnessApp.Data.Models;

public class FitnessEvent
{
	[Comment("Primary key")]
	[Key]
	public int Id { get; set; }

	[Comment("Title of the fitness event")]
	[MaxLength(TitleMaxLength)]
	public required string Title { get; set; }

	[Comment("Description of the fitness event")]
	[MaxLength(DescriptionMaxLength)]
	public required string Description { get; set; }

	[Comment("Location of the fitness event")]
	[MaxLength(LocationMaxLength)]
	public required string Location { get; set; }

	[Comment("Start date of the fitness event")]
	[DataType(DataType.Date)]
	[DisplayFormat(DataFormatString = DateTimeFormat, ApplyFormatInEditMode = true)]
	public required DateTime StartDate { get; set; }

	[Comment("End date of the fitness event")]
	[DataType(DataType.Date)]
	[DisplayFormat(DataFormatString = DateTimeFormat, ApplyFormatInEditMode = true)]
	public required DateTime EndDate { get; set; }

	[Comment("Members registered for the fitness event")]
	public virtual ICollection<EventRegistration> EventsRegistrations { get; set; } = new List<EventRegistration>();
}