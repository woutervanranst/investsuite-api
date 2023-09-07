using Microsoft.AspNetCore.Mvc;
using UserApi.Database;

namespace UserApi.Controllers;

public class UserController : ControllerBase
{
    private readonly UserCosmosDbContext context;

    public UserController(UserCosmosDbContext context)
    {
        this.context = context;
    }

    public override async Task<ActionResult<User>> UsersGet(string userId)
    {
        var r = await context.GetUserAsync(userId);

        if (r is null)
            return NotFound();
        else
            return Ok(r);
    }

    public override Task<ActionResult<User>> UsersPatch(User body, string userId)
    {
        throw new NotImplementedException();
    }

    public override Task<IActionResult> UsersPost(User body, string userId)
    {
        throw new NotImplementedException();
    }

    public override Task<IActionResult> UsersDelete(string userId)
    {
        throw new NotImplementedException();
    }
}