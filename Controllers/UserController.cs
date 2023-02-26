using AutoMapper;
using bgbrokersapi.Models;
using bgbrokersapi.Models.InputModels;
using bgbrokersapi.Services.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace bgbrokersapi.Controllers
{
    [Route("User")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;
        private readonly IMapper mapper;

        public UserController(IUserService userService,
            IMapper mapper)
        {
            this.userService = userService;
            this.mapper = mapper;
        }

        [HttpGet("{userId:guid}")]
        public async Task<IActionResult> GetById([FromRoute] string userId)
        {
            var response = new UserResponseModel();

            try
            {
                var user = await userService.GetById(userId);

                response.Status = StatusCodes.Status200OK;
                response.User = mapper.Map<UserModel>(user);

            }
            catch (Exception e)
            {
                response.Status = StatusCodes.Status404NotFound;
                response.Message = e.Message;

                return NotFound(response);
            }

            return Ok(response);
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterInputModel model)
        {
            var response = await userService.RegisterUser(model);

            if (!response.Success)
            {
                return BadRequest(response.Message);
            }

            return CreatedAtAction(nameof(GetById), new {userId = response.User.Id}, response);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginInputModel model)
        {
            var response = await userService.Login(model);

            if (response.Success)
            {
                return Ok(response);
            }

            return Unauthorized(response.Message);
        }

        [Authorize]
        [HttpGet]
        public IActionResult TestAuth()
        {
            return Ok("You are authenticated");
        }
    }
}
