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
	[Comment("Appointment date for the spa service")]
	[DataType(DataType.Date)]
	[DisplayFormat(DataFormatString = AppointmentDateTimeFormat, ApplyFormatInEditMode = true)]
	public DateTime AppointmentDate { get; set; }
}