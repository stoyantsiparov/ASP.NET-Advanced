using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using static FitnessApp.Common.EntityValidationConstants.Class;

namespace FitnessApp.Data.Models;

public class Class
{
	[Comment("Primary key")]
	[Key]
	public int Id { get; set; }

	[Required]
	[Comment("Name of the fitness class")]
	[MaxLength(NameMaxLength)]
	public string Name { get; set; } = null!;

	[Comment("Image URL of the fitness class")]
	public string? ImageUrl { get; set; }

	[Required]
	[Comment("Description of the fitness class")]
	[MaxLength(DescriptionMaxLength)]
	public string Description { get; set; } = null!;

	[Required]
	[Comment("Price for the fitness class")]
	[Range((double)PriceMinValue, (double)PriceMaxValue)]
	public decimal Price { get; set; }

	[Required]
	[Comment("Date and time of the fitness class")]
	[DataType(DataType.Date)]
	[DisplayFormat(DataFormatString = ScheduleDateTimeFormat, ApplyFormatInEditMode = true)]
	public DateTime Schedule { get; set; }

	[Required]
	[Comment("Duration of the fitness class in minutes")]
	[Range(DurationMinValue, DurationMaxValue)]
	public int Duration { get; set; }

	[Comment("Instructor of the fitness class")]
	[ForeignKey(nameof(Instructor))]
	public int InstructorId { get; set; }
	public Instructor Instructor { get; set; } = null!;

	[Comment("Members registered for the fitness class")]
	public virtual ICollection<ClassRegistration> ClassesRegistrations { get; set; } = new List<ClassRegistration>();
}