using bgbrokersapi.Data;
using bgbrokersapi.Data.Models.User;
using bgbrokersapi.Models;
using bgbrokersapi.Models.InputModels;
using bgbrokersapi.Services.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace bgbrokersapi.Services.Admin.User
{
    public class UserAdminService : IUserAdminService
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IUserService userService;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<ApplicationUser> userManager;

        public UserAdminService(ApplicationDbContext dbContext,
            IUserService userService,
            RoleManager<IdentityRole> roleManager,
            UserManager<ApplicationUser> userManager)
        {
            this.dbContext = dbContext;
            this.userService = userService;
            this.roleManager = roleManager;
            this.userManager = userManager;
        }

        public async Task<ResponseModel> SetRoleToUser(AddRoleToUserInputModel model)
        {
            var responseModel = new ResponseModel();

            var user = await dbContext
                .Users
                .FirstOrDefaultAsync(x => x.Id == model.UserId);

            var roles = await roleManager.Roles.ToListAsync();

            if (user == null)
            {
                responseModel.Status = StatusCodes.Status404NotFound;
                responseModel.Message = "User not found.";

                return responseModel;
            }

            try
            {
                var userRoles = await userManager.GetRolesAsync(user);

                await userManager.RemoveFromRolesAsync(user, userRoles);

                await userManager.AddToRolesAsync(user, model.RolesToAdd);

                await dbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                responseModel.Status = StatusCodes.Status404NotFound;
                responseModel.Message = e.Message;

                return responseModel;
            }

            responseModel.Status = StatusCodes.Status204NoContent;

            return responseModel;
        }
    }
}
