using bgbrokersapi.Data.Models.User;
using bgbrokersapi.Models;
using bgbrokersapi.Models.InputModels;

namespace bgbrokersapi.Services.User
{
    public interface IUserService
    {
        Task<RegisterResponseModel> RegisterUser(RegisterInputModel model);

        Task<AuthenticationResponseModel> Login(LoginInputModel model);

        Task<ApplicationUser> GetById (string userId);
        Task<bool> UserExist(string? userId);
    }
}
