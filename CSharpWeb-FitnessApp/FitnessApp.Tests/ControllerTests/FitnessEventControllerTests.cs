using System.Security.Claims;
using FitnessApp.Services.Data.Contracts;
using FitnessApp.Web.Controllers;
using FitnessApp.Web.ViewModels.FitnessEventViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace FitnessApp.Tests.ControllerTests;

[TestFixture]
public class FitnessEventControllerTests : IDisposable
{
    private Mock<IFitnessEventService> _mockFitnessEventService;
    private FitnessEventController _controller;

    [SetUp]
    public void SetUp()
    {
        _mockFitnessEventService = new Mock<IFitnessEventService>();
        _controller = new FitnessEventController(_mockFitnessEventService.Object);
    }


    [Test]
    public async Task Details_ReturnsViewResult_WithModel()
    {
        // Arrange
        int eventId = 1;
        var model = new FitnessEventDetailsViewModel();
        _mockFitnessEventService.Setup(service => service.GetFitnessEventDetailsAsync(eventId))
            .ReturnsAsync(model);

        // Act
        var result = await _controller.Details(eventId);

        // Assert
        Assert.IsInstanceOf<ViewResult>(result);
        var viewResult = result as ViewResult;
        Assert.That(viewResult?.Model, Is.EqualTo(model));
    }

    [Test]
    public async Task MyFitnessEvents_ReturnsViewResult_WithModel()
    {
        // Arrange
        var userId = "testUserId";
        var model = new List<AllFitnessEventsViewModel>();
        _mockFitnessEventService.Setup(service => service.GetMyFitnessEventsAsync(userId))
            .ReturnsAsync(model);
        _controller.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext
            {
                User = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, userId)
                }, "mock"))
            }
        };

        // Act
        var result = await _controller.MyFitnessEvents();

        // Assert
        Assert.IsInstanceOf<ViewResult>(result);
        var viewResult = result as ViewResult;
        Assert.That(viewResult?.Model, Is.EqualTo(model));
    }
    [Test]
    public async Task Index_ReturnsViewResult_WithModel()
    {
        // Arrange
        string? searchQuery = "testQuery";
        var model = new List<AllFitnessEventsViewModel>();
        _mockFitnessEventService.Setup(service => service.GetAllFitnessEventsAsync(searchQuery))
            .ReturnsAsync(model);

        // Act
        var result = await _controller.Index(searchQuery);

        // Assert
        Assert.IsInstanceOf<ViewResult>(result);
        var viewResult = result as ViewResult;
        Assert.That(viewResult?.Model, Is.EqualTo(model));
    }

    [Test]
    public async Task Index_ReturnsViewResult_WithModel_WhenSearchQueryIsNull()
    {
        // Arrange
        string? searchQuery = null;
        var model = new List<AllFitnessEventsViewModel>();
        _mockFitnessEventService.Setup(service => service.GetAllFitnessEventsAsync(searchQuery))
            .ReturnsAsync(model);

        // Act
        var result = await _controller.Index(searchQuery);

        // Assert
        Assert.IsInstanceOf<ViewResult>(result);
        var viewResult = result as ViewResult;
        Assert.That(viewResult?.Model, Is.EqualTo(model));
    }

    [TearDown]
    public void TearDown()
    {
        _mockFitnessEventService = null!;
        _controller.Dispose();
    }

    public void Dispose()
    {
        _controller?.Dispose();
    }
}