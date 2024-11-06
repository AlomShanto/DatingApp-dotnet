using System;
using Server.DTO;
using Server.Models;

namespace Server.Interfaces;

public interface IUserRepository
{
    void Update(UserModel user);
    Task<bool> SaveAllAsync();
    Task<IEnumerable<UserModel>> GetUsersAsync();
    Task<UserModel?> GetUserByIdAsync(int id);
    Task<UserModel?> GetUserByNameAsync(string username);
    Task<IEnumerable<MemberDto>> GetMembersAsync();
    Task<MemberDto?> GetMemberAsync(string username);
}
