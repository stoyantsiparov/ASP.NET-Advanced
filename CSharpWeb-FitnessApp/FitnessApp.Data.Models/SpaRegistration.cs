using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace FitnessApp.Data.Models;

public class SpaRegistration
{
    [ForeignKey(nameof(Member))]
    public string MemberId { get; set; } = null!;
    public IdentityUser Member { get; set; } = null!;

    [ForeignKey(nameof(SpaProcedure))]
    public int SpaProcedureId { get; set; }
    public SpaProcedure SpaProcedure { get; set; } = null!;
}