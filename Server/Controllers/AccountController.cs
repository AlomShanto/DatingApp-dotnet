using System;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Server.Data;
using Server.DTO;
using Server.Models;

namespace Server.Controllers;

public class AccountController(DataContext context) : BaseApiController
{
    [HttpPost("register")]
    public async Task<ActionResult<UserModel>> Register(RegisterDto registerDto)
    {
        if(await UserExists(registerDto.Username))
            return BadRequest("Username is taken");
        using var hmac = new HMACSHA512();

        var user = new UserModel{
            UserName = registerDto.Username.ToLower(),
            PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
            PasswordSalt = hmac.Key
        };


        context.Users.Add(user);

        await context.SaveChangesAsync();
        return user;
    }


    [HttpPost("login")]
    public async Task<ActionResult<UserModel>> Login(LoginDto loginDto){
        var user = await context.Users.FirstOrDefaultAsync( x=> x.UserName == loginDto.Username.ToLower());
        if(user == null) return Unauthorized("Invalid username");

        using var hmac = new HMACSHA512(user.PasswordSalt);

        var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));

        for (int i = 0; i < computedHash.Length; i++)
        {
            if(computedHash[i] != user.PasswordHash[i])
                return Unauthorized("Invalid Password");
        }

        return user;
    }

    private async Task<bool> UserExists(string userName){
        return await context.Users.AnyAsync(x=> x.UserName.ToLower() == userName.ToLower());
    }
}