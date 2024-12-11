using FitnessApp.Services.Data.Contracts;
using FitnessApp.Web.Controllers;
using FitnessApp.Web.ViewModels.MembershipTypeViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static FitnessApp.Common.ApplicationsConstants;
using static FitnessApp.Common.SuccessfulValidationMessages.MembershipType;
using static FitnessApp.Common.ErrorMessages.MembershipType;

namespace FitnessApp.Web.Areas.Admin.Controllers;

[Area(AdminRole)]
[Authorize(Roles = AdminRole)]
public class MembershipTypesManagementController : BaseController
{
    private readonly IMembershipTypeService _membershipTypeService;

    public MembershipTypesManagementController(IMembershipTypeService membershipTypeService)
    {
        _membershipTypeService = membershipTypeService;
    }

    public async Task<IActionResult> Index()
    {
        var membershipTypes = await _membershipTypeService.GetAllMembershipTypesAsync();

        return View(membershipTypes);
    }

    [HttpGet]
    public async Task<IActionResult> Add()
    {
        var model = await _membershipTypeService.GetMembershipTypeForAddAsync();

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Add(AddMembershipTypeViewModel model)
    {
        if (!ModelState.IsValid)
        {
            TempData["ErrorMessage"] = InvalidData;
            return View(model);
        }

        var userId = GetUserId();
        await _membershipTypeService.AddMembershipTypeAsync(model, userId);

        TempData["SuccessMessage"] = MembershipTypeAddedSuccessfully;
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var model = await _membershipTypeService.GetMembershipTypeByIdAsync(id);

        if (model == null)
        {
            TempData["ErrorMessage"] = MembershipNotFound;
            return RedirectToAction(nameof(Index));
        }

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(MembershipTypeViewModel model)
    {
        if (!ModelState.IsValid)
        {
            TempData["ErrorMessage"] = InvalidData;
            return View(model);
        }

        var userId = GetUserId();

        await _membershipTypeService.EditMembershipTypeAsync(model, userId);

        TempData["SuccessMessage"] = MembershipTypeUpdatedSuccessfully;
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        var model = await _membershipTypeService.GetMembershipTypeForDeleteAsync(id);

        if (model == null)
        {
            TempData["ErrorMessage"] = MembershipNotFound;
            return RedirectToAction(nameof(Index));
        }

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Delete(DeleteMembershipTypeViewModel model)
    {
        var userId = GetUserId();

        await _membershipTypeService.DeleteMembershipTypeAsync(model.Id, userId);

        TempData["SuccessMessage"] = MembershipTypeDeletedSuccessfully;
        return RedirectToAction(nameof(Index));
    }
}