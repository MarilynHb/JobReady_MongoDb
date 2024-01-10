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
        var users = await postService.GetAllUser();
        ViewBag.Users = new SelectList(users.ToList(), "Id", "Username");
        var post = await postService.GetByIdAsync(id.ToString());
        var ownerId = await postService.GetPostOwnerId(id.ToString());
        var postRequest = new AddPostRequest() { Id = post.Id.ToString(), CreatedOn = post.CreatedOn,  Content = post.Content, OwnerId = ownerId };
        return View(postRequest);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Content,OwnerId")] AddPostRequest postRequest)
    {
        if (ModelState.IsValid)
        {
            await postService.CreateAsync(postRequest);
            return RedirectToAction(nameof(Index));
        }
        return View(postRequest);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(string id, [Bind("Id,Content,OwnerId,CreatedOn")] Post post)
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
        var post = await postService.GetByIdAsync(id.ToString());
        if (post == null)
        {
            return NotFound();
        }
        return View(post);
    }

    public async Task<IActionResult> Delete(string id)
    {
        await postService.DeleteAsync(id.ToString());
        return RedirectToAction(nameof(Index));
    }
}