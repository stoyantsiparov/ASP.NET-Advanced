namespace FitnessApp.Common;

public static class EntityValidationConstants
{
    public static class MembershipType
	{
		public const int NameMaxLength = 30;
		public const int NameMinLength = 2;
		public const decimal PriceMinValue = 7.99m;
		public const decimal PriceMaxValue = 1199.00m;
		public const int DurationMinValue = 30;
		public const int DurationMaxValue = 365;
		public const int DescriptionMaxLength = 5000;
		public const int DescriptionMinLength = 10;
	}
	
	public static class Class
	{
		public const int NameMaxLength = 30;
		public const int NameMinLength = 2;
		public const int DescriptionMaxLength = 5000;
		public const int DescriptionMinLength = 10;
		public const int DurationMinValue = 30;
		public const int DurationMaxValue = 180;
		public const string ScheduleDateTimeFormat = "dd-MM-yyyy HH:mm";
	}

	public static class FitnessEvent
	{
		public const int TitleMaxLength = 50;
		public const int TitleMinLength = 2;
		public const int LocationMaxLength = 100;
		public const int LocationMinLength = 2;
		public const int DescriptionMaxLength = 5000;
		public const int DescriptionMinLength = 10;
		public const string DateTimeFormat = "dd-MM-yyyy HH:mm";
    }

	public static class Instructor
	{
		public const int FirstNameMaxLength = 50;
		public const int FirstNameMinLength = 2;
		public const int LastNameMaxLength = 50;
		public const int LastNameMinLength = 2;
        public const int BioMaxLength = 5000;
        public const int BioMinLength = 10;
        public const int SpecializationMaxLength = 50;
		public const int SpecializationMinLength = 2;
	}

	public static class SpaProcedure
	{
		public const int NameMaxLength = 50;
		public const int NameMinLength = 2;
		public const int DescriptionMaxLength = 5000;
		public const int DescriptionMinLength = 10;
        public const int DurationMinValue = 30;
		public const int DurationMaxValue = 180;
		public const decimal PriceMinValue = 99.00m;
		public const decimal PriceMaxValue = 399.00m;
        public const string AppointmentDateTimeFormat = "dd-MM-yyyy HH:mm";
    }
}