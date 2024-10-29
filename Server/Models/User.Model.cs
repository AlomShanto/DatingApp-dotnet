namespace Server.Models;

public class UserModel
{
    public int Id { get; set; }

    public required string UserName { get; set; }

    public required byte[] PasswordHash  { get; set; }

    public required byte[] PasswordSalt { get; set; }

}