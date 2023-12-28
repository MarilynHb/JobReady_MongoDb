using JobReady.Models;
using JobReady.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

public class JobpostController : Controller
{
    private readonly JobPostService jobPostService;
    public JobpostController(IJobPostService postService)
    {
        this.jobPostService = (JobPostService)postService;
    }

    public async Task<IActionResult> Index()
    {
        var post = await jobPostService.GetAllAsync();
        return View(post);
    }
    public async Task<IActionResult> CreateAsync()
    {
        var users = await jobPostService.GetAllUser();
        ViewBag.Users = new SelectList(users.ToList(), "Id", "Username");
        return View();
    }
    public async Task<IActionResult> Edit(string id)
    {
        var university = await jobPostService.GetByIdAsync(id.ToString());
        return View(university);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,OwnerId,Title,Description,JobType")] AddJobPostRequest postRequest)
    {
        if (ModelState.IsValid)
        {
            await jobPostService.CreateAsync(postRequest);
            return RedirectToAction(nameof(Index));
        }
        return View(postRequest);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(string id, [Bind("Id,OwnerId,Title,Description,JobType")] JobPost post)
    {
        if (id != post.Id.ToString())
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            await jobPostService.UpdateAsync(id, post);
            return RedirectToAction(nameof(Index));
        }
        return View(post);
    }
    public async Task<IActionResult> Details(string id)
    {
        var university = await jobPostService.GetByIdAsync(id.ToString());
        if (university == null)
        {
            return NotFound();
        }
        return View(university);
    }
}