namespace FitnessApp.Common;

public static class ErrorMessages
{
    public static class SpaProcedure
    {
        public const string PastAppointmentDate = "Appointment date and time cannot be in the past.";
        public const string AlreadyBookedAppointment = "You have already booked this appointment.";
        public const string SpaAppointmentNotBooked = "You haven't booked a spa appointment";
    }

    public static class FitnessEvent
    {
        public const string FitnessEventDoesNotExist = "The specified event does not exist.";
        public const string AlreadyRegisteredForEvent = "You have already signed up for this event.";
        public const string UserNotRegisteredForEvent = "You are not registered for this event.";
    }

    public static class Class
    {
        public const string FitnessClassDoesNotExist = "The specified class does not exist.";
        public const string AlreadyRegisteredForClass = "You have already signed up for this class.";
        public const string UserNotRegisteredForClass = "You are not registered for this class.";
    }

    public static class MembershipType
    {
        public const string MembershipTypeDoesNotExist = "The membership type does not exist.";
        public const string OnlyOneMembershipTypeAllowed = "You can only have one membership type at a time.";
        public const string MembershipNotPurchased = "You have not purchased a membership";
    }
}