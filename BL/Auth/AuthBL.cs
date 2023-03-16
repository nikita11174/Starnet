using System.ComponentModel.DataAnnotations;
using Flenov.DAL;
using Flenov.DAL.Models;

namespace Flenov.BL.Auth
{
    public class AuthBL : IAuthBL
    {
        private readonly IAuthDAL authDal;
        private readonly IEncypt encypt;
        private readonly IHttpContextAccessor httpContextAccessor;

        public AuthBL(IAuthDAL authDal,
            IEncypt encrypt,
            IHttpContextAccessor httpContextAccessor)
        {
            this.authDal = authDal;
            this.encypt = encrypt;
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task<int> CreateUser(UserModel user)
        {
            user.Salt = Guid.NewGuid().ToString();
            user.Password = encypt.HashPassword(user.Password, user.Salt);
            int id = await authDal.CreateUser(user);
            Login(id);
            return id;
        }

        public void Login(int id)
        {
            httpContextAccessor.HttpContext?.Session.SetInt32(AuthConstants.AUTH_SESSION_PARAM_NAME, id);
        }

        public async Task<int> Authinticate(string email, string password, bool rememberMe)
        {
            var user = await authDal.GetUser(email);
            if (user.Password == encypt.HashPassword(password, user.Salt))
            {
                Login(user.UserId ?? 0);
                return user.UserId ?? 0;
            }

            return 0;
        }

        public async Task<ValidationResult?> ValidateEmail(string email)
        {
            var user = await authDal.GetUser(email);
            if (user.UserId != null)
            {
                return new ValidationResult("Email уже существует");
            }

            return null;
        }
    }
}