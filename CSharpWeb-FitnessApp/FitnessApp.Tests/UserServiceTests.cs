using FitnessApp.Data;
using FitnessApp.Data.Models;
using FitnessApp.Services.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MockQueryable;
using MockQueryable.Moq;
using Moq;

namespace FitnessApp.Tests;

[TestFixture]
public class UserServiceTests
{
    private Mock<UserManager<IdentityUser>> _userManagerMock;
    private Mock<RoleManager<IdentityRole>> _roleManagerMock;
    private UserService _userService;
    private Mock<ApplicationDbContext> _mockContext;

    private void SeedData()
    {
        var users = new List<IdentityUser>
        {
            new IdentityUser { Id = "1", Email = "user1@example.com" },
            new IdentityUser { Id = "2", Email = "user2@example.com" }
        };

        var classesRegistrations = new List<ClassRegistration>
        {
            new ClassRegistration { MemberId = "1", ClassId = 1},
            new ClassRegistration { MemberId = "2", ClassId = 2}
        };

        var spaRegistrations = new List<SpaRegistration>
        {
            new SpaRegistration { MemberId = "1", SpaProcedureId = 1 },
            new SpaRegistration { MemberId = "2", SpaProcedureId = 2 }
        };

        var eventRegistrations = new List<EventRegistration>
        {
            new EventRegistration { MemberId = "1", EventId = 1 },
            new EventRegistration { MemberId = "2", EventId = 2 }
        };

        _mockContext.Setup(c => c.Users).Returns(users.AsQueryable().BuildMockDbSet().Object);
        _mockContext.Setup(c => c.ClassesRegistrations).Returns(classesRegistrations.AsQueryable().BuildMockDbSet().Object);
        _mockContext.Setup(c => c.SpaRegistrations).Returns(spaRegistrations.AsQueryable().BuildMockDbSet().Object);
        _mockContext.Setup(c => c.EventRegistrations).Returns(eventRegistrations.AsQueryable().BuildMockDbSet().Object);
    }

    [SetUp]
    public void SetUp()
    {
        var store = new Mock<IUserStore<IdentityUser>>();
        var roleStore = new Mock<IRoleStore<IdentityRole>>();
        _userManagerMock = new Mock<UserManager<IdentityUser>>(store.Object, null, null, null, null, null, null, null, null);
        _roleManagerMock = new Mock<RoleManager<IdentityRole>>(roleStore.Object, null, null, null, null);
        _userService = new UserService(_userManagerMock.Object, _roleManagerMock.Object, null);
    }

    [Test]
    public async Task GetAllUsersAsync_ReturnsAllUsersWithRoles()
    {
        // Arrange
        var users = new List<IdentityUser>
        {
            new IdentityUser { Id = "1", Email = "user1@example.com" },
            new IdentityUser { Id = "2", Email = "user2@example.com" }
        };

        _userManagerMock.Setup(um => um.Users).Returns(users.AsQueryable().BuildMock());
        _userManagerMock.Setup(um => um.GetRolesAsync(It.IsAny<IdentityUser>())).ReturnsAsync(new List<string> { "Role1", "Role2" });

        // Act
        var result = await _userService.GetAllUsersAsync();

        // Assert
        Assert.AreEqual(2, result.Count());
        Assert.AreEqual("user1@example.com", result.First().Email);
        Assert.AreEqual("Role1", result.First().Roles.First());
    }

    [Test]
    public async Task UserExistsByIdAsync_UserExists_ReturnsTrue()
    {
        // Arrange
        var userId = "1";
        var user = new IdentityUser { Id = userId, Email = "user1@example.com" };
        _userManagerMock.Setup(um => um.FindByIdAsync(userId)).ReturnsAsync(user);

        // Act
        var result = await _userService.UserExistsByIdAsync(userId);

        // Assert
        Assert.IsTrue(result);
    }

    [Test]
    public async Task UserExistsByIdAsync_UserDoesNotExist_ReturnsFalse()
    {
        // Arrange
        var userId = "1";
        _userManagerMock.Setup(um => um.FindByIdAsync(userId)).ReturnsAsync((IdentityUser)null);

        // Act
        var result = await _userService.UserExistsByIdAsync(userId);

        // Assert
        Assert.IsFalse(result);
    }
    [Test]
    public async Task AssignUserToRoleAsync_UserAndRoleExist_ReturnsTrue()
    {
        // Arrange
        var userId = "1";
        var role = "Admin";
        var user = new IdentityUser { Id = userId, Email = "user1@example.com" };

        _userManagerMock.Setup(um => um.FindByIdAsync(userId)).ReturnsAsync(user);
        _roleManagerMock.Setup(rm => rm.RoleExistsAsync(role)).ReturnsAsync(true);
        _userManagerMock.Setup(um => um.IsInRoleAsync(user, role)).ReturnsAsync(false);
        _userManagerMock.Setup(um => um.AddToRoleAsync(user, role)).ReturnsAsync(IdentityResult.Success);

        // Act
        var result = await _userService.AssignUserToRoleAsync(userId, role);

        // Assert
        Assert.IsTrue(result);
    }

    [Test]
    public async Task AssignUserToRoleAsync_UserDoesNotExist_ReturnsFalse()
    {
        // Arrange
        var userId = "1";
        var role = "Admin";

        _userManagerMock.Setup(um => um.FindByIdAsync(userId)).ReturnsAsync((IdentityUser)null);
        _roleManagerMock.Setup(rm => rm.RoleExistsAsync(role)).ReturnsAsync(true);

        // Act
        var result = await _userService.AssignUserToRoleAsync(userId, role);

        // Assert
        Assert.IsFalse(result);
    }

    [Test]
    public async Task AssignUserToRoleAsync_RoleDoesNotExist_ReturnsFalse()
    {
        // Arrange
        var userId = "1";
        var role = "Admin";
        var user = new IdentityUser { Id = userId, Email = "user1@example.com" };

        _userManagerMock.Setup(um => um.FindByIdAsync(userId)).ReturnsAsync(user);
        _roleManagerMock.Setup(rm => rm.RoleExistsAsync(role)).ReturnsAsync(false);

        // Act
        var result = await _userService.AssignUserToRoleAsync(userId, role);

        // Assert
        Assert.IsFalse(result);
    }

    [Test]
    public async Task AssignUserToRoleAsync_UserAlreadyInRole_ReturnsTrue()
    {
        // Arrange
        var userId = "1";
        var role = "Admin";
        var user = new IdentityUser { Id = userId, Email = "user1@example.com" };

        _userManagerMock.Setup(um => um.FindByIdAsync(userId)).ReturnsAsync(user);
        _roleManagerMock.Setup(rm => rm.RoleExistsAsync(role)).ReturnsAsync(true);
        _userManagerMock.Setup(um => um.IsInRoleAsync(user, role)).ReturnsAsync(true);

        // Act
        var result = await _userService.AssignUserToRoleAsync(userId, role);

        // Assert
        Assert.IsTrue(result);
    }

    [Test]
    public async Task AssignUserToRoleAsync_AddToRoleFails_ReturnsFalse()
    {
        // Arrange
        var userId = "1";
        var role = "Admin";
        var user = new IdentityUser { Id = userId, Email = "user1@example.com" };

        _userManagerMock.Setup(um => um.FindByIdAsync(userId)).ReturnsAsync(user);
        _roleManagerMock.Setup(rm => rm.RoleExistsAsync(role)).ReturnsAsync(true);
        _userManagerMock.Setup(um => um.IsInRoleAsync(user, role)).ReturnsAsync(false);
        _userManagerMock.Setup(um => um.AddToRoleAsync(user, role)).ReturnsAsync(IdentityResult.Failed());

        // Act
        var result = await _userService.AssignUserToRoleAsync(userId, role);

        // Assert
        Assert.IsFalse(result);
    }
    [Test]
    public async Task RemoveUserRoleAsync_UserAndRoleExist_ReturnsTrue()
    {
        // Arrange
        var userId = "1";
        var role = "Admin";
        var user = new IdentityUser { Id = userId, Email = "user1@example.com" };

        _userManagerMock.Setup(um => um.FindByIdAsync(userId)).ReturnsAsync(user);
        _roleManagerMock.Setup(rm => rm.RoleExistsAsync(role)).ReturnsAsync(true);
        _userManagerMock.Setup(um => um.IsInRoleAsync(user, role)).ReturnsAsync(true);
        _userManagerMock.Setup(um => um.RemoveFromRoleAsync(user, role)).ReturnsAsync(IdentityResult.Success);

        // Act
        var result = await _userService.RemoveUserRoleAsync(userId, role);

        // Assert
        Assert.IsTrue(result);
    }

    [Test]
    public async Task RemoveUserRoleAsync_UserDoesNotExist_ReturnsFalse()
    {
        // Arrange
        var userId = "1";
        var role = "Admin";

        _userManagerMock.Setup(um => um.FindByIdAsync(userId)).ReturnsAsync((IdentityUser)null);
        _roleManagerMock.Setup(rm => rm.RoleExistsAsync(role)).ReturnsAsync(true);

        // Act
        var result = await _userService.RemoveUserRoleAsync(userId, role);

        // Assert
        Assert.IsFalse(result);
    }

    [Test]
    public async Task RemoveUserRoleAsync_RoleDoesNotExist_ReturnsFalse()
    {
        // Arrange
        var userId = "1";
        var role = "Admin";
        var user = new IdentityUser { Id = userId, Email = "user1@example.com" };

        _userManagerMock.Setup(um => um.FindByIdAsync(userId)).ReturnsAsync(user);
        _roleManagerMock.Setup(rm => rm.RoleExistsAsync(role)).ReturnsAsync(false);

        // Act
        var result = await _userService.RemoveUserRoleAsync(userId, role);

        // Assert
        Assert.IsFalse(result);
    }

    [Test]
    public async Task RemoveUserRoleAsync_UserNotInRole_ReturnsTrue()
    {
        // Arrange
        var userId = "1";
        var role = "Admin";
        var user = new IdentityUser { Id = userId, Email = "user1@example.com" };

        _userManagerMock.Setup(um => um.FindByIdAsync(userId)).ReturnsAsync(user);
        _roleManagerMock.Setup(rm => rm.RoleExistsAsync(role)).ReturnsAsync(true);
        _userManagerMock.Setup(um => um.IsInRoleAsync(user, role)).ReturnsAsync(false);

        // Act
        var result = await _userService.RemoveUserRoleAsync(userId, role);

        // Assert
        Assert.IsTrue(result);
    }

    [Test]
    public async Task RemoveUserRoleAsync_RemoveFromRoleFails_ReturnsFalse()
    {
        // Arrange
        var userId = "1";
        var role = "Admin";
        var user = new IdentityUser { Id = userId, Email = "user1@example.com" };

        _userManagerMock.Setup(um => um.FindByIdAsync(userId)).ReturnsAsync(user);
        _roleManagerMock.Setup(rm => rm.RoleExistsAsync(role)).ReturnsAsync(true);
        _userManagerMock.Setup(um => um.IsInRoleAsync(user, role)).ReturnsAsync(true);
        _userManagerMock.Setup(um => um.RemoveFromRoleAsync(user, role)).ReturnsAsync(IdentityResult.Failed());

        // Act
        var result = await _userService.RemoveUserRoleAsync(userId, role);

        // Assert
        Assert.IsFalse(result);
    }

    [Test]
    public async Task DeleteUserAsync_UserExists_ReturnsTrue()
    {
        // Arrange
        var userId = "1";
        var user = new IdentityUser { Id = userId, Email = "user1@example.com" };

        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

        _mockContext = new Mock<ApplicationDbContext>(options);
        SeedData();

        _userManagerMock.Setup(um => um.FindByIdAsync(userId)).ReturnsAsync(user);
        _userManagerMock.Setup(um => um.DeleteAsync(user)).ReturnsAsync(IdentityResult.Success);
        _mockContext.Setup(c => c.ClassesRegistrations.RemoveRange(It.IsAny<IEnumerable<ClassRegistration>>()));
        _mockContext.Setup(c => c.SpaRegistrations.RemoveRange(It.IsAny<IEnumerable<SpaRegistration>>()));
        _mockContext.Setup(c => c.EventRegistrations.RemoveRange(It.IsAny<IEnumerable<EventRegistration>>()));
        _mockContext.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

        _userService = new UserService(_userManagerMock.Object, _roleManagerMock.Object, _mockContext.Object);

        // Act
        var result = await _userService.DeleteUserAsync(userId);

        // Assert
        Assert.IsTrue(result);
    }

    [Test]
    public async Task DeleteUserAsync_UserDoesNotExist_ReturnsFalse()
    {
        // Arrange
        var userId = "1";

        _userManagerMock.Setup(um => um.FindByIdAsync(userId)).ReturnsAsync((IdentityUser)null);

        // Act
        var result = await _userService.DeleteUserAsync(userId);

        // Assert
        Assert.IsFalse(result);
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