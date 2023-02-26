using AutoMapper;
using bgbrokersapi.Constants;
using bgbrokersapi.Data;
using bgbrokersapi.Data.Models.User;
using bgbrokersapi.Models;
using bgbrokersapi.Models.InputModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace bgbrokersapi.Services.User
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IMapper mapper;
        private readonly IConfiguration config;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public UserService(ApplicationDbContext dbContext,
            IMapper mapper,
            IConfiguration config,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
            this.config = config;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        public async Task<AuthenticationResponseModel> Login(LoginInputModel model) //TODO Reseacrch how to save JWT in DB !
        {
            var user = await userManager.FindByNameAsync(model.Username);

            if (user != null && await userManager.CheckPasswordAsync(user, model.Password))
            {
                var userRoles = await userManager.GetRolesAsync(user);

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                var token = GetToken(authClaims);

                return new AuthenticationResponseModel()
                {
                    Token = new JwtSecurityTokenHandler().WriteToken(token),
                    ValidTo = token.ValidTo,
                    Success = true,
                };
            }

            return new AuthenticationResponseModel() { Message = "Username or Password is invalid"};
        }

        public async Task<RegisterResponseModel> RegisterUser(RegisterInputModel model)
        {
            var userExists = await userManager.FindByNameAsync(model.Username);

            if (userExists != null)
            {
                return new RegisterResponseModel { Message = "User already exists!" };
            }

            var user = new ApplicationUser()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username,
                FirstName = model.FirstName,
                LastName = model.LastName,
                PhoneNumber = model.PhoneNumber,
                Town = model.Town,
            };

            var result = await userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                return new RegisterResponseModel { Message = "User creation failed! Please check user details and try again." };
            }

            var defaultRole = await roleManager.FindByNameAsync(RoleConstants.UserRole);

            if (defaultRole != null)
            {
                var roleResult = await userManager.AddToRoleAsync(user, defaultRole.Name);

                if (!roleResult.Succeeded)
                {
                    return new RegisterResponseModel { Message = "Cant set role to this user! Please check user details and try again." };
                }
            }
            
            var userModel = mapper.Map<UserModel>(user);

            return new RegisterResponseModel { Success = true, Message = "User created successfully!", User =  userModel};
        }

        public async Task<ApplicationUser> GetById(string userId)
        {
            var user = await dbContext
                .Users
                .FirstOrDefaultAsync(x => x.Id == userId);

            if (user == null)
            {
                throw new ArgumentException("User is not found");
            }

            return user;
        }

        private JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: config["JWT:ValidIssuer"],
                audience: config["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }

        public async Task<bool> UserExist(string? userId)
        {
            return await dbContext.Users.AnyAsync(x => x.Id == userId);
        }
    }
}
