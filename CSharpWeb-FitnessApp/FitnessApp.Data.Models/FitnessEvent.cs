using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using static FitnessApp.Common.EntityValidationConstants.FitnessEvent;

namespace FitnessApp.Data.Models;

public class FitnessEvent
{
	[Comment("Primary key")]
	[Key]
	public int Id { get; set; }

	[Required]
	[Comment("Title of the fitness event")]
	[MaxLength(TitleMaxLength)]
	public string Title { get; set; } = null!;

	public string? ImageUrl { get; set; }

	[Required]
	[Comment("Description of the fitness event")]
	[MaxLength(DescriptionMaxLength)]
	public string Description { get; set; } = null!;

	[Required]
	[Comment("Location of the fitness event")]
	[MaxLength(LocationMaxLength)]
	public string Location { get; set; } = null!;

	[Required]
	[Comment("Start date of the fitness event")]
	[DataType(DataType.Date)]
	[DisplayFormat(DataFormatString = DateTimeFormat, ApplyFormatInEditMode = true)]
	public DateTime StartDate { get; set; }

	[Required]
	[Comment("End date of the fitness event")]
	[DataType(DataType.Date)]
	[DisplayFormat(DataFormatString = DateTimeFormat, ApplyFormatInEditMode = true)]
	public DateTime EndDate { get; set; }

	[Comment("Members registered for the fitness event")]
	public virtual ICollection<EventRegistration> EventsRegistrations { get; set; } = new List<EventRegistration>();
}