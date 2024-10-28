using Microsoft.EntityFrameworkCore;
using Server.Models;

namespace Server.Data;

public class DataContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<UserModel> Users  { get; set; }
}