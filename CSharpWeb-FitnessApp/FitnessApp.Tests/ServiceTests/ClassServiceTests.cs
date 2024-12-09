using FitnessApp.Data;
using FitnessApp.Data.Models;
using FitnessApp.Services.Data;
using FitnessApp.Services.Data.Contracts;
using FitnessApp.Web.ViewModels.ClassViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;

namespace FitnessApp.Tests.ServiceTests;

[TestFixture]
public class ClassServiceTests
{
    private Mock<IClassService> _mockClassService;
    private Mock<DbContext> _mockContext;
    private Mock<UserManager<IdentityUser>> _mockUserManager;

    [SetUp]
    public void Setup()
    {
        // Setting up the in-memory database to mock real interaction
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase("FitnessAppTestDb")
            .Options;
        var context = new ApplicationDbContext(options);

        // Initializing mock services
        var store = new Mock<IUserStore<IdentityUser>>();
        _mockUserManager = new Mock<UserManager<IdentityUser>>(store.Object, null, null, null, null, null, null, null, null);

        // Seed initial class and instructor data
        SeedData(context);
    }

    private void SeedData(ApplicationDbContext context)
    {
        var instructors = new List<Instructor>
        {
            new Instructor
            {
                Id = 1,
                FirstName = "InstructorFirstName",
                LastName = "InstructorLastName",
                Bio = "InstructorBio",
                Specialization = "Yoga Specialist"
            },
            new Instructor
            {
                Id = 2,
                FirstName = "Instructor2FirstName",
                LastName = "Instructor2LastName",
                Bio = "Instructor2Bio",
                Specialization = "HIIT Specialist"
            }
        };

        var classes = new List<Class>
        {
            new Class
            {
                Id = 1,
                Name = "Morning Yoga",
                InstructorId = 1,
                Schedule = DateTime.Parse("2024-12-05T07:00:00"),
                Duration = 60,
                Description = "A calm and peaceful yoga session to start your day.",
                Price = 50.00m,
                ImageUrl = "https://yogajala.com/wp-content/uploads/8-Benefits-Of-Morning-Yoga.jpg"
            },
            new Class
            {
                Id = 2,
                Name = "HIIT Challenge",
                InstructorId = 2,
                Schedule = DateTime.Parse("2024-12-05T18:00:00"),
                Duration = 45,
                Description = "An intense, high-energy interval training session.",
                Price = 50.00m,
                ImageUrl = "https://i.ytimg.com/vi/66_hHeSUrzU/hq720.jpg?sqp=-oaymwEhCK4FEIIDSFryq4qpAxMIARUAAAAAGAElAADIQjZQw/2.jpg"
            }
        };

        context.Instructors.AddRange(instructors);  // Add instructors first
        context.Classes.AddRange(classes);  // Add classes
        context.SaveChanges();
    }

    [Test]
    public async Task GetAllClassesAsync_ShouldReturnAllClasses()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase("FitnessAppTestDb")
            .Options;
        var context = new ApplicationDbContext(options);
        var classService = new ClassService(context, _mockUserManager.Object);

        // Act
        var result = await classService.GetAllClassesAsync(null, null, null);

        // Assert
        Assert.That(result.Count, Is.EqualTo(2));  // We have seeded 2 classes
    }

    [Test]
    public async Task GetAllClassesAsync_WithSearchQuery_ShouldReturnFilteredClasses()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase("FitnessAppTestDb")
            .Options;
        var context = new ApplicationDbContext(options);
        var classService = new ClassService(context, _mockUserManager.Object);

        // Act
        var result = await classService.GetAllClassesAsync("Yoga", null, null);

        // Assert
        var allClassesViewModels = result.ToList();
        Assert.That(allClassesViewModels.Count, Is.EqualTo(1));  // "Morning Yoga" matches
        Assert.That(allClassesViewModels[0].Name, Is.EqualTo("Morning Yoga"));
    }

    [Test]
    public async Task GetAllClassesAsync_WithDurationFilter_ShouldReturnFilteredClasses()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase("FitnessAppTestDb")
            .Options;
        var context = new ApplicationDbContext(options);
        var classService = new ClassService(context, _mockUserManager.Object);

        // Act
        var result = await classService.GetAllClassesAsync(null, 50, 60);

        // Assert
        var allClassesViewModels = result.ToList();
        Assert.That(allClassesViewModels.Count, Is.EqualTo(1));  // "Morning Yoga" matches the duration
        Assert.That(allClassesViewModels[0].Name, Is.EqualTo("Morning Yoga"));
    }

    [Test]
    public async Task GetAllClassesAsync_WithMinDuration_ShouldReturnFilteredClasses()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase("FitnessAppTestDb")
            .Options;
        var context = new ApplicationDbContext(options);
        var classService = new ClassService(context, _mockUserManager.Object);

        // Act
        var result = await classService.GetAllClassesAsync(null, 60, null);

        // Assert
        var allClassesViewModels = result.ToList();
        Assert.That(allClassesViewModels.Count, Is.EqualTo(1));  // "Morning Yoga" matches min duration
        Assert.That(allClassesViewModels[0].Name, Is.EqualTo("Morning Yoga"));
    }

    [Test]
    public async Task GetAllClassesAsync_WithMaxDuration_ShouldReturnFilteredClasses()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase("FitnessAppTestDb")
            .Options;
        var context = new ApplicationDbContext(options);
        var classService = new ClassService(context, _mockUserManager.Object);

        // Act
        var result = await classService.GetAllClassesAsync(null, null, 45);

        // Assert
        var allClassesViewModels = result.ToList();
        Assert.That(allClassesViewModels.Count, Is.EqualTo(1));  // "HIIT Challenge" matches max duration
        Assert.That(allClassesViewModels[0].Name, Is.EqualTo("HIIT Challenge"));
    }

    [Test]
    public async Task GetClassDetailsAsync_ShouldReturnClassDetails()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase("FitnessAppTestDb")
            .Options;
        var context = new ApplicationDbContext(options);
        var classService = new ClassService(context, _mockUserManager.Object);
        var expectedClassId = 1; // Use the seeded class ID

        // Act
        var result = await classService.GetClassDetailsAsync(expectedClassId);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Id, Is.EqualTo(expectedClassId));  // Check if the class Id matches
        Assert.That(result.Name, Is.EqualTo("Morning Yoga"));  // Check the class name
        Assert.That(result.Description, Is.EqualTo("A calm and peaceful yoga session to start your day."));  // Check the description
        Assert.That(result.Price, Is.EqualTo(50.00m));  // Check the price
        Assert.That(result.ImageUrl, Is.EqualTo("https://yogajala.com/wp-content/uploads/8-Benefits-Of-Morning-Yoga.jpg"));  // Check the Image URL
        Assert.That(result.Schedule, Is.EqualTo("05-12-2024 07:00"));  // Check the schedule (ensure it matches the format)
        Assert.That(result.Duration, Is.EqualTo(60));  // Check the duration

        // Verify instructor details
        Assert.That(result.Instructor.Id, Is.EqualTo(1));  // Instructor Id
        Assert.That(result.Instructor.FirstName, Is.EqualTo("InstructorFirstName"));  // Assuming you seeded the instructor's first name
        Assert.That(result.Instructor.LastName, Is.EqualTo("InstructorLastName"));  // Assuming you seeded the instructor's last name
        Assert.That(result.Instructor.Bio, Is.EqualTo("InstructorBio"));  // Assuming you seeded the instructor's bio
        Assert.That(result.Instructor.Specialization, Is.EqualTo("Yoga Specialist"));  // Assuming you seeded the instructor's specialization
    }

    [Test]
    public async Task GetAllClassesAsync_EmptyDatabase_ShouldReturnNoClasses()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase("EmptyTestDb")
            .Options;
        var context = new ApplicationDbContext(options);
        var classService = new ClassService(context, _mockUserManager.Object);

        // Act
        var result = await classService.GetAllClassesAsync(null, null, null);

        // Assert
        Assert.That(result.Count, Is.EqualTo(0));  // No classes should be in the database
    }

    [Test]
    public async Task GetClassDetailsAsync_NonExistingClassId_ShouldReturnNull()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase("FitnessAppTestDb")
            .Options;
        var context = new ApplicationDbContext(options);
        var classService = new ClassService(context, _mockUserManager.Object);
        var nonExistingClassId = 999; // Non-existing class ID

        // Act
        var result = await classService.GetClassDetailsAsync(nonExistingClassId);

        // Assert
        Assert.That(result, Is.Null);  // Should return null for non-existing class
    }

    [Test]
    public async Task GetAllClassesAsync_WithNullDurationFilters_ShouldReturnAllClasses()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase("FitnessAppTestDb")
            .Options;
        var context = new ApplicationDbContext(options);
        var classService = new ClassService(context, _mockUserManager.Object);

        // Act
        var result = await classService.GetAllClassesAsync(null, null, null);

        // Assert
        Assert.That(result.Count, Is.EqualTo(2));  // Should return all classes as both filters are null
    }

    [Test]
    public async Task GetAllClassesAsync_WithExactMatchingDuration_ShouldReturnMatchingClasses()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase("FitnessAppTestDb")
            .Options;
        var context = new ApplicationDbContext(options);
        var classService = new ClassService(context, _mockUserManager.Object);

        // Act
        var result = await classService.GetAllClassesAsync(null, 45, 45);

        // Assert
        var allClassesViewModels = result.ToList();
        Assert.That(allClassesViewModels.Count, Is.EqualTo(1));  // "HIIT Challenge" matches the exact duration of 45
        Assert.That(allClassesViewModels[0].Name, Is.EqualTo("HIIT Challenge"));
    }

    [Test]
    public async Task GetClassForAddAsync_ShouldReturnAddClassViewModelWithInstructors()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase("FitnessAppTestDb_" + Guid.NewGuid())  // Ensure unique DB name for each test
            .Options;
        var context = new ApplicationDbContext(options);

        // Seed instructors with all required properties
        var instructors = new List<Instructor>
        {
            new Instructor
            {
                Id = 1,
                FirstName = "John",  // Required property
                LastName = "Doe",    // Required property
                Bio = "Experienced yoga instructor",  // Required property
                Specialization = "Yoga Specialist"
            },
            new Instructor
            {
                Id = 2,
                FirstName = "Jane",  // Required property
                LastName = "Smith",  // Required property
                Bio = "Pilates instructor with 10 years of experience",  // Required property
                Specialization = "Pilates Expert"
            }
        };
        context.Instructors.AddRange(instructors);
        await context.SaveChangesAsync();

        var classService = new ClassService(context, _mockUserManager.Object);

        // Act
        var result = await classService.GetClassForAddAsync();

        // Assert
        Assert.That(result, Is.Not.Null);  // Ensure that a result is returned
        Assert.That(result.Instructors, Is.Not.Null);  // Ensure that instructors list is not null
        Assert.That(result.Instructors.Count, Is.EqualTo(2));  // Ensure that both instructors are returned
    }

    [Test]
    public async Task GetClassForAddAsync_ShouldReturnEmptyInstructorsListWhenNoInstructors()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase("FitnessAppTestDb")
            .EnableSensitiveDataLogging()  // To log more details about the exception
            .Options;

        var context = new ApplicationDbContext(options);

        var classService = new ClassService(context, _mockUserManager.Object);

        // Act
        var result = await classService.GetClassForAddAsync();

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Instructors.Count, Is.EqualTo(0));  // No instructors should be returned
    }

    [Test]
    public async Task GetClassForAddAsync_ShouldNotReturnNullWhenInstructorsExist()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase("FitnessAppTestDb_" + Guid.NewGuid())  // Ensure unique DB name for each test
            .Options;
        var context = new ApplicationDbContext(options);

        // Seed instructors into the context with unique Ids
        var instructors = new List<Instructor>
        {
            new Instructor
            {
                Id = 1,
                Specialization = "Yoga Specialist",
                Bio = "Experienced yoga instructor",
                FirstName = "John",
                LastName = "Doe"
            },
            new Instructor
            {
                Id = 2,
                Specialization = "Pilates Expert",
                Bio = "Pilates instructor with 10 years of experience",
                FirstName = "Jane",
                LastName = "Smith"
            }
        };
        context.Instructors.AddRange(instructors);
        await context.SaveChangesAsync();

        var classService = new ClassService(context, _mockUserManager.Object);

        // Act
        var result = await classService.GetClassForAddAsync();

        // Assert
        Assert.That(result, Is.Not.Null);  // Ensure that a result is returned
        Assert.That(result.Instructors, Is.Not.Null);  // Ensure that instructors list is not null
        Assert.That(result.Instructors.Count, Is.EqualTo(2));  // Ensure that both instructors are returned
    }

    [Test]
    public async Task GetClassForAddAsync_ShouldHandleEmptyDatabaseGracefully()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase("EmptyDatabaseTestDb")
            .Options;
        var context = new ApplicationDbContext(options);

        var classService = new ClassService(context, _mockUserManager.Object);

        // Act
        var result = await classService.GetClassForAddAsync();

        // Assert
        Assert.That(result, Is.Not.Null);  // Ensure that result is not null
        Assert.That(result.Instructors.Count, Is.EqualTo(0));  // No instructors should be returned for an empty database
    }

    [Test]
    public async Task GetClassForDeleteAsync_ShouldReturnClassForDelete()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase("GetClassForDeleteTestDb")
            .Options;
        var context = new ApplicationDbContext(options);

        var classEntity = new Class
        {
            Id = 1,
            Name = "Test Class",
            Description = "Test Description"
        };
        context.Classes.Add(classEntity);
        await context.SaveChangesAsync();

        var classService = new ClassService(context, _mockUserManager.Object);

        // Act
        var result = await classService.GetClassForDeleteAsync(1);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Id, Is.EqualTo(1));
        Assert.That(result.Name, Is.EqualTo("Test Class"));
        Assert.That(result.Description, Is.EqualTo("Test Description"));
    }

    [Test]
    public async Task GetClassForDeleteAsync_ShouldReturnNullForNonExistingClass()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase("GetClassForDeleteTestDb_NonExisting")
            .Options;
        var context = new ApplicationDbContext(options);

        var classService = new ClassService(context, _mockUserManager.Object);

        // Act
        var result = await classService.GetClassForDeleteAsync(999); // Non-existing class ID

        // Assert
        Assert.That(result, Is.Null);
    }

    [Test]
    public async Task RemoveFromMyClassesAsync_ShouldRemoveClassRegistration()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase("RemoveClassRegistrationTestDb")
            .Options;
        var context = new ApplicationDbContext(options);

        var userId = "testUserId";
        var classId = 1;

        var classRegistration = new ClassRegistration
        {
            MemberId = userId,
            ClassId = classId
        };
        context.ClassesRegistrations.Add(classRegistration);
        await context.SaveChangesAsync();

        var classService = new ClassService(context, _mockUserManager.Object);

        var classesViewModel = new ClassesViewModel
        {
            Id = classId
        };

        // Act
        await classService.RemoveFromMyClassesAsync(userId, classesViewModel);

        // Assert
        var result = await context.ClassesRegistrations
            .FirstOrDefaultAsync(cr => cr.MemberId == userId && cr.ClassId == classId);
        Assert.That(result, Is.Null);
    }

    [Test]
    public void RemoveFromMyClassesAsync_ShouldThrowInvalidOperationException_WhenUserNotRegistered()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase("UserNotRegisteredTestDb")
            .Options;
        var context = new ApplicationDbContext(options);

        var userId = "testUserId";
        var classId = 1;

        var classService = new ClassService(context, _mockUserManager.Object);

        var classesViewModel = new ClassesViewModel
        {
            Id = classId
        };

        // Act & Assert
        Assert.ThrowsAsync<InvalidOperationException>(async () =>
            await classService.RemoveFromMyClassesAsync(userId, classesViewModel));
    }

    [Test]
    public async Task AddClassAsync_ShouldAddClassSuccessfully()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase("AddClassTestDb")
            .Options;
        var context = new ApplicationDbContext(options);

        var userManagerMock = new Mock<UserManager<IdentityUser>>(
            new Mock<IUserStore<IdentityUser>>().Object, null, null, null, null, null, null, null, null);
        var user = new IdentityUser { Id = "testUserId" };
        userManagerMock.Setup(um => um.FindByIdAsync(It.IsAny<string>())).ReturnsAsync(user);
        userManagerMock.Setup(um => um.GetRolesAsync(It.IsAny<IdentityUser>())).ReturnsAsync(new List<string> { "Admin" });

        var classService = new ClassService(context, userManagerMock.Object);

        var model = new AddClassViewModel
        {
            Name = "Morning Yoga",
            Description = "A refreshing morning yoga session.",
            Price = 30.00m,
            ImageUrl = "https://example.com/morning-yoga.jpg",
            Schedule = "05-12-2024 08:00",
            Duration = 60,
            InstructorId = 1
        };

        // Act
        await classService.AddClassAsync(model, "testUserId");

        // Assert
        var addedClass = await context.Classes.FirstOrDefaultAsync(c => c.Name == "Morning Yoga");
        Assert.That(addedClass, Is.Not.Null);
        Assert.That(addedClass.Description, Is.EqualTo("A refreshing morning yoga session."));
        Assert.That(addedClass.Price, Is.EqualTo(30.00m));
        Assert.That(addedClass.ImageUrl, Is.EqualTo("https://example.com/morning-yoga.jpg"));
        Assert.That(addedClass.Schedule, Is.EqualTo(DateTime.Parse("05-12-2024 08:00")));
        Assert.That(addedClass.Duration, Is.EqualTo(60));
        Assert.That(addedClass.InstructorId, Is.EqualTo(1));
    }

    [Test]
    public void AddClassAsync_ShouldThrowUnauthorizedAccessException_WhenUserIsNotAdmin()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase("UnauthorizedAddClassTestDb")
            .Options;
        var context = new ApplicationDbContext(options);

        var userManagerMock = new Mock<UserManager<IdentityUser>>(
            new Mock<IUserStore<IdentityUser>>().Object, null, null, null, null, null, null, null, null);
        var user = new IdentityUser { Id = "testUserId" };
        userManagerMock.Setup(um => um.FindByIdAsync(It.IsAny<string>())).ReturnsAsync(user);
        userManagerMock.Setup(um => um.GetRolesAsync(It.IsAny<IdentityUser>())).ReturnsAsync(new List<string> { "User" });

        var classService = new ClassService(context, userManagerMock.Object);

        var model = new AddClassViewModel
        {
            Name = "Morning Yoga",
            Description = "A refreshing morning yoga session.",
            Price = 30.00m,
            ImageUrl = "https://example.com/morning-yoga.jpg",
            Schedule = "05-12-2024 08:00",
            Duration = 60,
            InstructorId = 1
        };

        // Act & Assert
        Assert.ThrowsAsync<UnauthorizedAccessException>(() => classService.AddClassAsync(model, "testUserId"));
    }

    [Test]
    public void AddClassAsync_ShouldThrowArgumentException_WhenNameOrScheduleIsEmpty()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase("InvalidAddClassTestDb")
            .Options;
        var context = new ApplicationDbContext(options);

        var userManagerMock = new Mock<UserManager<IdentityUser>>(
            new Mock<IUserStore<IdentityUser>>().Object, null, null, null, null, null, null, null, null);
        var user = new IdentityUser { Id = "testUserId" };
        userManagerMock.Setup(um => um.FindByIdAsync(It.IsAny<string>())).ReturnsAsync(user);
        userManagerMock.Setup(um => um.GetRolesAsync(It.IsAny<IdentityUser>())).ReturnsAsync(new List<string> { "Admin" });

        var classService = new ClassService(context, userManagerMock.Object);

        var model = new AddClassViewModel
        {
            Name = "",
            Description = "A refreshing morning yoga session.",
            Price = 30.00m,
            ImageUrl = "https://example.com/morning-yoga.jpg",
            Schedule = "",
            Duration = 60,
            InstructorId = 1
        };

        // Act & Assert
        Assert.ThrowsAsync<ArgumentException>(() => classService.AddClassAsync(model, "testUserId"));
    }

    [Test]
    public void AddClassAsync_ShouldThrowArgumentException_WhenScheduleIsInvalid()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase("InvalidScheduleAddClassTestDb")
            .Options;
        var context = new ApplicationDbContext(options);

        var userManagerMock = new Mock<UserManager<IdentityUser>>(
            new Mock<IUserStore<IdentityUser>>().Object, null, null, null, null, null, null, null, null);
        var user = new IdentityUser { Id = "testUserId" };
        userManagerMock.Setup(um => um.FindByIdAsync(It.IsAny<string>())).ReturnsAsync(user);
        userManagerMock.Setup(um => um.GetRolesAsync(It.IsAny<IdentityUser>())).ReturnsAsync(new List<string> { "Admin" });

        var classService = new ClassService(context, userManagerMock.Object);

        var model = new AddClassViewModel
        {
            Name = "Morning Yoga",
            Description = "A refreshing morning yoga session.",
            Price = 30.00m,
            ImageUrl = "https://example.com/morning-yoga.jpg",
            Schedule = "invalid date",
            Duration = 60,
            InstructorId = 1
        };

        // Act & Assert
        Assert.ThrowsAsync<ArgumentException>(() => classService.AddClassAsync(model, "testUserId"));
    }

    [Test]
    public async Task AddClassAsync_ShouldThrowInvalidOperationException_WhenClassWithSameNameAndScheduleExists()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase("DuplicateClassAddClassTestDb")
            .Options;
        var context = new ApplicationDbContext(options);

        var userManagerMock = new Mock<UserManager<IdentityUser>>(
            new Mock<IUserStore<IdentityUser>>().Object, null, null, null, null, null, null, null, null);
        var user = new IdentityUser { Id = "testUserId" };
        userManagerMock.Setup(um => um.FindByIdAsync(It.IsAny<string>())).ReturnsAsync(user);
        userManagerMock.Setup(um => um.GetRolesAsync(It.IsAny<IdentityUser>())).ReturnsAsync(new List<string> { "Admin" });

        var classService = new ClassService(context, userManagerMock.Object);

        var existingClass = new Class
        {
            Name = "Morning Yoga",
            Description = "A refreshing morning yoga session.",
            Price = 30.00m,
            ImageUrl = "https://example.com/morning-yoga.jpg",
            Schedule = DateTime.Parse("05-12-2024 08:00"),
            Duration = 60,
            InstructorId = 1
        };
        context.Classes.Add(existingClass);
        await context.SaveChangesAsync();

        var model = new AddClassViewModel
        {
            Name = "Morning Yoga",
            Description = "A refreshing morning yoga session.",
            Price = 30.00m,
            ImageUrl = "https://example.com/morning-yoga.jpg",
            Schedule = "05-12-2024 08:00",
            Duration = 60,
            InstructorId = 1
        };

        // Act & Assert
        Assert.ThrowsAsync<InvalidOperationException>(() => classService.AddClassAsync(model, "testUserId"));
    }

    [Test]
    public async Task EditClassAsync_ShouldEditClassSuccessfully()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase("EditClassTestDb")
            .Options;
        var context = new ApplicationDbContext(options);

        var userManagerMock = new Mock<UserManager<IdentityUser>>(
            new Mock<IUserStore<IdentityUser>>().Object, null, null, null, null, null, null, null, null);
        var user = new IdentityUser { Id = "testUserId" };
        userManagerMock.Setup(um => um.FindByIdAsync(It.IsAny<string>())).ReturnsAsync(user);
        userManagerMock.Setup(um => um.GetRolesAsync(It.IsAny<IdentityUser>())).ReturnsAsync(new List<string> { "Admin" });

        var classService = new ClassService(context, userManagerMock.Object);

        // Seed a class into the context
        var classEntity = new Class
        {
            Id = 1,
            Name = "Morning Yoga",
            Description = "A refreshing morning yoga session.",
            ImageUrl = "http://example.com/image.jpg",
            Price = 20,
            Schedule = DateTime.Now,
            Duration = 60,
            InstructorId = 1
        };
        context.Classes.Add(classEntity);
        await context.SaveChangesAsync();

        var model = new ClassesViewModel
        {
            Id = 1,
            Name = "Evening Yoga",
            Description = "A relaxing evening yoga session.",
            ImageUrl = "http://example.com/newimage.jpg",
            Price = 25,
            Schedule = DateTime.Now.AddHours(1).ToString(),
            Duration = 75,
            InstructorId = 2
        };

        // Act
        await classService.EditClassAsync(model, "testUserId");

        // Assert
        var editedClass = await context.Classes.FindAsync(1);
        Assert.That(editedClass, Is.Not.Null);
        Assert.That(editedClass.Name, Is.EqualTo("Evening Yoga"));
        Assert.That(editedClass.Description, Is.EqualTo("A relaxing evening yoga session."));
        Assert.That(editedClass.ImageUrl, Is.EqualTo("http://example.com/newimage.jpg"));
        Assert.That(editedClass.Price, Is.EqualTo(25));
        Assert.That(editedClass.Schedule, Is.EqualTo(DateTime.Parse(model.Schedule)));
        Assert.That(editedClass.Duration, Is.EqualTo(75));
        Assert.That(editedClass.InstructorId, Is.EqualTo(2));
    }

    [Test]
    public void EditClassAsync_ShouldThrowUnauthorizedAccessException_WhenUserIsNotAdmin()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase("UnauthorizedEditClassTestDb")
            .Options;
        var context = new ApplicationDbContext(options);

        var userManagerMock = new Mock<UserManager<IdentityUser>>(
            new Mock<IUserStore<IdentityUser>>().Object, null, null, null, null, null, null, null, null);
        var user = new IdentityUser { Id = "testUserId" };
        userManagerMock.Setup(um => um.FindByIdAsync(It.IsAny<string>())).ReturnsAsync(user);
        userManagerMock.Setup(um => um.GetRolesAsync(It.IsAny<IdentityUser>())).ReturnsAsync(new List<string> { "User" });

        var classService = new ClassService(context, userManagerMock.Object);

        var model = new ClassesViewModel
        {
            Id = 1,
            Name = "Evening Yoga",
            Description = "A relaxing evening yoga session.",
            ImageUrl = "http://example.com/newimage.jpg",
            Price = 25,
            Schedule = DateTime.Now.AddHours(1).ToString(),
            Duration = 75,
            InstructorId = 2
        };

        // Act & Assert
        Assert.ThrowsAsync<UnauthorizedAccessException>(async () => await classService.EditClassAsync(model, "testUserId"));
    }

    [Test]
    public async Task DeleteClassAsync_ShouldDeleteClassSuccessfully()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase("DeleteClassTestDb")
            .Options;
        var context = new ApplicationDbContext(options);

        var userManagerMock = new Mock<UserManager<IdentityUser>>(
            new Mock<IUserStore<IdentityUser>>().Object, null, null, null, null, null, null, null, null);
        var user = new IdentityUser { Id = "testUserId" };
        userManagerMock.Setup(um => um.FindByIdAsync(It.IsAny<string>())).ReturnsAsync(user);
        userManagerMock.Setup(um => um.GetRolesAsync(It.IsAny<IdentityUser>())).ReturnsAsync(new List<string> { "Admin" });

        var classService = new ClassService(context, userManagerMock.Object);

        var classEntity = new Class
        {
            Id = 1,
            Name = "Morning Yoga",
            Description = "A refreshing morning yoga session."
        };
        context.Classes.Add(classEntity);
        await context.SaveChangesAsync();

        // Act
        await classService.DeleteClassAsync(1, "testUserId");

        // Assert
        var deletedClass = await context.Classes.FindAsync(1);
        Assert.That(deletedClass, Is.Null);
    }

    [Test]
    public void DeleteClassAsync_ShouldThrowUnauthorizedAccessException_WhenUserIsNotAdmin()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase("UnauthorizedDeleteClassTestDb")
            .Options;
        var context = new ApplicationDbContext(options);

        var userManagerMock = new Mock<UserManager<IdentityUser>>(
            new Mock<IUserStore<IdentityUser>>().Object, null, null, null, null, null, null, null, null);
        var user = new IdentityUser { Id = "testUserId" };
        userManagerMock.Setup(um => um.FindByIdAsync(It.IsAny<string>())).ReturnsAsync(user);
        userManagerMock.Setup(um => um.GetRolesAsync(It.IsAny<IdentityUser>())).ReturnsAsync(new List<string> { "User" });

        var classService = new ClassService(context, userManagerMock.Object);

        // Act & Assert
        Assert.ThrowsAsync<UnauthorizedAccessException>(() => classService.DeleteClassAsync(1, "testUserId"));
    }

    [Test]
    public async Task GetClassByIdAsync_ShouldReturnNull_WhenClassDoesNotExist()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase("GetClassByIdTestDb")
            .Options;
        var context = new ApplicationDbContext(options);

        var classService = new ClassService(context, null);

        // Act
        var result = await classService.GetClassByIdAsync(999); // Non-existing ID

        // Assert
        Assert.That(result, Is.Null);
    }

    [Test]
    public async Task GetMyClassesAsync_ShouldReturnUserClasses()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase("GetMyClassesTestDb")
            .Options;
        var context = new ApplicationDbContext(options);

        var userManagerMock = new Mock<UserManager<IdentityUser>>(
            new Mock<IUserStore<IdentityUser>>().Object,
            new Mock<IOptions<IdentityOptions>>().Object,
            new Mock<IPasswordHasher<IdentityUser>>().Object,
            new IUserValidator<IdentityUser>[0],
            new IPasswordValidator<IdentityUser>[0],
            new Mock<ILookupNormalizer>().Object,
            new Mock<IdentityErrorDescriber>().Object,
            new Mock<IServiceProvider>().Object,
            new Mock<ILogger<UserManager<IdentityUser>>>().Object);

        var user = new IdentityUser { Id = "testUserId" };
        userManagerMock.Setup(um => um.FindByIdAsync(It.IsAny<string>())).ReturnsAsync(user);

        var classService = new ClassService(context, userManagerMock.Object);

        var classEntity = new Class
        {
            Id = 1,
            Name = "Morning Yoga",
            ImageUrl = "http://example.com/image.jpg",
            Schedule = DateTime.Now,
            Duration = 60,
            Description = "A relaxing morning yoga session."
        };
        context.Classes.Add(classEntity);

        var classRegistration = new ClassRegistration
        {
            ClassId = 1,
            MemberId = "testUserId"
        };
        context.ClassesRegistrations.Add(classRegistration);

        await context.SaveChangesAsync();

        // Act
        var result = await classService.GetMyClassesAsync("testUserId");

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Count(), Is.EqualTo(1));
        var classViewModel = result.First();
        Assert.That(classViewModel.Id, Is.EqualTo(1));
        Assert.That(classViewModel.Name, Is.EqualTo("Morning Yoga"));
        Assert.That(classViewModel.ImageUrl, Is.EqualTo("http://example.com/image.jpg"));
        Assert.That(classViewModel.Schedule, Is.EqualTo(classEntity.Schedule.ToString("dd-MM-yyyy HH:mm")));
        Assert.That(classViewModel.Duration, Is.EqualTo(60));
    }

    [Test]
    public async Task AddToMyClassesAsync_ShouldAddClassRegistrationSuccessfully()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase("AddToMyClassesTestDb")
            .Options;
        var context = new ApplicationDbContext(options);

        var userManagerMock = new Mock<UserManager<IdentityUser>>(
            new Mock<IUserStore<IdentityUser>>().Object, null, null, null, null, null, null, null, null);
        var user = new IdentityUser { Id = "testUserId" };
        userManagerMock.Setup(um => um.FindByIdAsync(It.IsAny<string>())).ReturnsAsync(user);
        userManagerMock.Setup(um => um.GetRolesAsync(It.IsAny<IdentityUser>())).ReturnsAsync(new List<string> { "Member" });

        var classService = new ClassService(context, userManagerMock.Object);

        var classEntity = new Class
        {
            Id = 1,
            Name = "Morning Yoga",
            Description = "A refreshing morning yoga session.",
            Price = 30.00m,
            ImageUrl = "https://example.com/morning-yoga.jpg",
            Schedule = DateTime.Now,
            Duration = 60,
            InstructorId = 1
        };
        context.Classes.Add(classEntity);
        await context.SaveChangesAsync();

        var model = new ClassesViewModel
        {
            Id = 1,
            Name = "Morning Yoga",
            Description = "A refreshing morning yoga session.",
            Price = 30.00m,
            ImageUrl = "https://example.com/morning-yoga.jpg",
            Schedule = DateTime.Now.ToString(),
            Duration = 60,
            InstructorId = 1
        };

        // Act
        await classService.AddToMyClassesAsync("testUserId", model);

        // Assert
        var registration = await context.ClassesRegistrations
            .FirstOrDefaultAsync(cr => cr.MemberId == "testUserId" && cr.ClassId == 1);
        Assert.That(registration, Is.Not.Null);
    }

    [Test]
    public void AddToMyClassesAsync_ShouldThrowArgumentException_WhenUserIdIsEmpty()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase("AddToMyClassesTestDb")
            .Options;
        var context = new ApplicationDbContext(options);

        var userManagerMock = new Mock<UserManager<IdentityUser>>(
            new Mock<IUserStore<IdentityUser>>().Object, null, null, null, null, null, null, null, null);

        var classService = new ClassService(context, userManagerMock.Object);

        var model = new ClassesViewModel
        {
            Id = 1,
            Name = "Morning Yoga",
            Description = "A refreshing morning yoga session.",
            Price = 30.00m,
            ImageUrl = "https://example.com/morning-yoga.jpg",
            Schedule = DateTime.Now.ToString(),
            Duration = 60,
            InstructorId = 1
        };

        // Act & Assert
        Assert.ThrowsAsync<ArgumentException>(() => classService.AddToMyClassesAsync("", model));
    }

    [Test]
    public void AddToMyClassesAsync_ShouldThrowArgumentNullException_WhenClassesViewModelIsNull()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase("AddToMyClassesTestDb")
            .Options;
        var context = new ApplicationDbContext(options);

        var userManagerMock = new Mock<UserManager<IdentityUser>>(
            new Mock<IUserStore<IdentityUser>>().Object, null, null, null, null, null, null, null, null);

        var classService = new ClassService(context, userManagerMock.Object);

        // Act & Assert
        Assert.ThrowsAsync<ArgumentNullException>(() => classService.AddToMyClassesAsync("testUserId", null));
    }

    [Test]
    public async Task AddToMyClassesAsync_ShouldThrowInvalidOperationException_WhenClassDoesNotExist()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase("AddToMyClassesTestDb")
            .Options;
        var context = new ApplicationDbContext(options);

        var userManagerMock = new Mock<UserManager<IdentityUser>>(
            new Mock<IUserStore<IdentityUser>>().Object, null, null, null, null, null, null, null, null);

        var classService = new ClassService(context, userManagerMock.Object);

        var model = new ClassesViewModel
        {
            Id = 999, // Non-existing class ID
            Name = "Morning Yoga",
            Description = "A refreshing morning yoga session.",
            Price = 30.00m,
            ImageUrl = "https://example.com/morning-yoga.jpg",
            Schedule = DateTime.Now.ToString(),
            Duration = 60,
            InstructorId = 1
        };

        // Act & Assert
        Assert.ThrowsAsync<InvalidOperationException>(() => classService.AddToMyClassesAsync("testUserId", model));
    }

    [TearDown]
    public void TearDown()
    {
        // Ensure the database is deleted after the test
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase("FitnessAppTestDb")
            .Options;
        var context = new ApplicationDbContext(options);
        context.Database.EnsureDeleted();
    }
}