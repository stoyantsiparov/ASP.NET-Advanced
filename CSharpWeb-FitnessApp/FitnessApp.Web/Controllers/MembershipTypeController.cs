using FitnessApp.Services.Data.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static FitnessApp.Common.ErrorMessages.MembershipType;
using static FitnessApp.Common.SuccessfulValidationMessages.MembershipType;

namespace FitnessApp.Web.Controllers;
public class MembershipTypeController : BaseController
{
    private readonly IMembershipTypeService _membershipTypeService;

    public MembershipTypeController(IMembershipTypeService membershipTypeService)
    {
        _membershipTypeService = membershipTypeService;
    }

    [AllowAnonymous]
    public async Task<IActionResult> Index()
    {
        var model = await _membershipTypeService.GetAllMembershipTypesAsync();

        return View(model);
    }

    public async Task<IActionResult> MyMembershipType()
    {
        var userId = GetUserId();

        var model = await _membershipTypeService.GetMyMembershipTypesAsync(userId);

        return View(model);
    }

    [AllowAnonymous]
    public async Task<IActionResult> Details(int id)
    {
        var model = await _membershipTypeService.GetMembershipTypeDetailsAsync(id);

        if (model == null)
        {
            TempData["ErrorMessage"] = MembershipNotPurchased;
            return RedirectToAction(nameof(Index));
        }

        return View(model);
    }

    public async Task<IActionResult> AddMyMembership(int id)
    {
        var model = await _membershipTypeService.GetMembershipTypeByIdAsync(id);

        if (model == null)
        {
            TempData["ErrorMessage"] = MembershipNotPurchased;
            return RedirectToAction(nameof(Details));
        }

        var userId = GetUserId();

        try
        {
            await _membershipTypeService.AddMyMembershipAsync(userId, model);
            TempData["SuccessMessage"] = MembershipPurchasedSuccessfully;
        }
        catch (InvalidOperationException ex)
        {
            TempData["ErrorMessage"] = ex.Message;
            return RedirectToAction(nameof(Details));
        }

        return RedirectToAction(nameof(MyMembershipType));
    }

    public async Task<IActionResult> RemoveMyMembership(int id)
    {
        var model = await _membershipTypeService.GetMembershipTypeByIdAsync(id);

        if (model == null)
        {
            TempData["ErrorMessage"] = MembershipTypeDoesNotExist;
            return RedirectToAction(nameof(MyMembershipType));
        }

        var userId = GetUserId();

        try
        {
            await _membershipTypeService.RemoveMyMembershipAsync(userId, model);
            TempData["SuccessMessage"] = MembershipTypeRemovedSuccessfully;
        }
        catch (InvalidOperationException ex)
        {
            TempData["ErrorMessage"] = ex.Message;
            return RedirectToAction(nameof(MyMembershipType));
        }

        return RedirectToAction(nameof(MyMembershipType));
    }

}