using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using static FitnessApp.Common.EntityValidationConstants.Class;

namespace FitnessApp.Data.Models;

public class Class
{
	[Comment("Primary key")]
	[Key]
	public required int Id { get; set; }

	[Comment("Name of the fitness class")]
	[MaxLength(NameMaxLength)]
	public required string Name { get; set; }

	[Comment("Description of the fitness class")]
	[MaxLength(DescriptionMaxLength)]
	public required string Description { get; set; }

	[Comment("Date and time of the fitness class")]
	[DataType(DataType.Date)]
	[DisplayFormat(DataFormatString = ScheduleDateTimeFormat, ApplyFormatInEditMode = true)]
	public required DateTime Schedule { get; set; }

	[Comment("Duration of the fitness class in minutes")]
	[Range(DurationMinValue, DurationMaxValue)]
	public required int Duration { get; set; }


	[Comment("Instructor of the fitness class")]
	[ForeignKey(nameof(Instructor))]
	public required int InstructorId { get; set; }
	public required Instructor Instructor { get; set; }

	[Comment("Members registered for the fitness class")]
	public virtual ICollection<ClassRegistration> ClassesRegistrations { get; set; } = new List<ClassRegistration>();
}