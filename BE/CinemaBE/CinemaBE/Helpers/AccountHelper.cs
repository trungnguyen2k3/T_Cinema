namespace CinemaBE.Helpers;
using BCrypt.Net;

using System.Net.NetworkInformation;

public class AccountHelper
    {
    public static string HashPassword(string password) {
        return BCrypt.HashPassword(password);
    }
    public static bool VerifyPassword(string inputPassword, string hashedPassword)
    {
        return BCrypt.Verify(inputPassword, hashedPassword);
    }
}
