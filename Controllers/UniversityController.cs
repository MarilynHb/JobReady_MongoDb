using JobReady.Interfaces;
using JobReady.Models;
using JobReady.Services;
using Microsoft.AspNetCore.Mvc;

namespace JobReady.Controllers;
public class UniversityController : Controller
{
    private readonly UniversityService universityService;
    public UniversityController(IUniversityService universityService)
    {
        this.universityService = (UniversityService)universityService;
    }
    public async Task<IActionResult> Index()
    {
        var universities = await universityService.GetAllAsync();
        return View(universities);
    }
    public IActionResult Create()
    {
        return View();
    }
    public async Task<IActionResult> Edit(string id)
    {
        var university = await universityService.GetByIdAsync(id.ToString());
        return View(university);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Name,Location")] University university)
    {
        if (ModelState.IsValid)
        {
            await universityService.CreateAsync(university);
            return RedirectToAction(nameof(Index));
        }
        return View(university);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(string id, [Bind("Id,Name,Location")] University university)
    {
        if (id != university.Id.ToString())
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            await universityService.UpdateAsync(id, university);
            return RedirectToAction(nameof(Index));
        }
        return View(university);
    }
    public async Task<IActionResult> Details(string id)
    {
        var university = await universityService.GetByIdAsync(id.ToString());
        if (university == null)
        {
            return NotFound();
        }
        return View(university);
    }
    public async Task<IActionResult> Delete(string id)
    {
        await universityService.DeleteAsync(id.ToString());
        return RedirectToAction(nameof(Index));
    }


}
