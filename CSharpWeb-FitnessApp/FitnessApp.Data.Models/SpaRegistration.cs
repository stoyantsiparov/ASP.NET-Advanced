using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using static FitnessApp.Common.EntityValidationConstants.SpaRegistration;

namespace FitnessApp.Data.Models;

public class SpaRegistration
{
	[ForeignKey(nameof(Member))]
	public required int MemberId { get; set; }
	public required Member Member { get; set; }

	[ForeignKey(nameof(SpaProcedure))]
	public required int SpaProcedureId { get; set; }
	public required SpaProcedure SpaProcedure { get; set; }

	[Comment("Appointment date for the spa service")]
	[DataType(DataType.Date)]
	[DisplayFormat(DataFormatString = AppointmentDateTimeFormat, ApplyFormatInEditMode = true)]
	public required DateTime AppointmentDate { get; set; }
}