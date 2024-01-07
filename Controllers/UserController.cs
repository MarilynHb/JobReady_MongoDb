using JobReady.Interfaces;
using JobReady.Models;
using JobReady.Services;
using Microsoft.AspNetCore.Mvc;

namespace JobReady.Controllers;
public class UserController : Controller
{
    private readonly UserService UserService;
    public UserController(IUserService UserService)
    {
        this.UserService = (UserService)UserService;
    }
    public async Task<IActionResult> Index()
    {
        var users = await UserService.GetAllAsync();
        return View(users);
    }
    public IActionResult Create()
    {
        return View();
    }
    public async Task<IActionResult> Edit(string id)
    {
        var User = await UserService.GetByIdAsync(id.ToString());
        return View(User);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,Username,Type,Gender,Industry")] User User)
    {
        if (ModelState.IsValid)
        {
            await UserService.CreateAsync(User);
            return RedirectToAction(nameof(Index));
        }
        return View(User);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(string id, [Bind("Id,FirstName,LastName,Username,Type,Gender,Industry")] User User)
    {
        if (id != User.Id.ToString())
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            await UserService.UpdateAsync(id, User);
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
