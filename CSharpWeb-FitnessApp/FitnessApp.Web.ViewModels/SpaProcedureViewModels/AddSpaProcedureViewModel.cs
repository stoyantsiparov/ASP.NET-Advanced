﻿using System.ComponentModel.DataAnnotations;

namespace FitnessApp.Web.ViewModels.SpaProcedureViewModels;
using static FitnessApp.Common.EntityValidationConstants.SpaProcedure;

public class AddSpaProcedureViewModel
{
    [Required]
    [MaxLength(NameMaxLength)]
    [MinLength(NameMinLength)]
    public string Name { get; set; } = null!;

    public string? ImageUrl { get; set; }

    [Required]
    [MaxLength(DescriptionMaxLength)]
    [MinLength(DescriptionMinLength)]
    public string Description { get; set; } = null!;

    [Required]
    [Range(DurationMinValue, DurationMaxValue)]
    public int Duration { get; set; }

    [Required]
    [Range((double)PriceMinValue, (double)PriceMaxValue)]
    public decimal Price { get; set; }
}