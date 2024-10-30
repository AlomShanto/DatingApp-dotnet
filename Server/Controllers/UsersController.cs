
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Server.Controllers;
using Server.Data;
using Server.Models;

namespace Server;
public class UsersController(DataContext context) : BaseApiController
{
    [AllowAnonymous]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserModel>>> GetUsers(){
        var users = await context.Users.ToListAsync();
        return users;
    }

    [Authorize]
    [HttpGet("{id:int}")]
    public async Task<ActionResult<UserModel>> GetUsers(int id){
        var user = await context.Users.FindAsync(id);

        if(user == null) return NotFound();

        return user;
    }
}
