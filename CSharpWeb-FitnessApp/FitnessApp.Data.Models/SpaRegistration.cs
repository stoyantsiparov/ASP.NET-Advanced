using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using static FitnessApp.Common.EntityValidationConstants.SpaRegistration;

namespace FitnessApp.Data.Models;

public class SpaRegistration
{
    [ForeignKey(nameof(Member))]
    public string MemberId { get; set; } = null!;
    public IdentityUser Member { get; set; } = null!;

    [ForeignKey(nameof(SpaProcedure))]
    public int SpaProcedureId { get; set; }
    public SpaProcedure SpaProcedure { get; set; } = null!;

    [Required]
    [Comment("Appointment day for the spa service")]
    [RegularExpression(TreatmentDayRegex, ErrorMessage = "Treatment day must be either Saturday or Sunday.")]
    public string TreatmentDay { get; set; } = null!;
}