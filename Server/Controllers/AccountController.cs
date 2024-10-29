using System;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Server.Data;
using Server.Models;

namespace Server.Controllers;

public class AccountController(DataContext context) : BaseApiController
{
    [HttpPost("register")]
    public async Task<ActionResult<UserModel>> Register(string username, string password)
    {
        using var hmac = new HMACSHA512();

        var user = new UserModel{
            UserName = username,
            PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password)),
            PasswordSalt = hmac.Key
        };

        context.Users.Add(user);

        await context.SaveChangesAsync();
        return user;
    }
}
