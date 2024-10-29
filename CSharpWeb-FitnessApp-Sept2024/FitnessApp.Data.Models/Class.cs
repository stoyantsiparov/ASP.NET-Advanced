using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using static FitnessApp.Common.EntityValidationConstants.Class;

namespace FitnessApp.Data.Models;

public class Class
{
	[Key]
	public required int Id { get; set; }

	[MaxLength(NameMaxLength)]
	public required string Name { get; set; }

	[MaxLength(DescriptionMaxLength)]
	public required string Description { get; set; }

	[DataType(DataType.Date)]
	[DisplayFormat(DataFormatString = ScheduleDateTimeFormat, ApplyFormatInEditMode = true)]
	public required DateTime Schedule { get; set; }

	[Range(DurationMinValue, DurationMaxValue)]
	public required int Duration { get; set; }

	[ForeignKey(nameof(Instructor))]
	public required int InstructorId { get; set; }
	public required Instructor Instructor { get; set; }

	public virtual ICollection<ClassRegistration> ClassesRegistrations { get; set; } = new List<ClassRegistration>();
}