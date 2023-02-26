using bgbrokersapi.Models.InputModels;
using bgbrokersapi.Services.Admin.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace bgbrokersapi.Controllers.Admin
{
    [Route("/Admin/User")]
    [ApiController]
    [Authorize(Roles = Constants.RoleConstants.AdminRole)]
    public class UserController : ControllerBase
    {
        private readonly IUserAdminService userAdminService;
        private readonly RoleManager<IdentityRole> roleManager;

        public UserController(IUserAdminService userAdminService,
            RoleManager<IdentityRole> roleManager)
        {
            this.userAdminService = userAdminService;
            this.roleManager = roleManager;
        }

        [HttpPost("Role")]
        public async Task<IActionResult> Role(AddRoleToUserInputModel model)
        {
            var responseModel = await userAdminService.SetRoleToUser(model);

            if (responseModel.Status == StatusCodes.Status404NotFound)
            {
                return NotFound(responseModel);
            }

            return NoContent();
        }

        [HttpPost("CreateRole")]
        public async Task<IActionResult> CreateRole(string roleName)
        {
            await roleManager.CreateAsync(new IdentityRole()
            {
                Name = roleName
            });

            return Ok();
        }
    }
}
