using bgbrokersapi.Models;
using bgbrokersapi.Models.InputModels;

namespace bgbrokersapi.Services.Admin.User
{
    public interface IUserAdminService
    {
        Task<ResponseModel> SetRoleToUser(AddRoleToUserInputModel model);
    }
}
