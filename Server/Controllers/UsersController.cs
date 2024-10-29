
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Server.Controllers;
using Server.Data;
using Server.Models;

namespace Server;
public class UsersController(DataContext context) : BaseApiController
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserModel>>> GetUsers(){
        var users = await context.Users.ToListAsync();
        return users;
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<UserModel>> GetUsers(int id){
        var user = await context.Users.FindAsync(id);

        if(user == null) return NotFound();

        return user;
    }
}
