using System.Security.Claims;
using FitnessApp.Services.Data.Contracts;
using FitnessApp.Web.Controllers;
using FitnessApp.Web.ViewModels.SpaProcedureViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Moq;

namespace FitnessApp.Tests.ControllerTests;

[TestFixture]
public class SpaProcedureControllerTests : IDisposable
{
    private Mock<ISpaProcedureService> _mockSpaProcedureService;
    private SpaProcedureController _controller;

    [SetUp]
    public void SetUp()
    {
        _mockSpaProcedureService = new Mock<ISpaProcedureService>();
        _controller = new SpaProcedureController(_mockSpaProcedureService.Object)
        {
            TempData = new TempDataDictionary(new DefaultHttpContext(), Mock.Of<ITempDataProvider>())
        };
    }

    [Test]
    public async Task Index_ReturnsViewResult_WithModel()
    {
        // Arrange
        string? searchQuery = "test";
        int pageNumber = 1;
        int pageSize = 4;
        var model = new PaginatedSpaProceduresViewModel();
        _mockSpaProcedureService.Setup(service => service.GetAllSpaProceduresPaginationAsync(searchQuery, pageNumber, pageSize))
            .ReturnsAsync(model);

        // Act
        var result = await _controller.Index(searchQuery, pageNumber, pageSize);

        // Assert
        Assert.IsInstanceOf<ViewResult>(result);
        var viewResult = result as ViewResult;
        Assert.That(viewResult?.Model, Is.EqualTo(model));
    }

    [Test]
    public async Task Details_ReturnsViewResult_WithModel()
    {
        // Arrange
        int procedureId = 1;
        var model = new SpaProceduresDetailsViewModel();
        _mockSpaProcedureService.Setup(service => service.GetSpaProceduresDetailsAsync(procedureId))
            .ReturnsAsync(model);

        // Act
        var result = await _controller.Details(procedureId);

        // Assert
        Assert.IsInstanceOf<ViewResult>(result);
        var viewResult = result as ViewResult;
        Assert.That(viewResult?.Model, Is.EqualTo(model));
    }

    [Test]
    public async Task MySpaAppointments_ReturnsViewResult_WithModel()
    {
        // Arrange  
        var userId = "testUserId";
        var model = new List<AllSpaProceduresViewModel>();
        _mockSpaProcedureService.Setup(service => service.GetMySpaProceduresAsync(userId))
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
        var result = await _controller.MySpaAppointments();

        // Assert  
        Assert.IsInstanceOf<ViewResult>(result);
        var viewResult = result as ViewResult;
        Assert.That(viewResult?.Model, Is.EqualTo(model));
    }

    [Test]
    public async Task AddToMySpaAppointments_ReturnsRedirectToActionResult_WhenModelIsNull()
    {
        // Arrange
        int procedureId = 1;
        DateTime appointmentDateTime = DateTime.Now;
        _mockSpaProcedureService.Setup(service => service.GetSpaProceduresByIdAsync(procedureId))
            .ReturnsAsync((SpaProceduresViewModel?)null);

        // Act
        var result = await _controller.AddToMySpaAppointments(procedureId, appointmentDateTime);

        // Assert
        Assert.IsInstanceOf<RedirectToActionResult>(result);
        var redirectResult = result as RedirectToActionResult;
        Assert.That(redirectResult?.ActionName, Is.EqualTo(nameof(_controller.MySpaAppointments)));
        Assert.That(_controller.TempData["ErrorMessage"], Is.EqualTo("You haven't booked a spa appointment"));
    }

    [Test]
    public async Task AddToMySpaAppointments_ReturnsRedirectToActionResult_WhenExceptionIsThrown()
    {
        // Arrange
        int procedureId = 1;
        DateTime appointmentDateTime = DateTime.Now;
        var model = new SpaProceduresViewModel();
        var userId = "testUserId";
        _mockSpaProcedureService
            .Setup(service => service.GetSpaProceduresByIdAsync(procedureId))
            .ReturnsAsync(model);
        _mockSpaProcedureService
            .Setup(service => service.AddToMySpaAppointmentsAsync(userId, model, appointmentDateTime))
            .ThrowsAsync(new InvalidOperationException("Test exception message"));
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
        var result = await _controller.AddToMySpaAppointments(procedureId, appointmentDateTime);

        // Assert
        Assert.IsInstanceOf<RedirectToActionResult>(result);
        var redirectResult = result as RedirectToActionResult;
        Assert.That(redirectResult?.ActionName, Is.EqualTo(nameof(_controller.Details)));
        Assert.That(redirectResult?.RouteValues["id"], Is.EqualTo(procedureId));
        Assert.That(_controller.TempData["ErrorMessage"], Is.EqualTo("Test exception message"));
    }

    [Test]
    public async Task AddToMySpaAppointments_ReturnsRedirectToActionResult_WhenSuccessful()
    {
        // Arrange
        int procedureId = 1;
        DateTime appointmentDateTime = DateTime.Now;
        var model = new SpaProceduresViewModel();
        var userId = "testUserId";
        _mockSpaProcedureService
            .Setup(service => service.GetSpaProceduresByIdAsync(procedureId))
            .ReturnsAsync(model);
        _mockSpaProcedureService
            .Setup(service => service.AddToMySpaAppointmentsAsync(userId, model, appointmentDateTime))
            .Returns(Task.CompletedTask);
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
        var result = await _controller.AddToMySpaAppointments(procedureId, appointmentDateTime);

        // Assert
        Assert.IsInstanceOf<RedirectToActionResult>(result);
        var redirectResult = result as RedirectToActionResult;
        Assert.That(redirectResult?.ActionName, Is.EqualTo(nameof(_controller.MySpaAppointments)));
        Assert.That(_controller.TempData["SuccessMessage"], Is.EqualTo("Spa appointment added successfully."));
    }

    [Test]
    public async Task RemoveFromMySpaAppointment_ReturnsRedirectToActionResult_WhenModelIsNull()
    {
        // Arrange
        int procedureId = 1;
        _mockSpaProcedureService.Setup(service => service.GetSpaProceduresByIdAsync(procedureId))
            .ReturnsAsync((SpaProceduresViewModel?)null);

        // Act
        var result = await _controller.RemoveFromMySpaAppointment(procedureId);

        // Assert
        Assert.IsInstanceOf<RedirectToActionResult>(result);
        var redirectResult = result as RedirectToActionResult;
        Assert.That(redirectResult?.ActionName, Is.EqualTo(nameof(_controller.MySpaAppointments)));
        Assert.That(_controller.TempData["ErrorMessage"], Is.EqualTo("You haven't booked a spa appointment"));
    }

    [Test]
    public async Task RemoveFromMySpaAppointment_ReturnsRedirectToActionResult_WhenExceptionIsThrown()
    {
        // Arrange
        int procedureId = 1;
        var model = new SpaProceduresViewModel();
        var userId = "testUserId";
        _mockSpaProcedureService
            .Setup(service => service.GetSpaProceduresByIdAsync(procedureId))
            .ReturnsAsync(model);
        _mockSpaProcedureService
            .Setup(service => service.RemoveFromMySpaAppointmentsAsync(userId, model))
            .ThrowsAsync(new InvalidOperationException("Test exception message"));
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
        var result = await _controller.RemoveFromMySpaAppointment(procedureId);

        // Assert
        Assert.IsInstanceOf<RedirectToActionResult>(result);
        var redirectResult = result as RedirectToActionResult;
        Assert.That(redirectResult?.ActionName, Is.EqualTo(nameof(_controller.MySpaAppointments)));
        Assert.That(_controller.TempData["ErrorMessage"], Is.EqualTo("Test exception message"));
    }

    [Test]
    public async Task RemoveFromMySpaAppointment_ReturnsRedirectToActionResult_WhenSuccessful()
    {
        // Arrange
        int procedureId = 1;
        var model = new SpaProceduresViewModel();
        var userId = "testUserId";
        _mockSpaProcedureService
            .Setup(service => service.GetSpaProceduresByIdAsync(procedureId))
            .ReturnsAsync(model);
        _mockSpaProcedureService
            .Setup(service => service.RemoveFromMySpaAppointmentsAsync(userId, model))
            .Returns(Task.CompletedTask);
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
        var result = await _controller.RemoveFromMySpaAppointment(procedureId);

        // Assert
        Assert.IsInstanceOf<RedirectToActionResult>(result);
        var redirectResult = result as RedirectToActionResult;
        Assert.That(redirectResult?.ActionName, Is.EqualTo(nameof(_controller.MySpaAppointments)));
        Assert.That(_controller.TempData["SuccessMessage"], Is.EqualTo("Spa appointment removed successfully."));
    }

    [TearDown]
    public void TearDown()
    {
        _mockSpaProcedureService = null!;
        _controller.Dispose();
    }

    public void Dispose()
    {
        _controller?.Dispose();
    }
}
