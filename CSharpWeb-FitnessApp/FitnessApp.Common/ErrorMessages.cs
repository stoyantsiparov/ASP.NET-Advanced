namespace FitnessApp.Common;

public static class ErrorMessages
{
    public static class SpaProcedure
    {
        public const string PastAppointmentDate = "Appointment date and time cannot be in the past.";
        public const string AlreadyBookedAppointment = "You have already booked this appointment.";
        public const string SpaAppointmentNotBooked = "You haven't booked a spa appointment";
        public const string SpaProcedureNotFound = "Spa procedure not found.";
        public const string OnlyMembersCanBookSpaProcedures = "Only members can book spa procedures.";
    }
        
    public static class FitnessEvent
    {
        public const string FitnessEventDoesNotExist = "The specified event does not exist.";
        public const string FitnessEventNotFound = "Fitness event not found.";
        public const string AlreadyRegisteredForEvent = "You have already signed up for this event.";
        public const string UserNotRegisteredForEvent = "You are not registered for this event.";
        public const string EndDateMustBeLaterThanStartDate = "End Date must be later than Start Date.";
        public const string StartDateCannotBeInThePast = "Start Date cannot be in the past.";
        public const string OnlyMembersCanRegisterForThisEvent = "Only members can register for fitness events.";
    }

    public static class Class
    {
        public const string FitnessClassDoesNotExist = "The specified class does not exist.";
        public const string ClassNotFound = "Class not found.";
        public const string AlreadyRegisteredForClass = "You have already signed up for this class.";
        public const string UserNotRegisteredForClass = "You are not registered for this class.";
        public const string ClassViewModelCannotBeNull = "Class view model cannot be null.";
        public const string UserIdCannotBeEmpty = "User ID cannot be null or empty.";
        public const string ClassWithTheSameNameAndScheduleAlreadyExists = "A class with the same name and schedule already exists.";
        public const string InvalidScheduleFormat = "Invalid schedule format.";
        public const string OnlyMembersCanRegisterForThisClass = "Only members can register for fitness class.";
        
    }

    public static class MembershipType
    {
        public const string MembershipTypeDoesNotExist = "The membership type does not exist.";
        public const string MembershipNotFound = "Membership type not found.";
        public const string OnlyOneMembershipTypeAllowed = "You can only have one membership type at a time.";
        public const string MembershipNotPurchased = "You have not purchased a membership.";
        public const string FailedToAssignMemberRole = "Failed to assign Member role.";
        public const string FailedToRemoveMemberRole = "Failed to remove Member role.";
    }

    public static class Instructor
    {
        public const string InstructorNotFound = "Instructor not found.";
        public const string InstructorViewModelCannotBeNull = "Instructor view model cannot be null.";
        public const string UserIdCannotBeEmpty = "User ID cannot be null or empty.";
        public const string InvalidInstructorId = "Invalid instructor ID.";
        public const string InstructorAddError = "An error occurred while adding the instructor.";
        public const string InstructorEditError = "An error occurred while editing the instructor.";
        public const string InstructorDeleteError = "An error occurred while deleting the instructor.";
    }

    public static class Roles
    {
        public const string YouAreNotAuthorizedToAdd = "You are not authorized to add this element.";
        public const string YouAreNotAuthorizedToEdit = "You are not authorized to edit this element.";
        public const string YouAreNotAuthorizedToDelete = "You are not authorized to delete this element.";
    }

    public static class User
    {
        public const string UserIdOrRoleCannotBeEmpty = "User ID or role cannot be empty.";
        public const string UserDoesNotExist = "User does not exist.";
        public const string FailedToAssignRole = "Failed to assign role. Please try again.";
        public const string FailedToRemoveRole = "Failed to remove role. Please try again.";
        public const string FailedToDeleteRole = "Failed to delete user. Please try again.";
    }
}