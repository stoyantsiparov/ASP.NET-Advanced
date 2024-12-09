using FitnessApp.Data;
using FitnessApp.Data.Models;
using FitnessApp.Services.Data;
using FitnessApp.Services.Data.Contracts;
using FitnessApp.Web.ViewModels.SpaProcedureViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace FitnessApp.Tests;

[TestFixture]
public class SpaProcedureServiceTests
{
    private Mock<ISpaProcedureService> _mockClassService;
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
        var spaProcedures = new List<SpaProcedure>
        {
            new SpaProcedure
            {
                Id = 1,
                Name = "Massage",
                Description = "Relaxing massage",
                Duration = 30,
                Price = 20,
                ImageUrl = "https://www.example.com/image.jpg"
            },
            new SpaProcedure
            {
                Id = 2,
                Name = "Facial",
                Description = "Deep cleansing facial",
                Duration = 45,
                Price = 30,
                ImageUrl = "https://www.example.com/image.jpg"
            }
        };

        context.AddRange(spaProcedures);
        context.SaveChanges();
    }

    [Test]
    public async Task GetAllSpaProceduresAsync_ReturnsAllSpaProcedures()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString()) // Use a unique database name for each test
            .Options;
        var context = new ApplicationDbContext(options);
        SeedData(context);

        var service = new SpaProcedureService(context, _mockUserManager.Object);

        // Act
        var result = await service.GetAllSpaProceduresAsync();

        // Assert
        Assert.That(result.Count(), Is.EqualTo(2));
        Assert.IsTrue(result.Any(sp => sp.Name == "Massage"));
        Assert.IsTrue(result.Any(sp => sp.Name == "Facial"));
    }

    [Test]
    public async Task GetAllSpaProceduresAsync_ReturnsEmptyList_WhenNoSpaProcedures()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString()) // Use a unique database name for each test
            .Options;
        var context = new ApplicationDbContext(options);

        var service = new SpaProcedureService(context, _mockUserManager.Object);

        // Act
        var result = await service.GetAllSpaProceduresAsync();

        // Assert
        Assert.That(result, Is.Empty);
    }

    [Test]
    public async Task GetAllSpaProceduresAsync_MapsDataCorrectly()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString()) // Use a unique database name for each test
            .Options;
        var context = new ApplicationDbContext(options);
        SeedData(context);

        var service = new SpaProcedureService(context, _mockUserManager.Object);

        // Act
        var result = await service.GetAllSpaProceduresAsync();
        var firstProcedure = result.FirstOrDefault(sp => sp.Id == 1);

        // Assert
        Assert.IsNotNull(firstProcedure);
        Assert.That(firstProcedure.Name, Is.EqualTo("Massage"));
        Assert.That(firstProcedure.Description, Is.EqualTo("Relaxing massage"));
        Assert.That(firstProcedure.ImageUrl, Is.EqualTo("https://www.example.com/image.jpg"));
    }

    [Test]
    public async Task GetAllSpaProceduresPaginationAsync_ReturnsPaginatedResults()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString()) // Use a unique database name for each test
            .Options;
        var context = new ApplicationDbContext(options);
        SeedData(context);

        var service = new SpaProcedureService(context, _mockUserManager.Object);

        // Act
        var result = await service.GetAllSpaProceduresPaginationAsync(null, 1, 1);

        // Assert
        Assert.That(result.SpaProcedures.Count, Is.EqualTo(1));
        Assert.That(result.TotalPages, Is.EqualTo(2));
        Assert.That(result.PageNumber, Is.EqualTo(1));
        Assert.That(result.PageSize, Is.EqualTo(1));
        Assert.IsTrue(result.SpaProcedures.Any(sp => sp.Name == "Massage" || sp.Name == "Facial"));
    }

    [Test]
    public async Task GetAllSpaProceduresPaginationAsync_ReturnsFilteredResults()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString()) // Use a unique database name for each test
            .Options;
        var context = new ApplicationDbContext(options);
        SeedData(context);

        var service = new SpaProcedureService(context, _mockUserManager.Object);

        // Act
        var result = await service.GetAllSpaProceduresPaginationAsync("Massage", 1, 10);

        // Assert
        Assert.That(result.SpaProcedures.Count, Is.EqualTo(1));
        Assert.That(result.TotalPages, Is.EqualTo(1));
        Assert.That(result.PageNumber, Is.EqualTo(1));
        Assert.That(result.PageSize, Is.EqualTo(10));
        Assert.IsTrue(result.SpaProcedures.Any(sp => sp.Name == "Massage"));
    }

    [Test]
    public async Task GetAllSpaProceduresPaginationAsync_ReturnsEmptyList_WhenNoMatches()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString()) // Use a unique database name for each test
            .Options;
        var context = new ApplicationDbContext(options);
        SeedData(context);

        var service = new SpaProcedureService(context, _mockUserManager.Object);

        // Act
        var result = await service.GetAllSpaProceduresPaginationAsync("NonExistent", 1, 10);

        // Assert
        Assert.That(result.SpaProcedures, Is.Empty);
        Assert.That(result.TotalPages, Is.EqualTo(0));
        Assert.That(result.PageNumber, Is.EqualTo(1));
        Assert.That(result.PageSize, Is.EqualTo(10));
    }

    [Test]
    public async Task GetSpaProceduresByIdAsync_ReturnsCorrectSpaProcedure()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString()) // Use a unique database name for each test
            .Options;
        var context = new ApplicationDbContext(options);
        SeedData(context);

        var service = new SpaProcedureService(context, _mockUserManager.Object);

        // Act
        var result = await service.GetSpaProceduresByIdAsync(1);

        // Assert
        Assert.IsNotNull(result);
        Assert.That(result.Id, Is.EqualTo(1));
        Assert.That(result.Name, Is.EqualTo("Massage"));
        Assert.That(result.Description, Is.EqualTo("Relaxing massage"));
        Assert.That(result.Price, Is.EqualTo(20));
        Assert.That(result.Duration, Is.EqualTo(30));
        Assert.That(result.ImageUrl, Is.EqualTo("https://www.example.com/image.jpg"));
        Assert.That(result.AppointmentDateTime, Is.EqualTo(DateTime.MinValue.ToString("dd-MM-yyyy HH:mm")));
    }

    [Test]
    public async Task GetSpaProceduresByIdAsync_ReturnsNull_WhenIdNotFound()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString()) // Use a unique database name for each test
            .Options;
        var context = new ApplicationDbContext(options);
        SeedData(context);

        var service = new SpaProcedureService(context, _mockUserManager.Object);

        // Act
        var result = await service.GetSpaProceduresByIdAsync(999); // Assuming 999 is an ID that does not exist

        // Assert
        Assert.IsNull(result);
    }

    [Test]
    public async Task GetSpaProceduresDetailsAsync_ReturnsCorrectDetails()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString()) // Use a unique database name for each test
            .Options;
        var context = new ApplicationDbContext(options);
        SeedData(context);

        var service = new SpaProcedureService(context, _mockUserManager.Object);

        // Act
        var result = await service.GetSpaProceduresDetailsAsync(1);

        // Assert
        Assert.IsNotNull(result);
        Assert.That(result.Id, Is.EqualTo(1));
        Assert.That(result.Name, Is.EqualTo("Massage"));
        Assert.That(result.Description, Is.EqualTo("Relaxing massage"));
        Assert.That(result.Price, Is.EqualTo(20));
        Assert.That(result.Duration, Is.EqualTo(30));
        Assert.That(result.ImageUrl, Is.EqualTo("https://www.example.com/image.jpg"));
        Assert.IsNull(result.AppointmentDateTime);
    }

    [Test]
    public async Task GetSpaProceduresDetailsAsync_ReturnsNull_WhenIdNotFound()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString()) // Use a unique database name for each test
            .Options;
        var context = new ApplicationDbContext(options);
        SeedData(context);

        var service = new SpaProcedureService(context, _mockUserManager.Object);

        // Act
        var result = await service.GetSpaProceduresDetailsAsync(999); // Assuming 999 is an ID that does not exist

        // Assert
        Assert.IsNull(result);
    }

    [Test]
    public async Task GetMySpaProceduresAsync_ReturnsCorrectSpaProceduresForUser()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString()) // Use a unique database name for each test
            .Options;
        var context = new ApplicationDbContext(options);

        // Seed data
        var userId = "test-user-id";
        var spaProcedures = new List<SpaProcedure>
        {
            new SpaProcedure
            {
                Id = 1,
                Name = "Massage",
                Description = "Relaxing massage",
                Duration = 30,
                Price = 20,
                ImageUrl = "https://www.example.com/image.jpg",
                AppointmentDateTime = DateTime.Now
            },
            new SpaProcedure
            {
                Id = 2,
                Name = "Facial",
                Description = "Deep cleansing facial",
                Duration = 45,
                Price = 30,
                ImageUrl = "https://www.example.com/image.jpg",
                AppointmentDateTime = DateTime.Now
            }
        };

        var spaRegistrations = new List<SpaRegistration>
        {
            new SpaRegistration
            {
                MemberId = userId,
                SpaProcedureId = 1,
                SpaProcedure = spaProcedures[0]
            },
            new SpaRegistration
            {
                MemberId = userId,
                SpaProcedureId = 2,
                SpaProcedure = spaProcedures[1]
            }
        };

        context.AddRange(spaProcedures);
        context.AddRange(spaRegistrations);
        context.SaveChanges();

        var service = new SpaProcedureService(context, _mockUserManager.Object);

        // Act
        var result = await service.GetMySpaProceduresAsync(userId);

        // Assert
        Assert.That(result.Count(), Is.EqualTo(2));
        Assert.IsTrue(result.Any(sp => sp.Name == "Massage"));
        Assert.IsTrue(result.Any(sp => sp.Name == "Facial"));
    }

    [Test]
    public void AddToMySpaAppointmentsAsync_ThrowsException_WhenAppointmentDateTimeIsInThePast()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        var context = new ApplicationDbContext(options);
        var service = new SpaProcedureService(context, _mockUserManager.Object);
        var spaProcedure = new SpaProceduresViewModel { Id = 1 };

        // Act & Assert
        var ex = Assert.ThrowsAsync<InvalidOperationException>(async () =>
            await service.AddToMySpaAppointmentsAsync("test-user-id", spaProcedure, DateTime.Now.AddDays(-1)));
        Assert.That(ex.Message, Is.EqualTo("Appointment date and time cannot be in the past."));
    }

    [Test]
    public async Task AddToMySpaAppointmentsAsync_ThrowsException_WhenUserIsNotFound()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        var context = new ApplicationDbContext(options);
        var service = new SpaProcedureService(context, _mockUserManager.Object);
        var spaProcedure = new SpaProceduresViewModel { Id = 1 };

        _mockUserManager.Setup(um => um.FindByIdAsync(It.IsAny<string>())).ReturnsAsync((IdentityUser?)null);

        // Act & Assert
        var ex = Assert.ThrowsAsync<InvalidOperationException>(async () =>
            await service.AddToMySpaAppointmentsAsync("test-user-id", spaProcedure, DateTime.Now.AddDays(1)));
        Assert.That(ex.Message, Is.EqualTo("Only members can book spa procedures."));
    }

    [Test]
    public async Task AddToMySpaAppointmentsAsync_ThrowsException_WhenUserIsNotMember()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        var context = new ApplicationDbContext(options);
        var service = new SpaProcedureService(context, _mockUserManager.Object);
        var spaProcedure = new SpaProceduresViewModel { Id = 1 };

        var user = new IdentityUser { Id = "test-user-id" };
        _mockUserManager.Setup(um => um.FindByIdAsync(It.IsAny<string>())).ReturnsAsync(user);
        _mockUserManager.Setup(um => um.IsInRoleAsync(user, It.IsAny<string>())).ReturnsAsync(false);

        // Act & Assert
        var ex = Assert.ThrowsAsync<InvalidOperationException>(async () =>
            await service.AddToMySpaAppointmentsAsync("test-user-id", spaProcedure, DateTime.Now.AddDays(1)));
        Assert.That(ex.Message, Is.EqualTo("Only members can book spa procedures."));
    }

    [Test]
    public async Task AddToMySpaAppointmentsAsync_ThrowsException_WhenAlreadyBooked()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        var context = new ApplicationDbContext(options);
        var service = new SpaProcedureService(context, _mockUserManager.Object);
        var spaProcedure = new SpaProceduresViewModel { Id = 1 };

        var user = new IdentityUser { Id = "test-user-id" };
        _mockUserManager.Setup(um => um.FindByIdAsync(It.IsAny<string>())).ReturnsAsync(user);
        _mockUserManager.Setup(um => um.IsInRoleAsync(user, It.IsAny<string>())).ReturnsAsync(true);

        var spaRegistration = new SpaRegistration { MemberId = "test-user-id", SpaProcedureId = 1 };
        context.SpaRegistrations.Add(spaRegistration);
        context.SaveChanges();

        // Act & Assert
        var ex = Assert.ThrowsAsync<InvalidOperationException>(async () =>
            await service.AddToMySpaAppointmentsAsync("test-user-id", spaProcedure, DateTime.Now.AddDays(1)));
        Assert.That(ex.Message, Is.EqualTo("You have already booked this appointment."));
    }

    [Test]
    public async Task AddToMySpaAppointmentsAsync_AddsAppointmentSuccessfully()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        var context = new ApplicationDbContext(options);
        var service = new SpaProcedureService(context, _mockUserManager.Object);
        var spaProcedure = new SpaProceduresViewModel { Id = 1 };

        var user = new IdentityUser { Id = "test-user-id" };
        _mockUserManager.Setup(um => um.FindByIdAsync(It.IsAny<string>())).ReturnsAsync(user);
        _mockUserManager.Setup(um => um.IsInRoleAsync(user, It.IsAny<string>())).ReturnsAsync(true);

        var spaProcedureEntity = new SpaProcedure
        {
            Id = 1,
            Name = "Massage",
            Description = "Relaxing massage",
            Duration = 30,
            Price = 20,
            ImageUrl = "https://www.example.com/image.jpg"
        };
        context.SpaProcedures.Add(spaProcedureEntity);
        context.SaveChanges();

        var appointmentDateTime = DateTime.Now.AddDays(1);

        // Act
        await service.AddToMySpaAppointmentsAsync("test-user-id", spaProcedure, appointmentDateTime);

        // Assert
        var registration = context.SpaRegistrations.FirstOrDefault(sr => sr.MemberId == "test-user-id" && sr.SpaProcedureId == 1);
        Assert.IsNotNull(registration);
        var updatedProcedure = context.SpaProcedures.FirstOrDefault(sp => sp.Id == 1);
        Assert.IsNotNull(updatedProcedure);
        Assert.That(updatedProcedure.AppointmentDateTime, Is.EqualTo(appointmentDateTime));
    }

    [Test]
    public async Task GetSpaProcedureForAddAsync_ReturnsDefaultAddSpaProcedureViewModel()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        var context = new ApplicationDbContext(options);
        var service = new SpaProcedureService(context, _mockUserManager.Object);

        // Act
        var result = await service.GetSpaProcedureForAddAsync();

        // Assert
        Assert.IsNotNull(result);
        Assert.That(result.Name, Is.EqualTo(string.Empty));
        Assert.That(result.ImageUrl, Is.EqualTo(string.Empty));
        Assert.That(result.Description, Is.EqualTo(string.Empty));
        Assert.That(result.Duration, Is.EqualTo(0));
        Assert.That(result.Price, Is.EqualTo(0.0m));
    }

    [Test]
    public async Task RemoveFromMySpaAppointmentsAsync_RemovesAppointmentSuccessfully()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        var context = new ApplicationDbContext(options);
        var service = new SpaProcedureService(context, _mockUserManager.Object);

        var userId = "test-user-id";
        var spaProcedure = new SpaProcedure
        {
            Id = 1,
            Name = "Massage",
            Description = "Relaxing massage",
            Duration = 30,
            Price = 20,
            ImageUrl = "https://www.example.com/image.jpg"
        };
        var spaRegistration = new SpaRegistration
        {
            MemberId = userId,
            SpaProcedureId = spaProcedure.Id,
            SpaProcedure = spaProcedure
        };

        context.SpaProcedures.Add(spaProcedure);
        context.SpaRegistrations.Add(spaRegistration);
        context.SaveChanges();

        var spaProcedureViewModel = new SpaProceduresViewModel { Id = spaProcedure.Id };

        // Act
        await service.RemoveFromMySpaAppointmentsAsync(userId, spaProcedureViewModel);

        // Assert
        var registration = context.SpaRegistrations.FirstOrDefault(sr => sr.MemberId == userId && sr.SpaProcedureId == spaProcedure.Id);
        Assert.IsNull(registration);
    }

    [Test]
    public void RemoveFromMySpaAppointmentsAsync_ThrowsException_WhenAppointmentNotBooked()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        var context = new ApplicationDbContext(options);
        var service = new SpaProcedureService(context, _mockUserManager.Object);

        var spaProcedureViewModel = new SpaProceduresViewModel { Id = 1 };

        // Act & Assert
        var ex = Assert.ThrowsAsync<InvalidOperationException>(async () =>
            await service.RemoveFromMySpaAppointmentsAsync("test-user-id", spaProcedureViewModel));
        Assert.That(ex.Message, Is.EqualTo("You haven't booked a spa appointment"));
    }

    [Test]
    public async Task AddSpaProcedureAsync_AddsProcedureSuccessfully()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        var context = new ApplicationDbContext(options);
        var userManagerMock = new Mock<UserManager<IdentityUser>>(
            Mock.Of<IUserStore<IdentityUser>>(), null, null, null, null, null, null, null, null);
        var service = new SpaProcedureService(context, userManagerMock.Object);

        var user = new IdentityUser { Id = "admin-user-id" };
        userManagerMock.Setup(um => um.FindByIdAsync(It.IsAny<string>())).ReturnsAsync(user);
        userManagerMock.Setup(um => um.IsInRoleAsync(user, "Admin")).ReturnsAsync(true);

        var model = new AddSpaProcedureViewModel
        {
            Name = "New Procedure",
            ImageUrl = "https://www.example.com/image.jpg",
            Description = "New procedure description",
            Duration = 60,
            Price = 50
        };

        // Act
        await service.AddSpaProcedureAsync(model, "admin-user-id");

        // Assert
        var spaProcedure = context.SpaProcedures.FirstOrDefault(sp => sp.Name == "New Procedure");
        Assert.IsNotNull(spaProcedure);
        Assert.That(spaProcedure.Name, Is.EqualTo("New Procedure"));
        Assert.That(spaProcedure.ImageUrl, Is.EqualTo("https://www.example.com/image.jpg"));
        Assert.That(spaProcedure.Description, Is.EqualTo("New procedure description"));
        Assert.That(spaProcedure.Duration, Is.EqualTo(60));
        Assert.That(spaProcedure.Price, Is.EqualTo(50));
    }
    [Test]
    public async Task EditSpaProcedureAsync_ThrowsException_WhenUserIsNotAdmin()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        var context = new ApplicationDbContext(options);
        var service = new SpaProcedureService(context, _mockUserManager.Object);
        var spaProcedure = new SpaProceduresViewModel { Id = 1 };

        var user = new IdentityUser { Id = "test-user-id" };
        _mockUserManager.Setup(um => um.FindByIdAsync(It.IsAny<string>())).ReturnsAsync(user);
        _mockUserManager.Setup(um => um.IsInRoleAsync(user, It.IsAny<string>())).ReturnsAsync(false);

        // Act & Assert
        var ex = Assert.ThrowsAsync<UnauthorizedAccessException>(async () =>
            await service.EditSpaProcedureAsync(spaProcedure, "test-user-id"));
        Assert.That(ex.Message, Is.EqualTo("You are not authorized to edit this element."));
    }

    [Test]
    public async Task EditSpaProcedureAsync_ThrowsException_WhenSpaProcedureNotFound()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        var context = new ApplicationDbContext(options);
        var service = new SpaProcedureService(context, _mockUserManager.Object);
        var spaProcedure = new SpaProceduresViewModel { Id = 1 };

        var user = new IdentityUser { Id = "admin-user-id" };
        _mockUserManager.Setup(um => um.FindByIdAsync(It.IsAny<string>())).ReturnsAsync(user);
        _mockUserManager.Setup(um => um.IsInRoleAsync(user, "Admin")).ReturnsAsync(true);

        // Act & Assert
        var ex = Assert.ThrowsAsync<InvalidOperationException>(async () =>
            await service.EditSpaProcedureAsync(spaProcedure, "admin-user-id"));
        Assert.That(ex.Message, Is.EqualTo("Spa procedure not found."));
    }

    [Test]
    public async Task EditSpaProcedureAsync_EditsProcedureSuccessfully()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        var context = new ApplicationDbContext(options);
        var service = new SpaProcedureService(context, _mockUserManager.Object);

        var user = new IdentityUser { Id = "admin-user-id" };
        _mockUserManager.Setup(um => um.FindByIdAsync(It.IsAny<string>())).ReturnsAsync(user);
        _mockUserManager.Setup(um => um.IsInRoleAsync(user, "Admin")).ReturnsAsync(true);

        var spaProcedureEntity = new SpaProcedure
        {
            Id = 1,
            Name = "Old Name",
            ImageUrl = "https://www.example.com/old-image.jpg",
            Description = "Old description",
            Duration = 30,
            Price = 20
        };
        context.SpaProcedures.Add(spaProcedureEntity);
        context.SaveChanges();

        var spaProcedure = new SpaProceduresViewModel
        {
            Id = 1,
            Name = "New Name",
            ImageUrl = "https://www.example.com/new-image.jpg",
            Description = "New description",
            Duration = 60,
            Price = 50
        };

        // Act
        await service.EditSpaProcedureAsync(spaProcedure, "admin-user-id");

        // Assert
        var updatedProcedure = context.SpaProcedures.FirstOrDefault(sp => sp.Id == 1);
        Assert.IsNotNull(updatedProcedure);
        Assert.That(updatedProcedure.Name, Is.EqualTo("New Name"));
        Assert.That(updatedProcedure.ImageUrl, Is.EqualTo("https://www.example.com/new-image.jpg"));
        Assert.That(updatedProcedure.Description, Is.EqualTo("New description"));
        Assert.That(updatedProcedure.Duration, Is.EqualTo(60));
        Assert.That(updatedProcedure.Price, Is.EqualTo(50));
    }

    [Test]
    public async Task GetSpaProcedureForDeleteAsync_ReturnsCorrectViewModel()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        var context = new ApplicationDbContext(options);
        var service = new SpaProcedureService(context, _mockUserManager.Object);

        var spaProcedure = new SpaProcedure
        {
            Id = 1,
            Name = "Test Procedure",
            Description = "Test Description",
            Duration = 30,
            Price = 20,
            ImageUrl = "https://www.example.com/image.jpg"
        };
        context.SpaProcedures.Add(spaProcedure);
        context.SaveChanges();

        // Act
        var result = await service.GetSpaProcedureForDeleteAsync(1);

        // Assert
        Assert.IsNotNull(result);
        Assert.That(result.Id, Is.EqualTo(1));
        Assert.That(result.Name, Is.EqualTo("Test Procedure"));
        Assert.That(result.Description, Is.EqualTo("Test Description"));
    }

    [Test]
    public async Task GetSpaProcedureForDeleteAsync_ReturnsNull_WhenIdNotFound()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        var context = new ApplicationDbContext(options);
        var service = new SpaProcedureService(context, _mockUserManager.Object);

        // Act
        var result = await service.GetSpaProcedureForDeleteAsync(999); // Non-existent ID

        // Assert
        Assert.IsNull(result);
    }

    [Test]
    public async Task DeleteSpaProcedureAsync_ThrowsException_WhenUserIsNotAdmin()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        var context = new ApplicationDbContext(options);
        var userManagerMock = new Mock<UserManager<IdentityUser>>(
            Mock.Of<IUserStore<IdentityUser>>(), null, null, null, null, null, null, null, null);
        var service = new SpaProcedureService(context, userManagerMock.Object);

        var user = new IdentityUser { Id = "test-user-id" };
        userManagerMock.Setup(um => um.FindByIdAsync(It.IsAny<string>())).ReturnsAsync(user);
        userManagerMock.Setup(um => um.IsInRoleAsync(user, "Admin")).ReturnsAsync(false);

        // Act & Assert
        var ex = Assert.ThrowsAsync<UnauthorizedAccessException>(async () =>
            await service.DeleteSpaProcedureAsync(1, "test-user-id"));
        Assert.That(ex.Message, Is.EqualTo("You are not authorized to delete this element."));
    }

    [Test]
    public async Task DeleteSpaProcedureAsync_ThrowsException_WhenSpaProcedureNotFound()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        var context = new ApplicationDbContext(options);
        var userManagerMock = new Mock<UserManager<IdentityUser>>(
            Mock.Of<IUserStore<IdentityUser>>(), null, null, null, null, null, null, null, null);
        var service = new SpaProcedureService(context, userManagerMock.Object);

        var user = new IdentityUser { Id = "admin-user-id" };
        userManagerMock.Setup(um => um.FindByIdAsync(It.IsAny<string>())).ReturnsAsync(user);
        userManagerMock.Setup(um => um.IsInRoleAsync(user, "Admin")).ReturnsAsync(true);

        // Act & Assert
        var ex = Assert.ThrowsAsync<InvalidOperationException>(async () =>
            await service.DeleteSpaProcedureAsync(999, "admin-user-id")); // Non-existent ID
        Assert.That(ex.Message, Is.EqualTo("Spa procedure not found."));
    }

    [Test]
    public async Task DeleteSpaProcedureAsync_DeletesProcedureSuccessfully()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        var context = new ApplicationDbContext(options);
        var userManagerMock = new Mock<UserManager<IdentityUser>>(
            Mock.Of<IUserStore<IdentityUser>>(), null, null, null, null, null, null, null, null);
        var service = new SpaProcedureService(context, userManagerMock.Object);

        var user = new IdentityUser { Id = "admin-user-id" };
        userManagerMock.Setup(um => um.FindByIdAsync(It.IsAny<string>())).ReturnsAsync(user);
        userManagerMock.Setup(um => um.IsInRoleAsync(user, "Admin")).ReturnsAsync(true);

        var spaProcedure = new SpaProcedure
        {
            Id = 1,
            Name = "Test Procedure",
            Description = "Test Description",
            Duration = 30,
            Price = 20,
            ImageUrl = "https://www.example.com/image.jpg"
        };
        context.SpaProcedures.Add(spaProcedure);
        context.SaveChanges();

        // Act
        await service.DeleteSpaProcedureAsync(1, "admin-user-id");

        // Assert
        var deletedProcedure = context.SpaProcedures.FirstOrDefault(sp => sp.Id == 1);
        Assert.IsNull(deletedProcedure);
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