using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using static FitnessApp.Common.EntityValidationConstants.Instructor;

namespace FitnessApp.Data.Models;

public class Instructor
{
	[Comment("Primary key")]
	[Key]
	public int Id { get; set; }

	[Required]
	[Comment("First name of the fitness instructor")]
	[MaxLength(FirstNameMaxLength)]
	public string FirstName { get; set; } = null!;

	[Required]
	[Comment("Last name of the fitness instructor")]
	[MaxLength(LastNameMaxLength)]
	public string LastName { get; set; } = null!;

    //TODO: Add instructor bio

    [Required]
	[Comment("Specialization of the fitness instructor")]
	[MaxLength(SpecializationMaxLength)]
	public string Specialization { get; set; } = null!;

	[Comment("Image URL of the fitness instructor")]
	public string? ImageUrl { get; set; }

	[Comment("Classes taught by the fitness instructor")]
	public virtual ICollection<Class> Classes { get; set; } = new List<Class>();
}