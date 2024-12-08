using FitnessApp.Data;
using FitnessApp.Data.Models;
using FitnessApp.Services.Data;
using FitnessApp.Services.Data.Contracts;
using FitnessApp.Web.ViewModels.InstructorViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace FitnessApp.Tests;

[TestFixture]
public class InstructorServiceTests
{
    private Mock<IInstructorService> _mockClassService;
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
        var instructors = new List<Instructor>
        {
            new Instructor
            {
                Id = 1,
                FirstName = "John",
                LastName = "Doe",
                Specialization = "Yoga",
                Bio = "John is a certified Yoga instructor with 10 years of experience.",
                ImageUrl = "https://www.example.com/image.jpg"
            },
            new Instructor
            {
                Id = 2,
                FirstName = "Jane",
                LastName = "Doe",
                Specialization = "Pilates",
                Bio = "Jane is a certified Pilates instructor with 5 years of experience.",
                ImageUrl = "https://www.example.com/image.jpg"
            }
        };

        context.Instructors.AddRange(instructors);
        context.SaveChanges();
    }

    [Test]
    public async Task GetAllInstructorsAsync_ReturnsAllInstructors_WhenNoSearchQuery()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString()) // Use a unique database name for each test
            .Options;
        var context = new ApplicationDbContext(options);
        SeedData(context);

        var instructorService = new InstructorService(context, _mockUserManager.Object);

        // Act
        var result = await instructorService.GetAllInstructorsAsync();

        // Assert
        Assert.That(result.Count(), Is.EqualTo(2));
        Assert.IsTrue(result.Any(i => i.FirstName == "John" && i.LastName == "Doe"));
        Assert.IsTrue(result.Any(i => i.FirstName == "Jane" && i.LastName == "Doe"));
    }

    [Test]
    public async Task GetAllInstructorsAsync_ReturnsFilteredInstructors_WhenSearchQueryProvided()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString()) // Use a unique database name for each test
            .Options;
        var context = new ApplicationDbContext(options);
        SeedData(context);

        var instructorService = new InstructorService(context, _mockUserManager.Object);

        // Act
        var result = await instructorService.GetAllInstructorsAsync("Yoga");

        // Assert
        Assert.That(result.Count(), Is.EqualTo(1));
        Assert.IsTrue(result.Any(i => i.FirstName == "John" && i.LastName == "Doe"));
    }

    [Test]
    public async Task GetInstructorByIdAsync_ReturnsInstructor_WhenInstructorExists()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString()) // Use a unique database name for each test
            .Options;
        var context = new ApplicationDbContext(options);
        SeedData(context);

        var instructorService = new InstructorService(context, _mockUserManager.Object);

        // Act
        var result = await instructorService.GetInstructorByIdAsync(1);

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(1, result.Id);
        Assert.AreEqual("John", result.FirstName);
        Assert.AreEqual("Doe", result.LastName);
        Assert.AreEqual("Yoga", result.Specialization);
    }

    [Test]
    public async Task GetInstructorByIdAsync_ReturnsNull_WhenInstructorDoesNotExist()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString()) // Use a unique database name for each test
            .Options;
        var context = new ApplicationDbContext(options);
        SeedData(context);

        var instructorService = new InstructorService(context, _mockUserManager.Object);

        // Act
        var result = await instructorService.GetInstructorByIdAsync(999); // Assuming 999 is an ID that doesn't exist

        // Assert
        Assert.IsNull(result);
    }

    [Test]
    public async Task GetInstructorDetailsAsync_ReturnsInstructorDetails_WhenInstructorExists()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString()) // Use a unique database name for each test
            .Options;
        var context = new ApplicationDbContext(options);
        SeedData(context);

        var instructorService = new InstructorService(context, _mockUserManager.Object);

        // Act
        var result = await instructorService.GetInstructorDetailsAsync(1);

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(1, result.Id);
        Assert.AreEqual("John", result.FirstName);
        Assert.AreEqual("Doe", result.LastName);
        Assert.AreEqual("Yoga", result.Specialization);
        Assert.AreEqual("John is a certified Yoga instructor with 10 years of experience.", result.Bio);
        Assert.AreEqual("https://www.example.com/image.jpg", result.ImageUrl);
    }

    [Test]
    public async Task GetInstructorDetailsAsync_ReturnsNull_WhenInstructorDoesNotExist()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString()) // Use a unique database name for each test
            .Options;
        var context = new ApplicationDbContext(options);
        SeedData(context);

        var instructorService = new InstructorService(context, _mockUserManager.Object);

        // Act
        var result = await instructorService.GetInstructorDetailsAsync(999); // Assuming 999 is an ID that doesn't exist

        // Assert
        Assert.IsNull(result);
    }

    [Test]
    public async Task GetInstructorForAddAsync_ReturnsEmptyAddInstructorViewModel()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString()) // Use a unique database name for each test
            .Options;
        var context = new ApplicationDbContext(options);
        SeedData(context);

        var instructorService = new InstructorService(context, _mockUserManager.Object);

        // Act
        var result = await instructorService.GetInstructorForAddAsync();

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(string.Empty, result.FirstName);
        Assert.AreEqual(string.Empty, result.LastName);
        Assert.AreEqual(string.Empty, result.ImageUrl);
        Assert.AreEqual(string.Empty, result.Bio);
        Assert.AreEqual(string.Empty, result.Specialization);
    }

    [Test]
    public async Task AddInstructorAsync_AddsInstructor_WhenUserIsAdmin()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString()) // Use a unique database name for each test
            .Options;
        var context = new ApplicationDbContext(options);
        SeedData(context);

        var store = new Mock<IUserStore<IdentityUser>>();
        var userManager = new Mock<UserManager<IdentityUser>>(store.Object, null, null, null, null, null, null, null, null);

        var adminUser = new IdentityUser { Id = "adminId" };
        userManager.Setup(um => um.FindByIdAsync("adminId")).ReturnsAsync(adminUser);
        userManager.Setup(um => um.IsInRoleAsync(adminUser, "Admin")).ReturnsAsync(true);

        var instructorService = new InstructorService(context, userManager.Object);

        var model = new AddInstructorViewModel
        {
            FirstName = "New",
            LastName = "Instructor",
            ImageUrl = "https://www.example.com/newimage.jpg",
            Bio = "New instructor bio",
            Specialization = "New Specialization"
        };

        // Act
        await instructorService.AddInstructorAsync(model, "adminId");

        // Assert
        var instructors = context.Instructors.ToList();
        Assert.That(instructors.Count, Is.EqualTo(3)); // 2 seeded + 1 new
        Assert.IsTrue(instructors.Any(i => i.FirstName == "New" && i.LastName == "Instructor"));
    }

    [Test]
    public void AddInstructorAsync_ThrowsInvalidOperationException_WhenUserIsNotAdmin()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString()) // Use a unique database name for each test
            .Options;
        var context = new ApplicationDbContext(options);
        SeedData(context);

        var store = new Mock<IUserStore<IdentityUser>>();
        var userManager = new Mock<UserManager<IdentityUser>>(store.Object, null, null, null, null, null, null, null, null);

        var nonAdminUser = new IdentityUser { Id = "nonAdminId" };
        userManager.Setup(um => um.FindByIdAsync("nonAdminId")).ReturnsAsync(nonAdminUser);
        userManager.Setup(um => um.IsInRoleAsync(nonAdminUser, "Admin")).ReturnsAsync(false);

        var instructorService = new InstructorService(context, userManager.Object);

        var model = new AddInstructorViewModel
        {
            FirstName = "New",
            LastName = "Instructor",
            ImageUrl = "https://www.example.com/newimage.jpg",
            Bio = "New instructor bio",
            Specialization = "New Specialization"
        };

        // Act & Assert
        Assert.ThrowsAsync<InvalidOperationException>(async () => await instructorService.AddInstructorAsync(model, "nonAdminId"));
    }

    [Test]
    public void AddInstructorAsync_ThrowsArgumentNullException_WhenModelIsNull()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString()) // Use a unique database name for each test
            .Options;
        var context = new ApplicationDbContext(options);
        SeedData(context);

        var store = new Mock<IUserStore<IdentityUser>>();
        var userManager = new Mock<UserManager<IdentityUser>>(store.Object, null, null, null, null, null, null, null, null);

        var adminUser = new IdentityUser { Id = "adminId" };
        userManager.Setup(um => um.FindByIdAsync("adminId")).ReturnsAsync(adminUser);
        userManager.Setup(um => um.IsInRoleAsync(adminUser, "Admin")).ReturnsAsync(true);

        var instructorService = new InstructorService(context, userManager.Object);

        // Act & Assert
        Assert.ThrowsAsync<ArgumentNullException>(async () => await instructorService.AddInstructorAsync(null, "adminId"));
    }

    [Test]
    public async Task GetInstructorForDeleteAsync_ReturnsDeleteInstructorViewModel_WhenInstructorExists()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString()) // Use a unique database name for each test
            .Options;
        var context = new ApplicationDbContext(options);
        SeedData(context);

        var instructorService = new InstructorService(context, _mockUserManager.Object);

        // Act
        var result = await instructorService.GetInstructorForDeleteAsync(1);

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(1, result.Id);
        Assert.AreEqual("John", result.FirstName);
        Assert.AreEqual("Doe", result.LastName);
    }

    [Test]
    public async Task GetInstructorForDeleteAsync_ReturnsNull_WhenInstructorDoesNotExist()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString()) // Use a unique database name for each test
            .Options;
        var context = new ApplicationDbContext(options);
        SeedData(context);

        var instructorService = new InstructorService(context, _mockUserManager.Object);

        // Act
        var result = await instructorService.GetInstructorForDeleteAsync(999); // Assuming 999 is an ID that doesn't exist

        // Assert
        Assert.IsNull(result);
    }

    [Test]
    public async Task EditInstructorAsync_UpdatesInstructor_WhenUserIsAdmin()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString()) // Use a unique database name for each test
            .Options;
        var context = new ApplicationDbContext(options);
        SeedData(context);

        var store = new Mock<IUserStore<IdentityUser>>();
        var userManager = new Mock<UserManager<IdentityUser>>(store.Object, null, null, null, null, null, null, null, null);

        var adminUser = new IdentityUser { Id = "adminId" };
        userManager.Setup(um => um.FindByIdAsync("adminId")).ReturnsAsync(adminUser);
        userManager.Setup(um => um.IsInRoleAsync(adminUser, "Admin")).ReturnsAsync(true);

        var instructorService = new InstructorService(context, userManager.Object);

        var model = new InstructorViewModel
        {
            Id = 1,
            FirstName = "Updated",
            LastName = "Instructor",
            ImageUrl = "https://www.example.com/updatedimage.jpg",
            Bio = "Updated instructor bio",
            Specialization = "Updated Specialization"
        };

        // Act
        await instructorService.EditInstructorAsync(model, "adminId");

        // Assert
        var instructor = context.Instructors.Find(1);
        Assert.IsNotNull(instructor);
        Assert.AreEqual("Updated", instructor.FirstName);
        Assert.AreEqual("Instructor", instructor.LastName);
        Assert.AreEqual("https://www.example.com/updatedimage.jpg", instructor.ImageUrl);
        Assert.AreEqual("Updated instructor bio", instructor.Bio);
        Assert.AreEqual("Updated Specialization", instructor.Specialization);
    }

    [Test]
    public void EditInstructorAsync_ThrowsInvalidOperationException_WhenUserIsNotAdmin()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString()) // Use a unique database name for each test
            .Options;
        var context = new ApplicationDbContext(options);
        SeedData(context);

        var store = new Mock<IUserStore<IdentityUser>>();
        var userManager = new Mock<UserManager<IdentityUser>>(store.Object, null, null, null, null, null, null, null, null);

        var nonAdminUser = new IdentityUser { Id = "nonAdminId" };
        userManager.Setup(um => um.FindByIdAsync("nonAdminId")).ReturnsAsync(nonAdminUser);
        userManager.Setup(um => um.IsInRoleAsync(nonAdminUser, "Admin")).ReturnsAsync(false);

        var instructorService = new InstructorService(context, userManager.Object);

        var model = new InstructorViewModel
        {
            Id = 1,
            FirstName = "Updated",
            LastName = "Instructor",
            ImageUrl = "https://www.example.com/updatedimage.jpg",
            Bio = "Updated instructor bio",
            Specialization = "Updated Specialization"
        };

        // Act & Assert
        Assert.ThrowsAsync<InvalidOperationException>(async () => await instructorService.EditInstructorAsync(model, "nonAdminId"));
    }

    [Test]
    public void EditInstructorAsync_ThrowsArgumentNullException_WhenModelIsNull()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString()) // Use a unique database name for each test
            .Options;
        var context = new ApplicationDbContext(options);
        SeedData(context);

        var store = new Mock<IUserStore<IdentityUser>>();
        var userManager = new Mock<UserManager<IdentityUser>>(store.Object, null, null, null, null, null, null, null, null);

        var adminUser = new IdentityUser { Id = "adminId" };
        userManager.Setup(um => um.FindByIdAsync("adminId")).ReturnsAsync(adminUser);
        userManager.Setup(um => um.IsInRoleAsync(adminUser, "Admin")).ReturnsAsync(true);

        var instructorService = new InstructorService(context, userManager.Object);

        // Act & Assert
        Assert.ThrowsAsync<ArgumentNullException>(async () => await instructorService.EditInstructorAsync(null, "adminId"));
    }

    [Test]
    public void EditInstructorAsync_ThrowsInvalidOperationException_WhenInstructorDoesNotExist()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString()) // Use a unique database name for each test
            .Options;
        var context = new ApplicationDbContext(options);
        SeedData(context);

        var store = new Mock<IUserStore<IdentityUser>>();
        var userManager = new Mock<UserManager<IdentityUser>>(store.Object, null, null, null, null, null, null, null, null);

        var adminUser = new IdentityUser { Id = "adminId" };
        userManager.Setup(um => um.FindByIdAsync("adminId")).ReturnsAsync(adminUser);
        userManager.Setup(um => um.IsInRoleAsync(adminUser, "Admin")).ReturnsAsync(true);

        var instructorService = new InstructorService(context, userManager.Object);

        var model = new InstructorViewModel
        {
            Id = 999, // Assuming 999 is an ID that doesn't exist
            FirstName = "Updated",
            LastName = "Instructor",
            ImageUrl = "https://www.example.com/updatedimage.jpg",
            Bio = "Updated instructor bio",
            Specialization = "Updated Specialization"
        };

        // Act & Assert
        Assert.ThrowsAsync<InvalidOperationException>(async () => await instructorService.EditInstructorAsync(model, "adminId"));
    }

    [Test]
    public async Task DeleteInstructorAsync_DeletesInstructor_WhenUserIsAdmin()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString()) // Use a unique database name for each test
            .Options;
        var context = new ApplicationDbContext(options);
        SeedData(context);

        var store = new Mock<IUserStore<IdentityUser>>();
        var userManager = new Mock<UserManager<IdentityUser>>(store.Object, null, null, null, null, null, null, null, null);

        var adminUser = new IdentityUser { Id = "adminId" };
        userManager.Setup(um => um.FindByIdAsync("adminId")).ReturnsAsync(adminUser);
        userManager.Setup(um => um.IsInRoleAsync(adminUser, "Admin")).ReturnsAsync(true);

        var instructorService = new InstructorService(context, userManager.Object);

        // Act
        await instructorService.DeleteInstructorAsync(1, "adminId");

        // Assert
        var instructor = context.Instructors.Find(1);
        Assert.IsNull(instructor);
    }

    [Test]
    public void DeleteInstructorAsync_ThrowsInvalidOperationException_WhenUserIsNotAdmin()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString()) // Use a unique database name for each test
            .Options;
        var context = new ApplicationDbContext(options);
        SeedData(context);

        var store = new Mock<IUserStore<IdentityUser>>();
        var userManager = new Mock<UserManager<IdentityUser>>(store.Object, null, null, null, null, null, null, null, null);

        var nonAdminUser = new IdentityUser { Id = "nonAdminId" };
        userManager.Setup(um => um.FindByIdAsync("nonAdminId")).ReturnsAsync(nonAdminUser);
        userManager.Setup(um => um.IsInRoleAsync(nonAdminUser, "Admin")).ReturnsAsync(false);

        var instructorService = new InstructorService(context, userManager.Object);

        // Act & Assert
        Assert.ThrowsAsync<InvalidOperationException>(async () => await instructorService.DeleteInstructorAsync(1, "nonAdminId"));
    }

    [Test]
    public void DeleteInstructorAsync_ThrowsInvalidOperationException_WhenInstructorDoesNotExist()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString()) // Use a unique database name for each test
            .Options;
        var context = new ApplicationDbContext(options);
        SeedData(context);

        var store = new Mock<IUserStore<IdentityUser>>();
        var userManager = new Mock<UserManager<IdentityUser>>(store.Object, null, null, null, null, null, null, null, null);

        var adminUser = new IdentityUser { Id = "adminId" };
        userManager.Setup(um => um.FindByIdAsync("adminId")).ReturnsAsync(adminUser);
        userManager.Setup(um => um.IsInRoleAsync(adminUser, "Admin")).ReturnsAsync(true);

        var instructorService = new InstructorService(context, userManager.Object);

        // Act & Assert
        Assert.ThrowsAsync<InvalidOperationException>(async () => await instructorService.DeleteInstructorAsync(999, "adminId")); // Assuming 999 is an ID that doesn't exist
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