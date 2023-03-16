using Flenov.DAL;
using Flenov.DAL.Models;

namespace Flenov.DAL;

public interface IAuthDAL
{
    Task<UserModel> GetUser(string email);
    Task<UserModel> GetUser(int id);
    Task<int> CreateUser(UserModel model);
}