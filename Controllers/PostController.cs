using JobReady.Models;
using JobReady.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

public class PostController : Controller
{
    private readonly PostService postService;
    public PostController(IPostService postService)
    {
        this.postService = (PostService)postService;
    }

    public async Task<IActionResult> Index()
    {
        var post = await postService.GetAllAsync();
        return View(post);
    }
    public async Task<IActionResult> CreateAsync()
    {
        var users = await postService.GetAllUser();
        ViewBag.Users = new SelectList(users.ToList(), "Id", "Username");
        return View();
    }
    public async Task<IActionResult> Edit(string id)
    {
        var university = await postService.GetByIdAsync(id.ToString());
        return View(university);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Content,OwnerId")] Post post)
    {
        if (ModelState.IsValid)
        {
            await postService.CreateAsync(post);
            return RedirectToAction(nameof(Index));
        }
        return View(post);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(string id, [Bind("Id,Name,Location")] Post post)
    {
        if (id != post.Id.ToString())
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            await postService.UpdateAsync(id, post);
            return RedirectToAction(nameof(Index));
        }
        return View(post);
    }
    public async Task<IActionResult> Details(string id)
    {
        var university = await postService.GetByIdAsync(id.ToString());
        if (university == null)
        {
            return NotFound();
        }
        return View(university);
    }
}