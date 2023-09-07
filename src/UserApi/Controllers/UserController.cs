using Microsoft.AspNetCore.Mvc;

namespace UserApi.Controllers;

public class UserController : ControllerBase
{
    private readonly UserCosmosDbContext context;

    public UserController(UserCosmosDbContext context)
    {
        this.context = context;
    }

    public override Task<ActionResult<User>> UsersGet(int userId)
    {
        throw new NotImplementedException();
    }

    public override Task<ActionResult<User>> UsersPatch(Body body, int userId)
    {
        throw new NotImplementedException();
    }

    public override Task<ActionResult<User>> User(Body2 body)
    {
        throw new NotImplementedException();
    }

    public override async Task<IActionResult> UsersExists(string userId)
    {
        var exists = await context.UserExistsAsync(userId);

        if (exists)
            return NoContent();
        else
            return NotFound();
    }
}