using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using static FitnessApp.Common.EntityValidationConstants.SpaRegistration;

namespace FitnessApp.Data.Models;

public class SpaRegistration

{
	[ForeignKey(nameof(Member))]
	public required int MemberId { get; set; }
	public required Member Member { get; set; }

	[ForeignKey(nameof(SpaType))]
	public required int SpaTypeId { get; set; }
	public required SpaType SpaType { get; set; }

	[DataType(DataType.Date)]
	[DisplayFormat(DataFormatString = AppointmentDateTimeFormat, ApplyFormatInEditMode = true)]
	public required DateTime AppointmentDate { get; set; }
}