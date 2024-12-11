using System.Security.Claims;
using FitnessApp.Services.Data.Contracts;
using FitnessApp.Web.Controllers;
using FitnessApp.Web.ViewModels.MembershipTypeViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Moq;

namespace FitnessApp.Tests.ControllerTests;

[TestFixture]
public class MembershipTypeControllerTests
{
    [Test]
    public async Task Index_ReturnsViewResult_WithListOfMembershipTypes()
    {
        // Arrange
        var mockService = new Mock<IMembershipTypeService>();
        mockService.Setup(service => service.GetAllMembershipTypesAsync())
            .ReturnsAsync(new List<AllMembershipTypeViewModel>());
        var controller = new MembershipTypeController(mockService.Object);

        // Act
        var result = await controller.Index();

        // Assert
        Assert.IsInstanceOf<ViewResult>(result);
        var viewResult = result as ViewResult;
        Assert.NotNull(viewResult);
        var model = viewResult.Model as List<AllMembershipTypeViewModel>;
        Assert.NotNull(model);
    }

    [Test]
    public async Task MyMembershipType_ReturnsViewResult_WithUserMembershipTypes()
    {
        // Arrange
        var mockService = new Mock<IMembershipTypeService>();
        var userId = "testUserId";
        mockService.Setup(service => service.GetMyMembershipTypesAsync(userId))
            .ReturnsAsync(new List<AllMembershipTypeViewModel>());
        var controller = new MembershipTypeController(mockService.Object);
        controller.ControllerContext = new ControllerContext
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
        var result = await controller.MyMembershipType();

        // Assert
        Assert.IsInstanceOf<ViewResult>(result);
        var viewResult = result as ViewResult;
        Assert.NotNull(viewResult);
        var model = viewResult.Model as List<AllMembershipTypeViewModel>;
        Assert.NotNull(model);
    }

    [Test]
    public async Task Details_ReturnsViewResult_WithMembershipTypeDetails()
    {
        // Arrange
        var mockService = new Mock<IMembershipTypeService>();
        var membershipTypeId = 1;
        var membershipTypeDetails = new MembershipTypeDetailsViewModel();
        mockService.Setup(service => service.GetMembershipTypeDetailsAsync(membershipTypeId))
            .ReturnsAsync(membershipTypeDetails);
        var controller = new MembershipTypeController(mockService.Object);

        // Act
        var result = await controller.Details(membershipTypeId);

        // Assert
        Assert.IsInstanceOf<ViewResult>(result);
        var viewResult = result as ViewResult;
        Assert.NotNull(viewResult);
        var model = viewResult.Model as MembershipTypeDetailsViewModel;
        Assert.NotNull(model);
    }

    [Test]
    public async Task RemoveMyMembership_SetsErrorMessage_WhenInvalidOperationExceptionIsThrown()
    {
        // Arrange
        var mockService = new Mock<IMembershipTypeService>();
        var userId = "testUserId";
        var membershipTypeId = 1;
        var membershipType = new MembershipTypeViewModel();
        var exceptionMessage = "Invalid operation.";
        mockService.Setup(service => service.GetMembershipTypeByIdAsync(membershipTypeId))
            .ReturnsAsync(membershipType);
        mockService.Setup(service => service.RemoveMyMembershipAsync(userId, membershipType))
            .ThrowsAsync(new InvalidOperationException(exceptionMessage));
        var controller = new MembershipTypeController(mockService.Object);
        controller.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext
            {
                User = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, userId)
                }, "mock"))
            }
        };
        controller.TempData = new TempDataDictionary(new DefaultHttpContext(), Mock.Of<ITempDataProvider>());

        // Act
        var result = await controller.RemoveMyMembership(membershipTypeId);

        // Assert
        Assert.IsInstanceOf<RedirectToActionResult>(result);
        var redirectResult = result as RedirectToActionResult;
        Assert.NotNull(redirectResult);
        Assert.That(redirectResult.ActionName, Is.EqualTo(nameof(controller.MyMembershipType)));
        Assert.That(controller.TempData["ErrorMessage"], Is.EqualTo(exceptionMessage));
    }
}