using System.ComponentModel.DataAnnotations;
using Flenov.DAL.Models;
using Flenov.DAL;


namespace Flenov.BL.Auth
{
    public interface IAuthBL
    {
        Task<int> CreateUser(UserModel user);

        Task<int> Authinticate(string email, string password, bool rememberMe);

        Task<ValidationResult?> ValidateEmail(string email);
        
    }
}
