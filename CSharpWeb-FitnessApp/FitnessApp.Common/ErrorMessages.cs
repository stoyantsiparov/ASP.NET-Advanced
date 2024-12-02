﻿namespace FitnessApp.Common;

public static class ErrorMessages
{
    public static class SpaProcedure
    {
        public const string PastAppointmentDate = "Appointment date and time cannot be in the past.";
        public const string AlreadyBookedAppointment = "You have already booked this appointment.";
        public const string SpaAppointmentNotBooked = "You haven't booked a spa appointment";
        public const string InvalidAppointmentDateFormat = "Invalid appointment date format.";
        public const string SpaProcedureNotFound = "Spa procedure not found.";
    }

    public static class FitnessEvent
    {
        public const string FitnessEventDoesNotExist = "The specified event does not exist.";
        public const string AlreadyRegisteredForEvent = "You have already signed up for this event.";
        public const string UserNotRegisteredForEvent = "You are not registered for this event.";
        public const string EndDateMustBeLaterThanStartDate = "End Date must be later than Start Date.";
    }

    public static class Class
    {
        public const string FitnessClassDoesNotExist = "The specified class does not exist.";
        public const string AlreadyRegisteredForClass = "You have already signed up for this class.";
        public const string UserNotRegisteredForClass = "You are not registered for this class.";
        public const string ClassViewModelCannotBeNull = "Class view model cannot be null.";
        public const string UserIdCannotBeEmpty = "User ID cannot be null or empty.";
        public const string ClassWithTheSameNameAndScheduleAlreadyExists = "A class with the same name and schedule already exists.";
        public const string InvalidScheduleFormat = "Invalid schedule format.";
        
    }

    public static class MembershipType
    {
        public const string MembershipTypeDoesNotExist = "The membership type does not exist.";
        public const string OnlyOneMembershipTypeAllowed = "You can only have one membership type at a time.";
        public const string MembershipNotPurchased = "You have not purchased a membership";
    }
}