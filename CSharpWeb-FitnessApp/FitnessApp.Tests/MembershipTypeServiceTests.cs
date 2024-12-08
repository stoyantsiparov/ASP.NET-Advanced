using FitnessApp.Data;
using FitnessApp.Data.Models;
using FitnessApp.Services.Data;
using FitnessApp.Services.Data.Contracts;
using FitnessApp.Web.ViewModels.MembershipTypeViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace FitnessApp.Tests;

[TestFixture]
public class MembershipTypeServiceTests
{
    private Mock<IMembershipTypeService> _mockClassService;
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
        var membershipTypes = new List<MembershipType>
        {
            new MembershipType
            {
                Id = 1,
                Name = "Basic",
                Price = 20,
                Duration = 30,
                Description = "Basic membership type",
                ImageUrl = "https://www.example.com/image1.jpg"
            },
            new MembershipType
            {
                Id = 2,
                Name = "Premium",
                Price = 40,
                Duration = 60,
                Description = "Premium membership type",
                ImageUrl = "https://www.example.com/image2.jpg"
            }
        };

        context.MembershipTypes.AddRange(membershipTypes);
        context.SaveChanges();
    }

    [Test]
    public async Task GetMembershipTypeDetailsAsync_ReturnsCorrectMembershipTypeDetails()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString()) // Use a unique database name for each test
            .Options;
        var context = new ApplicationDbContext(options);
        SeedData(context);

        var service = new MembershipTypeService(context, _mockUserManager.Object);

        // Act
        var result = await service.GetMembershipTypeDetailsAsync(1);

        // Assert
        Assert.IsNotNull(result);
        Assert.That(result.Id, Is.EqualTo(1));
        Assert.That(result.Name, Is.EqualTo("Basic"));
        Assert.That(result.Price, Is.EqualTo(20));
        Assert.That(result.Duration, Is.EqualTo(30));
        Assert.That(result.Description, Is.EqualTo("Basic membership type"));
        Assert.That(result.ImageUrl, Is.EqualTo("https://www.example.com/image1.jpg"));
    }

    [Test]
    public async Task GetMembershipTypeDetailsAsync_ReturnsNullForNonExistentId()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString()) // Use a unique database name for each test
            .Options;
        var context = new ApplicationDbContext(options);
        SeedData(context);

        var service = new MembershipTypeService(context, _mockUserManager.Object);

        // Act
        var result = await service.GetMembershipTypeDetailsAsync(999); // Non-existent ID

        // Assert
        Assert.IsNull(result);
    }

    [Test]
    public async Task GetMembershipTypeByIdAsync_ReturnsCorrectMembershipType()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString()) // Use a unique database name for each test
            .Options;
        var context = new ApplicationDbContext(options);
        SeedData(context);

        var service = new MembershipTypeService(context, _mockUserManager.Object);

        // Act
        var result = await service.GetMembershipTypeByIdAsync(1);

        // Assert
        Assert.IsNotNull(result);
        Assert.That(result.Id, Is.EqualTo(1));
        Assert.That(result.Name, Is.EqualTo("Basic"));
        Assert.That(result.Price, Is.EqualTo(20));
        Assert.That(result.Duration, Is.EqualTo(30));
        Assert.That(result.Description, Is.EqualTo("Basic membership type"));
        Assert.That(result.ImageUrl, Is.EqualTo("https://www.example.com/image1.jpg"));
    }

    [Test]
    public async Task GetMembershipTypeByIdAsync_ReturnsNullForNonExistentId()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString()) // Use a unique database name for each test
            .Options;
        var context = new ApplicationDbContext(options);
        SeedData(context);

        var service = new MembershipTypeService(context, _mockUserManager.Object);

        // Act
        var result = await service.GetMembershipTypeByIdAsync(999); // Non-existent ID

        // Assert
        Assert.IsNull(result);
    }

    [Test]
    public async Task GetAllMembershipTypesAsync_ReturnsAllMembershipTypes()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString()) // Use a unique database name for each test
            .Options;
        var context = new ApplicationDbContext(options);
        SeedData(context);

        var service = new MembershipTypeService(context, _mockUserManager.Object);

        // Act
        var result = await service.GetAllMembershipTypesAsync();

        // Assert
        Assert.That(result.Count(), Is.EqualTo(2));
        Assert.IsTrue(result.Any(m => m.Name == "Basic"));
        Assert.IsTrue(result.Any(m => m.Name == "Premium"));
    }

    [Test]
    public async Task GetAllMembershipTypesAsync_ReturnsCorrectViewModel()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString()) // Use a unique database name for each test
            .Options;
        var context = new ApplicationDbContext(options);
        SeedData(context);

        var service = new MembershipTypeService(context, _mockUserManager.Object);

        // Act
        var result = await service.GetAllMembershipTypesAsync();

        // Assert
        Assert.That(result, Is.All.InstanceOf<AllMembershipTypeViewModel>());
        Assert.That(result.Count(), Is.EqualTo(2));
        Assert.IsTrue(result.Any(m => m.Name == "Basic" && m.Price == 20 && m.Duration == 30 && m.ImageUrl == "https://www.example.com/image1.jpg"));
        Assert.IsTrue(result.Any(m => m.Name == "Premium" && m.Price == 40 && m.Duration == 60 && m.ImageUrl == "https://www.example.com/image2.jpg"));
    }

    [Test]
    public async Task GetMyMembershipTypesAsync_ReturnsCorrectMembershipTypesForUser()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString()) // Use a unique database name for each test
            .Options;
        var context = new ApplicationDbContext(options);
        SeedData(context);

        // Seed additional data for MembershipRegistrations
        var userId = "test-user-id";
        var membershipRegistrations = new List<MembershipRegistration>
        {
            new MembershipRegistration
            {
                MemberId = userId,
                MembershipTypeId = 1,
                MembershipType = context.MembershipTypes.Find(1)
            },
            new MembershipRegistration
            {
                MemberId = userId,
                MembershipTypeId = 2,
                MembershipType = context.MembershipTypes.Find(2)
            }
        };
        context.MembershipRegistrations.AddRange(membershipRegistrations);
        context.SaveChanges();

        var service = new MembershipTypeService(context, _mockUserManager.Object);

        // Act
        var result = await service.GetMyMembershipTypesAsync(userId);

        // Assert
        Assert.IsNotNull(result);
        Assert.That(result.Count(), Is.EqualTo(2));
        Assert.IsTrue(result.Any(m => m.Name == "Basic" && m.Price == 20 && m.Duration == 30 && m.ImageUrl == "https://www.example.com/image1.jpg"));
        Assert.IsTrue(result.Any(m => m.Name == "Premium" && m.Price == 40 && m.Duration == 60 && m.ImageUrl == "https://www.example.com/image2.jpg"));
    }

    [Test]
    public async Task AddMyMembershipAsync_AddsMembershipSuccessfully()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString()) // Use a unique database name for each test
            .Options;
        var context = new ApplicationDbContext(options);
        SeedData(context);

        var service = new MembershipTypeService(context, _mockUserManager.Object);
        var userId = "test-user-id";
        var membershipTypeViewModel = new MembershipTypeViewModel
        {
            Id = 1,
            Name = "Basic",
            Price = 20,
            Duration = 30,
            Description = "Basic membership type",
            ImageUrl = "https://www.example.com/image1.jpg"
        };

        // Act
        await service.AddMyMembershipAsync(userId, membershipTypeViewModel);

        // Assert
        var registration = await context.MembershipRegistrations.FirstOrDefaultAsync(r => r.MemberId == userId);
        Assert.IsNotNull(registration);
        Assert.That(registration.MembershipTypeId, Is.EqualTo(1));
    }

    [Test]
    public void AddMyMembershipAsync_ThrowsExceptionForNonExistentMembershipType()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString()) // Use a unique database name for each test
            .Options;
        var context = new ApplicationDbContext(options);
        SeedData(context);

        var service = new MembershipTypeService(context, _mockUserManager.Object);
        var userId = "test-user-id";
        var membershipTypeViewModel = new MembershipTypeViewModel
        {
            Id = 999, // Non-existent ID
            Name = "NonExistent",
            Price = 0,
            Duration = 0,
            Description = "Non-existent membership type",
            ImageUrl = "https://www.example.com/image999.jpg"
        };

        // Act & Assert
        var ex = Assert.ThrowsAsync<InvalidOperationException>(() => service.AddMyMembershipAsync(userId, membershipTypeViewModel));
        Assert.That(ex.Message, Is.EqualTo("The membership type does not exist."));
    }

    [Test]
    public void AddMyMembershipAsync_ThrowsExceptionForExistingMembership()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString()) // Use a unique database name for each test
            .Options;
        var context = new ApplicationDbContext(options);
        SeedData(context);

        var service = new MembershipTypeService(context, _mockUserManager.Object);
        var userId = "test-user-id";
        var membershipTypeViewModel = new MembershipTypeViewModel
        {
            Id = 1,
            Name = "Basic",
            Price = 20,
            Duration = 30,
            Description = "Basic membership type",
            ImageUrl = "https://www.example.com/image1.jpg"
        };

        // Seed an existing membership for the user
        var existingMembership = new MembershipRegistration
        {
            MemberId = userId,
            MembershipTypeId = 1
        };
        context.MembershipRegistrations.Add(existingMembership);
        context.SaveChanges();

        // Act & Assert
        var ex = Assert.ThrowsAsync<InvalidOperationException>(() => service.AddMyMembershipAsync(userId, membershipTypeViewModel));
        Assert.That(ex.Message, Is.EqualTo("You can only have one membership type at a time."));
    }

    [Test]
    public async Task RemoveMyMembershipAsync_RemovesMembershipSuccessfully()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString()) // Use a unique database name for each test
            .Options;
        var context = new ApplicationDbContext(options);
        SeedData(context);

        var service = new MembershipTypeService(context, _mockUserManager.Object);
        var userId = "test-user-id";
        var membershipTypeViewModel = new MembershipTypeViewModel
        {
            Id = 1,
            Name = "Basic",
            Price = 20,
            Duration = 30,
            Description = "Basic membership type",
            ImageUrl = "https://www.example.com/image1.jpg"
        };

        // Seed an existing membership for the user
        var existingMembership = new MembershipRegistration
        {
            MemberId = userId,
            MembershipTypeId = 1
        };
        context.MembershipRegistrations.Add(existingMembership);
        context.SaveChanges();

        // Act
        await service.RemoveMyMembershipAsync(userId, membershipTypeViewModel);

        // Assert
        var registration = await context.MembershipRegistrations.FirstOrDefaultAsync(r => r.MemberId == userId);
        Assert.IsNull(registration);
    }

    [Test]
    public void RemoveMyMembershipAsync_ThrowsExceptionForNonExistentMembership()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString()) // Use a unique database name for each test
            .Options;
        var context = new ApplicationDbContext(options);
        SeedData(context);

        var service = new MembershipTypeService(context, _mockUserManager.Object);
        var userId = "test-user-id";
        var membershipTypeViewModel = new MembershipTypeViewModel
        {
            Id = 999, // Non-existent ID
            Name = "NonExistent",
            Price = 0,
            Duration = 0,
            Description = "Non-existent membership type",
            ImageUrl = "https://www.example.com/image999.jpg"
        };

        // Act & Assert
        var ex = Assert.ThrowsAsync<InvalidOperationException>(() => service.RemoveMyMembershipAsync(userId, membershipTypeViewModel));
        Assert.That(ex.Message, Is.EqualTo("You have not purchased a membership"));
    }

    [Test]
    public async Task GetMembershipTypeForAddAsync_ReturnsDefaultAddMembershipTypeViewModel()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString()) // Use a unique database name for each test
            .Options;
        var context = new ApplicationDbContext(options);
        var service = new MembershipTypeService(context, _mockUserManager.Object);

        // Act
        var result = await service.GetMembershipTypeForAddAsync();

        // Assert
        Assert.IsNotNull(result);
        Assert.That(result.Name, Is.EqualTo(string.Empty));
        Assert.That(result.ImageUrl, Is.EqualTo(string.Empty));
        Assert.That(result.Description, Is.EqualTo(string.Empty));
        Assert.That(result.Price, Is.EqualTo(0.0m));
        Assert.That(result.Duration, Is.EqualTo(0));
    }

    [Test]
    public async Task AddMembershipTypeAsync_AddsMembershipTypeSuccessfully()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString()) // Use a unique database name for each test
            .Options;
        var context = new ApplicationDbContext(options);
        SeedData(context);

        var user = new IdentityUser { Id = "admin-user-id" };
        _mockUserManager.Setup(m => m.FindByIdAsync(It.IsAny<string>())).ReturnsAsync(user);
        _mockUserManager.Setup(m => m.IsInRoleAsync(user, "Admin")).ReturnsAsync(true);

        var service = new MembershipTypeService(context, _mockUserManager.Object);
        var model = new AddMembershipTypeViewModel
        {
            Name = "Gold",
            ImageUrl = "https://www.example.com/image3.jpg",
            Description = "Gold membership type",
            Price = 50,
            Duration = 90
        };

        // Act
        await service.AddMembershipTypeAsync(model, user.Id);

        // Assert
        var membershipType = await context.MembershipTypes.FirstOrDefaultAsync(m => m.Name == "Gold");
        Assert.IsNotNull(membershipType);
        Assert.That(membershipType.Name, Is.EqualTo("Gold"));
        Assert.That(membershipType.ImageUrl, Is.EqualTo("https://www.example.com/image3.jpg"));
        Assert.That(membershipType.Description, Is.EqualTo("Gold membership type"));
        Assert.That(membershipType.Price, Is.EqualTo(50));
        Assert.That(membershipType.Duration, Is.EqualTo(90));
    }

    [Test]
    public void AddMembershipTypeAsync_ThrowsExceptionForNonAdminUser()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString()) // Use a unique database name for each test
            .Options;
        var context = new ApplicationDbContext(options);
        SeedData(context);

        var user = new IdentityUser { Id = "non-admin-user-id" };
        _mockUserManager.Setup(m => m.FindByIdAsync(It.IsAny<string>())).ReturnsAsync(user);
        _mockUserManager.Setup(m => m.IsInRoleAsync(user, "Admin")).ReturnsAsync(false);

        var service = new MembershipTypeService(context, _mockUserManager.Object);
        var model = new AddMembershipTypeViewModel
        {
            Name = "Gold",
            ImageUrl = "https://www.example.com/image3.jpg",
            Description = "Gold membership type",
            Price = 50,
            Duration = 90
        };

        // Act & Assert
        var ex = Assert.ThrowsAsync<InvalidOperationException>(() => service.AddMembershipTypeAsync(model, user.Id));
        Assert.That(ex.Message, Is.EqualTo("You are not authorized to add this element."));
    }

    [Test]
    public async Task EditMembershipTypeAsync_EditsMembershipTypeSuccessfully()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString()) // Use a unique database name for each test
            .Options;
        var context = new ApplicationDbContext(options);
        SeedData(context);

        var user = new IdentityUser { Id = "admin-user-id" };
        _mockUserManager.Setup(m => m.FindByIdAsync(It.IsAny<string>())).ReturnsAsync(user);
        _mockUserManager.Setup(m => m.IsInRoleAsync(user, "Admin")).ReturnsAsync(true);

        var service = new MembershipTypeService(context, _mockUserManager.Object);
        var model = new MembershipTypeViewModel
        {
            Id = 1,
            Name = "Updated Basic",
            Price = 25,
            Duration = 35,
            Description = "Updated description",
            ImageUrl = "https://www.example.com/updatedimage1.jpg"
        };

        // Act
        await service.EditMembershipTypeAsync(model, user.Id);

        // Assert
        var membershipType = await context.MembershipTypes.FindAsync(1);
        Assert.IsNotNull(membershipType);
        Assert.That(membershipType.Name, Is.EqualTo("Updated Basic"));
        Assert.That(membershipType.Price, Is.EqualTo(25));
        Assert.That(membershipType.Duration, Is.EqualTo(35));
        Assert.That(membershipType.Description, Is.EqualTo("Updated description"));
        Assert.That(membershipType.ImageUrl, Is.EqualTo("https://www.example.com/updatedimage1.jpg"));
    }

    [Test]
    public void EditMembershipTypeAsync_ThrowsExceptionForNonAdminUser()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString()) // Use a unique database name for each test
            .Options;
        var context = new ApplicationDbContext(options);
        SeedData(context);

        var user = new IdentityUser { Id = "non-admin-user-id" };
        _mockUserManager.Setup(m => m.FindByIdAsync(It.IsAny<string>())).ReturnsAsync(user);
        _mockUserManager.Setup(m => m.IsInRoleAsync(user, "Admin")).ReturnsAsync(false);

        var service = new MembershipTypeService(context, _mockUserManager.Object);
        var model = new MembershipTypeViewModel
        {
            Id = 1,
            Name = "Updated Basic",
            Price = 25,
            Duration = 35,
            Description = "Updated description",
            ImageUrl = "https://www.example.com/updatedimage1.jpg"
        };

        // Act & Assert
        var ex = Assert.ThrowsAsync<InvalidOperationException>(() => service.EditMembershipTypeAsync(model, user.Id));
        Assert.That(ex.Message, Is.EqualTo("You are not authorized to edit this element."));
    }

    [Test]
    public void EditMembershipTypeAsync_ThrowsExceptionForNonExistentMembershipType()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString()) // Use a unique database name for each test
            .Options;
        var context = new ApplicationDbContext(options);
        SeedData(context);

        var user = new IdentityUser { Id = "admin-user-id" };
        _mockUserManager.Setup(m => m.FindByIdAsync(It.IsAny<string>())).ReturnsAsync(user);
        _mockUserManager.Setup(m => m.IsInRoleAsync(user, "Admin")).ReturnsAsync(true);

        var service = new MembershipTypeService(context, _mockUserManager.Object);
        var model = new MembershipTypeViewModel
        {
            Id = 999, // Non-existent ID
            Name = "NonExistent",
            Price = 0,
            Duration = 0,
            Description = "Non-existent membership type",
            ImageUrl = "https://www.example.com/image999.jpg"
        };

        // Act & Assert
        var ex = Assert.ThrowsAsync<InvalidOperationException>(() => service.EditMembershipTypeAsync(model, user.Id));
        Assert.That(ex.Message, Is.EqualTo("The membership type does not exist."));
    }

    [Test]
    public async Task GetMembershipTypeForDeleteAsync_ReturnsCorrectViewModel()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString()) // Use a unique database name for each test
            .Options;
        var context = new ApplicationDbContext(options);
        SeedData(context);

        var service = new MembershipTypeService(context, _mockUserManager.Object);
        var membershipType = new MembershipType
        {
            Id = 1,
            Name = "Basic",
            Description = "Basic membership type"
        };

        // Ensure the context is not tracking any other instances of the same entity
        context.Entry(membershipType).State = EntityState.Detached;

        // Act
        var result = await service.GetMembershipTypeForDeleteAsync(1);

        // Assert
        Assert.IsNotNull(result);
        Assert.That(result.Id, Is.EqualTo(1));
        Assert.That(result.Name, Is.EqualTo("Basic"));
        Assert.That(result.Description, Is.EqualTo("Basic membership type"));
    }

    [Test]
    public async Task GetMembershipTypeForDeleteAsync_ReturnsNullForNonExistentId()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString()) // Use a unique database name for each test
            .Options;
        var context = new ApplicationDbContext(options);
        SeedData(context);

        var service = new MembershipTypeService(context, _mockUserManager.Object);

        // Act
        var result = await service.GetMembershipTypeForDeleteAsync(999); // Non-existent ID

        // Assert
        Assert.IsNull(result);
    }

    [Test]
    public void DeleteMembershipTypeAsync_ThrowsExceptionForNonAdminUser()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString()) // Use a unique database name for each test
            .Options;
        var context = new ApplicationDbContext(options);
        SeedData(context);

        var user = new IdentityUser { Id = "non-admin-user-id" };
        _mockUserManager.Setup(m => m.FindByIdAsync(It.IsAny<string>())).ReturnsAsync(user);
        _mockUserManager.Setup(m => m.IsInRoleAsync(user, "Admin")).ReturnsAsync(false);

        var service = new MembershipTypeService(context, _mockUserManager.Object);

        // Act & Assert
        var ex = Assert.ThrowsAsync<InvalidOperationException>(() => service.DeleteMembershipTypeAsync(1, user.Id));
        Assert.That(ex.Message, Is.EqualTo("You are not authorized to delete this element."));
    }

    [Test]
    public void DeleteMembershipTypeAsync_ThrowsExceptionForNonExistentMembershipType()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString()) // Use a unique database name for each test
            .Options;
        var context = new ApplicationDbContext(options);
        SeedData(context);

        var user = new IdentityUser { Id = "admin-user-id" };
        _mockUserManager.Setup(m => m.FindByIdAsync(It.IsAny<string>())).ReturnsAsync(user);
        _mockUserManager.Setup(m => m.IsInRoleAsync(user, "Admin")).ReturnsAsync(true);

        var service = new MembershipTypeService(context, _mockUserManager.Object);

        // Act & Assert
        var ex = Assert.ThrowsAsync<InvalidOperationException>(() => service.DeleteMembershipTypeAsync(999, user.Id)); // Non-existent ID
        Assert.That(ex.Message, Is.EqualTo("The membership type does not exist."));
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