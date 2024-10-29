using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using static FitnessApp.Common.EntityValidationConstants.Instructor;

namespace FitnessApp.Data.Models;

public class Instructor
{
	[Comment("Primary key")]
	[Key]
	public required int Id { get; set; }

	[Comment("First name of the fitness instructor")]
	[MaxLength(FirstNameMaxLength)]
	public required string FirstName { get; set; }

	[Comment("Last name of the fitness instructor")]
	[MaxLength(LastNameMaxLength)]
	public required string LastName { get; set; }

	[Comment("Specialization of the fitness instructor")]
	[MaxLength(SpecializationMaxLength)]
	public required string Specialization { get; set; }

	[Comment("Classes taught by the fitness instructor")]
	public virtual ICollection<Class> Classes { get; set; } = new List<Class>();
}