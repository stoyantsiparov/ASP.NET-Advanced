using FitnessApp.Data;
using FitnessApp.Data.Models;
using FitnessApp.Services.Data;
using FitnessApp.Services.Data.Contracts;
using FitnessApp.Web.ViewModels.FitnessEventViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace FitnessApp.Tests;

[TestFixture]
public class FitnessEventServiceTests
{
    private Mock<IFitnessEventService> _mockClassService;
    private Mock<ApplicationDbContext> _mockContext;
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
        var fitnessEvents = new List<FitnessEvent>
        {
            new FitnessEvent
            {
                Id = 1,
                Title = "Test Event 1",
                Location = "Test Location 1",
                StartDate = DateTime.UtcNow,
                EndDate = DateTime.UtcNow.AddHours(1),
                ImageUrl = "https://test.com/image1.jpg",
                Description = "Description for Test Event 1"
            },
            new FitnessEvent
            {
                Id = 2,
                Title = "Test Event 2",
                Location = "Test Location 2",
                StartDate = DateTime.UtcNow,
                EndDate = DateTime.UtcNow.AddHours(1),
                ImageUrl = "https://test.com/image2.jpg",
                Description = "Description for Test Event 2"
            }
        };

        context.FitnessEvents.AddRange(fitnessEvents);
        context.SaveChanges();
    }

    [Test]
    public async Task GetAllFitnessEventsAsync_ShouldReturnAllFitnessEvents()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase("FitnessAppTestDb")
            .Options;
        var context = new ApplicationDbContext(options);
        var eventService = new FitnessEventService(context, _mockUserManager.Object);

        // Act
        var result = await eventService.GetAllFitnessEventsAsync(null);

        // Assert
        Assert.That(result.Count, Is.EqualTo(2));  // We have seeded 2 fitness events
    }

    [Test]
    public async Task GetAllFitnessEventsAsync_WithSearchTerm_ShouldReturnFilteredFitnessEvents()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase("FitnessAppTestDb")
            .Options;
        var context = new ApplicationDbContext(options);
        var eventService = new FitnessEventService(context, _mockUserManager.Object);

        // Act
        var result = await eventService.GetAllFitnessEventsAsync("Test Location 1");

        // Assert
        Assert.That(result.Count, Is.EqualTo(1));  // Only one event matches the search term
        Assert.That(result.First().Title, Is.EqualTo("Test Event 1"));
    }

    [Test]
    public async Task GetFitnessEventByIdAsync_ShouldReturnCorrectFitnessEvent()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase("FitnessAppTestDb")
            .Options;
        var context = new ApplicationDbContext(options);
        var eventService = new FitnessEventService(context, _mockUserManager.Object);

        // Act
        var result = await eventService.GetFitnessEventByIdAsync(1);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Id, Is.EqualTo(1));
        Assert.That(result.Title, Is.EqualTo("Test Event 1"));
        Assert.That(result.Location, Is.EqualTo("Test Location 1"));
    }

    [Test]
    public async Task GetFitnessEventDetailsAsync_ShouldReturnCorrectFitnessEventDetails()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase("FitnessAppTestDb")
            .Options;
        var context = new ApplicationDbContext(options);
        var eventService = new FitnessEventService(context, _mockUserManager.Object);

        // Act
        var result = await eventService.GetFitnessEventDetailsAsync(1);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Id, Is.EqualTo(1));
        Assert.That(result.Title, Is.EqualTo("Test Event 1"));
        Assert.That(result.Location, Is.EqualTo("Test Location 1"));
        Assert.That(result.Description, Is.EqualTo("Description for Test Event 1"));
        Assert.That(result.ImageUrl, Is.EqualTo("https://test.com/image1.jpg"));
        Assert.That(result.StartDateTime, Is.EqualTo(DateTime.UtcNow.ToString("dd-MM-yyyy HH:mm")));
        Assert.That(result.EndDateTime, Is.EqualTo(DateTime.UtcNow.AddHours(1).ToString("dd-MM-yyyy HH:mm")));
    }

    [Test]
    public async Task GetMyFitnessEventsAsync_ShouldReturnUserSpecificFitnessEvents()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase("FitnessAppTestDb")
            .Options;
        var context = new ApplicationDbContext(options);

        // Seed data
        var userId = "test-user-id";
        var fitnessEvent = new FitnessEvent
        {
            Id = 3, // Changed Id to avoid conflict
            Title = "Test Event 1",
            Location = "Test Location 1",
            StartDate = DateTime.UtcNow,
            EndDate = DateTime.UtcNow.AddHours(1),
            ImageUrl = "https://test.com/image1.jpg",
            Description = "Description for Test Event 1"
        };
        var eventRegistration = new EventRegistration
        {
            MemberId = userId,
            FitnessEvent = fitnessEvent
        };
        context.FitnessEvents.Add(fitnessEvent);
        context.EventRegistrations.Add(eventRegistration);
        context.SaveChanges();

        var eventService = new FitnessEventService(context, _mockUserManager.Object);

        // Act
        var result = await eventService.GetMyFitnessEventsAsync(userId);

        // Assert
        Assert.That(result.Count(), Is.EqualTo(1));
        var returnedEvent = result.First();
        Assert.That(returnedEvent.Id, Is.EqualTo(fitnessEvent.Id));
        Assert.That(returnedEvent.Title, Is.EqualTo(fitnessEvent.Title));
        Assert.That(returnedEvent.Location, Is.EqualTo(fitnessEvent.Location));
        Assert.That(returnedEvent.ImageUrl, Is.EqualTo(fitnessEvent.ImageUrl));
        Assert.That(returnedEvent.StartDateTime, Is.EqualTo(fitnessEvent.StartDate.ToString("dd-MM-yyyy HH:mm")));
        Assert.That(returnedEvent.EndDateTime, Is.EqualTo(fitnessEvent.EndDate.ToString("dd-MM-yyyy HH:mm")));
    }

    [Test]
    public async Task AddToMyFitnessEventsAsync_ShouldAddEventForMember()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase("FitnessAppTestDb")
            .Options;
        var context = new ApplicationDbContext(options);

        // Seed data
        var userId = "test-user-id";
        var user = new IdentityUser { Id = userId, UserName = "testuser" };
        await context.Users.AddAsync(user);
        await context.SaveChangesAsync();

        var fitnessEvent = new FitnessEvent
        {
            Id = 3,
            Title = "Test Event 3",
            Location = "Test Location 3",
            StartDate = DateTime.UtcNow,
            EndDate = DateTime.UtcNow.AddHours(1),
            ImageUrl = "https://test.com/image3.jpg",
            Description = "Description for Test Event 3"
        };
        await context.FitnessEvents.AddAsync(fitnessEvent);
        await context.SaveChangesAsync();

        var fitnessEventViewModel = new FitnessEventViewModel
        {
            Id = fitnessEvent.Id,
            Title = fitnessEvent.Title,
            Location = fitnessEvent.Location,
            StartDate = fitnessEvent.StartDate.ToString("dd-MM-yyyy HH:mm"),
            EndDate = fitnessEvent.EndDate.ToString("dd-MM-yyyy HH:mm"),
            ImageUrl = fitnessEvent.ImageUrl,
            Description = fitnessEvent.Description
        };

        var store = new Mock<IUserStore<IdentityUser>>();
        var userManager = new Mock<UserManager<IdentityUser>>(store.Object, null, null, null, null, null, null, null, null);
        userManager.Setup(um => um.IsInRoleAsync(It.IsAny<IdentityUser>(), "Member")).ReturnsAsync(true);

        var eventService = new FitnessEventService(context, userManager.Object);

        // Act
        await eventService.AddToMyFitnessEventsAsync(userId, fitnessEventViewModel);

        // Assert
        var registration = await context.EventRegistrations
            .FirstOrDefaultAsync(er => er.MemberId == userId && er.EventId == fitnessEventViewModel.Id);
        Assert.That(registration, Is.Not.Null);
        Assert.That(registration.MemberId, Is.EqualTo(userId));
        Assert.That(registration.EventId, Is.EqualTo(fitnessEventViewModel.Id));
    }

    [Test]
    public async Task RemoveFromMyFitnessEventsAsync_ShouldRemoveEventForMember()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase("FitnessAppTestDb")
            .Options;
        var context = new ApplicationDbContext(options);

        // Seed data
        var userId = "test-user-id";
        var user = new IdentityUser { Id = userId, UserName = "testuser" };
        await context.Users.AddAsync(user);
        await context.SaveChangesAsync();

        var fitnessEvent = new FitnessEvent
        {
            Id = 3,
            Title = "Test Event 3",
            Location = "Test Location 3",
            StartDate = DateTime.UtcNow,
            EndDate = DateTime.UtcNow.AddHours(1),
            ImageUrl = "https://test.com/image3.jpg",
            Description = "Description for Test Event 3"
        };
        await context.FitnessEvents.AddAsync(fitnessEvent);
        await context.SaveChangesAsync();

        var eventRegistration = new EventRegistration
        {
            MemberId = userId,
            EventId = fitnessEvent.Id
        };
        await context.EventRegistrations.AddAsync(eventRegistration);
        await context.SaveChangesAsync();

        var fitnessEventViewModel = new FitnessEventViewModel
        {
            Id = fitnessEvent.Id,
            Title = fitnessEvent.Title,
            Location = fitnessEvent.Location,
            StartDate = fitnessEvent.StartDate.ToString("dd-MM-yyyy HH:mm"),
            EndDate = fitnessEvent.EndDate.ToString("dd-MM-yyyy HH:mm"),
            ImageUrl = fitnessEvent.ImageUrl,
            Description = fitnessEvent.Description
        };

        var store = new Mock<IUserStore<IdentityUser>>();
        var userManager = new Mock<UserManager<IdentityUser>>(store.Object, null, null, null, null, null, null, null, null);
        userManager.Setup(um => um.IsInRoleAsync(It.IsAny<IdentityUser>(), "Member")).ReturnsAsync(true);

        var eventService = new FitnessEventService(context, userManager.Object);

        // Act
        await eventService.RemoveFromMyFitnessEventsAsync(userId, fitnessEventViewModel);

        // Assert
        var registration = await context.EventRegistrations
            .FirstOrDefaultAsync(er => er.MemberId == userId && er.EventId == fitnessEventViewModel.Id);
        Assert.That(registration, Is.Null);
    }

    [Test]
    public async Task GetFitnessEventForAddAsync_ShouldReturnEmptyAddFitnessEventViewModel()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase("FitnessAppTestDb")
            .Options;
        var context = new ApplicationDbContext(options);
        var eventService = new FitnessEventService(context, _mockUserManager.Object);

        // Act
        var result = await eventService.GetFitnessEventForAddAsync();

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Title, Is.EqualTo(string.Empty));
        Assert.That(result.Description, Is.EqualTo(string.Empty));
        Assert.That(result.Location, Is.EqualTo(string.Empty));
        Assert.That(result.ImageUrl, Is.EqualTo(string.Empty));
        Assert.That(result.StartDate, Is.EqualTo(DateTime.Now.ToString("dd-MM-yyyy HH:mm")));
        Assert.That(result.EndDate, Is.EqualTo(DateTime.Now.AddHours(1).ToString("dd-MM-yyyy HH:mm")));
    }

    [Test]
    public async Task AddFitnessEventAsync_ShouldAddEvent_WhenModelIsValidAndUserIsAdmin()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase("FitnessAppTestDb")
            .Options;
        var context = new ApplicationDbContext(options);

        var userId = "admin-user-id";
        var user = new IdentityUser { Id = userId, UserName = "adminuser" };
        await context.Users.AddAsync(user);
        await context.SaveChangesAsync();

        var model = new AddFitnessEventViewModel
        {
            Title = "New Event",
            Description = "New Event Description",
            Location = "New Location",
            ImageUrl = "https://test.com/newimage.jpg",
            StartDate = DateTime.UtcNow.ToString("dd-MM-yyyy HH:mm"),
            EndDate = DateTime.UtcNow.AddHours(1).ToString("dd-MM-yyyy HH:mm")
        };

        var store = new Mock<IUserStore<IdentityUser>>();
        var userManager = new Mock<UserManager<IdentityUser>>(store.Object, null, null, null, null, null, null, null, null);
        userManager.Setup(um => um.FindByIdAsync(userId)).ReturnsAsync(user);
        userManager.Setup(um => um.IsInRoleAsync(user, "Admin")).ReturnsAsync(true);

        var eventService = new FitnessEventService(context, userManager.Object);

        // Act
        await eventService.AddFitnessEventAsync(model, userId);

        // Assert
        var addedEvent = await context.FitnessEvents.FirstOrDefaultAsync(e => e.Title == model.Title);
        Assert.That(addedEvent, Is.Not.Null);
        Assert.That(addedEvent.Title, Is.EqualTo(model.Title));
        Assert.That(addedEvent.Description, Is.EqualTo(model.Description));
        Assert.That(addedEvent.Location, Is.EqualTo(model.Location));
        Assert.That(addedEvent.ImageUrl, Is.EqualTo(model.ImageUrl));
        Assert.That(addedEvent.StartDate, Is.EqualTo(DateTime.Parse(model.StartDate)));
        Assert.That(addedEvent.EndDate, Is.EqualTo(DateTime.Parse(model.EndDate)));
    }

    [Test]
    public void AddFitnessEventAsync_ShouldThrowArgumentNullException_WhenModelIsNull()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase("FitnessAppTestDb")
            .Options;
        var context = new ApplicationDbContext(options);

        var store = new Mock<IUserStore<IdentityUser>>();
        var userManager = new Mock<UserManager<IdentityUser>>(store.Object, null, null, null, null, null, null, null, null);
        var eventService = new FitnessEventService(context, userManager.Object);

        // Act & Assert
        Assert.ThrowsAsync<ArgumentNullException>(() => eventService.AddFitnessEventAsync(null, "user-id"));
    }

    [Test]
    public void AddFitnessEventAsync_ShouldThrowUnauthorizedAccessException_WhenUserIsNotAdmin()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase("FitnessAppTestDb")
            .Options;
        var context = new ApplicationDbContext(options);

        var userId = "non-admin-user-id";
        var user = new IdentityUser { Id = userId, UserName = "nonadminuser" };
        context.Users.Add(user);
        context.SaveChanges();

        var model = new AddFitnessEventViewModel
        {
            Title = "New Event",
            Description = "New Event Description",
            Location = "New Location",
            ImageUrl = "https://test.com/newimage.jpg",
            StartDate = DateTime.UtcNow.ToString("dd-MM-yyyy HH:mm"),
            EndDate = DateTime.UtcNow.AddHours(1).ToString("dd-MM-yyyy HH:mm")
        };

        var store = new Mock<IUserStore<IdentityUser>>();
        var userManager = new Mock<UserManager<IdentityUser>>(store.Object, null, null, null, null, null, null, null, null);
        userManager.Setup(um => um.FindByIdAsync(userId)).ReturnsAsync(user);
        userManager.Setup(um => um.IsInRoleAsync(user, "Admin")).ReturnsAsync(false);

        var eventService = new FitnessEventService(context, userManager.Object);

        // Act & Assert
        Assert.ThrowsAsync<UnauthorizedAccessException>(() => eventService.AddFitnessEventAsync(model, userId));
    }

    [Test]
    public void AddFitnessEventAsync_ShouldThrowInvalidOperationException_WhenEndDateIsBeforeStartDate()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase("FitnessAppTestDb")
            .Options;
        var context = new ApplicationDbContext(options);

        var userId = "admin-user-id";
        var user = new IdentityUser { Id = userId, UserName = "adminuser" };
        context.Users.Add(user);
        context.SaveChanges();

        var model = new AddFitnessEventViewModel
        {
            Title = "New Event",
            Description = "New Event Description",
            Location = "New Location",
            ImageUrl = "https://test.com/newimage.jpg",
            StartDate = DateTime.UtcNow.AddHours(1).ToString("dd-MM-yyyy HH:mm"),
            EndDate = DateTime.UtcNow.ToString("dd-MM-yyyy HH:mm")
        };

        var store = new Mock<IUserStore<IdentityUser>>();
        var userManager = new Mock<UserManager<IdentityUser>>(store.Object, null, null, null, null, null, null, null, null);
        userManager.Setup(um => um.FindByIdAsync(userId)).ReturnsAsync(user);
        userManager.Setup(um => um.IsInRoleAsync(user, "Admin")).ReturnsAsync(true);

        var eventService = new FitnessEventService(context, userManager.Object);

        // Act & Assert
        Assert.ThrowsAsync<InvalidOperationException>(() => eventService.AddFitnessEventAsync(model, userId));
    }

    [Test]
    public async Task EditFitnessEventAsync_ShouldEditEvent_WhenModelIsValidAndUserIsAdmin()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString()) // Use unique database name
            .Options;
        var context = new ApplicationDbContext(options);

        var userId = "admin-user-id";
        var user = new IdentityUser { Id = userId, UserName = "adminuser" };
        await context.Users.AddAsync(user);
        await context.SaveChangesAsync();

        var fitnessEvent = new FitnessEvent
        {
            Id = 1,
            Title = "Old Event",
            Description = "Old Description",
            Location = "Old Location",
            ImageUrl = "https://test.com/oldimage.jpg",
            StartDate = DateTime.UtcNow,
            EndDate = DateTime.UtcNow.AddHours(1)
        };
        await context.FitnessEvents.AddAsync(fitnessEvent);
        await context.SaveChangesAsync();

        var model = new FitnessEventViewModel
        {
            Id = fitnessEvent.Id,
            Title = "New Event",
            Description = "New Description",
            Location = "New Location",
            ImageUrl = "https://test.com/newimage.jpg",
            StartDate = DateTime.UtcNow.ToString("dd-MM-yyyy HH:mm"),
            EndDate = DateTime.UtcNow.AddHours(2).ToString("dd-MM-yyyy HH:mm")
        };

        var store = new Mock<IUserStore<IdentityUser>>();
        var userManager = new Mock<UserManager<IdentityUser>>(store.Object, null, null, null, null, null, null, null, null!);
        userManager.Setup(um => um.FindByIdAsync(userId)).ReturnsAsync(user);
        userManager.Setup(um => um.IsInRoleAsync(user, "Admin")).ReturnsAsync(true);

        var eventService = new FitnessEventService(context, userManager.Object);

        // Act
        await eventService.EditFitnessEventAsync(model, userId);

        // Assert
        var editedEvent = await context.FitnessEvents.FirstOrDefaultAsync(e => e.Id == model.Id);
        Assert.That(editedEvent, Is.Not.Null);
        Assert.That(editedEvent.Title, Is.EqualTo(model.Title));
        Assert.That(editedEvent.Description, Is.EqualTo(model.Description));
        Assert.That(editedEvent.Location, Is.EqualTo(model.Location));
        Assert.That(editedEvent.ImageUrl, Is.EqualTo(model.ImageUrl));
        Assert.That(editedEvent.StartDate, Is.EqualTo(DateTime.Parse(model.StartDate)));
        Assert.That(editedEvent.EndDate, Is.EqualTo(DateTime.Parse(model.EndDate)));
    }

    [Test]
    public void EditFitnessEventAsync_ShouldThrowUnauthorizedAccessException_WhenUserIsNotAdmin()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase("FitnessAppTestDb")
            .Options;
        var context = new ApplicationDbContext(options);

        var userId = "non-admin-user-id";
        var user = new IdentityUser { Id = userId, UserName = "nonadminuser" };
        context.Users.Add(user);
        context.SaveChanges();

        var model = new FitnessEventViewModel
        {
            Id = 1,
            Title = "New Event",
            Description = "New Description",
            Location = "New Location",
            ImageUrl = "https://test.com/newimage.jpg",
            StartDate = DateTime.UtcNow.ToString("dd-MM-yyyy HH:mm"),
            EndDate = DateTime.UtcNow.AddHours(2).ToString("dd-MM-yyyy HH:mm")
        };

        var store = new Mock<IUserStore<IdentityUser>>();
        var userManager = new Mock<UserManager<IdentityUser>>(store.Object, null, null, null, null, null, null, null, null);
        userManager.Setup(um => um.FindByIdAsync(userId)).ReturnsAsync(user);
        userManager.Setup(um => um.IsInRoleAsync(user, "Admin")).ReturnsAsync(false);

        var eventService = new FitnessEventService(context, userManager.Object);

        // Act & Assert
        Assert.ThrowsAsync<UnauthorizedAccessException>(() => eventService.EditFitnessEventAsync(model, userId));
    }

    [Test]
    public void EditFitnessEventAsync_ShouldThrowInvalidOperationException_WhenEndDateIsBeforeStartDate()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase("FitnessAppTestDb")
            .Options;
        var context = new ApplicationDbContext(options);

        var userId = "admin-user-id";
        var user = new IdentityUser { Id = userId, UserName = "adminuser" };
        context.Users.Add(user);
        context.SaveChanges();

        var model = new FitnessEventViewModel
        {
            Id = 1,
            Title = "New Event",
            Description = "New Description",
            Location = "New Location",
            ImageUrl = "https://test.com/newimage.jpg",
            StartDate = DateTime.UtcNow.AddHours(2).ToString("dd-MM-yyyy HH:mm"),
            EndDate = DateTime.UtcNow.ToString("dd-MM-yyyy HH:mm")
        };

        var store = new Mock<IUserStore<IdentityUser>>();
        var userManager = new Mock<UserManager<IdentityUser>>(store.Object, null, null, null, null, null, null, null, null);
        userManager.Setup(um => um.FindByIdAsync(userId)).ReturnsAsync(user);
        userManager.Setup(um => um.IsInRoleAsync(user, "Admin")).ReturnsAsync(true);

        var eventService = new FitnessEventService(context, userManager.Object);

        // Act & Assert
        Assert.ThrowsAsync<InvalidOperationException>(() => eventService.EditFitnessEventAsync(model, userId));
    }

    [Test]
    public async Task GetFitnessEventForDeleteAsync_ShouldReturnCorrectViewModel()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString()) // Use unique database name
            .Options;
        var context = new ApplicationDbContext(options);

        var fitnessEvent = new FitnessEvent
        {
            Id = 1,
            Title = "Test Event",
            Description = "Test Description",
            Location = "Test Location",
            ImageUrl = "https://test.com/image.jpg",
            StartDate = DateTime.UtcNow,
            EndDate = DateTime.UtcNow.AddHours(1)
        };
        await context.FitnessEvents.AddAsync(fitnessEvent);
        await context.SaveChangesAsync();

        var mockUserManager = new Mock<UserManager<IdentityUser>>(
            Mock.Of<IUserStore<IdentityUser>>(), null, null, null, null, null, null, null, null);

        var eventService = new FitnessEventService(context, mockUserManager.Object);

        // Act
        var result = await eventService.GetFitnessEventForDeleteAsync(fitnessEvent.Id);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Id, Is.EqualTo(fitnessEvent.Id));
        Assert.That(result.Title, Is.EqualTo(fitnessEvent.Title));
        Assert.That(result.Description, Is.EqualTo(fitnessEvent.Description));
    }

    [Test]
    public async Task DeleteFitnessEventAsync_ShouldDeleteEvent_WhenUserIsAdmin()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString()) // Use unique database name
            .Options;
        var context = new ApplicationDbContext(options);

        var userId = "admin-user-id";
        var user = new IdentityUser { Id = userId, UserName = "adminuser" };
        await context.Users.AddAsync(user);
        await context.SaveChangesAsync();

        var fitnessEvent = new FitnessEvent
        {
            Id = 1,
            Title = "Test Event",
            Description = "Test Description",
            Location = "Test Location",
            ImageUrl = "https://test.com/image.jpg",
            StartDate = DateTime.UtcNow,
            EndDate = DateTime.UtcNow.AddHours(1)
        };
        await context.FitnessEvents.AddAsync(fitnessEvent);
        await context.SaveChangesAsync();

        var store = new Mock<IUserStore<IdentityUser>>();
        var userManager = new Mock<UserManager<IdentityUser>>(store.Object, null, null, null, null, null, null, null, null);
        userManager.Setup(um => um.FindByIdAsync(userId)).ReturnsAsync(user);
        userManager.Setup(um => um.IsInRoleAsync(user, "Admin")).ReturnsAsync(true);

        var eventService = new FitnessEventService(context, userManager.Object);

        // Act
        await eventService.DeleteFitnessEventAsync(fitnessEvent.Id, userId);

        // Assert
        var deletedEvent = await context.FitnessEvents.FindAsync(fitnessEvent.Id);
        Assert.That(deletedEvent, Is.Null);
    }

    [Test]
    public void DeleteFitnessEventAsync_ShouldThrowInvalidOperationException_WhenEventDoesNotExist()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase("FitnessAppTestDb")
            .Options;
        var context = new ApplicationDbContext(options);

        var userId = "admin-user-id";
        var user = new IdentityUser { Id = userId, UserName = "adminuser" };
        context.Users.Add(user);
        context.SaveChanges();

        var store = new Mock<IUserStore<IdentityUser>>();
        var userManager = new Mock<UserManager<IdentityUser>>(store.Object, null, null, null, null, null, null, null, null);
        userManager.Setup(um => um.FindByIdAsync(userId)).ReturnsAsync(user);
        userManager.Setup(um => um.IsInRoleAsync(user, "Admin")).ReturnsAsync(true);

        var eventService = new FitnessEventService(context, userManager.Object);

        // Act & Assert
        Assert.ThrowsAsync<InvalidOperationException>(() => eventService.DeleteFitnessEventAsync(999, userId));
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