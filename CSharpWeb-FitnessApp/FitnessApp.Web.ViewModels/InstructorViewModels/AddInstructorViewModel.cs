using System.ComponentModel.DataAnnotations;

namespace FitnessApp.Web.ViewModels.InstructorViewModels;
using static FitnessApp.Common.EntityValidationConstants.Instructor;

public class AddInstructorViewModel
{
    [Required]
    [MaxLength(FirstNameMaxLength)]
    [MinLength(FirstNameMinLength)]
    public string FirstName { get; set; } = null!;

    [Required]
    [MaxLength(LastNameMaxLength)]
    [MinLength(LastNameMinLength)]
    public string LastName { get; set; } = null!;

    public string? ImageUrl { get; set; }

    [Required]
    [MaxLength(BioMaxLength)]
    [MinLength(BioMinLength)]
    public string Bio { get; set; } = null!;

    [Required]
    [MaxLength(SpecializationMaxLength)]
    [MinLength(SpecializationMinLength)]
    public string Specialization { get; set; } = null!;
}