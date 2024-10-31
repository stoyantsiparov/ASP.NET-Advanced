using AutoMapper;

namespace FitnessApp.Services.Mapping;

public interface IHaveCustomMappings
{
	void CreateMappings(IProfileExpression configuration);
}