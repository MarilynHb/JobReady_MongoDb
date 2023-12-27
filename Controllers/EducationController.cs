using JobReady.Interfaces;
using JobReady.Models;
using JobReady.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace JobReady.Controllers;
public class EducationController : Controller
{
    private readonly UserService UserService;
    public EducationController(IUserService UserService)
    {
        this.UserService = (UserService)UserService;
    }
    public async Task<IActionResult> Index()
    {
        var users = await UserService.GetEducationByUsers();
        return View(users);
    }
    public async Task<IActionResult> CreateAsync()
    {
        var users = await UserService.GetAllAsync();
        var universities = await UserService.GetAllUniversitiesAsync();
        ViewBag.Users = new SelectList(users.ToList(), "Id", "Username");
        ViewBag.Universities = new SelectList(universities.ToList(), "Id", "Name");
        return View();
    }
    public async Task<IActionResult> Edit(string id, int index)
    {
        var universities = await UserService.GetAllUniversitiesAsync();
        var users = await UserService.GetAllAsync();
        var user = users.First(t => t.Id.ToString() == id.ToString());
        var education = user.Education.ElementAt(index);
        var educationToEdit = new AddEducationRequest()
        {
            Index = index,
            OwnerId = user.Id.ToString(),
            Major = education.Major,
            Degree = education.Degree,
            StartDate = education.StartDate,
            EndDate = education.EndDate,
            UniversityId = education.UniversityId
        };
        ViewBag.Users = new SelectList(users.ToList(), "Id", "Username");
        ViewBag.Universities = new SelectList(universities.ToList(), "Id", "Name");

        return View(educationToEdit);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("OwnerId,UniversityId,Major,Degree,StartDate,EndDate")] AddEducationRequest request)
    {
        if (ModelState.IsValid)
        {
            await UserService.AddEducation(request);
            return RedirectToAction(nameof(Index));
        }
        return View(User);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(string id, [Bind("OwnerId,Index,UniversityId,Major,Degree,StartDate,EndDate")] AddEducationRequest request)
    {
        if (ModelState.IsValid)
        {
            await UserService.UpdateEducation(request);
            return RedirectToAction(nameof(Index));
        }
        return View(User);
    }
    public async Task<IActionResult> Details(string id)
    {
        var User = await UserService.GetByIdAsync(id.ToString());
        if (User == null)
        {
            return NotFound();
        }
        return View(User);
    }

}
