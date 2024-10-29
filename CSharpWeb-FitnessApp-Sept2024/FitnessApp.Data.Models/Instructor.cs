using System.ComponentModel.DataAnnotations;
using static FitnessApp.Common.EntityValidationConstants.Instructor;

namespace FitnessApp.Data.Models;

public class Instructor
{
	[Key]
	public required int Id { get; set; }

	[MaxLength(FirstNameMaxLength)]
	public required string FirstName { get; set; }

	[MaxLength(LastNameMaxLength)]
	public required string LastName { get; set; }

	[MaxLength(SpecializationMaxLength)]
	public required string Specialization { get; set; }

	public virtual ICollection<Class> Classes { get; set; } = new List<Class>();
}