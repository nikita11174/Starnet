namespace Flenov.BL.Auth;

public interface IEncypt
{
    string HashPassword(string password, string salt);
}