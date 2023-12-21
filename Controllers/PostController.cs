using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class PostController : ControllerBase{
    private readonly PostService postService; 
    public PostController(IPostService postService){
        this.postService = (PostService)postService; 
    }
    
    [HttpGet]
    public async Task<IActionResult>GetAll(){
        return Ok(await postService.GetAllAsync());
    }

    [HttpGet("{id}:length(24)")]
    public async Task<IActionResult> Get(string id){
        var post = await postService.GetByIdAsync(id);
        if(post == null){
            return NotFound();
        }
        return Ok(post);
    }

    [HttpPost]
    public async Task<IActionResult> Create(Post post){
        if(!ModelState.IsValid){
            return BadRequest();
        }
        await postService.CreateAsync(post);
        return Ok(post.Id);
    }

    [HttpPut("{id}:length(24)")]
    public async Task<IActionResult> Update(string id, Post postIn){
        var post = await postService.GetByIdAsync(id);
        if (post == null){
            return NotFound();
        }
        await postService.UpdateAsync(id, post);
        return NoContent();
    }

    [HttpDelete("{id}:length(24)")]
    public async Task<IActionResult> Delete(string id){
        var post = await postService.GetByIdAsync(id);
        if(post == null){
            return NotFound();
        }
        await postService.DeleteAsync(post.Id.ToString());
        return NoContent();
    }
}