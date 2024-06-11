using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ServiceHub.BL.DTOs;
using ServiceHub.DAL.Enums;
using ServiceHub.DAL.Helper;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ServiceHub.PL.Controllers
{//
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IConfiguration configuration;
        private readonly IMapper mapper;

        public AccountController(UserManager<ApplicationUser> userManager, IConfiguration configuration, RoleManager<IdentityRole<int>> roleManager,IMapper mapper)
        {
            this.userManager = userManager;
            this.configuration = configuration;
            this.mapper = mapper;
        }
   
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegistrationDTO model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = new ApplicationUser { UserName = model.FullName, Email = model.Email , PasswordHash = model.Password };
                    var result = await userManager.CreateAsync(user, model.Password);//
                    await userManager.AddToRoleAsync(user, model.Role.ToString());

                    if (result.Succeeded)
                    {
                        if (model.Role == Role.Worker)
                        {
                            user.JobId = model.JobId;
                            user.DistrictId = model.DistrictId;
                            await userManager.UpdateAsync(user);
                            return Ok("Worker registered successfully");
                        }
                        else if(model.Role == Role.User)
                        {
                            return Ok("User registered successfully");
                        }

                    }
                    return BadRequest(result.Errors.FirstOrDefault()?.Description);
                }

                return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error processing request: {ex.Message}");
            }
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDTO userlog)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = await userManager.FindByEmailAsync(userlog.Email);
                    if (user != null)
                    {
                        bool found = await userManager.CheckPasswordAsync(user, userlog.Password);
                        if (found)
                        {
                            List<Claim> userData =
                            [
                                new Claim("id",user.Id.ToString()),
                                new Claim(ClaimTypes.Name, user.UserName),
                                new Claim(ClaimTypes.Role,string.Join(',', (await userManager.GetRolesAsync(user)))),
                            ];

                            SecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]));
                            SigningCredentials Signcer = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                            //create Token
                            var token = new JwtSecurityToken(
                                issuer: configuration["JWT:ValidIssuer"],
                                audience: configuration["JWT:ValidAudiance"],
                                 claims: userData,
                                 expires: DateTime.Now.AddDays(1),
                                 signingCredentials: Signcer
                            );
                            return Ok(new JwtSecurityTokenHandler().WriteToken(token));

                            //return Ok(new
                            //{
                            //    stringToken = new JwtSecurityTokenHandler().WriteToken(token),
                            //    expairation = token.ValidTo
                            //});

                        }
                    }
                    return Unauthorized();
                }
                return Unauthorized();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving data : {ex}");

            }
        }
    }
}
